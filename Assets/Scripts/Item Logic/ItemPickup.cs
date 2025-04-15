using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemPickup : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public InventorySlot inventorySlot;
    public Item[] itemsToPickup;
    public GameObject player;

    public void Pickupitem(int id)
    {
        inventoryManager.AddItem(itemsToPickup[id]);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Pickupitem(0);
            Debug.Log("Player in Box");
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("left");
        inventorySlot.RemoveItem();
    }
}
