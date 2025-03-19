using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChange : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnimator;

    void Start()
    {
        playerAnimator.SetBool("JumpingPool", true);
    }
}
