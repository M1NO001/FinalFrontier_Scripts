using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Unique_AddCri : MonoBehaviour, IInteractable
{
    public ItemData item;
    [SerializeField] private float CriticalChanceIncrement = 0.5f;
    private const float MaxCriticalChance = 1f;
    float criticalChance;
    float criticalMultiple;
    public void GetInteractPrompt()
    {
        TooltipSystem.Show(item.description, item.displayName);
    }

    public void OnInteract()
    {
        if (InventoryManager.instance == null || InventoryManager.instance.player == null)
        {
            Debug.LogError("InventoryManager 또는 player 인스턴스가 null입니다.");
            return;
        }
        RefreshVariables();
        InventoryManager.instance.AddItem(item);

        if (criticalChance < MaxCriticalChance)
        {
            InventoryManager.instance.player._rangedWeaponModifier.CriticalDamageChanceModifier += CriticalChanceIncrement;
            RefreshVariables();
            if (criticalChance > MaxCriticalChance)
            {
                float overflowChance = criticalChance - MaxCriticalChance;
                InventoryManager.instance.player._rangedWeaponModifier.CriticalDamageChanceModifier = MaxCriticalChance;
                InventoryManager.instance.player._rangedWeaponModifier.CriticalDamageMultiplierModifier += overflowChance;
            }
        }
        else
        {
            InventoryManager.instance.player._rangedWeaponModifier.CriticalDamageMultiplierModifier += CriticalChanceIncrement;
        }

        Destroy(gameObject);
    }
    void RefreshVariables()
    {
        criticalChance = InventoryManager.instance.player._rangedWeaponModifier.CriticalDamageChanceModifier;
        criticalMultiple = InventoryManager.instance.player._rangedWeaponModifier.CriticalDamageMultiplierModifier;
    }
    public void ExitInteractPrompt()
    {
        TooltipSystem.Hide();
    }
}
