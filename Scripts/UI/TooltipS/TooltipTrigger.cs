using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ItemData item;
    Vector3 tooltipPosition = new Vector3(0,-350,0);
    public void SetItem(ItemData _item)
    {
        item = _item;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
        {
            TooltipSystem.Show(item.description, item.displayName, tooltipPosition);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }

}
