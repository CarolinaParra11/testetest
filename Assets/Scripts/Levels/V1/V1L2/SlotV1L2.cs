using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotV1L2 : MonoBehaviour, IDropHandler
{
    public V1L2 v1l2;
    public int id;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<DragDrop>().SetPossibleSlot(true);
        eventData.pointerDrag.GetComponent<DragDrop>().SetInteractable(false);
        eventData.pointerDrag.GetComponent<Image>().enabled = false;
        eventData.pointerDrag.transform.GetChild(0).gameObject.SetActive(false);

        anim.SetTrigger("Jiggle");
        if (eventData.pointerDrag.GetComponent<DragDrop>().id == id) v1l2.Part1Choice(true);
        else v1l2.Part1Choice(false);

        eventData.pointerDrag.GetComponent<DragDrop>().enabled = false;
    }
}