using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace B25.Boludin.V2.L29
{
    public delegate void ButtonPressed(SelectObjects selectObjects);

    [Serializable]
    public class GasStation
    {
        [SerializeField]
        private Sprite background;

        [SerializeField]
        private GameObject selectionCanvas;

        #region Gas Button
        [SerializeField]
        private Button gasButton;

        [SerializeField]
        private TextMeshProUGUI gasButtonText;

        [SerializeField]
        private TextMeshProUGUI gasCostText;

        private SelectObjects gasObjectSelected;

        public ButtonPressed GasButtonPressed;
        #endregion

        #region Alcohol Button
        [SerializeField]
        private Button alcoholButton;

        [SerializeField]
        private TextMeshProUGUI alcoholButtonText;

        [SerializeField]
        private TextMeshProUGUI alcoholCostText;

        private SelectObjects alcoholObjectSelected;

        public ButtonPressed AlcoholButtonPressed;
        #endregion

        public void SetUp(Image backgroundImage)
        {
            if (!CheckObjects())
            {
                return;
            }

            backgroundImage.sprite = background;
            selectionCanvas.SetActive(true);

            gasObjectSelected = gasButton.GetComponent<SelectObjects>();
            alcoholObjectSelected = alcoholButton.GetComponent<SelectObjects>();

            if (gasButtonText != null)
                gasButtonText.text = gasObjectSelected.Option.Cost.ToString();

            if (alcoholButtonText != null)
                alcoholButtonText.text = alcoholObjectSelected.Option.Cost.ToString();

            if (gasCostText != null)
                gasCostText.text = gasObjectSelected.Option.Cost.ToString();

            if (alcoholCostText != null)
                alcoholCostText.text = alcoholObjectSelected.Option.Cost.ToString();

            gasButton.onClick.RemoveAllListeners();
            alcoholButton.onClick.RemoveAllListeners();

            gasButton.onClick.AddListener(() =>
            {
                if (!CanSelect())
                    return;
                GasButtonPressed?.Invoke(gasObjectSelected);
            });

            alcoholButton.onClick.AddListener(() =>
            {
                if (!CanSelect())
                    return;
                AlcoholButtonPressed?.Invoke(alcoholObjectSelected);
            });
        }

        public void Disable()
        {
            selectionCanvas.SetActive(false);
            gasButton.onClick.RemoveAllListeners();
            alcoholButton.onClick.RemoveAllListeners();
        }

        public bool CanSelect()
        {
            return !gasObjectSelected.Selected && !alcoholObjectSelected.Selected;
        }

        private bool CheckObjects()
        {
            return true;
        }
    }
}
