using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uncommon_ReduceReloading : MonoBehaviour, IInteractable
{
    public ItemData item;
    [SerializeField] private float ReloadingTimeChange = 0.9f;

    public void GetInteractPrompt()
    {
        TooltipSystem.Show(item.description, item.displayName);
    }

    public void OnInteract()
    {
        InventoryManager.instance.AddItem(item);
        InventoryManager.instance.player._rangedWeaponModifier.ReloadingTimeStandingModifier *= ReloadingTimeChange;
        InventoryManager.instance.player._rangedWeaponModifier.ReloadingTimeCrouchingModifier *= ReloadingTimeChange;
        Destroy(gameObject);
    }
    public void ExitInteractPrompt()
    {
        TooltipSystem.Hide();
    }
}
