using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotV1L1 : MonoBehaviour, IDropHandler
{
    public V1L1 v1l1;
    public int id;

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<DragDrop>().SetPossibleSlot(true);
        eventData.pointerDrag.GetComponent<DragDrop>().SetInteractable(false);
        eventData.pointerDrag.GetComponent<Image>().enabled = false;
        eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;

        v1l1.Part1OnDrop(id);
    }
}