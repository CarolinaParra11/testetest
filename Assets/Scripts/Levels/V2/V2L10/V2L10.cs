using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V2L10 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject giftA, giftB, giftC;
    public GameObject optionPanel;
    public Button button1, button2;
    public TextMeshProUGUI bonus1, bonus2, value1, value2;
    public TextMeshProUGUI counterText;
    public GameObject lidClosed, lidOpen;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
      //  AudioManager.am.PlayVoice(AudioManager.am.v1start[9]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Hoje é o dia do cofre. Vamos ver se você conseguiu realizar o primeiro sonho.";
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); AudioManager.am.voiceChannel.Stop(); });
    }

    private IEnumerator Part1()
    {
        conceitos.Add("34", 2);
        conceitos.Add("23", 3);
        conceitos.Add("20", 4);
        conceitos.Add("28", 2);
        conceitos.Add("53", 4);
        conceitos.Add("54", 3);
        conceitos.Add("22", 4);

        Animator textAnim = counterText.GetComponent<Animator>();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);

        float currentTime = 4;

        while (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;

            textAnim.SetTrigger("Start");

            counterText.text = ((int)currentTime).ToString();
            yield return null;
        }

        FlavorManager.fm.BigPuff();

        if (lidClosed.activeInHierarchy)
        {
            lidClosed.SetActive(false);
            lidOpen.SetActive(true);
        }

        if (PlayerManager.pm.gift1 == 1) FlavorManager.fm.ShowHidePanel(giftA, true);
        if (PlayerManager.pm.gift1 == 2) FlavorManager.fm.ShowHidePanel(giftB, true);
        if (PlayerManager.pm.gift1 == 3) FlavorManager.fm.ShowHidePanel(giftC, true);

        counterText.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);

        if (PlayerManager.pm.safe1 == 15) StartCoroutine(End());
        else if (PlayerManager.pm.safe1 >= 30) Part2();
    }

    private void Part2() 
    {
        // 15 30 40 100
        // 15 - 15 = 0 || 30 - 15 = 15 || 40 - 15 = 25 || 100 - 15 == 85
        // 40 - 40 = 0 || 55 - 40 = 15 || 100 - 40 == 60 || 115 - 40 = 75
        // 15 - 100 = -85 || 30 - 100 = -70 || 40 - 100 = -60 || 100 - 100 = 0

        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        if (PlayerManager.pm.safe1 == 30)
        {
            bonus1.text = "15"; value1.text = "Pote 2:\nJá tem " + PlayerManager.pm.safe2.ToString() + " reais.";
            bonus2.text = "15"; value2.text = "Pote 3:\nJá tem " + PlayerManager.pm.safe3.ToString() + " reais.";
        }
        else if (PlayerManager.pm.safe1 == 40)
        {
            bonus1.text = "25"; value1.text = "Pote 2:\nJá tem " + PlayerManager.pm.safe2.ToString() + " reais.";
            bonus2.text = "25"; value2.text = "Pote 3:\nJá tem " + PlayerManager.pm.safe3.ToString() + " reais.";
        }
        else if (PlayerManager.pm.safe1 == 100)
        {
            bonus1.text = "85"; value1.text = "Pote 2:\nJá tem " + PlayerManager.pm.safe2.ToString() + " reais.";
            bonus2.text = "85"; value2.text = "Pote 3:\nJá tem " + PlayerManager.pm.safe3.ToString() + " reais.";
        }

        button1.onClick.AddListener(delegate 
        {
            if (PlayerManager.pm.safe1 == 30) PlayerManager.pm.safe2 += 15;
            else if (PlayerManager.pm.safe1 == 40) PlayerManager.pm.safe2 += 25;
            else if (PlayerManager.pm.safe1 == 100) PlayerManager.pm.safe2 += 85;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            StartCoroutine(End());
        });
        button2.onClick.AddListener(delegate 
        {
            if (PlayerManager.pm.safe1 == 30) PlayerManager.pm.safe3 += 15;
            else if (PlayerManager.pm.safe1 == 40) PlayerManager.pm.safe3 += 25;
            else if (PlayerManager.pm.safe1 == 100) PlayerManager.pm.safe3 += 85;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            StartCoroutine(End());
        });
    }

    private IEnumerator End()
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Parabéns, conseguiu alcançar o sonho 1!\n\nAté a próxima!";

        APIManager.am.Relatorio(conceitos);
        PlayerManager.pm.AddLevel();
    }
}