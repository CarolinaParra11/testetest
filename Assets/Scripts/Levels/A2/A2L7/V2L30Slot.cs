using B25.Boludin.A1.L7;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace B25.Boludin.V2.L30
{
    public class V2L30Slot : MonoBehaviour, IDropHandler
    {
        public IBuyDragNDrop manager { get; set; }

        [SerializeField]
        private Transform usualTranform;

        [SerializeField]
        private Transform essencialTranform;

        public void OnDrop(PointerEventData eventData)
        {
            DragDrop dd = eventData.pointerDrag.GetComponent<DragDrop>();
            DragDropItem item = eventData.pointerDrag.GetComponent<DragDropItem>();
            OptionDragDrop options = (OptionDragDrop)item.Options;

            if (manager == null)
            {
                Debug.LogError("The manager is not setted");
                return;
            }

            if (options == null)
            {
                Debug.LogError("Item selected haven't an options component attached");
                return;
            }

            if (!manager.CanBuyItem(options))
                return;

            manager.BuyItem(options.ItemType, options);

            var transform = options.ItemType == ItemType.Essencial ? essencialTranform : usualTranform;
            eventData.pointerDrag.transform.SetParent(transform, false);

            dd.SetPossibleSlot(true);
            dd.SetInteractable(false);

            dd.textObj.GetComponent<TextMeshProUGUI>().enabled = false;
            item.SetActiveBucks(false);
            dd.enabled = false;
        }
    }
}
