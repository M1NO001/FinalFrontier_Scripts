using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common_HpRegen : MonoBehaviour, IInteractable
{
    public ItemData item;
    [SerializeField] private float HealthPointRecoveryIncrease = 0.1f;
    public void GetInteractPrompt()
    {
        TooltipSystem.Show(item.description, item.displayName);
    }

    public void OnInteract()
    {
        InventoryManager.instance.AddItem(item);
        InventoryManager.instance.player._playerModifier.HealthPointRecoveryModifier += HealthPointRecoveryIncrease;
        Destroy(gameObject);
    }
    public void ExitInteractPrompt()
    {
        TooltipSystem.Hide();
    }
}
