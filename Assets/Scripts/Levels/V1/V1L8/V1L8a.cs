using System.Collections;
using TMPro;
using UnityEngine;

public class V1L8a : MonoBehaviour
{
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    //public GameObject hintButton;

    private V1L8Helper v1l8Helper;
    public GameObject scene1, scene2, scene3, scene4;
    public GameObject chars1, chars2, chars3, chars4;
    public float waitTime = 5;

    private void Start()
    {
        //hintButton.SetActive(true);
        v1l8Helper = GameObject.Find("V1L8Helper").GetComponent<V1L8Helper>();

        if (v1l8Helper.choice == 1) { scene1.SetActive(true); chars1.SetActive(true); }
        if (v1l8Helper.choice == 2) { scene2.SetActive(true); chars2.SetActive(true); }
        if (v1l8Helper.choice == 3) { scene3.SetActive(true); chars3.SetActive(true); }
        if (v1l8Helper.choice == 4) { scene4.SetActive(true); chars4.SetActive(true); }

        resultText.text = "Tudo feito! Até a próxima!";

        StartCoroutine(End());
    }

    private IEnumerator End()
    {
        //hintButton.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
    }
}
