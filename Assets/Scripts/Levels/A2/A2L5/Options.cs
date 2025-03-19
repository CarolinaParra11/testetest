using B25.Boludin.V2.L28;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Options Data", menuName = "ScriptableObjects/Options/Options Data", order = 1)]
public class Options : ScriptableObject
{
    [SerializeField]
    protected int cost;

    public int Cost
    {
        get { return cost; }
    }

    [SerializeField]
    protected int bonus;

    public int Bonus
    {
        get { return bonus; }
        set { bonus = value; }
    }


    [SerializeField]
    [TextArea]
    protected string text;

    public string Text
    {
        get { return text; }
    }

    [SerializeField]
    protected GameObject image;

    public GameObject Image { get => image; }

    [SerializeField]
    protected Concepts concepts;

    public Concepts Concepts { get => concepts; }

    [SerializeField]
    protected ChoseAnimation chosenAnimation;

    public ChoseAnimation ChosenAnimation
    {
        get { return chosenAnimation; }
        set { chosenAnimation = value; }
    }
}

