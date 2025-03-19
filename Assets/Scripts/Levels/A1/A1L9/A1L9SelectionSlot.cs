using B25.Boludin.A1.L7;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace B25.Boludin.A1.L9
{
    public class A1L9SelectionSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private A1L9 manager;

        [SerializeField]
        private Options options;

        public void OnDrop(PointerEventData eventData)
        {
            DragDrop dd = eventData.pointerDrag.GetComponent<DragDrop>();
            DragDropItem item = eventData.pointerDrag.GetComponent<DragDropItem>();
            DragNDropIndex index = eventData.pointerDrag.GetComponent<DragNDropIndex>();

            if (options == null)
            {
                Debug.LogError("Item selected haven't an options component attached");
                return;
            }

            manager.SetOptionOrder(index.Index, options);
            eventData.pointerDrag.transform.SetParent(transform, false);

            dd.SetPossibleSlot(true);
            dd.SetInteractable(false);

            item.SetActiveBucks(false);
            dd.enabled = false;
        }
    } 
}
