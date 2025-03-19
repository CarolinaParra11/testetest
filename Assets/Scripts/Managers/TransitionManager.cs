using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionManager : MonoBehaviour
{
    public static TransitionManager tm;

    [Header("Transitions")]
    private Animator transition;

    private void Awake()
    {
        tm = this;

        transition = GetComponentInChildren<Animator>();
    }

    public void SetTriggerAnimator(string transName)
    {
        transition.SetTrigger(transName);
    }

    #region TRANSITIONS
    public IEnumerator LoadTransition(string sceneName, float transTime,  float posTransTime)
    {
        transition.SetTrigger("StartTransition");
        AudioManager.am.PlayTransition(AudioManager.am.transitionIn);
        yield return new WaitForSeconds(transTime + 1f);
        
        AsyncOperation oper = SceneManager.LoadSceneAsync(sceneName);
        while(!oper.isDone)
        {
            yield return null;
        }

        if (sceneName.Contains("MainMenu"))
            AudioManager.am.PlayMusic(AudioManager.am.mainMenu);

        if (sceneName.Contains("V1") || sceneName.Contains("V2"))
            AudioManager.am.PlayRandomMusic();

        transition.SetTrigger("EndTransition");
        AudioManager.am.PlayTransition(AudioManager.am.transitionOut);
        yield return new WaitForSeconds(posTransTime);

    }
    #endregion

}
