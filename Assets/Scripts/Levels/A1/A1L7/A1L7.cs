using B25.Boludin.V2.L30;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace B25.Boludin.A1.L7
{
    public class A1L7 : LevelManagerBase, IBuyDragNDrop
    {
        [Header("Texts")]
        [TextArea]
        [SerializeField]
        private string introText;

        [SerializeField]
        private string noEnoughMoney;

        [SerializeField]
        private string cantBuyDropItem;

        [SerializeField]
        private string resultText;

        [SerializeField]
        private int introSoundIndex;

        [Header("Transaction")]
        [SerializeField]
        private Image background;

        [SerializeField]
        private Animator animatorTransiction;

        [SerializeField]
        private Sprite backgroundStep3;

        private TransitionCallback callback;

        [SerializeField]
        private int bucksStep1;

        [SerializeField]
        private int bucksStep2;

        [Serializable]
        private struct TextResult
        {
            [TextArea]
            [SerializeField]
            private string text;

            public string Text
            {
                get { return text; }
            }

            [SerializeField]
            private int cost;

            public int Cost
            {
                get { return cost; }
            }
        }

        [SerializeField]
        private TextResult[] textResult;

        private int optionSelectedStep1;

        [SerializeField]
        private GameObject AnimationPart2GameObject;

        [SerializeField]
        private int bucksStep3;

        #region DragNDrop
        [Header("Drag 'N' Drop")]
        [SerializeField]
        private Canvas canvasDragDrop;

        [SerializeField]
        private GridLayoutGroup gridLayout;

        private List<Options> selectedEssencialItems = new List<Options>();
        private List<Options> selectedUsualItems = new List<Options>();

        [SerializeField]
        private V2L30Slot[] slots;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            InitializeButtons();
            InitializeOptionPanels();
            callback = animatorTransiction.GetComponent<TransitionCallback>();
            foreach (var item in slots)
                item.manager = this;
        }

        void Start()
        {
            StartCoroutine(ShowIntro());
            AnimationPart2GameObject.SetActive(false);
        }

        private void SetMaxCost(int maxCost)
        {
            this.maxCost = maxCost;
            Cost = 0;
        }

        #region Initialize
        protected override void InitializeButtons()
        {
            base.InitializeButtons();
            CreateDragDropItens(optionsPanel[2].OptionButtons.ButtonPrefab, optionsPanel[2].OptionButtons.Options, optionsPanel[2].OptionButtons.Parent, canvasDragDrop, gridLayout, OnInitializeDragNDrop);
        }

        private void OnInitializeDragNDrop(GameObject obj, Options option)
        {
            var dragDrop = obj.GetComponent<DragDrop>();

            dragDrop.id = (int)((OptionDragDrop)option).ItemType;
            dragDrop.essential = ((OptionDragDrop)option).ItemType == ItemType.Essencial ? "Essencial" : "Não essencial";
        }

        private void InitializeOptionPanels()
        {
            optionsPanel[0].Initialize = Part1;
            optionsPanel[0].OnFinish = Part1End;
            optionsPanel[0].OnSelection = SelectButtonStep1;
            optionsPanel[0].ValidateOnSelectionObject = CanSelectStep1;
            optionsPanel[0].CanFinish = CanFinishStep1;

            //optionsPanel[1].Initialize = Part2;
            //optionsPanel[1].OnFinish = Part2End;
            optionsPanel[1].OnSelection = SelectButtonStep2;
            optionsPanel[1].ValidateOnSelectionObject = CanSelectStep2;
            optionsPanel[1].CanFinish = CanFinishStep2;

            optionsPanel[2].Initialize = Part3;
            optionsPanel[2].OnFinish = Part3End;
        }
        #endregion

        #region Steps
        private IEnumerator ShowIntro()
        {
            yield return new WaitForSeconds(0.5f);
            AudioManager.am.PlayVoice(AudioManager.am.v2start[introSoundIndex]);
            SetMaxCost(bucksStep1);
            ShowInfoPanel(introText, () => {
                AudioManager.am.voiceChannel.Stop();
                FlavorManager.fm.ShowHidePanel(infoPanel, false);
                StartCoroutine(NextOptionPanel());
            });
        }

        #region Step 1 Functions
        private void Part1()
        {
            //SetMaxCost(bucksStep1);
            animatorTransiction.SetTrigger("EndTransition");
            callback.AddCallback(() =>
            {
                StartCoroutine(NextOptionPanel());
            });
        }

        private void Part1End()
        {
            var selected = optionsPanel[step].OptionButtons.SelectOptions
                        .Where(s => s.Selected).FirstOrDefault();

            var result = textResult[optionSelectedStep1];
            Cost += result.Cost;
            totalCoins += maxCost - Cost;
            optionsPanel[step].TextOnFinish = result.Text;
            //step++;
            ShowInfoPanel(result.Text, () => StartCoroutine(Part1Animation()));
        }

        private IEnumerator Part1Animation()
        {
            Action Callback = () =>
            {
                animatorTransiction.SetTrigger("StartTransition");
            };

            if (optionSelectedStep1 == 1)
            {
                AnimationPart2GameObject.SetActive(true);
                yield return new WaitForSeconds(5.5f);
                Callback.Invoke();
            }
            else
            {
                Callback.Invoke();
            }
            yield return new WaitForSeconds(2f);
            Part2();
        }

        private void SelectButtonStep1(SelectObjects selected)
        {
            var selectedOption = optionsPanel[step].OptionButtons.SelectOptions
                        .Where(s => s.Selected).FirstOrDefault();

            Cost = (selectedOption == null ?
                                0 :
                                Cost += selectedOption.Option.Cost);

            if (selectedOption == null)
                return;

            optionSelectedStep1 = Array.IndexOf(optionsPanel[step].OptionButtons.SelectOptions, selectedOption);
        }

        private bool CanSelectStep1(SelectObjects selectedObject)
        {
            return (optionsPanel[step].OptionButtons.SelectOptions
                        .FirstOrDefault(s => s.Selected) == null) || selectedObject.Selected;
        }

        private bool CanFinishStep1()
        {
            return optionsPanel[step].OptionButtons.SelectOptions
                        .FirstOrDefault(s => s.Selected) != null;
        }
        #endregion

        #region Step 2 Functions 
        private void Part2()
        {
            SetMaxCost(bucksStep2);
            AnimationPart2GameObject.SetActive(false);
            background.sprite = backgroundStep3;
            animatorTransiction.SetTrigger("EndTransition");
            callback.CleanAllCallbacks();
            callback.AddCallback(() =>
            {
                StartCoroutine(NextOptionPanel());
            });
        }

        private void SelectButtonStep2(SelectObjects selected)
        {
            var selectedOption = optionsPanel[step].OptionButtons.SelectOptions
                        .Where(s => s.Selected).FirstOrDefault();

            Cost = (selectedOption == null ?
                                0 :
                                Cost += selectedOption.Option.Cost);
        }

        private bool CanSelectStep2(SelectObjects selectedObject)
        {
            return (optionsPanel[step].OptionButtons.SelectOptions
                        .FirstOrDefault(s => s.Selected) == null) || selectedObject.Selected;
        }

        private bool CanFinishStep2()
        {
            return optionsPanel[step].OptionButtons.SelectOptions
                        .FirstOrDefault(s => s.Selected) != null;
        }
        #endregion

        #region Step 3 Function
        private void Part3()
        {
            //yield return new WaitForSeconds(0.5f);
            SetMaxCost(bucksStep3 + (maxCost - Cost));
            //ShowInfoPanel(step4IntroText, () =>
            //{
            //    FlavorManager.fm.ShowHidePanel(dragDropPanel, true);
            //    optionsPanel[3].ResultButton.onClick.AddListener(() =>
            //    {
            //        FlavorManager.fm.ShowHidePanel(dragDropPanel, false);

            //        StartCoroutine(End());
            //    });
            //});
        }

        private void Part3End()
        {
            totalCoins += maxCost - Cost;
            StartCoroutine(NextOptionPanel());
        }

        public void BuyItem(ItemType itemType, Options option)
        {
            if (!CanBuyItem(option))
            {
                Debug.LogError("This item cannot be buyed");
                return;
            }

            Cost += option.Cost;
            switch (itemType)
            {
                case ItemType.Essencial:
                    selectedEssencialItems.Add(option);
                    break;
                case ItemType.Usual:
                    selectedUsualItems.Add(option);
                    break;
            }

            if (optionsPanel[step].OptionButtons.Options.Min(s => s.Cost) > (maxCost - Cost) || !CanBuyMoreItens())
                optionsPanel[step].ResultButton.onClick.Invoke();
        }

        public bool CanBuyItem(Options option)
        {
            bool enoughMoney = HasEnoughMoney(option.Cost);
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

        private bool CanBuyMoreItens()
        {
            return selectedEssencialItems.Count() + selectedUsualItems.Count() < 5;
        }
        #endregion

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
        #endregion
    }
}