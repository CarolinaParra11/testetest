using B25.Boludin.A2.L9;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace B25.Boludin.V2.L32
{
    public class V2L32 : LevelManagerBase
    {
        #region Texts
        [Header("Texts")]
        [SerializeField]
        [TextArea]
        private string introText;

        [SerializeField]
        [TextArea]
        private string passNextStepText;

        [SerializeField]
        [TextArea]
        private string notPassText;

        [SerializeField]
        private string selectAnOptionText;

        [SerializeField]
        private string Step2CantFinishText;

        [SerializeField]
        private string resultText;
        
        #endregion

        [SerializeField]
        private int maxCostStep4;

        [SerializeField]
        private Canvas canvasDragDrop;

        [SerializeField]
        private GridLayoutGroup gridLayout;

        private List<Options> prizeItens = new List<Options>();

        [SerializeField]
        private int introSoundIndex;

        [SerializeField]
        private int[] correctAnswers;

        [SerializeField]
        private int amountOfCorrectAnswers;

        [SerializeField]
        private AdultMaratonSkin skin;

        [SerializeField]
        private string sceneName = "A2L9a";

        protected override void Awake()
        {
            base.Awake();
            InitializeButtons();
        }

        private void Start()
        {
            StartCoroutine(ShowIntro());
            InitializeOptionPanels();
        }

        protected override void InitializeButtons()
        {
            base.InitializeButtons();

            var optionPanel = optionsPanel[4];
            CreateDragDropItens(optionPanel.OptionButtons.ButtonPrefab, optionPanel.OptionButtons.Options, optionPanel.OptionButtons.Parent);
        }

        private void CreateDragDropItens(GameObject dragDropPrefab, Options[] options, RectTransform parent)
        {
            var itensPerLine = 0;

            for (int i = 0; i < options.Length; i++)
            {
                if (options[i] == null)
                {
                    continue;
                }

                GameObject obj = Instantiate(dragDropPrefab, parent);

                var selectObject = obj.GetComponent<DragDropItem>();
                var dragDrop = obj.GetComponent<DragDrop>();

                selectObject.Options = options[i];
                dragDrop.canvas = canvasDragDrop;
                dragDrop.GridLayout = gridLayout;
                itensPerLine++;
            }
        }

        private void InitializeOptionPanels()
        {
            if (optionsPanel.Length < 1)
            {
                Debug.LogError("The options panel is not implemented");
                return;
            }

            optionsPanel[0].ValidateOnSelectionObject = CanSelectObjectStep1;
            optionsPanel[0].CanFinish = CanFinishStep1;

            optionsPanel[1].ValidateOnSelectionObject = CanSelectObjectStep2;
            optionsPanel[1].CanFinish = CanFinishStep2;

            optionsPanel[2].ValidateOnSelectionObject = CanSelectObjectStep1;
            optionsPanel[2].CanFinish = CanFinishStep1;
            optionsPanel[2].OnFinish = CheckSteps;

            optionsPanel[3].ValidateOnSelectionObject = CanSelectObjectStep1;
            optionsPanel[3].CanFinish = CanFinishStep1;
            optionsPanel[3].OnSelection = SelectObjectStep4;

            optionsPanel[4].ValidateOnSelectionObject = CanSelectObjectStep4;
            optionsPanel[4].CanFinish = CanFinishStep4;
            optionsPanel[4].Initialize = InitializeStep4;

            optionsPanel[5].ValidateOnSelectionObject = CanSelectObjectStep1;
            optionsPanel[5].CanFinish = CanFinishStep1;

            optionsPanel[6].ValidateOnSelectionObject = CanSelectObjectStep1;
            optionsPanel[6].CanFinish = CanFinishStep1;
        }

        private IEnumerator ShowIntro() {
            yield return new WaitForSeconds(0.5f);
            AudioManager.am.PlayVoice(AudioManager.am.v2start[introSoundIndex]);
            ShowInfoPanel(introText, () => {
                AudioManager.am.voiceChannel.Stop();
                FlavorManager.fm.ShowHidePanel(infoPanel, false);
                StartCoroutine(NextOptionPanel());
            });
        }
        
        #region Step 1
        public bool CanSelectObjectStep1(SelectObjects selected)
        {
            var optionSelected = optionsPanel[step]
                        .OptionButtons
                        .SelectOptions
                        .FirstOrDefault(s => s.Selected);

            return (optionSelected == null || selected.Selected);
        }

        private bool CanFinishStep1()
        {
            selectOptionText = selectAnOptionText;
            return optionsPanel[step]
                        .OptionButtons
                        .SelectOptions
                        .FirstOrDefault(s => s.Selected) != null;
        }

        private int CheckStep1() => optionsPanel[0].OptionButtons.SelectOptions[2].Selected ? 1 : 0;
        #endregion

        #region Step 2
        private bool CanSelectObjectStep2(SelectObjects selected)
        {
            var optionSelected = optionsPanel[step]
                        .OptionButtons
                        .SelectOptions
                        .Where(s => s.Selected);

            return (optionSelected == null || selected.Selected || optionSelected.Count() < 2);
        }

        private bool CanFinishStep2()
        {
            var optionSelected = optionsPanel[step]
                         .OptionButtons
                         .SelectOptions
                         .Where(s => s.Selected);

            selectOptionText = Step2CantFinishText;
            return optionSelected.Count() == 2;
        }

        private int CheckStep2() => optionsPanel[1].OptionButtons.SelectOptions[0].Selected && optionsPanel[1].OptionButtons.SelectOptions[3].Selected ? 1 : 0;
        #endregion

        #region Step 3
        private int CheckStep3() => optionsPanel[2].OptionButtons.SelectOptions[0].Selected ? 1 : 0;

        private void CheckSteps()
        {
            var count = CheckStep1() + CheckStep2() + CheckStep3();

            Action callback = () => StartCoroutine(NextOptionPanel());
            if (count >= amountOfCorrectAnswers)
            {
                ShowInfoPanel(passNextStepText, callback);
                return;
            }
            
            step = 0;
            ShowInfoPanel(notPassText, callback);
        }
        #endregion

        #region Step 4
        private void SelectObjectStep4(SelectObjects obj)
        {
            if (optionsPanel[3].OptionButtons.SelectOptions[0].Selected)
            {
                optionsPanel[3].TextOnFinish = "Essa opção não é a ideal. Você usou 3 x120=360 reais do seu dinheiro mas na outra fábrica você usaria menos, 260 reais e pouparia 100 reais";
            }
            else
            {
                optionsPanel[3].TextOnFinish = "Parabéns você escolheu a opção ideal. Você gastou 200 reais para 100 água mais 60 reais para completar o pacote de 120. Você fez uma boa jogada";
            }
        }

        private void InitializeStep4()
        {
            maxCost = maxCostStep4;
            Cost = 0;
        }

        private bool CanSelectObjectStep4(SelectObjects selected)
        {
            return HasEnoughMoney(selected.Option.Cost);
        }

        public bool CanBuyItem(Options options)
        {
            return HasEnoughMoney(options.Cost);
        }

        public void BuyItem(Options option)
        {
            if (!CanBuyItem(option))
            {
                Debug.LogError("This item cannot be buyed");
                return;
            }

            Cost += option.Cost;
            totalCoins += option.Bonus;
            prizeItens.Add(option);

            if (optionsPanel[step].OptionButtons.Options.Min(s => s.Cost) > maxCost - Cost)
                optionsPanel[step].ResultButton.onClick.Invoke();
        }

        private bool CanFinishStep4()
        {
            selectOptionText = selectAnOptionText;
            return prizeItens.Count() > 0;
        }
        #endregion

        protected override IEnumerator End()
        {
            //conceptManager.AddConcepts(selected);
            yield return new WaitForSeconds(0.5f);

            skin.Gender = ((PodiumOption)optionsPanel.Last().OptionButtons.SelectOptions.First(s => s.Selected).Option).Gender;
            
            APIManager.am.Relatorio(conceptManager.GetResult());
            PlayerManager.pm.AddCoins(totalCoins);
            PlayerManager.pm.AddLevel();

            ShowInfoPanel(resultText, () =>
            {
                if (!string.IsNullOrEmpty(sceneName))
                {
                    GameManager.gm.LoadScene(sceneName);
                    return;
                }

                PlayerManager.pm.AddLevel();
            });
        }
    }
}
