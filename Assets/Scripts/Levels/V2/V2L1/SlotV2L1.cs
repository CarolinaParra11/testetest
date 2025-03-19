using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotV2L1 : MonoBehaviour, IDropHandler
{
    public V2L1 v2l1;
    public int id;

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<DragDrop>().SetPossibleSlot(true);
        eventData.pointerDrag.GetComponent<DragDrop>().SetInteractable(false);
        eventData.pointerDrag.GetComponent<Image>().enabled = false;

        v2l1.Part1(id);
    }
}