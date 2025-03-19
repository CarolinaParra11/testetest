using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorClappingHelper : MonoBehaviour
{
    public Animator[] anim;

    private void Start()
    {
        for(int i = 0; i > anim.Length; i++)
        {
            anim[i].SetBool("clapping", true);
            anim[i].speed = Random.Range(0.8f,1.2f);
        }
    }

}
