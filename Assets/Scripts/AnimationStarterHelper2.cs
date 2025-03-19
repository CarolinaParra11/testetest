using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStarterHelper2 : MonoBehaviour
{
    public Animator[] anim;
    public string parametro;

    private void Start()
    {
        for(int i = 0; i < anim.Length; i++)
        {
            anim[i].SetBool(parametro, true);
            anim[i].speed = Random.Range(0.5f, 1.2f);
        }
    }

}
