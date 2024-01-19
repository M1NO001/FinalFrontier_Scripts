using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    Player player;
    PlayerInput input;
    Animator animator;
    [SerializeField] TwoBoneIKConstraint twoBoneIKConstraintLeftHand;

    public bool skillStartAnimation = false;
    public bool skillProgressAnimation = false;
    public bool skillEndAnimation = false;

    public GameObject skillPref;
    public GameObject UltskillPref;
    [SerializeField] Transform skillPosition;
    [SerializeField] Transform UltSkillPosition;
    GameObject skill;

    public float UltSpeed;
    public float skillSpeed;
    public float skillCoolTime;
    public float UltSkillCoolTime;
    public float skillTimer;
    public float UltSkillTimer;

    private bool isUlt;
    private bool isSetUlt = false;

    //UI
    [SerializeField] Image skillUI;
    [SerializeField] Image skillUIbg;
    [SerializeField] Image UltskillUI;
    [SerializeField] Image UltskillUIbg;

    private void Awake()
    {
        player = GetComponent<Player>();
        input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        input.PlayerActions.Skill.performed += SkillReady;
        input.PlayerActions.Skill.canceled += SkillFire;

        input.PlayerActions.Ult.performed += UltSkillReady;
        input.PlayerActions.Ult.canceled += UltSkillFire;

        SetSkillData();

        skillUI.gameObject.SetActive(false);
        UltskillUI.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (skill)
        {
            skill.transform.position = skillPosition.position;
        }

        if (skillTimer < skillCoolTime)
        {
            skillTimer += Time.deltaTime;
        }

        if (UltSkillTimer < UltSkillCoolTime)
        {
            UltSkillTimer += Time.deltaTime;
        }

        if(skillUI && skillPref)
        {
            skillUI.gameObject.SetActive(true);
            skillUI.fillAmount = skillTimer / skillCoolTime;
        }

        if(UltskillUI && UltskillPref)
        {
            UltskillUI.gameObject.SetActive(true);
            UltskillUI.fillAmount = UltSkillTimer / UltSkillCoolTime;
        }
    }

    private void SetSkillData()
    {
        skillPref = GameManager.Instance.playerSkillData.skillEffect;
        skillSpeed = GameManager.Instance.playerSkillData.projectileSpeed;
        skillCoolTime = GameManager.Instance.playerSkillData.coolTime;
        skillTimer = skillCoolTime;

        if (skillUI)
        {
            skillUI.sprite = GameManager.Instance.playerSkillData.icon;
            skillUI.type = Image.Type.Filled;
        }
        if (skillUIbg)
        {
            skillUIbg.sprite = GameManager.Instance.playerSkillData.icon;
            Color c = skillUIbg.color;
            c.a = 0.5f;
            skillUIbg.color = c;
        }
    }

    public void SetUltSkillData()
    {
        UltskillPref = SkillManager.Instance.GetUltSkillData().skillEffect;
        UltSpeed = SkillManager.Instance.GetUltSkillData().projectileSpeed;
        UltSkillCoolTime = SkillManager.Instance.GetUltSkillData().coolTime;
        UltSkillTimer = UltSkillCoolTime;

        if (UltskillUI)
        {
            UltskillUI.sprite = SkillManager.Instance.GetUltSkillData().icon;
            UltskillUI.type = Image.Type.Filled;
        }
        if (UltskillUIbg)
        {
            UltskillUIbg.sprite = SkillManager.Instance.GetUltSkillData().icon;
            Color c = UltskillUIbg.color;
            c.a = 0.5f;
            UltskillUIbg.color = c;
        }
    }


    private void UltSkillReady(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!UltskillPref) return;
        isUlt = true;
        if (!skillStartAnimation && !skillEndAnimation && !skillProgressAnimation && !player.isReloading && UltSkillTimer >= UltSkillCoolTime && UltskillPref != null)
        {
            StartCoroutine(SmoothAnimationStart());
            Vector3 skillDir = (player.mouseWorldPosition - skillPosition.position).normalized;

            skill = ObjectPoolManager.Instance
                    .GetObjectFromPool(UltskillPref, skillPosition.position, Quaternion.LookRotation(skillDir, Vector3.up));
            skill.GetComponentInChildren<Rigidbody>().useGravity = false;
        }
    }
    private void UltSkillFire(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (skillStartAnimation && !skillEndAnimation && !skillProgressAnimation)
        {
            skillProgressAnimation = true;
            skillStartAnimation = false;
            twoBoneIKConstraintLeftHand.weight = 0f;
            animator.SetLayerWeight(2, 1);

            animator.SetTrigger("Skill");
        }
        else if (!skillStartAnimation && !skillEndAnimation && skillProgressAnimation)
        {
            animator.SetTrigger("Skill");
        }
    }



    private void SkillReady(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!skillPref) return;
        isUlt = false;
        if (!skillStartAnimation && !skillEndAnimation && !skillProgressAnimation && !player.isReloading && skillTimer >= skillCoolTime && skillPref!= null)
        {
            StartCoroutine(SmoothAnimationStart());
            Vector3 skillDir = (player.mouseWorldPosition - skillPosition.position).normalized;

            skill = ObjectPoolManager.Instance
                    .GetObjectFromPool(skillPref, skillPosition.position, Quaternion.LookRotation(skillDir, Vector3.up));
            skill.GetComponentInChildren<Rigidbody>().useGravity = false;
        }
        
    }

    private void SkillFire(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (skillStartAnimation && !skillEndAnimation && !skillProgressAnimation)
        {
            skillProgressAnimation = true;
            skillStartAnimation = false;
            twoBoneIKConstraintLeftHand.weight = 0f;
            animator.SetLayerWeight(2, 1);

            animator.SetTrigger("Skill");
        }
        else if(!skillStartAnimation && !skillEndAnimation && skillProgressAnimation)
        {
            animator.SetTrigger("Skill");
        }
    }

    public void SkillReady()
    {
        
    }

    public void SkillShot()
    {
        if (skill)
        {
            Vector3 skillDir;
            float speed;
            if (isUlt)
            {
                speed = UltSpeed;
                skillDir = UltSkillPosition.position.normalized;
            }
            else
            {
                speed = skillSpeed;
                skillDir = (player.mouseWorldPosition - player._rangedWeaponModifier.spawnBulletPosition.position).normalized;
            }
            skill.transform.rotation = Quaternion.LookRotation(skillDir, Vector3.up);
            skill.GetComponentInChildren<Rigidbody>().velocity = transform.forward * speed;
            //skill.GetComponentInChildren<Rigidbody>().useGravity = true;
            skill = null;
            if (isUlt == false)
            {
                skillTimer = 0f;
            }
            else
            {
                UltSkillTimer = 0f;
            }
        }
    }

    private void SkillEnds()
    {
        if (!skillStartAnimation && !skillEndAnimation && skillProgressAnimation)
        {
            StartCoroutine(SmoothAnimationEnd());
        }
    }

    IEnumerator SmoothAnimationStart()
    {
        skillStartAnimation = true;

        float start = 0f;
        float target = 1f;
        float timeDelta = 0f;
        float tmp=0f;
        while (tmp < target && skillStartAnimation)
        {
            timeDelta += Time.deltaTime*5f;
            tmp = Mathf.Lerp(start, target, timeDelta);
            animator.SetLayerWeight(2, tmp);
            twoBoneIKConstraintLeftHand.weight = 1 - tmp;
            yield return null;
        }

        skillProgressAnimation = true;
        skillStartAnimation = false;

        yield break;
    }

    IEnumerator SmoothAnimationEnd()
    {
        skillEndAnimation = true;
        skillProgressAnimation = false;

        float start = 0f;
        float target = 1f;
        float timeDelta = 0f;
        float tmp = 0f;
        while (tmp < target && skillEndAnimation)
        {
            timeDelta += Time.deltaTime * 5f;
            tmp = Mathf.Lerp(start, target, timeDelta);
            animator.SetLayerWeight(2, 1-tmp);
            twoBoneIKConstraintLeftHand.weight = tmp;


            yield return null;
        }

        skillEndAnimation = false;

        yield break;
    }

    private void OnDisable()
    {
        input.PlayerActions.Skill.performed -= SkillReady;
        input.PlayerActions.Skill.canceled -= SkillFire;


        input.PlayerActions.Ult.performed -= SkillFire;
        input.PlayerActions.Ult.canceled -= SkillFire;
    }
}
