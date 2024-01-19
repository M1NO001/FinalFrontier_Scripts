using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem instance;

    public Tooltip tooltip;

    private void Awake()
    {
        instance = this;
    }

    public static void Show(string content, string header, Vector3? position = null)
    {
        instance.tooltip.SetText(content, header);
        instance.tooltip.gameObject.SetActive(true);
        if (position != null)
        {
            instance.tooltip.SetPosition(position);
        }
        else { instance.tooltip.SetPosition(); }
    }

    public static void Hide()
    {
        instance.tooltip.gameObject.SetActive(false);
    }
}
