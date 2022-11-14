using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Image image;
    //[SerializeField] private Canvas canvas;

    //private RectTransform rectTransform;
    //private CanvasGroup canvasGroup;

    [HideInInspector]public Transform parentAfterDraw;

    //private void Awake()
    //{
    //    rectTransform = GetComponent<RectTransform>();
    //    canvasGroup = GetComponent<CanvasGroup>();
    //}
    private void Start()
    {
        image = transform.GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        parentAfterDraw = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        //canvasGroup.alpha = 0.6f;
        //canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        //rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        transform.SetParent(parentAfterDraw);
        image.raycastTarget = true;
        //canvasGroup.alpha = 1f;
        //canvasGroup.blocksRaycasts = true;
    }

}
