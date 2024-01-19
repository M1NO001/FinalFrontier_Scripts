using System.Collections;
using UnityEngine;

public class Leg : MonoBehaviour
{
    [SerializeField] private Transform skeletonTransform;
    [SerializeField] private Transform rayOrigin;
    public Transform ikTarget;
    public Vector3 curTip { get; private set; }

    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private AnimationCurve heightCurve;

    private float tipMaxHeight = 5.0f;
    private float tipAnimationTime = 0.3f;
    private float tipAnimationFrameTime = 1 / 20.0f;

    private float ikOffset = 3.0f;
    private float tipMoveDist = 2.0f;
    private float maxRayDist = 15.0f;

    private Coroutine curCoroutine;

    public Vector3 TipPos { get; private set; }
    public Vector3 TipUpDir { get; private set; }
    public Vector3 RaycastTipPos { get; private set; }
    public Vector3 RaycastTipNormal { get; private set; }

    public bool Animating { get; private set; } = false;
    public bool Movable { get; set; } = false;
    public float TipDistance { get; private set; }

    private void Awake()
    {
        TipPos = ikTarget.transform.position;
        RaycastTipNormal = transform.up;
    }

    private void Start()
    {
        UpdateIKTargetTransform();
    }

    private void Update()
    {
        if (Animating) return;

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin.position, -skeletonTransform.forward.normalized, out hit, maxRayDist))
        {
            RaycastTipPos = hit.point;
            RaycastTipNormal = hit.normal;
        }

        TipDistance = (RaycastTipPos - TipPos).magnitude;

        if ((TipDistance > tipMoveDist && Movable))
        {
            if (curCoroutine != null) StopCoroutine(curCoroutine);
            curCoroutine = StartCoroutine(AnimateLeg());
        }
    }

    private IEnumerator AnimateLeg()
    {
        Animating = true;

        float timer = 0.0f;
        float animTime;

        Vector3 startingTipPos = TipPos;
        Vector3 tipDirVec = RaycastTipPos - TipPos;

        TipUpDir = RaycastTipNormal.normalized;

        while (timer < tipAnimationTime + tipAnimationFrameTime)
        {
            animTime = speedCurve.Evaluate(timer / tipAnimationTime);

            float tipAcceleration = Mathf.Max((RaycastTipPos - startingTipPos).magnitude / tipDirVec.magnitude, 1.0f);

            TipPos = startingTipPos + tipDirVec * tipAcceleration * animTime;
            TipPos += TipUpDir * heightCurve.Evaluate(animTime) * tipMaxHeight;

            UpdateIKTarget();

            timer += tipAnimationFrameTime;

            yield return new WaitForSeconds(tipAnimationFrameTime);
        }
        UpdateIKTargetTransform();
        Animating = false;
    }

    private void UpdateIKTargetTransform()
    {
        ikTarget.transform.position = TipPos + RaycastTipNormal.normalized * ikOffset;
        curTip = TipPos;
    }

    public void UpdateIKTarget()
    {
        ikTarget.transform.position = TipPos + RaycastTipNormal.normalized * ikOffset;
    }
}