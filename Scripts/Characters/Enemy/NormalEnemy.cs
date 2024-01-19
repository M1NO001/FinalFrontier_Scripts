using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class NormalEnemy : Enemy
{
    [SerializeField] private EnemySkillDataSO skillData;
    private Coroutine skillCor;
    private Coroutine stealthSkillCor;
    private Renderer _renderer;
    private Collider _collider;
    private Material[] materials = new Material[2];
    private int AvoidancePriority;

    protected override void Awake()
    {
        base.Awake();
        Agent = GetComponent<NavMeshAgent>();
        if (skillData.skillName == SkillName.Stealth)
        {
            _renderer = GetComponentInChildren<Renderer>();
            _collider = GetComponentInChildren<Collider>();
        }
        materials[0] = Resources.Load<Material>("Materials/Enemy/DiffuseMat");
        materials[1] = Resources.Load<Material>("Materials/Enemy/Enemy Stealth Shader");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.ChangeState(stateMachine.WanderingState);

        skillCor = StartCoroutine(skillAttack());

    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (Reused)
        {
            stateMachine.ChangeState(stateMachine.WanderingState);
            skillCor = StartCoroutine(skillAttack());
        }
    }

    private void OnDisable()
    {
        if(skillCor != null)
        {
            StopCoroutine(skillCor);
        }
        if (stealthSkillCor != null)
        {
            StopCoroutine(stealthSkillCor);
            _renderer.enabled = true;
            _renderer.sharedMaterial = materials[0];
            _collider.enabled = true;
        }
    }

    public void SetAvoidancePriority(int priority)
    {
        Agent.avoidancePriority = priority;
        AvoidancePriority = priority;
    }

    protected override void OnDie()
    {
        GamePlaySceneManager.Instance.avoidancePriorityList.Add(AvoidancePriority);

        ObjectPoolManager.Instance.GetObjectFromPool
            (ResourceManager.Instance.LoadResource<GameObject>("Prefabs/Monster/Effect/Enemy Death Effect"), transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);

        Invoke("SetActiveFalse", 1.0f); 

        base.OnDie();
    }
    private void SetActiveFalse()
    {
        ObjectPoolManager.Instance.ReleaseObjectToPool(gameObject);
    }
    private IEnumerator skillAttack()
    {
        WaitForSeconds wait = new WaitForSeconds(skillData.enemySkillInterval);

        while (true)
        {
            if(IsChasing == true && IsAttacking == false)
            {
                switch (skillData.skillName)
                {
                    case SkillName.Tornado:
                        ToradoSkill();
                        break;
                    case SkillName.Poison:
                        PoisonSkill();
                        break;
                    case SkillName.Stealth:
                        if (_renderer != null && _collider != null)
                        {
                            if (stealthSkillCor != null) StopCoroutine(stealthSkillCor);
                            stealthSkillCor = StartCoroutine(StealthSkill());
                        }
                        break;
                }
                yield return wait;
            }
            yield return null;
        }
    }

    private IEnumerator StealthSkill()
    {
        _renderer.enabled = true;
        _renderer.sharedMaterial = materials[1];
        _collider.enabled = false;

        yield return new WaitForSeconds(skillData.enemySkillInterval * 0.6f);

        _renderer.sharedMaterial = materials[0];
        _collider.enabled = true;
    }

    private void PoisonSkill()
    {
        ObjectPoolManager.Instance.GetObjectFromPool
                (ResourceManager.Instance.LoadResource<GameObject>("Prefabs/Monster/Effect/PoisonSkillEffect"), transform.position + new Vector3(0, 1.5f, 0), transform.rotation);
    }

    private void ToradoSkill()
    {
        ObjectPoolManager.Instance.GetObjectFromPool
                (ResourceManager.Instance.LoadResource<GameObject>("Prefabs/Monster/Effect/TornadoSkillEffect"), transform.position, Quaternion.identity);
    }
}
