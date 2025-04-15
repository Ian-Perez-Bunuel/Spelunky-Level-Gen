using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    //these are the ground slots
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public float timeStamp;

    public Item[] itemsInInventory;
    public int entryPlacment = 0;

    private void Awake()
    {
        entryPlacment = 0;
    }

    public void AddItem(Item item)
    {
        // Find any empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            //check if theres an item in the slot
            ItemDragging itemInSlot = slot.GetComponentInChildren<ItemDragging>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return;
            }
        }
    }


    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        ItemDragging inventoryItem = newItemGo.GetComponent<ItemDragging>();
        inventoryItem.InitialiseItem(item);
        slot.timeStamp = Time.time; //set the timestamp for the slot
        slot.uniqueID = entryPlacment++;
    }

   


}
