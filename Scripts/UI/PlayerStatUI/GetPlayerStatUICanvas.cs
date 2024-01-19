using System.Collections;
using UnityEngine;

public class GetPlayerStatUICanvas : MonoBehaviour
{
    public GameObject PlayerStatUI;
    private SkillSO[] skillDataArr;

    public bool isActivePlayerUICanvas = false;
    private bool activeFirstSkillCoroutine = false;
    private bool activeSecondSkillCoroutine = false;
    private bool activeThirdSkillCoroutine = false;

    private Vector3 spawnPosition;
    public Transform player;

    private string firstSkillKey = "Z";
    private string secondSkillKey = "X";
    private string thirdSkillKey = "V";

    private float firstSkillTimer;
    private float secondSkillTimer;
    private float thirdSkillTimer;

    private float firstCoolTime;
    private float secondCoolTime;
    private float thirdCoolTime;

    private void Start()
    {
        skillDataArr = new SkillSO[3];
        spawnPosition = new Vector3(0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GamePaused();
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!activeFirstSkillCoroutine && (SkillManager.Instance.GetCurSkillData().ContainsKey(firstSkillKey) && firstSkillTimer >= firstCoolTime))
            {
                skillDataArr[0] = SkillManager.Instance.GetCurSkillData()[firstSkillKey];
                firstCoolTime = skillDataArr[0].coolTime;
                StartCoroutine(ActiveFirstKeySkill());
            }

        }

        else if (Input.GetKeyDown(KeyCode.X))
        {
            if (!activeSecondSkillCoroutine && (SkillManager.Instance.GetCurSkillData().ContainsKey(secondSkillKey)) && secondSkillTimer >= secondCoolTime)
            {
                skillDataArr[1] = SkillManager.Instance.GetCurSkillData()[secondSkillKey];
                secondCoolTime = skillDataArr[1].coolTime;
                StartCoroutine(ActiveSecondKeySkill());
            }

        }

        else if (Input.GetKeyDown(KeyCode.V))
        {
            if (!activeThirdSkillCoroutine && (SkillManager.Instance.GetCurSkillData().ContainsKey(thirdSkillKey)) && thirdSkillTimer >= thirdCoolTime)
            {
                skillDataArr[2] = SkillManager.Instance.GetCurSkillData()[thirdSkillKey];
                thirdCoolTime = skillDataArr[2].coolTime;
                StartCoroutine(ActiveThirdKeySkill());
            }

        }

        if(firstSkillTimer < firstCoolTime)
        {
            firstSkillTimer += Time.deltaTime;
        }

        if (secondSkillTimer < secondCoolTime)
        {
            secondSkillTimer += Time.deltaTime;
        }

        if (thirdSkillTimer < thirdCoolTime)
        {
            thirdSkillTimer += Time.deltaTime;
        }

    }

    public void GamePaused()
    {
        if (GamePlaySceneManager.Instance.pause)
        {

            GamePlaySceneManager.Instance.CloseGameMenu();
        }

        else
        {
            if (!isActivePlayerUICanvas)
            {
                ShowPlayerMenu();
            }
            else
            {
                ClosePlayerMenu();
            }
        }

    }

    public void ShowPlayerMenu()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SkillManager.Instance.player._playerInput.enabled = false;
        isActivePlayerUICanvas = true;
        Time.timeScale = 0f;
        
        PlayerStatUI.SetActive(isActivePlayerUICanvas);
    }

    public void ClosePlayerMenu()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SkillManager.Instance.player._playerInput.enabled = true;
        isActivePlayerUICanvas = false;
        Time.timeScale = 1f;

        PlayerStatUI.SetActive(isActivePlayerUICanvas);

    }

    IEnumerator ActiveFirstKeySkill()
    {
        activeFirstSkillCoroutine = true;
        GameObject curSkillEffect = (GameObject)Instantiate(skillDataArr[0].skillEffect, spawnPosition, Quaternion.identity);
        
        float skillTime = skillDataArr[0].skillTime;
        curSkillEffect.transform.parent = this.transform;
        curSkillEffect.transform.position = this.transform.position;
        curSkillEffect.SetActive(true);

        switch(skillDataArr[0].name)
        {
            case "DamageBuffBullet":
                curSkillEffect.GetComponent<DamageBuffBullet>().SetSkillData(skillDataArr[0]);
                break;
            case "AttackSpeedBuff":
                curSkillEffect.GetComponent<AttackSpeedBuff>().SetSkillData(skillDataArr[0]);
                break;

        }

        Destroy(curSkillEffect, skillTime+0.1f);
        activeFirstSkillCoroutine = false;
        firstSkillTimer = 0f;
        yield return null;
    }

    IEnumerator ActiveSecondKeySkill()
    {
        activeSecondSkillCoroutine = true;
        GameObject curSkillEffect = (GameObject)Instantiate(skillDataArr[1].skillEffect, spawnPosition, Quaternion.identity);
        
        float skillTime = skillDataArr[1].skillTime;
        curSkillEffect.transform.parent = this.transform;
        curSkillEffect.transform.position = this.transform.position;
        curSkillEffect.SetActive(true);

        switch (skillDataArr[1].name)
        {
            case "InstantHeal":
                curSkillEffect.GetComponent<InstantHeal>().SetSkillData(skillDataArr[1]);
                break;
            case "HealOverTime":
                curSkillEffect.GetComponent<HealOverTime>().SetSkillData(skillDataArr[1]);
                break;

        }
        Destroy(curSkillEffect, skillTime + 0.1f);
        activeSecondSkillCoroutine = false;
        secondSkillTimer = 0f;
        yield return null;
    }

    IEnumerator ActiveThirdKeySkill()
    {
        activeThirdSkillCoroutine = true;
        GameObject curSkillEffect = (GameObject)Instantiate(skillDataArr[2].skillEffect, spawnPosition, Quaternion.identity);
  
        float skillTime = skillDataArr[2].skillTime;
        curSkillEffect.transform.parent = this.transform;
        curSkillEffect.transform.position = this.transform.position;
        curSkillEffect.SetActive(true);

        switch (skillDataArr[2].name)
        {
            case "PlayerSkillCoolDownReduce":
                curSkillEffect.GetComponent<PlayerSkillCoolDown>().SetSkillData(skillDataArr[2]);
                break;
            case "PlayerSpeedBuff":
                curSkillEffect.GetComponent<PlayerSpeedBuff>().SetSkillData(skillDataArr[2]);
                break;
        }
        Destroy(curSkillEffect, skillTime + 0.1f);
        activeThirdSkillCoroutine = false;
        thirdSkillTimer = 0f;
        yield return null;
    }

}