using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common_AddAtk : MonoBehaviour, IInteractable
{
    public ItemData item;
    [SerializeField] private float ProjectileDamageIncrease = 0.1f;

    public void GetInteractPrompt()
    {
        TooltipSystem.Show(item.description, item.displayName);
    }

    public void OnInteract()
    {
        InventoryManager.instance.AddItem(item);
        InventoryManager.instance.player._rangedWeaponModifier.ProjectileDamageModifier += ProjectileDamageIncrease;
        Destroy(gameObject);
    }
    public void ExitInteractPrompt()
    {
        TooltipSystem.Hide();
    }
}
