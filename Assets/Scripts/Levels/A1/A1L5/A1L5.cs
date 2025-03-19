using B25.Boludin.V2.L28;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace B25.Boludin.A1.L5
{
    public class A1L5 : LevelManagerBase
    {
        [Header("Texts")]
        [SerializeField]
        private string introText;

        [SerializeField]
        private string noEnoughCash;

        [SerializeField]
        private string textStepResult;

        [SerializeField]
        private string textEndResult;

        [SerializeField]
        private int soundStartIndex;

        [SerializeField]
        private ChoseAnimation chosenAnimation;

        [SerializeField]
        private string sceneName = "A1L5a";

        [SerializeField]
        private int maxCostPart2 = 3;

        protected override void Awake()
        {
            base.Awake();
            InitializeButtons();
            InitializePanels();
        }

        private void Start()
        {
            Cost = 0;
            ShowIntro();
            for (int i = 0; i < optionsPanel.Length; i++)
            {
                optionsPanel[i].ValidateOnSelectionObject = CanSelectObject;
                optionsPanel[i].OnSelection = SelectObject;
            }
        }

        private void InitializePanels()
        {
            optionsPanel[0].CanFinish = CheckTravelStep; 
            optionsPanel[0].OnFinish = Part1End;
        }

        private void ShowIntro()
        {
            AudioManager.am.PlayVoice(AudioManager.am.v2start[soundStartIndex]);
            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoPanelText.text = introText;
            infoButton.onClick.AddListener(() =>
            {
                AudioManager.am.voiceChannel.Stop();
                FlavorManager.fm.ShowHidePanel(infoPanel, false);
                StartCoroutine(NextOptionPanel());
            });
        }

        private void CalculateMoney()
        {
            Cost = optionsPanel[0]
                    .OptionButtons
                    .SelectOptions
                    .Where(b => b.Selected)
                    .Select(b => b.Option.Cost)
                    .DefaultIfEmpty(0)
                    .Sum();

            Cost += optionsPanel[1]
                    .OptionButtons
                    .SelectOptions
                    .Where(b => b.Selected)
                    .Select(b => b.Option.Cost)
                    .DefaultIfEmpty(0)
                    .Sum();
        }

        #region Steps

        //private IEnumerator Part1()
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    FlavorManager.fm.ShowHidePanel(optionsPanel[0].GameobjectPanel, true);
        //    optionsPanel[0].ResultButton.onClick.AddListener(() =>
        //    {
        //        if (CheckTravelStep())
        //        {
        //            FlavorManager.fm.ShowHidePanel(optionsPanel[0].GameobjectPanel, false);
        //            StartCoroutine(Part1End());
        //        }
        //    });
        //}

        private void Part1End()
        {
            var selected = optionsPanel[0]
                    .OptionButtons
                    .SelectOptions
                    .Where(t => t.Selected)
                    .Select(t => t.Option.Concepts);

            conceptManager.AddConcepts(selected);

            maxCost = maxCostPart2;
            Cost = 0;
            StartCoroutine(NextOptionPanel());
        }

        //private IEnumerator Part2()
        //{
        //    yield return new WaitForSeconds(0.5f);
        //    maxCost = maxCostPart2;
        //    Cost = 0;
        //    FlavorManager.fm.ShowHidePanel(optionsPanel[1].GameobjectPanel, true);
        //    optionsPanel[1].ResultButton.onClick.AddListener(() =>
        //    {
        //        if (CheckBuyStep())
        //        {
        //            FlavorManager.fm.ShowHidePanel(optionsPanel[1].GameobjectPanel, false);
        //            StartCoroutine(End());
        //        }
        //    });
        //}

        protected override IEnumerator End()
        {
            var selected = optionsPanel[1]
                    .OptionButtons
                    .SelectOptions
                    .Where(t => t.Selected)
                    .Select(t => t.Option.Concepts);

            conceptManager.AddConcepts(selected);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FlavorManager.fm.SpawnBucksPosition(numberCoins, coinSpawn, coinWaypoint));

            resultPanelText.text = textEndResult;
            FlavorManager.fm.ShowHidePanel(resultPanel, true);

            APIManager.am.Relatorio(conceptManager.GetResult());
            totalCoins += maxCost - Cost;
            PlayerManager.pm.AddCoins(totalCoins);

            resultButton.onClick.AddListener(() =>
            {
                if (!string.IsNullOrEmpty(sceneName))
                {
                    GameManager.gm.LoadScene(sceneName);
                    return;
                }

                PlayerManager.pm.AddLevel();
            });
        }
        #endregion

        #region CheckSteps
        private bool CheckTravelStep()
        {
            var selected = optionsPanel[0]
                    .OptionButtons
                    .SelectOptions
                    .FirstOrDefault(t => t.Selected);

            chosenAnimation.Background = selected?.Option.ChosenAnimation.Background;
            chosenAnimation.Animation = selected?.Option.ChosenAnimation.Animation;
            return selected != null;
        }

        private bool CheckBuyStep()
        {
            return true;
        }
        #endregion

        #region Buttons
        private void SelectObject(SelectObjects select) => Cost = optionsPanel[step].OptionButtons
                                                                    .SelectOptions
                                                                    .Where(s => s.Selected)
                                                                    .Sum(s => s.Option.Cost);

        private bool CanSelectObject(SelectObjects select)
        {
            if (!select.Selected && !HasEnoughMoney(select.Option.Cost))
            {
                ShowInfoPanel(noEnoughCash);
                return false;
            }

            return true;
        }
        #endregion
    }
}
