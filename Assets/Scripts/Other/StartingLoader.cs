using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingLoader : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {      
        AsyncOperation oper = SceneManager.LoadSceneAsync("LoadManager");

        while(!oper.isDone)
        {
            yield return null;
        }
        anim.SetTrigger("EndTransition");
    }
}
