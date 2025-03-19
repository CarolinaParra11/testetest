using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L18 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject[] priority;
    public TextMeshProUGUI[] priorityText;
    public Button[] button;
    private int priorityCounter;
    public Button submitButton;
    private int coins;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        conceitos.Add("25", 0);
        conceitos.Add("1", 0);
        conceitos.Add("10", 0);
        conceitos.Add("23", 0);
        conceitos.Add("12", 0);
        conceitos.Add("20", 0);
        conceitos.Add("31", 0);
        conceitos.Add("7", 0);
        conceitos.Add("24", 0);

        conceitos["25"] += 0;
        conceitos["1"] += 0;
        conceitos["10"] += 0;
        conceitos["23"] += 0;
        conceitos["12"] += 0;
        conceitos["20"] += 0;
        conceitos["31"] += 0;
        conceitos["7"] += 0;
        conceitos["24"] += 0;

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[17]);
        infoText.text = "Escolha por ordem de importância, prestando atenção à sustentabilidade e a como poupar.";
        infoButton.onClick.AddListener(delegate 
        { 
            FlavorManager.fm.ShowHidePanel(infoPanel, false); 
            SetButtons(true);
            AudioManager.am.voiceChannel.Stop();
        });

        SetButtons(false);

        button[0].onClick.AddListener(delegate { Priority(priority[0], priorityText[0]); });
        button[1].onClick.AddListener(delegate { Priority(priority[1], priorityText[1]); });
        button[2].onClick.AddListener(delegate { Priority(priority[2], priorityText[2]); });
        submitButton.onClick.AddListener(delegate { End(); });
    }

    private void Priority(GameObject obj, TextMeshProUGUI text)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        if (!obj.activeSelf)
        {
            obj.SetActive(true);
            priorityCounter++;
            text.text = priorityCounter.ToString();
        }
        else
        {
            obj.SetActive(false);
            priorityCounter--;

            foreach (TextMeshProUGUI t in priorityText)
            {
                if (Convert.ToInt32(t.text) > Convert.ToInt32(text.text))
                {
                    int i = Convert.ToInt32(t.text);
                    i--;
                    t.text = i.ToString();
                }
            }

            text.text = "0";
        }

        if (priorityCounter == 3) submitButton.gameObject.SetActive(true);
        else submitButton.gameObject.SetActive(false);
    }

    private void End()
    {
        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        resultText.text = "";

        if (Convert.ToInt32(priorityText[0].text) == 1) 
        {
            conceitos["25"] += 2;
            conceitos["1"] += 1;
            conceitos["10"] += 1;
            conceitos["23"] += 2;
            conceitos["12"] += 2;
            conceitos["20"] += 2;
            conceitos["31"] += 2;
            conceitos["7"] += 1;
            conceitos["24"] += 1;

            resultText.text += "Chuveiro fechado em primeiro lugar: pensou no ecossistema e economizou dinheiro da conta de água. Ganhou 2 moedas!\n\n"; 
            coins += 2; 
        }
        else if (Convert.ToInt32(priorityText[0].text) == 2) { resultText.text += "Chuveiro fechado, mas não foi a prioridade e água foi perdida. Você perdeu 1 moedas!\n\n"; coins -= 1; }
        else { resultText.text += "Chuveiro fechado, mas muita água foi perdida. Você perdeu 2 moedas!\n\n"; coins -= 2; }

        if (Convert.ToInt32(priorityText[1].text) == 2) 
        {
            conceitos["25"] += 1;
            conceitos["1"] += 1;
            conceitos["10"] += 1;
            conceitos["23"] += 0;
            conceitos["12"] += 2;
            conceitos["20"] += 1;
            conceitos["31"] += 1;
            conceitos["7"] += 1;
            conceitos["24"] += 0;

            resultText.text += "\nLouça lavada na hora certa! As bactérias foram eliminadas, você ajudou o ecossistema! Ganhou 1 moeda!\n\n"; 
            coins += 1; 
        }
        else { resultText.text += "\nLouça lavada! Mas fechar o chuveiro é mais importante e arrumar sua cama é menos imporante. Não ganhou moedas.\n\n"; }

        conceitos["25"] += 0;
        conceitos["1"] += 0;
        conceitos["10"] += 1;
        conceitos["23"] += 0;
        conceitos["12"] += 0;
        conceitos["20"] += 0;
        conceitos["31"] += 0;
        conceitos["7"] += 0;
        conceitos["24"] += 0;

        resultText.text += "\nCama arrumada! Por ser uma tarefa que ajuda a casa, ganhou 1 moeda!\n\n"; coins += 1;

        resultText.text += "\n\nAté a próxima!";

        AudioManager.am.PlayVoice(AudioManager.am.v1l18[3]);

        /*if(coins < 1) AudioManager.am.PlayVoice(AudioManager.am.v1l18[0]);
        else if(coins == 1) AudioManager.am.PlayVoice(AudioManager.am.v1l18[1]);
        else AudioManager.am.PlayVoice(AudioManager.am.v1l18[2]);*/

        StartCoroutine(FlavorManager.fm.SpawnCoin(coins));
        PlayerManager.pm.AddCoins(coins);
        APIManager.am.Relatorio(conceitos);
        PlayerManager.pm.AddLevel();

        if (coins > 0)
        {
            FlavorManager.fm.ShowHidePanel(incomePanel, true);

            int counter = 0;

            while (counter < coins)
            {
                coinsPanel.GetChild(counter).gameObject.SetActive(true);
                counter++;
            }
        }
    }

    private void SetButtons(bool value)
    {
        foreach (Button b in button) b.enabled = value;
    }
}