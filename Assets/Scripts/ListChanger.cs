using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListChanger : MonoBehaviour
{
    private Transform backgroundIcon;
    private List<GameObject> bgIcons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        backgroundIcon = GetComponent<Transform>();
        GrabGridBG();

        VersionChanger();


    }

    void GrabGridBG()
    {
        foreach(RectTransform bg in backgroundIcon)
        {
        bg.gameObject.SetActive(false);
        bgIcons.Add(bg.gameObject);
        }
    }

    void VersionChanger()
    {
        if(PlayerManager.pm.v2)
        {
            for(int i = 0; i < 8; i++)
            {
                bgIcons[i].SetActive(true);
            }            
        }
        else
        {
            for(int i = 0; i < 6; i++)
            {
                bgIcons[i].SetActive(true);
            }         
        }

    }



}
