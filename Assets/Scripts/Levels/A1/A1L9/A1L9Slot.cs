using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace B25.Boludin.A1.L9
{
    public class A1L9Slot : MonoBehaviour, IDropHandler
    {
        [SerializeField]
        private A1L9 manager;

        public void OnDrop(PointerEventData eventData)
        {
            DragDrop dd = eventData.pointerDrag.GetComponent<DragDrop>();
            DragDropItem item = eventData.pointerDrag.GetComponent<DragDropItem>();
            Options options = item.Options;

            if (options == null)
            {
                Debug.LogError("Item selected haven't an options component attached");
                return;
            }

            if (!manager.CanBuyItem(options))
            {
                dd.ResetGrid();
                return;
            }

            manager.BuyItem(options);
            eventData.pointerDrag.transform.SetParent(transform, false);

            dd.SetPossibleSlot(true);
            dd.SetInteractable(false);

            dd.textObj.GetComponent<TextMeshProUGUI>().enabled = false;
            item.SetActiveBucks(false);
            dd.enabled = false;
        }
    }
}
