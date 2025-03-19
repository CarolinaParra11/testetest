using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResponsiveCanvas : MonoBehaviour
{
    float actualRatio;
    Canvas[] canvases;
    // Start is called before the first frame update
    void Start()
    {
        canvases = GameObject.FindObjectsOfType<Canvas>();
        Debug.Log(canvases.Length);
    }

    private void Update()
    {
        actualRatio = Screen.width / Screen.height;
        if (actualRatio < 1.7f)
        {
            for (int i = 0; i < canvases.Length; i++)
            {
                canvases[i].GetComponent<CanvasScaler>().matchWidthOrHeight = 0;
            }
        }
        else
        {
            for (int i = 0; i < canvases.Length; i++)
            {
                canvases[i].GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
            }
        }
    }
}
