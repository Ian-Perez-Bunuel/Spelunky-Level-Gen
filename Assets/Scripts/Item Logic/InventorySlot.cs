using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;
    public InventorySlot[] slotArray;

    public Transform slotTransform;
    public float timeStamp;
    public int uniqueID;

    public void RemoveItem()
    {
        float oldestTimestamp = float.MaxValue;
        int oldestIndex = -1;

        for (int i = 0; i < slotArray.Length; i++)
        {
            if (slotArray[i].timeStamp < oldestTimestamp ||
                (slotArray[i].timeStamp == oldestTimestamp && slotArray[i].uniqueID < oldestIndex))
            {
                oldestTimestamp = slotArray[i].timeStamp;
                oldestIndex = i;
            }
        }

        if (oldestIndex != -1)
        {
            Transform childTransform = slotArray[oldestIndex].slotTransform.GetChild(0);

            if (childTransform != null)
            {
                GameObject child = childTransform.gameObject;
                Destroy(child);
                slotArray[oldestIndex].timeStamp = 0f;
                slotArray[oldestIndex].uniqueID = 0; // Reset the unique ID
            }
            if (childTransform == null)
            {
                GameObject child = slotArray[0].transform.GetChild(0).gameObject;
                Destroy(child);
            }
        }
    }





    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.color = selectedColor;
    }
    public void Deselect()
    {
        image.color = notSelectedColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Deselect();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //not able to place 2 items in the same spot
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            ItemDragging draggableitem = dropped.GetComponent<ItemDragging>();
            draggableitem.parentAfterDrag = transform;
        }
    }
}
