using UnityEngine;
using UnityEngine.EventSystems;

public class SlotV1L5 : MonoBehaviour, IDropHandler
{
    public V1L5 v1l5;
    public int id;

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<DragDrop>().SetInteractable(false);
        eventData.pointerDrag.GetComponent<DragDrop>().SetPossibleSlot(true);
        eventData.pointerDrag.GetComponent<RectTransform>().localPosition = transform.localPosition;
        eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;

        if(eventData.pointerDrag.GetComponent<DragDrop>().id == id) v1l5.Part2Choice(true);
        else v1l5.Part2Choice(false);
    }
}