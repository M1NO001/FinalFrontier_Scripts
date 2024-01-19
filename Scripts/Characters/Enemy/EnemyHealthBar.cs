using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private void LateUpdate()
    {
        Vector3 targetDirection = Camera.main.transform.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        transform.rotation = lookRotation;
    }
}
