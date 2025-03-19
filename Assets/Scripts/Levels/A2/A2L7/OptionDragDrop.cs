using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B25.Boludin.V2.L30
{
    [CreateAssetMenu(fileName = "Drag 'n' drop options", menuName = "ScriptableObjects/Options/V2L30/Drag 'n' drop option", order = 1)]
    public class OptionDragDrop : Options
    {
        [SerializeField]
        private ItemType itemType;

        public ItemType ItemType
        {
            get { return itemType; }
            set { itemType = value; }
        }
    } 
}
