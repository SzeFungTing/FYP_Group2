using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public CanvasGroup canvasGroup;
    public Text countText;

   /* [HideInInspector]*/ public Item5 item;
   /* [HideInInspector]*/ public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;


    public void InitialiseItem(Item5 newItem, int c)
    {
        item = newItem;
        image.sprite = newItem.image;
        count = c;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        //image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(UIScripts.instance.backPackUI.transform.parent);
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        //image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
