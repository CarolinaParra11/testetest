using B25.Boludin.A1.L7;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace B25.Boludin.V2.L30
{
    public enum ItemType { Essencial, Usual }

    public class V2L30 : LevelManagerBase, IBuyDragNDrop
    {
        [Header("Texts")]
        [TextArea]
        [SerializeField]
        private string introText;

        [TextArea]
        [SerializeField]
        private string textToPayBill;

        [TextArea]
        [SerializeField]
        private string step2IntroText;

        [TextArea]
        [SerializeField]
        private string step3IntroText;

        [TextArea]
        [SerializeField]
        private string step4IntroText;

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
        private Options billOption;

        private bool payBill;

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

        private int optionSelectedStep2;

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

        [SerializeField]
        private GameObject dragDropPanel;

        [SerializeField]
        private GameObject lineDropItem;

        [SerializeField]
        private RectTransform lineParent;

        [SerializeField]
        private Options[] essencialItens;

        [SerializeField]
        private Options[] usualItens;

        [SerializeField]
        private GameObject dragDropPrefab;

        [SerializeField]
        private int maxItensEssencials;

        [SerializeField]
        private int maxItensUsual;

        [SerializeField]
        private int maxItemsPerLine;

        private List<Options> selectedEssencialItems = new List<Options>();
        private List<Options> selectedUsualItems = new List<Options>();

        private RectTransform[] dragDropParents;
        private int indexParentTransforms = 0;

        [SerializeField]
        private int bucksStep4;

        [SerializeField]
        private V2L30Slot[] slots;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            InitializeButtons();
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
            CreateDragDropItem(essencialItens, usualItens, dragDropParents);
        }

        protected void CreateDragDropItem(Options[] essencialItens, Options[] usualItens, RectTransform[] parents)
        {
            if (essencialItens == null || essencialItens.Length == 0)
            {
                Debug.LogError("essencialItens is null or empty to create Drag and Drop items");
                return;
            }

            if (usualItens == null || usualItens.Length == 0)
            {
                Debug.LogError("usualItens is null or empty to create Drag and Drop items");
                return;
            }

            CreateDragDropItens(optionsPanel[3].OptionButtons.Options, optionsPanel[3].OptionButtons.Parent);
        }

        private void CreateDragDropItens(Options[] options, RectTransform parent)
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
                dragDrop.id = (int)((OptionDragDrop)options[i]).ItemType;
                dragDrop.canvas = canvasDragDrop;
                dragDrop.GridLayout = gridLayout;
                dragDrop.essential = ((OptionDragDrop)options[i]).ItemType == ItemType.Essencial ? "Essencial" : "Não essencial";
                itensPerLine++;
            }
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
                StartCoroutine(Part1());
            });
        }

        #region Step 1 Functions
        private IEnumerator Part1()
        {
            yield return new WaitForSeconds(0.5f);
            FlavorManager.fm.ShowHidePanel(optionsPanel[0].GameobjectPanel, true);

            optionsPanel[0].OnSelection = SelectButtonStep1;
            optionsPanel[0].ValidateOnSelectionObject = CanSelectStep1;
            optionsPanel[0].ResultButton.onClick.AddListener(() =>
            {
                if (!CanFinishStep1())
                {
                    ShowInfoPanel(selectOptionText);
                    return;
                }

                AnimationBucks();
                animatorTransiction.SetTrigger("StartTransition");
                if (payBill)
                    totalCoins += optionsPanel[0].OptionButtons.SelectOptions.First(s => s.Selected).Option.Bonus;
                else
                    Cost += billOption.Cost;

                FlavorManager.fm.ShowHidePanel(optionsPanel[0].GameobjectPanel, false);
                totalCoins += maxCost - Cost;
                step++;
                StartCoroutine(Part2());
            });
        }

        private void SelectButtonStep1(SelectObjects selected)
        {
            var selectedOption = optionsPanel[0].OptionButtons
                        .SelectOptions
                        .Where(s => s.Selected)
                        .FirstOrDefault();

            Cost = (selectedOption == null ?
                                0 :
                                Cost += selectedOption.Option.Cost);

            if (selectedOption == null)
                return;

            payBill = selectedOption.Option.Text == billOption.Text ? true : false;
        }

        private bool CanSelectStep1(SelectObjects selectedObject)
        {
            return (optionsPanel[0].OptionButtons.SelectOptions
                        .FirstOrDefault(s => s.Selected) == null) || selectedObject.Selected;
        }

        private bool CanFinishStep1()
        {
            return optionsPanel[0].OptionButtons.SelectOptions
                        .FirstOrDefault(s => s.Selected) != null;
        }
        #endregion

        #region Step 2 Functions 
        private IEnumerator Part2()
        {
            SetMaxCost(bucksStep2);
            optionsPanel[1].OnSelection = SelectButtonStep2;
            optionsPanel[1].ValidateOnSelectionObject = CanSelectStep2;
            yield return new WaitForSeconds(1);
            animatorTransiction.SetTrigger("EndTransition");
            callback.AddCallback(() =>
            {
                if (payBill)
                {
                    StartCoroutine(Part2A());
                    return;
                }
                else
                {
                    ShowInfoPanel(textToPayBill, () => {
                        StartCoroutine(Part2A());
                    });
                }
            });
        }

        private IEnumerator Part2A()
        {
            yield return new WaitUntil(() => !infoPanel.activeInHierarchy);
            ShowInfoPanel(step2IntroText, () => { 
                FlavorManager.fm.ShowHidePanel(infoPanel, false);
                FlavorManager.fm.ShowHidePanel(optionsPanel[1].GameobjectPanel, true);
                optionsPanel[1].ResultButton.onClick.AddListener(() =>
                {
                    if (!CanFinishStep2())
                    {
                        ShowInfoPanel(selectOptionText);
                    }
                    else
                    {
                        FlavorManager.fm.ShowHidePanel(optionsPanel[1].GameobjectPanel, false);
                        StartCoroutine(Part2End());
                    }
                });
            });
        }

        private IEnumerator Part2End()
        {
            yield return new WaitForSeconds(0.5f);
            var selected = optionsPanel[1].OptionButtons.SelectOptions
                        .Where(s => s.Selected).FirstOrDefault();

            var result = textResult[optionSelectedStep2];
            Cost += result.Cost;
            totalCoins += maxCost - Cost;
            AnimationBucks();
            step++;
            ShowInfoPanel(result.Text, () => StartCoroutine(Part2Animation()));
        }

        private IEnumerator Part2Animation()
        {
            Action Callback = () =>
            {
                animatorTransiction.SetTrigger("StartTransition");
                StartCoroutine(Part3Intro());
            };

            if (optionSelectedStep2 == 1)
            {
                AnimationPart2GameObject.SetActive(true);
                yield return new WaitForSeconds(5.5f);
                Callback.Invoke();
            }
            else
            {
                Callback.Invoke();
            }
        }

        private void SelectButtonStep2(SelectObjects selected)
        {
            var selectedOption = optionsPanel[1].OptionButtons.SelectOptions
                        .Where(s => s.Selected).FirstOrDefault();

            Cost = (selectedOption == null ?
                                0 :
                                Cost += selectedOption.Option.Cost);

            if (selectedOption == null)
                return;

            optionSelectedStep2 = Array.IndexOf(optionsPanel[1].OptionButtons.SelectOptions, selectedOption);
        }

        private bool CanSelectStep2(SelectObjects selectedObject)
        {
            return (optionsPanel[1].OptionButtons.SelectOptions
                        .FirstOrDefault(s => s.Selected) == null) || selectedObject.Selected;
        }

        private bool CanFinishStep2()
        {
            return optionsPanel[1].OptionButtons.SelectOptions
                        .FirstOrDefault(s => s.Selected) != null;
        }
        #endregion
        
        #region Step 3 Function
        private IEnumerator Part3Intro()
        {
            SetMaxCost(bucksStep3);
            yield return new WaitForSeconds(1);
            AnimationPart2GameObject.SetActive(false);
            background.sprite = backgroundStep3;
            animatorTransiction.SetTrigger("EndTransition");
            callback.CleanAllCallbacks();
            callback.AddCallback(() =>
            {
                ShowInfoPanel(step3IntroText, () => { 
                    if (!CanFinishStep2())
                    {
                        ShowInfoPanel(selectOptionText);
                    }
                    else
                    {
                        FlavorManager.fm.ShowHidePanel(infoPanel, false);
                        StartCoroutine(Part3());
                    }
                });
            });
        }

        private IEnumerator Part3()
        {
            optionsPanel[2].OnSelection = SelectButtonStep3;
            optionsPanel[2].ValidateOnSelectionObject = CanSelectStep3;
            yield return new WaitForSeconds(0.5f);
            FlavorManager.fm.ShowHidePanel(optionsPanel[2].GameobjectPanel, true);
            optionsPanel[2].ResultButton.onClick.AddListener(() =>
            {
                if (!CanFinishStep3())
                {
                    ShowInfoPanel(selectOptionText);
                }

                AnimationBucks();
                FlavorManager.fm.ShowHidePanel(optionsPanel[2].GameobjectPanel, false);
                step++;
                StartCoroutine(Part4());
            });
        }

        private void SelectButtonStep3(SelectObjects selected)
        {
            var selectedOption = optionsPanel[2].OptionButtons.SelectOptions
                        .Where(s => s.Selected).FirstOrDefault();

            Cost = (selectedOption == null ?
                                0 :
                                Cost += selectedOption.Option.Cost);
        }

        private bool CanSelectStep3(SelectObjects selectedObject)
        {
            return (optionsPanel[2].OptionButtons.SelectOptions
                        .FirstOrDefault(s => s.Selected) == null) || selectedObject.Selected;
        }

        private bool CanFinishStep3()
        {
            return optionsPanel[2].OptionButtons.SelectOptions
                        .FirstOrDefault(s => s.Selected) != null;
        }
        #endregion

        #region Step 4 Functions
        private IEnumerator Part4()
        {
            yield return new WaitForSeconds(0.5f);
            SetMaxCost(bucksStep4 + (maxCost - Cost));
            ShowInfoPanel(step4IntroText, () => { 
                FlavorManager.fm.ShowHidePanel(dragDropPanel, true);
                optionsPanel[3].ResultButton.onClick.AddListener(() =>
                {
                    FlavorManager.fm.ShowHidePanel(dragDropPanel, false);
                    totalCoins += maxCost - Cost;
                    StartCoroutine(End());
                });
            });
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

            if (optionsPanel[step].OptionButtons.Options.Min(s => s.Cost) > (maxCost - Cost))
                optionsPanel[step].ResultButton.onClick.Invoke();
        }

        public bool CanBuyItem(Options option)
        {
            bool enoughMoney = HasEnoughMoney(option.Cost);
            StringBuilder message = new StringBuilder();

            if (!enoughMoney)
                message.Append(noEnoughMoney);

            bool canBuyItemType = selectedEssencialItems.Count() + selectedUsualItems.Count() < 5;

            if (!canBuyItemType)
                message.AppendLine(cantBuyDropItem);

            if (!enoughMoney || !canBuyItemType)
                ShowInfoPanel(message.ToString());

            return enoughMoney && canBuyItemType;
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
