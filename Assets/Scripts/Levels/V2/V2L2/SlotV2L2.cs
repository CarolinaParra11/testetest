using UnityEngine;
using UnityEngine.EventSystems;

public class SlotV2L2 : MonoBehaviour, IDropHandler
{
    public V2L2 level;

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.SetParent(transform, false);

        eventData.pointerDrag.GetComponent<DragDrop>().SetPossibleSlot(true);
        eventData.pointerDrag.GetComponent<DragDrop>().SetInteractable(false);

        eventData.pointerDrag.GetComponent<RectTransform>().localPosition = transform.localPosition;
        eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;

        level.Part3Choice(eventData.pointerDrag.GetComponent<DragDrop>().id);
    }
}