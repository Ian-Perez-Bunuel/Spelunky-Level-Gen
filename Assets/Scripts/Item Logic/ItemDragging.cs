using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDragging : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    [HideInInspector] public Item item;


    //assigns the sprite and item variable
    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        //mouse cannot see item while dragging
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //item follows mouse during drag
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    { 
        transform.SetParent(parentAfterDrag);
        //allows mouse to see item again
        image.raycastTarget = true;
    }
}
