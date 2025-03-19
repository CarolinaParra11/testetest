using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace B25.Boludin.V2.L29
{
    public class V2L29 : LevelManagerBase
    {
        [Header("Texts")]
        [SerializeField]
        [TextArea]
        private string introText;

        [SerializeField]
        [TextArea]
        private string bonusQuestionText;

        [SerializeField]
        private string textStepResult;

        [SerializeField]
        private string textEndResult;

        [SerializeField]
        private int[] soundStartIndex;

        [SerializeField]
        private Image background;

        [SerializeField]
        private string selectMoreThenOneText;

        [SerializeField]
        private GasStation[] gasStations;

        [SerializeField]
        private Animator timeLapseAnimator;

        [SerializeField]
        private GameObject fuelAnimation;

        [SerializeField]
        private RectTransform fireworksPosition;

        private int indexIntro;
        private int indexBackground;

        protected override void Awake()
        {
            base.Awake();
            InitializeButtons();
            indexIntro = 0;

            timeLapseAnimator.GetComponent<TransitionCallback>().AddCallback(SetupGasStation);
        }

        private void Start()
        {
            for (int i = 0; i < optionsPanel.Length; i++)
            {
                optionsPanel[i].ValidateOnSelectionObject = CanSelectObjectBonus;
                optionsPanel[i].CanFinish = CanFinish;
            }

            SetupGasStation();
            
            StartCoroutine(ShowIntro());
        }

        private IEnumerator ShowIntro()
        {
            fuelAnimation.SetActive(false);
            yield return new WaitUntil(() => !infoPanel.activeSelf);
            AudioManager.am.PlayVoice(AudioManager.am.v2start[soundStartIndex[indexIntro]]);
            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoPanelText.text = introText;
            infoButton.onClick.AddListener(() =>
            {
                AudioManager.am.voiceChannel.Stop();
                FlavorManager.fm.ShowHidePanel(infoPanel, false);
            });

            yield return new WaitForSeconds(5.5f);
            SetUpButtons();
        }

        #region Steps
        private IEnumerator GasStationLoop()
        {
            yield return new WaitForSeconds(2.5f);
            SetupGasStation();
            timeLapseAnimator.SetTrigger("EndTransition");
            yield return new WaitForSeconds(5.5f);
            SetUpButtons();
        }

        private void SetupGasStation()
        {
            var gasStation = gasStations[indexBackground];
            if (gasStation.Equals(null))
            {
                Debug.LogError($"The gasStation {indexBackground} is null");
                return;
            }

            gasStation.SetUp(background);
        }

        private void SetUpButtons()
        {
            var gasStation = gasStations[indexBackground];
            gasStation.AlcoholButtonPressed = SelectButtonGasOption;
            gasStation.GasButtonPressed = SelectButtonGasOption;
        }

        private void NextGasStation()
        {
            gasStations[indexBackground].Disable();
            if (indexBackground >= gasStations.Length - 1)
            {
                optionsPanel[0].IntroText = string.Format(bonusQuestionText, PlayerManager.pm.nome);
                StartCoroutine(NextOptionPanel());
            }
            else
            {
                indexBackground++;
                timeLapseAnimator.SetTrigger("StartTransition");
                StartCoroutine(GasStationLoop());
            }
        }

        private IEnumerator BarAnimation()
        {
            FlavorManager.fm.ShowHidePanel(fuelAnimation, true);
            yield return new WaitForSecondsRealtime(6);
            FlavorManager.fm.Fireworks(fireworksPosition);
            yield return new WaitForSecondsRealtime(1);
            FlavorManager.fm.ShowHidePanel(fuelAnimation, false);
            NextGasStation();
        }

        private bool CanFinish() => optionsPanel[step].OptionButtons
                                        .SelectOptions
                                        .FirstOrDefault(s => s.Selected) != null;

        protected override IEnumerator End()
        {
            yield return new WaitForSeconds(0.5f);
            AnimationBucks();

            resultPanelText.text = textEndResult;
            FlavorManager.fm.ShowHidePanel(resultPanel, true);

            APIManager.am.Relatorio(conceptManager.GetResult());
            PlayerManager.pm.AddLevel();
        }

        #endregion

        #region Button
        private void SelectButtonGasOption(SelectObjects selectObjects)
        {
            if (!CanSelectObject(selectObjects))
                return;

            selectObjects.Selected = !selectObjects.Selected;
            StartCoroutine(BarAnimation());
        }

        private bool CanSelectObjectBonus(SelectObjects obj)
        {
            var canSelect = obj.Selected || optionsPanel[step].OptionButtons.SelectOptions.FirstOrDefault(s => s.Selected) == null;
            if (!canSelect)
                ShowInfoPanel(selectMoreThenOneText);
            
            return canSelect;
        }

        private bool CanSelectObject(SelectObjects obj) => obj.Selected || gasStations[indexBackground].CanSelect();
        #endregion
    }
}

