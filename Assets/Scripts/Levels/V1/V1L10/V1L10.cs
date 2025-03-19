using TMPro;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class V1L10 : MonoBehaviour
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
        conceitos.Add("34", 1);
        conceitos.Add("23", 1);
        conceitos.Add("20", 2);
        conceitos.Add("28", 2);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[9]);
        infoText.text = "Hoje é o dia do cofre. Vamos ver se você conseguiu realizar o primeiro sonho.";
        infoButton.onClick.AddListener(delegate 
        { 
            StartCoroutine(Part1());
            AudioManager.am.voiceChannel.Stop();
        });

        APIManager.am.Relatorio(conceitos);
    }

    private IEnumerator Part1()
    {
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

        if (PlayerManager.pm.safe1 == 1) End();
        else if (PlayerManager.pm.safe1 > 1) Part2();
    }

    private void Part2()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        if (PlayerManager.pm.safe1 == 2)
        {
            //AudioManager.am.PlayVoice(AudioManager.am.v1l10[0]);
            bonus1.text = "1"; value1.text = "Cofre 2:\nJá tem " + PlayerManager.pm.safe2.ToString() + " moedas.";
            bonus2.text = "1"; value2.text = "Cofre 3:\nJá tem " + PlayerManager.pm.safe3.ToString() + " moedas.";
        }
        else if (PlayerManager.pm.safe1 == 3)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l10[1]);
            bonus1.text = "2"; value1.text = "Cofre 2:\nJá tem " + PlayerManager.pm.safe2.ToString() + " moedas.";
            bonus2.text = "2"; value2.text = "Cofre 3:\nJá tem " + PlayerManager.pm.safe3.ToString() + " moedas.";
        }

        button1.onClick.AddListener(delegate
        {
            if (PlayerManager.pm.safe1 == 2)
            {
                StartCoroutine(FlavorManager.fm.SpawnCoin(1));
                PlayerManager.pm.safe2 += 1;
            }
            else if (PlayerManager.pm.safe1 == 3)
            {
                StartCoroutine(FlavorManager.fm.SpawnCoin(2));
                PlayerManager.pm.safe2 += 2;
            }
            End();
        });
        button2.onClick.AddListener(delegate
        {
            if (PlayerManager.pm.safe1 == 2)
            {
                StartCoroutine(FlavorManager.fm.SpawnCoin(1));
                PlayerManager.pm.safe3 += 1;
            }
            else if (PlayerManager.pm.safe1 == 3)
            {
                StartCoroutine(FlavorManager.fm.SpawnCoin(2));
                PlayerManager.pm.safe3 += 2;
            }
            End();
        });
    }

    private void End()
    {
        AudioManager.am.voiceChannel.Stop();
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Parabéns, conseguiu alcançar o sonho 1!\n\nAté a próxima!";
        PlayerManager.pm.AddLevel();
    }
}