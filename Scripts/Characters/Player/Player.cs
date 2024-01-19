using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Data
    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }
    
    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    //Interact
    IInteractable interactableObject;

    //Camera
    public float LookSensitivity = 1.0f;
    public float AimSensitivity = 0.7f;

    [field: SerializeField] public GameObject _playerMainCamera;
    [field: SerializeField] public GameObject _playerFollowCamera;
    [field: SerializeField] public GameObject _playerAimCamera;
    public Transform _playerCameraRoot;

    //Rig
    [SerializeField] public Rig _targetTracking;
    [SerializeField] public Rig _idle;
    [SerializeField] public Rig _twoBoneIKConstraintMelee;

    //UI
    [SerializeField] public TMP_Text RemainingBulletUI;
    [SerializeField] public Image ReloadingUI;


    //Component
    public PlayerModifier _playerModifier { get; private set; }
    public RangedWeaponModifier _rangedWeaponModifier { get; private set; }
    public MeleeWeaponModifier _meleeWeaponModifier { get; private set; }
    public Animator _animator { get; private set; }

    public CharacterController _characterController { get; private set; }

    public PlayerInput _playerInput { get; private set; }

    public PlayerStateMachine _playerStateMachine;

    public PlayerSoundEffect _playerSoundEffect { get; private set; }

    public ForceReceiver _forceReceiver { get; private set; }
    public PlayerConditions _playerConditions { get; private set; }
    public PlayerSkill _playerSkill { get; private set; }



    //Shoot & Mouse
    Coroutine shoot;
    [SerializeField] public LayerMask aimColliderLayerMask;
    [SerializeField] public Transform AimingTarget;
    RaycastHit hit;
    public float shootDeltaTime { get; set; }
    public bool canShoot = true;
    public bool canAim = true;
    public bool canReload = true;
    public bool isReloading = false;
    public bool isAttack = false;
    public int bulletCapacity;
    public int remainingBullet;
    public ParticleSystem muzzleFlash;
    [field: SerializeField] private ParticleSystem Shell;

    public Vector3 mouseWorldPosition = Vector3.zero;

    private void Awake()
    {
        if (_playerMainCamera == null)
        {
            _playerMainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        AnimationData.Initialize();

        _playerConditions = GetComponent<PlayerConditions>();

        _playerModifier = GetComponent<PlayerModifier>();
        _rangedWeaponModifier = GetComponent<RangedWeaponModifier>();
        _meleeWeaponModifier = GetComponent<MeleeWeaponModifier>();

        _animator = GetComponent<Animator>();

        _playerInput = GetComponent<PlayerInput>();

        _characterController = GetComponent<CharacterController>();

        _forceReceiver = GetComponent<ForceReceiver>();

        _playerSkill = GetComponent<PlayerSkill>();

        _playerSoundEffect = GetComponent<PlayerSoundEffect>();


        _playerStateMachine = new PlayerStateMachine(this);

        _targetTracking.weight = 0;

        ReloadingUI.fillAmount = 0f;
    }

    private void Start()
    {
        ObjectPoolManager.Instance.CreateObjectPool(_rangedWeaponModifier.pfBulletProjectile);
        Cursor.lockState = CursorLockMode.Locked;
        _playerStateMachine.ChangeState(_playerStateMachine.IdleState);

        _playerInput.PlayerActions.Shoot.started += OnShootStarted;
        _playerInput.PlayerActions.Shoot.canceled += OnShootCanceled;

        _playerInput.PlayerActions.Reload.started += OnReloadStarted;
        
        _playerInput.PlayerActions.Interact.started += OnInteractStarted;

        bulletCapacity = _rangedWeaponModifier.BulletCapacity;
        remainingBullet = bulletCapacity;
    }

    private void Update()
    {
        _playerStateMachine.HandleInput();
        _playerStateMachine.Update();

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out hit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = hit.point;
            AimingTarget.position = mouseWorldPosition;

            if(hit.transform.gameObject.layer == 8 && hit.distance <= 7f)
            {
                //특정 레이어(아이템, 상호작용)을 조준했을 경우
                interactableObject = hit.transform.GetComponent<IInteractable>();
                interactableObject.GetInteractPrompt();
            }
            else
            {
                if (interactableObject != null)
                {
                    interactableObject.ExitInteractPrompt();
                    interactableObject = null;
                }
            }
        }
        if(bulletCapacity !=  _rangedWeaponModifier.BulletCapacity) { bulletCapacity = _rangedWeaponModifier.BulletCapacity; }

        RemainingBulletUI.text = remainingBullet.ToString() + " / " + bulletCapacity.ToString();
    }

    private void LateUpdate()
    {
        _playerStateMachine.LateUpdate();


        if (shootDeltaTime <= 0 ) { canShoot = true; }
        else
        {
            canShoot = false;
            shootDeltaTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        _playerStateMachine.PhysicsUpdate();
    }

    void OnDie()
    {
        _animator.SetTrigger("Die");
        enabled = false;
    }


    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        _playerInput.PlayerActions.Shoot.started -= OnShootStarted;
        _playerInput.PlayerActions.Shoot.canceled -= OnShootCanceled;

        _playerInput.PlayerActions.Reload.started -= OnReloadStarted;
        
        _playerInput.PlayerActions.Interact.started -= OnInteractStarted;
    }





    private void OnInteractStarted(InputAction.CallbackContext context)
    {
        if (interactableObject != null)
        {
            interactableObject.OnInteract();
        }
    }







    private void OnShootStarted(InputAction.CallbackContext context)
    {
        if (canShoot && !isReloading && remainingBullet > 0 && !isAttack)
        {
            _playerConditions.isFiring = true;
            _targetTracking.weight = 1f;
            shoot = StartCoroutine(Shoot());

        }
        else if(canShoot && !isReloading && remainingBullet == 0 && !isAttack)
        {
            // 틱틱 효과음
        }
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        StopCoroutine(shoot);

        if (canShoot) shootDeltaTime = 60 / _rangedWeaponModifier.FireRatePerMinute;
        _playerConditions.isFiring = false;
        if(!_animator.GetBool(AnimationData.AimParameterHash))
            _targetTracking.weight = 0f;
    }

    IEnumerator Shoot()
    {
        Coroutine recoil = null;
        
        while (true)
        {
            if (remainingBullet <= 0) break;
            remainingBullet--;

            if(recoil != null) StopCoroutine(recoil);
            recoil = StartCoroutine(ApplyRecoill());
            //_animator.SetTrigger(AnimationData.ShootParameterHash);
            
            
            Vector3 fireDir = (mouseWorldPosition - _rangedWeaponModifier.spawnBulletPosition.position).normalized + 
                new Vector3(UnityEngine.Random.Range(-_playerConditions.minuteOfAngle.curValue, _playerConditions.minuteOfAngle.curValue)
                          , UnityEngine.Random.Range(-_playerConditions.minuteOfAngle.curValue, _playerConditions.minuteOfAngle.curValue)
                          , UnityEngine.Random.Range(-_playerConditions.minuteOfAngle.curValue, _playerConditions.minuteOfAngle.curValue));

            _playerConditions.minuteOfAngle.Add( _rangedWeaponModifier.MinuteOfAngleIncreasePerFire);

            //BulletProjectile bullet = Instantiate(_rangedWeaponModifier.pfBulletProjectile, _rangedWeaponModifier.spawnBulletPosition.position, Quaternion.LookRotation(fireDir, Vector3.up)).GetComponent<BulletProjectile>();
            BulletProjectile bullet = ObjectPoolManager.Instance
                .GetObjectFromPool(_rangedWeaponModifier.pfBulletProjectile, _rangedWeaponModifier.spawnBulletPosition.position, Quaternion.LookRotation(fireDir, Vector3.up))
                .GetComponent<BulletProjectile>();
            
            for (int i = 0; i < _rangedWeaponModifier.ProjectileNumber; i++)
            {
                bullet.Shoot(_rangedWeaponModifier.ProjectileSpeed, _rangedWeaponModifier.ProjectileDamage, _rangedWeaponModifier.CriticalDamageChance, _rangedWeaponModifier.CriticalDamageMultiplier);
            }

            _playerSoundEffect.FireSoundEffect();

            if (muzzleFlash != null)
            {
                muzzleFlash.Emit(1);
            }

            if (Shell != null)
            {
                Shell.Emit(1);
            }

            yield return new WaitForSecondsRealtime(60 / _rangedWeaponModifier.FireRatePerMinute);
        }
        yield break;
    }

    IEnumerator ApplyRecoill()
    {
        float deltaTime = 0f;

        float originalTargetPitch = _playerStateMachine._cinemachineTargetPitch;
        //float originalTargetYaw = _playerStateMachine._cinemachineTargetYaw;

        float verticalTargetPitch = _playerStateMachine._cinemachineTargetPitch - _rangedWeaponModifier.VerticalRecoilPerFire;
        float horizontalTargetYaw = _playerStateMachine._cinemachineTargetYaw - UnityEngine.Random.Range(-_rangedWeaponModifier.HorizontalRecoilPerFire, _rangedWeaponModifier.HorizontalRecoilPerFire);
        while (_playerStateMachine._cinemachineTargetPitch >= verticalTargetPitch && deltaTime < 0.2f)
        {
            deltaTime += Time.deltaTime;
            _playerStateMachine._cinemachineTargetPitch =
                Mathf.Lerp(_playerStateMachine._cinemachineTargetPitch
                         , verticalTargetPitch
                         , 25f * Time.deltaTime);
            _playerStateMachine._cinemachineTargetYaw =
                Mathf.Lerp(_playerStateMachine._cinemachineTargetYaw
                         , horizontalTargetYaw
                         , 25f * Time.deltaTime);
            yield return null;
        }
        deltaTime = 0f;


        while (deltaTime < 0.1f)
        {
            deltaTime += Time.deltaTime;
            _playerStateMachine._cinemachineTargetPitch =
                Mathf.Lerp(_playerStateMachine._cinemachineTargetPitch
                         , originalTargetPitch
                         , 5f * Time.deltaTime);
            //_playerStateMachine._cinemachineTargetYaw =
            //    Mathf.Lerp(_playerStateMachine._cinemachineTargetYaw
            //             , originalTargetYaw
            //             , 5f * Time.deltaTime);
            yield return null;
        }



        yield break;
    }




    private void OnReloadStarted(InputAction.CallbackContext context)
    {
        Reloading();
    }

    private void Reloading()
    {
        if (!_playerSkill.skillStartAnimation && !_playerSkill.skillEndAnimation && !_playerSkill.skillProgressAnimation && !isReloading && canReload
            && remainingBullet < bulletCapacity)
        {
            _targetTracking.weight = 0f;
            if (_idle) _idle.weight = 0f;
            canAim = false;
            isReloading = true;
            _playerSoundEffect.ReloadSoundEffect();

            _animator.SetFloat(AnimationData.ReloadSpeedParameterHash, _rangedWeaponModifier.ReloadingTime);
            _animator.SetTrigger(AnimationData.ReloadParameterHash);
            StartCoroutine(ReloadingUIProgress());
        }
        else
        {
            return;
        }
    }

    IEnumerator ReloadingUIProgress()
    {
        while (ReloadingUI.fillAmount < 0.98)
        {
            AnimatorStateInfo currentInfo = _animator.GetCurrentAnimatorStateInfo(1);
            AnimatorStateInfo nextInfo = _animator.GetNextAnimatorStateInfo(1);
            if (_animator.IsInTransition(1) && nextInfo.IsTag("Reload"))
            {
                ReloadingUI.fillAmount= nextInfo.normalizedTime;
            }
            else if (!_animator.IsInTransition(1) && currentInfo.IsTag("Reload"))
            {
                ReloadingUI.fillAmount= currentInfo.normalizedTime;
            }
            yield return null;
        }

        if (_idle) _idle.weight = 1f;
        ReloadingUI.fillAmount = 0;
        yield break;
    }

    private void ReloadEnd()
    {
        remainingBullet = bulletCapacity;
        canAim = true;
        isReloading = false;
    }

    public void AddExp(int exp)
    {
        _playerModifier.EXP += exp;
    }

}
