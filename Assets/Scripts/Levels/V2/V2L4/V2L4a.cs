using System.Collections;
using TMPro;
using UnityEngine;

public class V2L4a : MonoBehaviour
{
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    private V2L4Helper v2l4Helper;
    public GameObject scene1, scene2, scene3, scene4;
    public GameObject chars1, chars2, chars3, chars4;
    public float waitTime = 5;

    private void Start()
    {
        v2l4Helper = GameObject.Find("V2L4Helper").GetComponent<V2L4Helper>();

        if (v2l4Helper.choice == 1) { scene1.SetActive(true); chars1.SetActive(true); }
        if (v2l4Helper.choice == 2) { scene2.SetActive(true); chars2.SetActive(true); }
        if (v2l4Helper.choice == 3) { scene3.SetActive(true); chars3.SetActive(true); }
        if (v2l4Helper.choice == 4) { scene4.SetActive(true); chars4.SetActive(true); }

        resultText.text = "Tudo feito! Até a próxima!";

        StartCoroutine(End());
    }

    private IEnumerator End()
    {
        yield return new WaitForSeconds(waitTime);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
    }
}
