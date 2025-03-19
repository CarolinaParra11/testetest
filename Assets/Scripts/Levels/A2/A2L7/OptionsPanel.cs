using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct OptionsPanel
{
    [SerializeField]
    private GameObject gameobjectPanel;

    [SerializeField]
    private TextMeshProUGUI panelText;

    [SerializeField]
    private Button resultButton;

    [SerializeField]
    [TextArea]
    private string introText;

    [SerializeField]
    [TextArea]
    private string textOnFinish;

    [SerializeField]
    private OptionsButton optionButtons;

    public GameObject GameobjectPanel { get => gameobjectPanel; }
    public TextMeshProUGUI PanelText { get => panelText; }
    public Button ResultButton { get => resultButton; }
    public OptionsButton OptionButtons { get => optionButtons; }
    public string IntroText { get => introText; set => introText = value; }
    public string TextOnFinish { get => textOnFinish; set => textOnFinish = value; }

    public Action Initialize;
    public Func<SelectObjects, bool> ValidateOnSelectionObject;
    public Action<SelectObjects> OnSelection;
    public Func<bool> CanFinish;
    public Action OnFinish;

    public void SetSelectableObjects(SelectObjects[] objects)
    {
        if (objects == null || objects.Length <= 0)
        {
            Debug.LogError("Select Objects cannot be null or empty");
            return;
        }

        optionButtons.SelectOptions = objects;
    }
}
