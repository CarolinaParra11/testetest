using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class V2L23Slot : MonoBehaviour, IDropHandler
{
    public V2L23 v2l23;
    public int id;

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.SetParent(transform, false);

        DragDrop dd = eventData.pointerDrag.GetComponent<DragDrop>();

        dd.SetPossibleSlot(true);
        dd.SetInteractable(false);
        
        if(dd.id == id) v2l23.AddItem(dd.essential, true, dd.textObj.GetComponent<TextMeshProUGUI>().text);
        else v2l23.AddItem(dd.essential, false, dd.textObj.GetComponent<TextMeshProUGUI>().text);

        dd.textObj.GetComponent<TextMeshProUGUI>().enabled = false;
        dd.enabled = false;
    }
}
