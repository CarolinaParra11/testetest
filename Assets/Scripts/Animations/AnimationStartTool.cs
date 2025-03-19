using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParameterType
{
    Boolean,
    Trigger,
}

public class AnimationStartTool : MonoBehaviour
{
    private Animator anim;
    public string stringName;
    public ParameterType parameterType;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
    
    
        if(parameterType == ParameterType.Boolean)
        {
            anim.SetBool(stringName, true);
        }
        if(parameterType == ParameterType.Trigger)
        {
            anim.SetTrigger(stringName);
        }
    }
}
