using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Common_AimStable : MonoBehaviour, IInteractable
{
    public ItemData item;
    [SerializeField] private float FireMinuteAngleChange = 0.9f;
    public void GetInteractPrompt()
    {
        TooltipSystem.Show(item.description, item.displayName);
    }

    public void OnInteract()
    {
        InventoryManager.instance.AddItem(item);
        InventoryManager.instance.player._rangedWeaponModifier.MinuteOfAngleIncreasePerFireModifier *= FireMinuteAngleChange;
        Destroy(gameObject);
    }
    public void ExitInteractPrompt()
    {
        TooltipSystem.Hide();
    }
}
