using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject selectedImage;

    [SerializeField]
    private bool selected;

    public bool Selected
    {
        get { return selected; }
        set
        {
            selected = value;
            ControlSelected();
        }
    }

    protected virtual void Start()
    {
        ControlSelected();
    }

    protected virtual void ControlSelected()
    {
        if (selectedImage == null)
        {
            Debug.LogWarning("The property selectedImage is not setted");
            return;
        }

        selectedImage?.gameObject.SetActive(selected);
    }
}
