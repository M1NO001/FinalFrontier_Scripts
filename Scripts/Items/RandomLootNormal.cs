using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Unity.VisualScripting;
using UnityEngine;


public class RandomLootNormal : MonoBehaviour, IInteractable
{
    private int randomNumber;
    private float randomItemNumber;
    private ItemData randomItem;

    [SerializeField] private Animator anim;
    [SerializeField] private ItemDatabase itemDB;
    [SerializeField] private Collider collider;
    private bool isOpened = false;
    [SerializeField] private string content;
    [SerializeField] private string header;
    [SerializeField] private int CommonWeight = 75;
    [SerializeField] private int UncommonWeight = 20;
    [SerializeField] private int UniqueWeight = 5;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void SpawnItem()
    {        
        int totalWeight = CommonWeight + UncommonWeight + UniqueWeight;
        randomNumber = Random.Range(0, totalWeight);

        if (randomNumber < CommonWeight) 
        {
            SpawnRandomItem(itemDB.CommonitemData);
        }
        else if(randomNumber < CommonWeight+UncommonWeight)
        {
            SpawnRandomItem(itemDB.UncommonitemData);
        }
        else
        {
            SpawnRandomItem(itemDB.UniqueitemData);            
        }       
    }   
    private void SpawnRandomItem(ItemData[] itemDatas) 
    {
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = transform.rotation;
        float totalWeight = 0f;
        for (int i = 0; i < itemDatas.Length; i++)
        {
            totalWeight += itemDatas[i].weight;
        }
        randomItemNumber = Random.Range(0, totalWeight);
        for (int i = 0;i< itemDatas.Length; i++)
        {
            if (randomItemNumber <= itemDatas[i].weight) 
            {
                randomItem = itemDatas[i];
                Instantiate(randomItem.dropPrefab, spawnPosition, spawnRotation);
                return;
            }
            else
            {
                randomItemNumber -= itemDatas[i].weight;
            }           
        }       
    }

    private void Loot()
    {
        SpawnItem();
        anim.SetBool("isOpen", true);
        isOpened = true;
        collider.enabled = false;
        
    }



    public void GetInteractPrompt()
    {
        TooltipSystem.Show(content, header);
    }

    public void OnInteract()
    {
        Loot();
    }

    public void ExitInteractPrompt()
    {
        TooltipSystem.Hide();
    }
}
