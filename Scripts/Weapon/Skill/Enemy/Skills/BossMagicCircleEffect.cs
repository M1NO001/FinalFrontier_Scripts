using System.Collections;
using UnityEngine;

public class BossMagicCircleEffect : EnemyEffect
{
    private Transform playerTransform;
    private Coroutine curMagicCircleAttack;
    private bool _attackSuccess;

    private void Start()
    {
        playerTransform = GamePlaySceneManager.Instance.PlayerClass.transform;
        if (curMagicCircleAttack == null)
        {
            curMagicCircleAttack = StartCoroutine(MagicCircleAttack());
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (playerTransform != null)
        {
            curMagicCircleAttack = StartCoroutine(MagicCircleAttack());
        }
        _attackSuccess = false;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        if(curMagicCircleAttack != null)
            StopCoroutine(curMagicCircleAttack);
    }

    private IEnumerator MagicCircleAttack()
    {
        WaitForSeconds wait = new WaitForSeconds(1.0f);

        while (true)
        {
            Vector2 distance = new Vector2(playerTransform.position.x - transform.position.x, playerTransform.position.z - transform.position.z);
            if (distance.sqrMagnitude < 430.0f)
            {
                GamePlaySceneManager.Instance.PlayerConditions.TakePhysicalDamage(damage);
                yield return wait;
            }
            yield return null;
        }
    }

}
