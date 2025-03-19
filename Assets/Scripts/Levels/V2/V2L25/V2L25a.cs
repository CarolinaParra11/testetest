using System.Collections;
using UnityEngine;

public class V2L25a : MonoBehaviour
{
    public GameObject resultPanel;
    public float waitTime = 5;

    void Start()
    {
        StartCoroutine(End());
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(waitTime);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
    }
}
