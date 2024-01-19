using System.Collections;
using UnityEngine;

public class BossEnemy : Enemy
{
    private Coroutine attackMoveCoroutine;
    private Coroutine phase1Cor;
    private Coroutine phase2Cor;
    private Coroutine phase3Cor;
    private bool isInPhase1;
    private bool isInPhase2;
    private bool isInPhase3;
    public BodyController bodyController { get; private set; }
    [SerializeField] private GameObject bossLootBox;

    protected override void Awake()
    {
        base.Awake();
        bodyController = GetComponent<BodyController>();
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.ChangeState(stateMachine.AttackState);
        phase1Cor = StartCoroutine(Phase1());
        CharacterHealth.OnPhase2 += () => phase2Cor = StartCoroutine(Phase2());
        CharacterHealth.OnPhase3 += () => phase3Cor = StartCoroutine(Phase3());
    }
    protected override void Update()
    {
        base.Update();
    }
    protected override void OnDie()
    {
        StopCoroutine(phase1Cor);
        StopCoroutine(phase2Cor);
        StopCoroutine(phase3Cor);

        bodyController.StopMove();

        ObjectPoolManager.Instance.GetObjectFromPool(ResourceManager.Instance.LoadResource<GameObject>("Prefabs/Monster/Effect/Boss Death Effect"), transform.position, Quaternion.identity);
        StartCoroutine(EndingCredit());
        base.OnDie();
    }
    private IEnumerator EndingCredit()
    {
        yield return new WaitForSeconds(5f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneLoadManager.Instance.LoadScene(Scenes.EndingCredit);
    }
    private IEnumerator Phase1()
    {
        WaitForSeconds wait1 = new WaitForSeconds(2.0f);
        WaitForSeconds wait2 = new WaitForSeconds(5.0f);
        WaitForSeconds wait3 = new WaitForSeconds(5.0f);

        while (true)
        {
            if (!isInPhase2 && !isInPhase3)
            {
                isInPhase1 = true;
                Animator.SetBool(AnimationData.AttackParameterHash, true);
                yield return wait1;
                Animator.SetBool(AnimationData.AttackParameterHash, false);
                ObjectPoolManager.Instance.GetObjectFromPool(ResourceManager.Instance.LoadResource<GameObject>("Prefabs/Monster/Effect/Boss Wide Effect"), transform.position, Quaternion.identity);
                yield return wait2;
                isInPhase1 = false;
                yield return wait3;
            }
            yield return null;
        }
    }

    private IEnumerator Phase2()
    {
        WaitForSeconds wait1 = new WaitForSeconds(5.0f);
        WaitForSeconds wait2 = new WaitForSeconds(3.0f);

        while (true)
        {
            if (!isInPhase1 && !isInPhase3)
            {
                isInPhase2 = true;
                ObjectPoolManager.Instance.GetObjectFromPool(ResourceManager.Instance.LoadResource<GameObject>("Prefabs/Monster/Effect/Boss MagicCircle Effect"),
                    transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
                yield return wait1;
                isInPhase2 = false;

                yield return wait2;
            }
            yield return null;
        }
    }

    private IEnumerator Phase3()
    {
        WaitForSeconds wait = new WaitForSeconds(20.0f);

        while (true)
        {
            if (!isInPhase1 && !isInPhase2)
            {
                isInPhase3 = true;
                GamePlaySceneManager.Instance.SpawnMonster(transform.position, MonsterDB.Spider_Fuga_Blue_Green);
                GamePlaySceneManager.Instance.SpawnMonster(transform.position, MonsterDB.Spider_Fuga_Red);
                GamePlaySceneManager.Instance.SpawnMonster(transform.position, MonsterDB.Spider_Fuga_Gray);
                isInPhase3 = false;
                yield return wait;
            }
            yield return null;
        }
    }

}
