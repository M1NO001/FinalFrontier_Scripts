using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class SkillTree : MonoBehaviour
{
    public GameObject[] skillTreeNodes;

    public SkillSO[] skillTreeDatabase;

    public int playerLevel;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillDesc;
    public TextMeshProUGUI skillTime;
    public TextMeshProUGUI coolTime;
    public List<GameObject> canActivateSkill;
    bool isUlt;

    public GameObject skillDescToolTip;
    public GameObject activeSkillBtn;

    private int firstSkillUnlockLv;
    private int secondSkillUnlockLv;
    private int thirdSkillUnlockLv;
    private int ultSkillUnlockLv;


    void Start()
    {
        firstSkillUnlockLv = 1;
        secondSkillUnlockLv = 3;
        thirdSkillUnlockLv = 5;
        ultSkillUnlockLv = 9;
        skillTreeDatabase = new SkillSO[7];
        SetSkillTreeUI();
    }

    void SetSkillTreeUI()
    {
        for(int i=0; i<7; i++)
        {
            skillTreeDatabase[i] = SkillManager.Instance.GetWeaponSkillDatabase()[i];

            Image skillSprite = skillTreeNodes[i].GetComponentInChildren<Image>();
            skillSprite.sprite = skillTreeDatabase[i].icon;

            TextMeshProUGUI levelData = skillTreeNodes[i].GetComponentInChildren<TextMeshProUGUI>();
            levelData.SetText(skillTreeDatabase[i].unlockLv.ToString());;
        }

    }

    public void OnClickNode()
    {
        playerLevel = SkillManager.Instance.player._playerModifier.PlayerLevel;
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        string clickObjectName = clickObject.name;

        clickObjectName = clickObjectName.Substring(13);
        int skillIdx = Convert.ToInt32(clickObjectName);
        
        skillName.SetText(skillTreeDatabase[skillIdx].displayName);
        skillDesc.SetText(skillTreeDatabase[skillIdx].description);
        skillTime.SetText(skillTreeDatabase[skillIdx].skillTime.ToString());
        coolTime.SetText(skillTreeDatabase[skillIdx].coolTime.ToString());

        if (!skillDescToolTip.activeSelf || skillDescToolTip == null)
        {
            skillDescToolTip.SetActive(true);
        }

        bool hasSkillData = false;

        Dictionary<string, SkillSO>.ValueCollection curSkillDataValues = SkillManager.Instance.GetCurSkillData().Values;

        if (SkillManager.Instance.GetCurSkillData() == null)
        {
            hasSkillData = false;
        }

        else
        {
            
            foreach (SkillSO skill in curSkillDataValues)
            {
                if (skillTreeDatabase[skillIdx].displayName.Equals(skill.displayName))
                {
                    hasSkillData = true;
                    break;
                }
            }
        }
        

        bool isLowerThanPlayerLevel = playerLevel >= skillTreeDatabase[skillIdx].unlockLv;
        bool hasSameunLockLevelSkill = false;

        foreach(SkillSO s in curSkillDataValues)
        {
            if(s.unlockLv == skillTreeDatabase[skillIdx].unlockLv)
            {
                hasSameunLockLevelSkill = true;
                break;
            }
        }

        isUlt = SkillManager.Instance.GetUltSkillData() == null ? true : false;


        if (!hasSkillData && isLowerThanPlayerLevel && !hasSameunLockLevelSkill && !isUlt)
        {
            activeSkillBtn.SetActive(true);
        }
        
        else 
        {
            activeSkillBtn.SetActive(false);
        }

        isUlt = false;
    }

    public void OnClickClosedBtn()
    {
        playerLevel = SkillManager.Instance.player._playerModifier.PlayerLevel;
        activeSkillBtn.SetActive(false);
        skillDescToolTip.SetActive(false);
    }

    public void OnClickActiveBtn()
    {
        string activeSkillName = skillName.text.ToString();
        for(int i=0; i<skillTreeDatabase.Length; i++)
        {
            string key = "";
            
            if (activeSkillName.Equals(skillTreeDatabase[i].displayName))
            {
                int skillUnlockLv = skillTreeDatabase[i].unlockLv;

                if (skillUnlockLv == firstSkillUnlockLv)
                    key = "Z";
                else if (skillUnlockLv == secondSkillUnlockLv)
                    key = "X";
                else if (skillUnlockLv == thirdSkillUnlockLv)
                    key = "V";
                else if(skillUnlockLv == ultSkillUnlockLv)
                    isUlt = true;

                if(isUlt)
                {
                    SkillManager.Instance.SetUltSkillData(skillTreeDatabase[i]);
                    SkillManager.Instance.player._playerSkill.SetUltSkillData();
                }
                
                else
                {
                    SkillManager.Instance.SetCurSkillData(key, skillTreeDatabase[i]);

                }

            }
        }

        activeSkillBtn.SetActive(false);
    }
    
}
