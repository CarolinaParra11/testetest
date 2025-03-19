using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class V2L12Slot : MonoBehaviour, IDropHandler
{
    public V2L12 v2l12;
    public Image dropImage;
    public int id;

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<DragDrop>().SetPossibleSlot(true);
        eventData.pointerDrag.GetComponent<DragDrop>().SetInteractable(false);
        eventData.pointerDrag.GetComponent<Image>().enabled = false;

        dropImage.enabled = true;
        v2l12.Part1(id);
    }
}