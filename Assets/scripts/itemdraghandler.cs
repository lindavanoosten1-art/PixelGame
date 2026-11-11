using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class itemdraghandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Transform originalParent;
    CanvasGroup canvasGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent; // safe OG parent
        transform.SetParent(transform.root); // above other canvas
        canvasGroup.blocksRaycasts = false; // allow raycast to go through
        canvasGroup.alpha = 0.6f; // make item transparent
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position; // follow mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true; // enables raycasts
        canvasGroup.alpha = 1f; // make item opaque

        slot dropSlot = eventData.pointerEnter?.GetComponent<slot>(); // check if we are over a slot
        slot originalSlot = originalParent.GetComponent<slot>(); // get the original slot

        if (dropSlot != null)
        {
            if (dropSlot.currentItem != null)
            {
                //slot had an item - swap items
                dropSlot.currentItem.transform.SetParent(originalSlot.transform);
                originalSlot.currentItem = dropSlot.currentItem;
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else
            {
                //slot was empty - move item to new slot
                originalSlot.currentItem = null;
            }
            //move item into drop slot
            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        }
        else
        {
            //not over a slot - return to original slot
            transform.SetParent(originalParent);
        }
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // reset position>
    }

   
}
