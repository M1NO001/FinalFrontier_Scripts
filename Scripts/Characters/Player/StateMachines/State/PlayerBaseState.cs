using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    //private float _jumpTimeoutDelta = 0.50f;
    //private float _fallTimeoutDelta = 0.15f;

    // cinemachine
    protected GameObject _mainCamera;
    GameObject _aimCamera;
    Transform CameraTarget;

    public bool canShoot = true;
    private bool isAim = false;

    public float LookSensitivity = 1.0f;

    public bool LockCameraPosition = false;

    private const float _threshold = 0.01f;
    public float TopClamp = 70.0f;
    public float BottomClamp = -30.0f;

    //Player Status
    PlayerModifier _playerModifier;

    private bool IsCurrentDeviceMouse
    {
        get
        {
#if ENABLE_INPUT_SYSTEM
            //return stateMachine.Player._playerInput.currentControlScheme == "KeyboardMouse";
            return true;
#else
				return false;
#endif
        }
    }


    public PlayerBaseState(PlayerStateMachine playerStateMachine)
    {
        stateMachine = playerStateMachine;
        player = stateMachine.Player;

        _mainCamera = player._playerMainCamera;
        _aimCamera = player._playerAimCamera;
        CameraTarget = player._playerCameraRoot;

        _playerModifier = player._playerModifier;

        LookSensitivity = player.LookSensitivity;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }
    

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }


    public virtual void HandleInput()
    {
        ReadMovementInput();
        ReadLookInput();
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {
        Move();
        CameraRotation();
    }

    public virtual void LateUpdate()
    {
    }

    
    private void ReadLookInput()
    {
        stateMachine.LookInput = player._playerInput.PlayerActions.Look.ReadValue<Vector2>();
    }

    private void ReadMovementInput()
    {
        stateMachine.MovementInput = player._playerInput.PlayerActions.Move.ReadValue<Vector2>();
    }
    private void CameraRotation()
    {

        if (stateMachine.LookInput.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            if (isAim) LookSensitivity = player.LookSensitivity;
            else LookSensitivity = player.AimSensitivity;

            stateMachine._cinemachineTargetYaw += stateMachine.LookInput.x * deltaTimeMultiplier * LookSensitivity;
            stateMachine._cinemachineTargetPitch += stateMachine.LookInput.y * deltaTimeMultiplier * LookSensitivity;
        }

        stateMachine._cinemachineTargetYaw = ClampAngle(stateMachine._cinemachineTargetYaw, float.MinValue, float.MaxValue);
        stateMachine._cinemachineTargetPitch = ClampAngle(stateMachine._cinemachineTargetPitch, BottomClamp, TopClamp);

        CameraTarget.rotation = Quaternion.Euler(stateMachine._cinemachineTargetPitch, stateMachine._cinemachineTargetYaw, 0.0f);

        
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }





    protected virtual void Move()
    {
        Vector3 movementDirection = Vector3.zero;

        player._characterController.Move(
            player._forceReceiver.Movement
            * Time.deltaTime
            );

        float _directionX = player._animator.GetFloat(player.AnimationData.DirectionX);
        float _directionY = player._animator.GetFloat(player.AnimationData.DirectionY);
        float _speed = player._animator.GetFloat(player.AnimationData.SpeedParameterHash);

        if (_directionY < -0.2) player._playerModifier.MovementDirectionModifier = player._playerModifier.BackwardSpeedModifier;
        else player._playerModifier.MovementDirectionModifier = player._playerModifier.ForwardSpeedModifier;

        if (_directionX > 0.1 || _directionX < -0.1) player._playerModifier.MovementSideDirectionModifier = player._playerModifier.SideSpeedModifier;
        else player._playerModifier.MovementSideDirectionModifier = player._playerModifier.Non_SideSpeedModifier;

        player._animator.SetFloat(player.AnimationData.DirectionX, Mathf.Lerp(_directionX, stateMachine.MovementInput.x, Time.deltaTime * 5f));
        player._animator.SetFloat(player.AnimationData.DirectionY, Mathf.Lerp(_directionY, stateMachine.MovementInput.y, Time.deltaTime * 5f));

        player._animator.SetFloat(player.AnimationData.SpeedParameterHash, Mathf.Lerp(_speed, player._playerModifier.MovementSpeed, Time.deltaTime * 5f));

        player.transform.forward = new Vector3(_mainCamera.transform.forward.x, 0, _mainCamera.transform.forward.z);
    }


    








    protected void StartAnimation(int animationHash)
    {
        player._animator.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        player._animator.SetBool(animationHash, false);
    }





    protected virtual void AddInputActionsCallbacks()
    {
        PlayerInput input = player._playerInput;
        
        input.PlayerActions.Move.started += OnMovementStarted;
        input.PlayerActions.Move.canceled += OnMovementCanceled;

        input.PlayerActions.Sprint.started += OnSprintStarted;

        input.PlayerActions.Crouch.started += OnCrouchStarted;

        input.PlayerActions.Jump.started += OnJumpStarted;

        input.PlayerActions.Aim.started += OnAimStarted;
        input.PlayerActions.Aim.canceled += OnAimCanceled;

        input.PlayerActions.Shoot.started += OnShootStarted;

        input.PlayerActions.MeleeAttack.performed += OnMeleeAttackPerformed;
        input.PlayerActions.MeleeAttack.canceled += OnMeleeAttackCanceled;


        input.PlayerActions.Interact.canceled += OnInteractCanceled;

        input.PlayerActions.Inventory.canceled += OnInventoryCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        PlayerInput input = player._playerInput;

        input.PlayerActions.Move.started -= OnMovementStarted;
        input.PlayerActions.Move.canceled -= OnMovementCanceled;
              
        input.PlayerActions.Sprint.started -= OnSprintStarted;

        input.PlayerActions.Crouch.started -= OnCrouchStarted;
         
        input.PlayerActions.Jump.started -= OnJumpStarted;

        input.PlayerActions.Aim.started -= OnAimStarted;
        input.PlayerActions.Aim.canceled -= OnAimCanceled;

        input.PlayerActions.Shoot.started -= OnShootStarted;

        input.PlayerActions.MeleeAttack.performed -= OnMeleeAttackPerformed;
        input.PlayerActions.MeleeAttack.canceled -= OnMeleeAttackCanceled;

        input.PlayerActions.Interact.canceled -= OnInteractCanceled;

        input.PlayerActions.Inventory.canceled -= OnInventoryCanceled;
    }

    private void OnInventoryCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("인벤토리");
        InventoryManager.instance.InventoryUI();
    }

    protected virtual void OnMeleeAttackCanceled(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = false;
    }

    protected virtual void OnMeleeAttackPerformed(InputAction.CallbackContext context)
    {
        stateMachine.IsAttacking = true;
    }


    protected virtual void OnCrouchStarted(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnShootStarted(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnInteractCanceled(InputAction.CallbackContext context)
    {
        
    }


    
    protected virtual void OnAimStarted(InputAction.CallbackContext context)
    {
        if (player.canAim)
        {
            _aimCamera.gameObject.SetActive(true);

            player._playerModifier.MovementAimgModifier = player._playerModifier.AimingMoveSpeedModifier;
            player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireAimModifier = player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireAimingModifier;
            player._rangedWeaponModifier.MinuteOfAngleMinAimModifier = player._rangedWeaponModifier.MinuteOfAngleMinAimingModifier;
            player._rangedWeaponModifier.MinuteOfAngleRecoveryAimModifier = player._rangedWeaponModifier.MinuteOfAngleRecoveryPerSecAimingModifier;
            player._rangedWeaponModifier.VerticalRecoilAimModifier = player._rangedWeaponModifier.RecoilAimingModifier;
            player._rangedWeaponModifier.HorizontalRecoilAimModifier = player._rangedWeaponModifier.RecoilAimingModifier;

            player._targetTracking.weight = 1;
            StartAnimation(player.AnimationData.AimParameterHash);

            isAim = true;
        }
    }
    
    protected virtual void OnAimCanceled(InputAction.CallbackContext context)
    {
        _aimCamera.gameObject.SetActive(false);
        player._playerModifier.MovementAimgModifier = player._playerModifier.Non_AimingMoveSpeedModifier;
        player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireAimModifier = player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireNon_AimingModifier;
        player._rangedWeaponModifier.MinuteOfAngleMinAimModifier = player._rangedWeaponModifier.MinuteOfAngleMinNon_AimingModifier;
        player._rangedWeaponModifier.MinuteOfAngleRecoveryAimModifier = player._rangedWeaponModifier.MinuteOfAngleRecoveryPerSecNon_AimingModifier;
        player._rangedWeaponModifier.VerticalRecoilAimModifier = player._rangedWeaponModifier.RecoilNon_AimingModifier;
        player._rangedWeaponModifier.HorizontalRecoilAimModifier = player._rangedWeaponModifier.RecoilNon_AimingModifier;
        player._targetTracking.weight = 0;
        StopAnimation(player.AnimationData.AimParameterHash);

        isAim = false;
    }

    protected virtual void OnJumpStarted(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnSprintStarted(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnMovementStarted(InputAction.CallbackContext context)
    {
        
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {
        
    }

    protected float GetNormalizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}