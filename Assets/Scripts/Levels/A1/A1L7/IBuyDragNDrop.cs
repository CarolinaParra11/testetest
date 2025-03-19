using B25.Boludin.V2.L30;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B25.Boludin.A1.L7
{
    public interface IBuyDragNDrop
    {
        bool CanBuyItem(Options option);

        void BuyItem(ItemType itemType, Options option);
    } 
}
