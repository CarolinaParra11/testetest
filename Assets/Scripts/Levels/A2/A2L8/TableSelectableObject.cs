using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace B25.Boludin.V2.L31
{
    public class TableSelectableObject : SelectObjects
    {
        [SerializeField]
        private TextMeshProUGUI chairTexts;

        [SerializeField]
        private TextMeshProUGUI tableTexts;

        protected override void Start()
        {
            base.Start();

            var tableOptions = (TableOptions)Option;

            if (tableOptions == null)
            {
                Debug.LogError("This options is not an Table Option");
                return;
            }

            if (chairTexts != null)
                chairTexts.text = $"x{tableOptions.ChairAmount}";

            if (tableTexts != null)
                tableTexts.text = $"x{tableOptions.TableAmount}";
        }
    } 
}
