using System.Collections;
using UnityEngine;
using DG.Tweening;

public class EnemyPoisonSkillEffect : EnemyEffect
{
    [SerializeField] private GameObject circle;
    private Collider _skillCollider;

    private void Awake()
    {
        _skillCollider = GetComponent<CapsuleCollider>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        _skillCollider.enabled = false;
        circle.SetActive(false);
        StartCoroutine(OpenCircle());
    }
    private IEnumerator OpenCircle()
    {
        Vector3 point = transform.position + transform.forward * speed;

        Ray ray = new Ray(point, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            point = hit.point;
        }
        else
        {
            point = new Vector3(point.x, point.y - 1.4f, point.z);
        }
        
        transform.DOMove(point, 0.5f);

        yield return new WaitForSeconds(0.5f);

        _skillCollider.enabled = true;
        circle.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            GamePlaySceneManager.Instance.PlayerConditions.TakePhysicalDamage(damage);
            ReleaseEffect();
        }
    }
}
