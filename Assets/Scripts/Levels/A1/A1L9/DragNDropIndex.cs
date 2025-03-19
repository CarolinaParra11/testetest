using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace B25.Boludin.A1.L9
{
    public class DragNDropIndex : MonoBehaviour
    {
        [SerializeField]
        private int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }
    } 
}
