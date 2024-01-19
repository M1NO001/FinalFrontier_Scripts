using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unique_ExplosionAtk : MonoBehaviour, IInteractable
{
    public ItemData item;

    public void GetInteractPrompt()
    {
        TooltipSystem.Show(item.description, item.displayName);
    }

    public void OnInteract()
    {
        InventoryManager.instance.AddItem(item);
        // 아이템 효과
        Destroy(gameObject);
    }
    public void ExitInteractPrompt()
    {
        TooltipSystem.Hide();
    }
}
