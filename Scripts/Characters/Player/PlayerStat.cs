using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{

    private Player player;
    private PlayerSO Data;


    public CharacterStat MoveSpeed;
    [SerializeField] int asdf = 8;

    private void Awake()
    {
        player = GetComponent<Player>();
        Data = player.Data;
    }

    

    void Start()
    {
        //MoveSpeed = new CharacterStat(Data.PlayerConditionData.MovementSpeed);
    }

    
    void Update()
    {
        
    }
}


[System.Serializable]
public class CharacterStat
{
    public float BaseStat;

    public float CurrentStat;

    public List<Modifier> Modifier;

    public Dictionary<string, Modifier> MultiplyModifier;

    public Dictionary<string, Modifier> AddSubModifier;

    public CharacterStat(float baseStat)
    {
        BaseStat = baseStat;
        CurrentStat = BaseStat;

        
        MultiplyModifier = new Dictionary<string, Modifier>();
        AddSubModifier = new Dictionary<string, Modifier>();
    }

    public float GetIntStat()
    {
        return (int)CurrentStat;
    }

    public float GetFloatStat()
    {
        return CurrentStat;
    }

    private void StatUpdate()
    {
        CurrentStat = BaseStat;

        if (MultiplyModifier != null)
        {
            foreach (Modifier i in MultiplyModifier.Values)
            {
                CurrentStat *= i.getValue();
            }
        }

        if (AddSubModifier != null)
        {
            foreach (Modifier i in AddSubModifier.Values)
            {
                CurrentStat += i.getValue();
            }
        }
    }

    public void Add_AddSubModifier(string name)
    {
        AddSubModifier.Add(name, new Modifier());
        StatUpdate();
    }

    public void Remove_AddSubModifier(string name)
    {
        AddSubModifier.Remove(name);
        StatUpdate();
    }

    public void Add_MultiplyModifier(string name)
    {
        MultiplyModifier.Add(name, new Modifier());
        StatUpdate();
    }

    public void Remove_MultiplyModifier(string name)
    {
        MultiplyModifier.Remove(name);
        StatUpdate();
    }

    public void Add_ModifierOption(string modifierName, string modifierOptionName, float value)
    {
        if (AddSubModifier.ContainsKey(modifierName))
        {
            AddSubModifier[modifierName].AddModifierOption(modifierOptionName, value);
        }
        else if (MultiplyModifier.ContainsKey(modifierName))
        {
            MultiplyModifier[modifierName].AddModifierOption(modifierOptionName, value);
        }
        else
        {
            Debug.Log("Error : " + modifierName + " Modifier Option doesn't exist");
        }

        StatUpdate();
    }

    public void Remove_ModifierOption(string modifierName, string modifierOptionName)
    {
        if (AddSubModifier.ContainsKey(modifierName))
        {
            AddSubModifier[modifierName].RemoveModifierOption(modifierOptionName);
        }
        else if (MultiplyModifier.ContainsKey(modifierName))
        {
            MultiplyModifier[modifierName].RemoveModifierOption(modifierOptionName);
        }
        else
        {
            Debug.Log("Error : " + modifierName + " Modifier Option doesn't exist");
        }

        StatUpdate();
    }

}

[System.Serializable]
public class Modifier
{
    string ModifierName;
    float CurrentModifierValue;

    public List<float> Option;
    Dictionary<string, float> modifierOption;

    public Modifier()
    {
        //ModifierName = name;
        modifierOption = new Dictionary<string, float>();
    }

    public void AddModifierOption(string name, float value)
    {
        modifierOption.Add(name, value);
    }

    public void RemoveModifierOption(string name)
    {
        modifierOption.Remove(name);
    }

    public void UpdateModifierOption(string name, float value)
    {
        if (modifierOption.ContainsKey(name))
        {
            modifierOption[name] = value;
        }
        else
        {
            Debug.Log("Error : " + name + " Modifier Option doesn't exist");
        }
    }

    public void setModifierOption(string name)
    {
        if (modifierOption.ContainsKey(name))
        {
            CurrentModifierValue = modifierOption[name];
        }
        else
        {
            Debug.Log("Error : " + name + " Modifier Option doesn't exist");
        }
    }

    public string getName()
    {
        return ModifierName;
    }

    public float getValue()
    {
        return CurrentModifierValue;
    }
}