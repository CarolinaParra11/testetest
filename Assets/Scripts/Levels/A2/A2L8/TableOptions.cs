using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace B25.Boludin.V2.L31
{
    [CreateAssetMenu(fileName = "Table Options Data", menuName = "ScriptableObjects/Options/V2L31/Table Options Data", order = 1)]
    public class TableOptions : Options
    {
        [SerializeField]
        private int tableAmount;
        [SerializeField]
        private int chairAmount;

        public int TableAmount { get => tableAmount; }
       
        public int ChairAmount { get => chairAmount; }
    } 
}
