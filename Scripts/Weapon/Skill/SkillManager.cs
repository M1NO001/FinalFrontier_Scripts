using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    public SkillDatabaseSO skillDatabase;

    private Dictionary<string, SkillSO> curSkillData; 

    public Player player;

    public string weaponName;

    public SkillSO ultSkill;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        curSkillData = new Dictionary<string, SkillSO>();
        ultSkill = new SkillSO();
        
        SetSkillTreeData();
        
    }

    private void SetSkillTreeData()
    {
        skillDatabase.weaponSkillDatabase.OrderBy(x => x.unlockLv);
    }

    public SkillSO GetSkillData(string skillName)
    {
        SkillSO skill = new SkillSO();
        foreach (SkillSO s in skillDatabase.weaponSkillDatabase)
        {
            if (s.displayName == skillName)
            {
                skill = s;
                break;
            }
        }

        return skill;
    }


    public SkillSO[] GetPlayerSkillDatabase()
    {
        return skillDatabase.playerSkillDatabase;
    }

    public SkillSO[] GetWeaponSkillDatabase()
    {
        return skillDatabase.weaponSkillDatabase;
    }


    public SkillSO[] GetUltimateSkillTreeDatabase()
    {
        return skillDatabase.ultimateSkillDatabase;
    }

    public Dictionary<string, SkillSO> GetCurSkillData()
    {
        return curSkillData;
    }

    public void SetCurSkillData(string key, SkillSO addSkillData)
    {
        curSkillData.Add(key, addSkillData);
    }

    public SkillSO GetUltSkillData()
    {
        return ultSkill;
    }

    public void SetUltSkillData(SkillSO skillData)
    {
        ultSkill = skillData;
    }

}