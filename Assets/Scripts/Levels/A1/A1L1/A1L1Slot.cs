using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class A1L1Slot : MonoBehaviour, IDropHandler
{
    public A1L1 a1l1;
    public int id;

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.transform.SetParent(transform, false);

        DragDrop dd = eventData.pointerDrag.GetComponent<DragDrop>();

        dd.SetPossibleSlot(true);
        dd.SetInteractable(false);
        
        if(dd.id == id) a1l1.AddItem(dd.essential, true, dd.textObj.GetComponent<TextMeshProUGUI>().text);
        else a1l1.AddItem(dd.essential, false, dd.textObj.GetComponent<TextMeshProUGUI>().text);

        dd.textObj.GetComponent<TextMeshProUGUI>().enabled = false;
        dd.enabled = false;
    }
}
