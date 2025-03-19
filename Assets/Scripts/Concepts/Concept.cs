using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Concept
{
    [SerializeField]
    private string key;

    public string Key
    {
        get { return key; }
        set { key = value; }
    }

    [SerializeField]
    private double reward;

    public double Reward
    {
        get { return reward; }
        set { reward = value; }
    }
}
