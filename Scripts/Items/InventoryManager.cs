using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject slotHolder;
    [SerializeField] private GameObject inventoryUI;
    public static InventoryManager instance;
    public List<InventorySlot> inventoryItem = new List<InventorySlot>();
    private GameObject[] slots;
    private Image[] slotImages;
    public Player player { get; private set; }
    

    private void Awake()
    {
        instance = this;
        
    }
    private void Start()
    {
        slots = new GameObject[slotHolder.transform.childCount];
        player = GamePlaySceneManager.Instance.Player.GetComponentInChildren<Player>();
        for (int i=0;i<slotHolder.transform.childCount;i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;
        }
        slotImages = new Image[slots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            slotImages[i] = slots[i].transform.GetComponent<Image>();
        }
        RefreshUI();
    }
    public void RefreshUI()
    {
        for(int i=0;i<slots.Length;i++)
        {
            try
            {
                slotImages[i].enabled = true;
                slotImages[i].sprite = inventoryItem[i].GetItem().icon;
                slots[i].transform.GetChild(0).GetComponent<Text>().text = inventoryItem[i].GetQuantity() + "";
                slots[i].transform.GetComponent<TooltipTrigger>().SetItem(inventoryItem[i].GetItem());
            }
            catch
            {
                slotImages[i].sprite = null;
                slotImages[i].enabled = false;
                slots[i].transform.GetChild(0).GetComponent<Text>().text = "";
            }
        }
    }

    public void AddItem(ItemData item)
    {
        //inventoryItem.Add(item);
        InventorySlot slot = Contains(item);
        if (slot != null)
            slot.AddQuantity(1);
        else
        {
            inventoryItem.Add(new InventorySlot(item, 1));
        }

        RefreshUI();
    }
    public void ClearInventory()
    {
        inventoryItem.Clear();
    }

    public InventorySlot Contains(ItemData item)
    {
        foreach (InventorySlot slot in inventoryItem)
        {
            if(slot.GetItem() == item) return slot;
        }
        return null;
    }

    public void InventoryUI()
    {
        AudioManager.Instance.PlaySFX(SoundType.InventorySound);
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
            Cursor.visible = false; // Ä¿¼­¸¦ ¼û±è
            Cursor.lockState = CursorLockMode.Locked;
            TooltipSystem.Hide();
        }
           
        else
        {
            inventoryUI.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
