using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        if(transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DragDrop dragDrop = dropped.GetComponent<DragDrop>();
            dragDrop.parentAfterDraw = transform;
        }
        
        
    
    }
}
