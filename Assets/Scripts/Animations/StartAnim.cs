using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Parameter
{
    Trigger,
    Bool,
}

public class StartAnim : MonoBehaviour
{
    public float maxspeed, minspeed;
    private Animator anim;
    public Parameter parameter;
    public string parameterName;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = Random.Range(minspeed, maxspeed);        
        if (parameter == Parameter.Bool) anim.SetBool(parameterName, true);
        if (parameter == Parameter.Trigger) anim.SetTrigger(parameterName);
    }

}
