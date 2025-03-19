using UnityEngine;
using UnityEngine.EventSystems;

public class SlotV1L6 : MonoBehaviour, IDropHandler
{
    public V1L6 v1l6;

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.SetParent(transform, false);

        eventData.pointerDrag.GetComponent<DragDrop>().SetPossibleSlot(true);
        eventData.pointerDrag.GetComponent<DragDrop>().SetInteractable(false);

        eventData.pointerDrag.GetComponent<RectTransform>().localPosition = transform.localPosition;
        eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;

        v1l6.Part2Choice(eventData.pointerDrag.GetComponent<DragDrop>().id);
    }
}
