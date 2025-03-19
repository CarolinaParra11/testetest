using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectObjects : ButtonsSelect
{
    [SerializeField]
    protected Options option;

    public Options Option { get => option; set => option = value; }

    [SerializeField]
    protected Button button;

    public Button Button
    {
        get { return button; }
        set { button = value; }
    }

    [SerializeField]
    protected TextMeshProUGUI optionText;

    [SerializeField]
    protected TextMeshProUGUI textCost;

    [SerializeField]
    protected GameObject image;

    protected override void Start()
    {
        base.Start();

        if (option.Image != null)
            Instantiate(option.Image, image.transform.position, Quaternion.identity, image.transform);

        if (textCost != null)
            textCost.text = option.Cost.ToString();

        if (optionText is null)
        {
            Debug.LogError("The property optionText cannot be null");
            return;
        }

        optionText.text = option.Text;
    }
}

