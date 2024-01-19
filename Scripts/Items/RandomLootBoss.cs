using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLootBoss : MonoBehaviour, IInteractable
{
    //public GameObject[] lights;
    private float randomNumber;
    private ItemData randomItem;

    [SerializeField] private Animator anim;
    [SerializeField] private ItemDatabase itemDB;
    [SerializeField] private Collider collider;
    private bool isOpened = false;
    [SerializeField] private string content;
    [SerializeField] private string header;
    public ItemData[] itemDatas { get; private set; }
    void Start()
    {
        anim = GetComponent<Animator>();
        itemDatas = itemDB.UniqueitemData;
    }

    private void SpawnRandomItem()
    {
        Vector3 spawnPosition = transform.position;
        Quaternion spawnRotation = transform.rotation;
        float totalWeight = 0f;
        for (int i = 0; i < itemDatas.Length; i++)
        {
            totalWeight += itemDatas[i].weight;
        }
        randomNumber = Random.Range(0, totalWeight);
        for (int i = 0;i < itemDatas.Length;i++)
        {
            if(randomNumber <= itemDatas[i].weight)
            {
                randomItem = itemDatas[i];
                return;
            }

            randomNumber -= itemDatas[i].weight;
        }
        Instantiate(randomItem.dropPrefab, spawnPosition, spawnRotation);
    }

    private void Loot()
    {
        SpawnRandomItem();
        anim.SetBool("isOpen", true);
        isOpened = true;
        collider.enabled = false;
    }


    public void GetInteractPrompt()
    {
        if (!isOpened)
        {
            TooltipSystem.Show(content, header);
        }
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
