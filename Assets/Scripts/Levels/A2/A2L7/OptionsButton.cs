using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct OptionsButton
{
    [SerializeField]
    private Options[] options;

    [SerializeField]
    private RectTransform parent;

    private SelectObjects[] selectOptions;

    [SerializeField]
    private GameObject buttonPrefab;

    public RectTransform Parent
    {
        get { return parent; }
    }

    public Options[] Options { get => options; }
    public SelectObjects[] SelectOptions { get => selectOptions; set => selectOptions = value; }
    public GameObject ButtonPrefab { get => buttonPrefab; }
}
