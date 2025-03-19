using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace B25.Boludin.V2.L28
{
    public class V2L28a : MonoBehaviour
    {
        [SerializeField]
        [Header("Texts")]
        private string infoText;

        [SerializeField]
        private Image background;

        [SerializeField]
        private RectTransform parent;

        [SerializeField]
        private ChoseAnimation choseAnimation;

        #region Result Panel
        [Header("Result panel")]
        [SerializeField]
        protected GameObject resultPanel;

        [SerializeField]
        protected TextMeshProUGUI resultPanelText;

        [SerializeField]
        protected Button resultButton;
        #endregion

        [SerializeField]
        private float secondsNextLevel;

        void Awake()
        {
            background.sprite = choseAnimation?.Background;
            Instantiate(choseAnimation?.Animation, parent.position, Quaternion.identity, parent);
        }

        private void Start()
        {
            StartCoroutine(EndLevel());
        }

        private IEnumerator EndLevel()
        {
            yield return new WaitForSeconds(secondsNextLevel);
            FlavorManager.fm.ShowHidePanel(resultPanel, true);
            resultPanelText.text = infoText;
            resultButton.onClick.AddListener(() =>
            {
                PlayerManager.pm.AddLevel();
            });
        }
    }
}


