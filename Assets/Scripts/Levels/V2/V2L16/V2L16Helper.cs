using System.Collections.Generic;
using UnityEngine;

public class V2L16Helper : MonoBehaviour
{
    public int coins = 1100;
    public List<int> choices;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        choices = new List<int>();
    }

    public int GetCoins()
    {
        return coins;
    }
    public void AddCoins(int i)
    {
        coins += i;
    }

    public void RemoveCoins(int i)
    {
        coins -= i;
    }

    public void AddChoice(int i)
    {
        choices.Add(i);
    }

    public void RemoveChoice(int i)
    {
        choices.RemoveAt(choices.IndexOf(i));
    }
}
