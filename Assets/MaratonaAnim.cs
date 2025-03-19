using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaratonaAnim : MonoBehaviour
{
    public Animator adult1;
    public Animator adult2;
    public Animator adult3;

    public Animator player;

    // Times
    float terceiro_lugar = 446f;
    float segundo_lugar = 1253f;
    float primeiro_lugar = 2058f;

    [SerializeField]
    private int secondsToFinish;

    [SerializeField]
    private GameObject resultPanel;

    private void Start()
    {
        StartCoroutine(MaratonaAnimation());
        StartCoroutine(End());
    }

    IEnumerator MaratonaAnimation()
    {
        player.SetTrigger("DistributeMedals");
        
        yield return new WaitForSeconds(terceiro_lugar);
        Podium(3);
        yield return new WaitForSeconds(segundo_lugar - terceiro_lugar);
        Podium(2);
        yield return new WaitForSeconds(primeiro_lugar - (segundo_lugar + terceiro_lugar));
        Podium(1);
    }

    void Podium(int place)
    {
        if (place == 3)
        {
            FlavorManager.fm.Puff(adult3.transform.position);
            adult3.SetTrigger("podium3");
        }
        else if (place == 2)
        {
            FlavorManager.fm.Puff(adult2.transform.position);
            adult2.SetTrigger("podium2");
        }
        else if (place == 1)
        {
            FlavorManager.fm.Puff(adult1.transform.position);
            adult1.SetTrigger("podium1");
        }

    }

    public IEnumerator End()
    {
        yield return new WaitForSecondsRealtime(secondsToFinish);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
    }
}
