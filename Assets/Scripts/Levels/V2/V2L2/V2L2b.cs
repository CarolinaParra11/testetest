using System.Collections;
using UnityEngine;

public class V2L2b : MonoBehaviour
{
    public GameObject resultPanel;
    public GameObject animation1, animation2, animation3, animation4;
    public float waitTime = 5;

    void Start()
    {
        if (PlayerManager.pm.professionId == 1) animation1.SetActive(true);
        else if (PlayerManager.pm.professionId == 2) animation2.SetActive(true);
        else if (PlayerManager.pm.professionId == 3) animation3.SetActive(true);
        else animation4.SetActive(true);

        StartCoroutine(End());
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(waitTime);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
    }
}
