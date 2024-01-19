using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common_AddHp : MonoBehaviour, IInteractable
{

    public ItemData item;
    [SerializeField] private float HealthPointIncrease = 0.1f;
    public void GetInteractPrompt()
    {
        TooltipSystem.Show(item.description, item.displayName);
    }

    public void OnInteract()
    {
        InventoryManager.instance.AddItem(item);
        InventoryManager.instance.player._playerModifier.HealthPointModifier += HealthPointIncrease;
        Destroy(gameObject);
    }
    public void ExitInteractPrompt()
    {
        TooltipSystem.Hide();
    }
}
