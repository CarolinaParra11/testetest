using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IconVersionChanger : MonoBehaviour
{
    public Sprite imV1, imV2;


    // Start is called before the first frame update
    void Start()
    {
        Image coinIm = GetComponent<Image>();

        if (PlayerManager.pm.v2)
        {
            coinIm.sprite = imV2;
        }
        else
        {
            coinIm.sprite = imV1;
        }
    }

}
