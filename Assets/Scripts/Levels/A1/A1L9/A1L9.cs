using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace B25.Boludin.A1.L9
{
    public class A1L9 : LevelManagerBase
    {
        #region Texts
        [Header("Texts")]
        [TextArea]
        [SerializeField]
        private string introText;

        [SerializeField]
        private string cantBuyDropItem;

        [SerializeField]
        private string noEnoughMoney;

        [TextArea]
        [SerializeField]
        private string resultText;
        #endregion

        [SerializeField]
        private int maxCostStep2;

        [SerializeField]
        private int introSoundIndex;

        private Options[] selectedOptionsStep3 = new Options[3];

        [Header("Drag 'N' Drop")]
        [SerializeField]
        private Canvas canvasDragDrop;

        [SerializeField]
        private GridLayoutGroup gridLayout;

        private IList<FoodOptions> itensBought = new List<FoodOptions>();

        [SerializeField]
        private GameObject[] prize;

        protected override void Awake()
        {
            base.Awake();
            InitializeButtons();
            var stepsOption = new int[] { 0, 3, 4 };
            for (int i = 0; i < stepsOption.Length; i++)
            {
                //optionsPanel[i].Initialize = InitializeStep;
                //optionsPanel[i].OnSelection = UseSelectButton;
                optionsPanel[stepsOption[i]].ValidateOnSelectionObject = CanSelectObject;
                optionsPanel[stepsOption[i]].CanFinish = CanFinishStep;
                //optionsPanel[i].OnFinish = FinishStep;
            }

            optionsPanel[1].Initialize = SetupPart2;
            optionsPanel[1].OnFinish = OnFinishPart2;

            optionsPanel[4].OnFinish = OnFinishPart4;

            foreach (var item in prize)
                item.SetActive(false);

            foreach (var item in optionsPanel)
                item.GameobjectPanel.SetActive(false);

            prize[1].GetComponent<TransitionCallback>().AddCallback(OnAnimationFinish);
        }

        private void OnFinishPart2()
        {
            var count = itensBought.Where(i => i.FoodType == FoodType.Healthy).Count();
            totalCoins += count + (maxCost - Cost);
            StartCoroutine(NextOptionPanel());
        }

        private void SetupPart2()
        {
            maxCost = maxCostStep2;
            Cost = 0;
        }

        protected override void InitializeButtons()
        {
            base.InitializeButtons();
            CreateDragDropItens(optionsPanel[1].OptionButtons.ButtonPrefab, optionsPanel[1].OptionButtons.Options, optionsPanel[1].OptionButtons.Parent, canvasDragDrop, gridLayout);
        }

        private void Start()
        {
            StartCoroutine(ShowIntro());
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

        private bool CanSelectObject(SelectObjects selected) => !ObjectSelected(step) || selected.Selected;

        private bool CanFinishStep()
        {
            return ObjectSelected(step);
        }

        private bool ObjectSelected(int step)
        {
            return optionsPanel[step]
                                    .OptionButtons
                                    .SelectOptions
                                    .FirstOrDefault(s => s.Selected) != null;
        }

        public void SetOptionOrder(int index, Options option)
        {
            selectedOptionsStep3[index] = option;
        }

        internal bool CanBuyItem(Options options) {

            bool enoughMoney = HasEnoughMoney(options.Cost);
            StringBuilder message = new StringBuilder();

            if (!enoughMoney)
                message.Append(noEnoughMoney);

            bool canBuyItemType = CanBuyMoreItens();

            if (!canBuyItemType)
                message.AppendLine(cantBuyDropItem);

            if (!enoughMoney || !canBuyItemType)
                ShowInfoPanel(message.ToString());

            return enoughMoney && canBuyItemType;
        }

        private bool CanBuyMoreItens() => itensBought.Count() < 6;

        internal void BuyItem(Options options)
        {
            if (!CanBuyItem(options))
            {
                Debug.LogError("You can't buy this item");
                return;
            }

            itensBought.Add((FoodOptions)options);
            Cost += options.Cost;

            var panel = optionsPanel[step];
            if (!HasEnoughMoney(panel.OptionButtons.Options.Min(s => s.Cost)) || !CanBuyMoreItens())
                panel.ResultButton.onClick.Invoke();
        }

        private void OnFinishPart4()
        {
            StartCoroutine(StartFinishAnimation());
        }

        private IEnumerator StartFinishAnimation()
        {
            yield return new WaitForSeconds(0.5f);
            prize[0].SetActive(true);
            yield return new WaitForSeconds(3.5f);
            prize[1].SetActive(true);
        }

        private void OnAnimationFinish()
        {
            StartCoroutine(NextOptionPanel());
        }

        protected override IEnumerator End()
        {
            //conceptManager.AddConcepts(selected);
            yield return new WaitForSeconds(0.5f);

            resultPanelText.text = resultText;
            FlavorManager.fm.ShowHidePanel(resultPanel, true);

            APIManager.am.Relatorio(conceptManager.GetResult());
            PlayerManager.pm.AddLevel();
            PlayerManager.pm.AddCoins(totalCoins);
        }
    } 
}
