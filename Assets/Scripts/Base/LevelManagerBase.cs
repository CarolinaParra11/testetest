using B25.Boludin.V2.L30;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ConceptManager))]
public abstract class LevelManagerBase : MonoBehaviour
{
    #region Info Panel
    [Header("Info panel")]
    [SerializeField]
    protected GameObject infoPanel;

    [SerializeField]
    protected TextMeshProUGUI infoPanelText;

    [SerializeField]
    protected Button infoButton;
    #endregion

    #region Result Panel
    [Header("Result panel")]
    [SerializeField]
    protected GameObject resultPanel;

    [SerializeField]
    protected TextMeshProUGUI resultPanelText;

    [SerializeField]
    protected Button resultButton;
    #endregion

    #region Coin spawn
    [Header("Coin Spawn")]
    [SerializeField]
    protected RectTransform coinSpawn;

    [SerializeField]
    protected RectTransform coinWaypoint;

    [SerializeField]
    protected int numberCoins = 5;
    
    [SerializeField]
    protected bool coins;
    #endregion

    [SerializeField]
    protected OptionsPanel[] optionsPanel;

    [SerializeField]
    protected TextMeshProUGUI moneyText;

    protected ConceptManager conceptManager;

    [SerializeField]
    private GameObject buttonPrefab;

    [SerializeField]
    protected int maxCost;

    protected string selectOptionText = "Selecione uma opção"; 

    private int cost;

    protected int step;

    protected int totalCoins;

    protected int RemainMoney { get => maxCost - Cost; }

    protected int Cost { 
        get => cost; 
        set
        {
            cost = value;
            UpdateMoneyText(cost);
        } 
    }

    protected virtual void Awake()
    {
        conceptManager = GetComponent<ConceptManager>();
        //InitializeButtons();
    }

    protected virtual void InitializeButtons()
    {
        if (optionsPanel == null || optionsPanel.Length <= 0)
        {
            Debug.LogError("Options panel is null or empty");
            return;
        }

        for (int i = 0; i < optionsPanel.Length; i++)
        {
            SelectObjects[] selectOptions = null;
            CreateButtons(optionsPanel[i].OptionButtons.ButtonPrefab, optionsPanel[i].OptionButtons.Options, out selectOptions, optionsPanel[i].OptionButtons.Parent);
            optionsPanel[i].SetSelectableObjects(selectOptions);
        }
    }

    protected void CreateButtons(GameObject buttonPrefab, Options[] options, out SelectObjects[] buttons, RectTransform transform)
    {
        buttons = null;
        if (!CanInitializeButtons(buttonPrefab, options, transform) || buttonPrefab.GetComponent<DragDropItem>() != null)
            return;

        buttons = new SelectObjects[options.Length];
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i] is null)
                continue;

            GameObject obj = Instantiate(buttonPrefab, transform.position, Quaternion.identity, transform);

            var index = i;
            var selectObject = obj.GetComponent<SelectObjects>();

            if (selectObject == null)
                return;

            buttons[index] = selectObject;
            buttons[index].Option = options[index];
            buttons[index].Button.onClick.AddListener(() =>
            {
                Debug.Log($"Called button {index}");
                SelectButton(selectObject);
            });
        }
    }

    protected void CreateButtons(Options[] options, out SelectObjects[] buttons, RectTransform transform)
    {
        CreateButtons(buttonPrefab, options, out buttons, transform);
    }

    protected void CreateDragDropItens(GameObject dragDropPrefab, Options[] options, RectTransform parent, Canvas canvasDragDrop, GridLayoutGroup gridLayout, Action<GameObject, Options> OnInitialize = null)
    {
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
            //dragDrop.id = (int)((OptionDragDrop)options[i]).ItemType;
            //dragDrop.essential = ((OptionDragDrop)options[i]).ItemType == ItemType.Essencial ? "Essencial" : "Não essencial";
            dragDrop.canvas = canvasDragDrop;
            dragDrop.GridLayout = gridLayout;
            OnInitialize?.Invoke(obj, options[i]);
        }
    }

    protected virtual bool CanInitializeButtons(GameObject buttonPrefab, Options[] options, RectTransform buttonParent)
    {
        var buttonIsNull = buttonPrefab is null;
        var optionsIsEmpty = options.Length == 0;
        var buttonParentIsNull = buttonParent is null;

        if (buttonIsNull)
        {
            Debug.LogError("buttonPrefab cannot be null!");
        }

        if (optionsIsEmpty)
        {
            Debug.LogError("Options array is empty");
        }

        if (buttonParentIsNull)
        {
            Debug.LogError("buttonParent cannot be null");
        }

        return (!buttonIsNull) &&
                !(optionsIsEmpty)
                && !(buttonParentIsNull);
    }

    protected void SelectButton(SelectObjects selectObjects)
    {
        var optionPanel = optionsPanel[step];
        var cost = selectObjects.Option.Cost;
        if (optionPanel.ValidateOnSelectionObject == null) 
        {
            Debug.LogWarning("The func CanSelect is not suplanted");
            return;
        }

        if (!optionPanel.ValidateOnSelectionObject.Invoke(selectObjects))
            return;

        selectObjects.Selected = !selectObjects.Selected;

        if (optionPanel.OnSelection == null)
            Debug.LogWarning("The func StepSelection is not suplanted");

        optionPanel.OnSelection?.Invoke(selectObjects);
    }

    protected void AnimationBucks()
    {
        if (coins)
        {
            StartCoroutine(FlavorManager.fm.SpawnCoinPosition(numberCoins, coinSpawn, coinWaypoint));
        }
        else { 
            StartCoroutine(FlavorManager.fm.SpawnBucksPosition(numberCoins, coinSpawn, coinWaypoint));
        }
    }

    protected void ShowInfoPanel(string text, Action callback = null)
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoPanelText.text = text;
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(() =>
        {
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
            callback?.Invoke();
        });
    }

    private void UpdateMoneyText(int cost)
    {
        if (moneyText == null)
        {
            Debug.LogWarning("MoneyText is null");
            return;
        }

        moneyText.text = (maxCost - cost).ToString();
    }

    protected bool HasEnoughMoney(int cost) => (Cost + cost) <= maxCost;

    private IEnumerator ShowOptionPanel()
    {
        yield return new WaitForSeconds(0.5f);
        if (optionsPanel[step].Initialize != null)
            optionsPanel[step].Initialize.Invoke();

        FlavorManager.fm.ShowHidePanel(optionsPanel[step].GameobjectPanel, true);
        optionsPanel[step].ResultButton.onClick.RemoveAllListeners();
        optionsPanel[step].ResultButton.onClick.AddListener(() =>
        {
            if (optionsPanel[step].CanFinish != null && !(bool)optionsPanel[step].CanFinish?.Invoke())
            {
                ShowInfoPanel(selectOptionText);
                return;
            }
            else
            {
                optionsPanel[step].ResultButton.onClick.RemoveAllListeners();
                AnimationBucks();
                FlavorManager.fm.ShowHidePanel(optionsPanel[step].GameobjectPanel, false);
                Action callNext = optionsPanel[step].OnFinish != null ? optionsPanel[step].OnFinish : () => StartCoroutine(NextOptionPanel());

                if (!string.IsNullOrEmpty(optionsPanel[step].TextOnFinish))
                    ShowInfoPanel(optionsPanel[step].TextOnFinish, callNext);
                else
                    callNext?.Invoke();
                step++;
            }
        });
    }

    protected IEnumerator NextOptionPanel()
    {
        yield return new WaitUntil(() => !infoPanel.activeInHierarchy);

        if (optionsPanel.Length - 1 < step)
        {
            StartCoroutine(End());
            yield return null;
        }

        string introText = optionsPanel[step].IntroText;
        Action callOptionPanel = () => {
            StartCoroutine("ShowOptionPanel");
        };

        if (!string.IsNullOrEmpty(introText))
            ShowInfoPanel(introText, callOptionPanel);
        else
            callOptionPanel.Invoke();
    }

    protected abstract IEnumerator End();
}
