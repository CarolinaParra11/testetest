using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCallback : MonoBehaviour
{
    private IList<Action> callback = new List<Action>();

    public void AddCallback(Action callback)
    {
        if (callback == null)
            return;

        this.callback.Add(callback);
    }

    public void RemoveCallback(Action callback)
    {
        if (this.callback.Contains(callback))
            this.callback.Remove(callback);
    }

    public void CleanAllCallbacks()
    {
        callback.Clear();
    }

    public void FireOnExpandCallback()
    {
        foreach (var item in callback)
            item?.Invoke();
    }
}
