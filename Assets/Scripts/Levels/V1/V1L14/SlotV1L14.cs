using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotV1L14 : MonoBehaviour, IDropHandler
{
    public V1L14 v1l14;
    public Image dropImage;
    public int id;

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<DragDrop>().SetPossibleSlot(true);
        eventData.pointerDrag.GetComponent<DragDrop>().SetInteractable(false);
        eventData.pointerDrag.GetComponent<Image>().enabled = false;

        dropImage.enabled = true;
        v1l14.Part1Choice(id);
    }
}