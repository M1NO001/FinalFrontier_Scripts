using System.Collections;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    public Leg[] legs;
    [SerializeField] private Transform skeletonTransform;
    [SerializeField] private Transform rayOriginsTransform;

    private float maxTipWait = 2.5f;

    private bool stepOrder = true;

    private Coroutine curCor;
    private Coroutine curMoveCor;

    private WaitForFixedUpdate waitFixedUpdate1;
    private WaitForFixedUpdate waitFixedUpdate2;
    private WaitForFixedUpdate waitFixedUpdate3;
    private WaitForFixedUpdate waitFixedUpdate4;

    public Vector3 TargetPosition = Vector3.zero;
    public Vector3 TargetMoveDirection = Vector3.zero;
    public float TargetMoceDistance = 0;
    public Vector3 RecentMovePoint = Vector3.zero;

    private Vector3 _recentMovePoint = Vector3.zero;

    private Quaternion defaultSkeletonRotOff;
    private float skeletonYValueOff;

    private void Start()
    {
        InitDefaultValue();

        curCor = StartCoroutine(Moving());
        curMoveCor = StartCoroutine(MoveToTargetCoroutine());
    }

    public void StopMove()
    {
        if(curCor != null)
            StopCoroutine(curCor);
        if (curMoveCor != null)
            StopCoroutine(curMoveCor);
    }

    private void InitDefaultValue()
    {
        waitFixedUpdate1 = new WaitForFixedUpdate();
        waitFixedUpdate2 = new WaitForFixedUpdate();
        waitFixedUpdate3 = new WaitForFixedUpdate();
        waitFixedUpdate4 = new WaitForFixedUpdate();

        RecentMovePoint = transform.position;
        _recentMovePoint = transform.position;

        defaultSkeletonRotOff = skeletonTransform.rotation;
        Vector3 cummulativeFeetPos = Vector3.zero;
        foreach (Leg leg in legs)
        {
            cummulativeFeetPos += leg.curTip;
        }
        cummulativeFeetPos /= legs.Length;
        skeletonYValueOff = skeletonTransform.position.y - cummulativeFeetPos.y;
    }


    private IEnumerator Moving()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        while (true)
        {
            MoveToTarget(GamePlaySceneManager.Instance.PlayerClass.transform.position);

            yield return wait;
        }
    }


    public void MoveToTarget(Vector3 movePoint)
    {
        if(_recentMovePoint == movePoint) return;
        Vector3 temp = new Vector3((movePoint - transform.position).x, 0, (movePoint - transform.position).z);

        _recentMovePoint = movePoint;
        TargetMoceDistance = temp.magnitude;
        if(TargetMoceDistance > 10)
        {
            TargetMoveDirection = temp.normalized * (TargetMoceDistance - 10);
            TargetPosition = transform.position + temp.normalized * (TargetMoceDistance - 10);
        }
        else
        {
            TargetMoveDirection = temp.normalized * (TargetMoceDistance);
            TargetPosition = transform.position + temp.normalized * (TargetMoceDistance);
        }
    }

    public IEnumerator MoveToTargetCoroutine()
    {
        while (true)
        {
            float stepAngle = 15;
            float rotationAngle = Vector2.SignedAngle(new Vector2((transform.forward).x, (transform.forward).z), new Vector2(TargetMoveDirection.x, TargetMoveDirection.z));
            float angleSign = (rotationAngle > 0 ? 1.0f : -1.0f);
            Quaternion defaultRotation = transform.rotation;

            rayOriginsTransform.parent = null;
            Vector3 defaultRayOriginsPosition = rayOriginsTransform.position;
            Vector3 defaultPosition = transform.position;
            rayOriginsTransform.parent = transform;
            Vector3 direction = TargetMoveDirection;
            float distance = TargetMoveDirection.magnitude > 1.0f ? TargetMoveDirection.magnitude : 1.0f;
            float percent = 4 * Time.fixedDeltaTime / distance;

            Vector3 recentTargetMovePosition = TargetPosition;

            for (int i = 0; i < (int)Mathf.Abs(rotationAngle / stepAngle); i++)
            {
                while (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, -stepAngle * angleSign * (i + 1), 0) * defaultRotation) > 2f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -stepAngle * angleSign, 0) * transform.rotation, 2 * Time.deltaTime);

                    if(PlanarDistance(defaultPosition + direction, transform.position) > 0.05f)
                    {
                        rayOriginsTransform.parent = null;
                        rayOriginsTransform.position = Vector3.Lerp(rayOriginsTransform.position, defaultRayOriginsPosition + direction, percent);
                        transform.position = Vector3.Lerp(transform.position, defaultPosition + direction, percent);
                        rayOriginsTransform.parent = transform;
                    }

                    yield return waitFixedUpdate1;
                    if (recentTargetMovePosition != TargetPosition)
                    {
                        break;
                    }
                }
                if (recentTargetMovePosition != TargetPosition)
                {
                    break;
                }
                transform.rotation = Quaternion.Euler(0, -stepAngle * angleSign * (i + 1), 0) * defaultRotation;

                yield return waitFixedUpdate2;
                if (recentTargetMovePosition != TargetPosition)
                {
                    break;
                }

                float time1 = 0; 
                float percent1 = Time.fixedDeltaTime / 3.0f;
                while (time1 < 1)
                {
                    time1 += percent1;
                    AdjustBodyTransform(percent1);
                }
            }
            if (recentTargetMovePosition != TargetPosition)
            {
                continue;
            }

            while (Quaternion.Angle(transform.rotation, Quaternion.Euler(0, -rotationAngle, 0) * defaultRotation) > 2f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, -rotationAngle, 0) * defaultRotation, 2 * Time.deltaTime);

                if (PlanarDistance(defaultPosition + direction, transform.position) > 0.05f)
                {
                    rayOriginsTransform.parent = null;
                    rayOriginsTransform.position = Vector3.Lerp(rayOriginsTransform.position, defaultRayOriginsPosition + direction, percent);
                    transform.position = Vector3.Lerp(transform.position, defaultPosition + direction, percent);
                    rayOriginsTransform.parent = transform;
                }

                yield return waitFixedUpdate3;
                if (recentTargetMovePosition != TargetPosition)
                {
                    break;
                }
            }
            if (recentTargetMovePosition != TargetPosition)
            {
                continue;
            }
            transform.rotation = Quaternion.Euler(0, -rotationAngle, 0) * defaultRotation;

            while (PlanarDistance(defaultPosition + direction, transform.position) > 0.05f)
            {
                rayOriginsTransform.parent = null;
                rayOriginsTransform.position = Vector3.Lerp(rayOriginsTransform.position, defaultRayOriginsPosition + direction, percent);
                transform.position = Vector3.Lerp(transform.position, defaultPosition + direction, percent);
                rayOriginsTransform.parent = transform;

                yield return waitFixedUpdate4;
                if (recentTargetMovePosition != TargetPosition)
                {
                    break;
                }
            }

            float time2 = 0;
            float percent2 = Time.fixedDeltaTime / 3.0f;
            while (time2 < 1)
            {
                time2 += percent2;
                AdjustBodyTransform(percent2);
            }
            if (recentTargetMovePosition != TargetPosition )
            {
                continue;
            }

            yield return null;
        }
    }

    private void Update()
    {
        if (legs.Length < 2) return;

        for (int i = 0; i < legs.Length; i++)
        {
            if (legs[i].TipDistance > maxTipWait)
            {
                stepOrder = i % 2 == 0;
                break;
            }
        }

        foreach (Leg leg in legs)
        {
            leg.Movable = stepOrder;
            stepOrder = !stepOrder;
        }

        int index = stepOrder ? 0 : 1;
    }


    private float PlanarDistance(Vector3 vec1, Vector3 vec2)
    {
        return (vec1.x - vec2.x) * (vec1.x - vec2.x) + (vec1.z - vec2.z) * (vec1.z - vec2.z);
    }

    private void AdjustBodyTransform(float percent)
    {
        Vector3 cummulativeLeftFeetPos = Vector3.zero;
        Vector3 cummulativeRightFeetPos = Vector3.zero;
        Vector3 cummulativeFrontFeetPos = Vector3.zero;
        Vector3 cummulativeHindFeetPos = Vector3.zero;
        Vector3 cummulativeFeetPos = Vector3.zero;
        for (int i = 0; i < legs.Length; i++)
        {
            if (i < legs.Length / 2)
            {
                cummulativeRightFeetPos += legs[i].curTip;
                cummulativeFeetPos += legs[i].curTip;
            }
            else
            {
                cummulativeLeftFeetPos += legs[i].curTip;
                cummulativeFeetPos += legs[i].curTip;
            }

            if (i < 6 && i > 1)
            {
                cummulativeHindFeetPos += legs[i].curTip;
            }
            else
            {
                cummulativeFrontFeetPos += legs[i].curTip;
            }
        }

        Vector3 averageLeftFeetPos = cummulativeLeftFeetPos / (legs.Length / 2);
        Vector3 averageRightFeetPos = cummulativeRightFeetPos / (legs.Length / 2);
        Vector3 averageFrontFeetPos = cummulativeFrontFeetPos / (legs.Length / 2);
        Vector3 averageHindFeetPos = cummulativeHindFeetPos / (legs.Length / 2);
        Vector3 averageFeetPos = cummulativeFeetPos / legs.Length;

        Vector3 rollVector = (averageLeftFeetPos - averageRightFeetPos);
        Vector3 pitchVector = (averageFrontFeetPos - averageHindFeetPos);

        float rollAngle = Vector3.SignedAngle(transform.right, rollVector, transform.forward);
        float pitchAngle = Vector3.SignedAngle(transform.forward, pitchVector, transform.right);

        Quaternion roll = Quaternion.AngleAxis(rollAngle, -skeletonTransform.up);
        Quaternion pitch = Quaternion.AngleAxis(pitchAngle, skeletonTransform.right);

        skeletonTransform.position = Vector3.Lerp(skeletonTransform.position, new Vector3(skeletonTransform.position.x, skeletonYValueOff + averageFeetPos.y, skeletonTransform.position.z), percent);
        skeletonTransform.rotation = Quaternion.Slerp(skeletonTransform.rotation, roll * pitch * transform.rotation * defaultSkeletonRotOff, percent);
    }
}
