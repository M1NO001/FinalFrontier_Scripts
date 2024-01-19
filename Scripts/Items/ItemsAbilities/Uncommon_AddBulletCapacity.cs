using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uncommon_AddBulletCapacity : MonoBehaviour, IInteractable
{
    public ItemData item;
    [SerializeField] private float BulletCapacityIncrease = 0.2f;
    public void GetInteractPrompt()
    {
        TooltipSystem.Show(item.description, item.displayName);
    }

    public void OnInteract()
    {
        InventoryManager.instance.AddItem(item);
        InventoryManager.instance.player._rangedWeaponModifier.BulletCapacityModifier += BulletCapacityIncrease;
        Destroy(gameObject);
    }
    public void ExitInteractPrompt()
    {
        TooltipSystem.Hide();
    }
}
