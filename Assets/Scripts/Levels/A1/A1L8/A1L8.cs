using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace B25.Boludin.A1.L8
{
    public class A1L8 : LevelManagerBase
    {
        #region Texts
        [Header("Texts")]
        [TextArea]
        [SerializeField]
        private string introText;

        [SerializeField]
        private string noEnoughMoney;

        [TextArea]
        [SerializeField]
        private string resultText;
        #endregion

        [SerializeField]
        private int[] maxCostStep;

        [SerializeField]
        private int introSoundIndex;

        

        protected override void Awake()
        {
            base.Awake();
            InitializeButtons();
            for (int i = 0; i < optionsPanel.Length; i++)
            {
                optionsPanel[i].Initialize = InitializeStep;
                optionsPanel[i].OnSelection = UseSelectButton;
                optionsPanel[i].ValidateOnSelectionObject = CanSelectObject;
                optionsPanel[i].CanFinish = CanFinish;
                optionsPanel[i].OnFinish = FinishStep;
            }

            foreach (var item in optionsPanel)
                item.GameobjectPanel.SetActive(false);
        }

        private void Start()
        {
            Cost = 0;
            StartCoroutine(ShowIntro());
            selectOptionText = "Selecione uma opção";
        }

        #region Steps
        private void InitializeStep()
        {
            maxCost = maxCostStep[step];
            Cost = 0;
        }

        private IEnumerator ShowIntro()
        {
            yield return new WaitForSeconds(0.5f);
            AudioManager.am.PlayVoice(AudioManager.am.v2start[introSoundIndex]);
            ShowInfoPanel(introText, () => {
                AudioManager.am.voiceChannel.Stop();
                FlavorManager.fm.ShowHidePanel(infoPanel, false);
                StartCoroutine(NextOptionPanel());
            });
        }

        private bool CanSelectObject(SelectObjects selected)
        {
            var optionSelected = optionsPanel[step]
                        .OptionButtons
                        .SelectOptions
                        .FirstOrDefault(s => s.Selected);

            if ((maxCost - selected.Option.Cost) <= 0)
            {
                ShowInfoPanel(noEnoughMoney);
                return false;
            }

            return (optionSelected == null || selected.Selected);
        }

        private void UseSelectButton(SelectObjects selected)
        {
            if (!selected.Selected)
            {
                Cost -= selected.Option.Cost;
                return;
            }

            Cost += selected.Option.Cost;
        }

        private bool CanFinish()
        {
            return optionsPanel[step]
                        .OptionButtons
                        .SelectOptions
                        .FirstOrDefault(s => s.Selected) != null;
        }

        private void FinishStep()
        {
            totalCoins += maxCost - Cost;
            StartCoroutine(NextOptionPanel());
        }

        protected override IEnumerator End()
        {
            //conceptManager.AddConcepts(selected);
            yield return new WaitForSeconds(0.5f);

            totalCoins += CalculateTotalCoins();

            resultPanelText.text = resultText;
            FlavorManager.fm.ShowHidePanel(resultPanel, true);

            APIManager.am.Relatorio(conceptManager.GetResult());
            PlayerManager.pm.AddLevel();
            PlayerManager.pm.AddCoins(totalCoins);
        }

        private int CalculateTotalCoins()
        {
            var count = 0;

            if (optionsPanel[2].OptionButtons.SelectOptions[2].Selected)
                count += 5;

            count += maxCost - Cost;

            return count;
        }
        #endregion
    }
}