using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common_AddAS : MonoBehaviour, IInteractable
{
    public ItemData item;
    [SerializeField]private float FireRateIncrease = 0.2f;

    public void GetInteractPrompt()
    {
        TooltipSystem.Show(item.description, item.displayName);
    }

    public void OnInteract()
    {
        InventoryManager.instance.AddItem(item);
        InventoryManager.instance.player._rangedWeaponModifier.FireRatePerMinuteModifier += FireRateIncrease;
        Destroy(gameObject);
    }

    public void ExitInteractPrompt()
    {
        TooltipSystem.Hide();
    }
}
