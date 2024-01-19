using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public EnemySO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public EnemyAnimationData AnimationData { get; private set; }
    public Animator Animator { get; private set; }
    [field: SerializeField] private Canvas monsterUICanvas;
    [field: SerializeField] private Image healthBar;
    public EnemyHealth CharacterHealth { get; private set; }
    protected EnemyStateMachine stateMachine;
    public NavMeshAgent Agent;

    public bool IsAttacking = false;
    public bool IsChasing = false;

    public bool Reused = false;
    private bool takenDamage = false;

    private Coroutine curChasingCoroutine;

    protected virtual void Awake()
    {
        AnimationData.Initialize();
        Animator = GetComponentInChildren<Animator>();
        CharacterHealth = GetComponent<EnemyHealth>();

        stateMachine = new EnemyStateMachine(this);
    }

    protected virtual void Start()
    {
        CharacterHealth.HealthInitialize(Data.MaxHealth);
        CharacterHealth.OnDie += OnDie;
        CharacterHealth.OnTakeDamage += OnTakeDamage;
        CharacterHealth.OnReusedAndInitHealth += OnReusedAndInitHealth;
        stateMachine.Targeting();

        Reused = true;
    }

    protected virtual void OnEnable()
    {
        if(Reused)
        {
            CharacterHealth.HealthInitialize(Data.MaxHealth);
        }
    }

    protected virtual void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    protected virtual void OnDie()
    {
        monsterUICanvas.gameObject.SetActive(false);
        takenDamage = false;
        IsChasing = false;
        stateMachine.ChangeState(stateMachine.DeathState);
        GamePlaySceneManager.Instance.MonstersCount--;
        GamePlaySceneManager.Instance.PlayerClass.AddExp(Data.Exp);
    }

    private void OnTakeDamage(float damage, float health)
    {
        if (!takenDamage)
        {
            takenDamage = true;
            monsterUICanvas.gameObject.SetActive(true);
        }
        healthBar.fillAmount = health / Data.MaxHealth;
    }
    private void OnReusedAndInitHealth()
    {
        healthBar.fillAmount = 1;
    }
    public void StartSetDestinationCoroutine()
    {
        curChasingCoroutine = StartCoroutine(SetDestinationCoroutine());
    }
    public IEnumerator SetDestinationCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(1);
        while (true)
        {
            if(Agent.remainingDistance < Data.AttackRange)
            {
                Agent.isStopped = true;
                Agent.velocity = Vector3.zero;
            }
            else
            {
                Agent.isStopped = false;
            }
            Agent.SetDestination(stateMachine.Target.transform.position);

            yield return wait;
        }
    }
    public void StopSetDestinationCoroutine()
    {
        if (curChasingCoroutine != null)
            StopCoroutine(curChasingCoroutine);
    }
}

