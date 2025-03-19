using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L17 : MonoBehaviour
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
    public RectTransform startT, endT;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;
    public int totalCoins;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        // Declaração
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

        AudioManager.am.PlayVoice(AudioManager.am.v2start[16]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Escolha por ordem de importância, analisando como poupar, e também a sustentabilidade.";
        infoButton.onClick.AddListener(delegate { FlavorManager.fm.ShowHidePanel(infoPanel, false); SetButtons(true); AudioManager.am.voiceChannel.Stop(); infoButton.onClick.RemoveAllListeners(); });

        SetButtons(false);

        button[0].onClick.AddListener(delegate { Priority(priority[0], priorityText[0]); });
        button[1].onClick.AddListener(delegate { Priority(priority[1], priorityText[1]); });
        button[2].onClick.AddListener(delegate { Priority(priority[2], priorityText[2]); });
        button[3].onClick.AddListener(delegate { Priority(priority[3], priorityText[3]); });
        button[4].onClick.AddListener(delegate { Priority(priority[4], priorityText[4]); });
        submitButton.onClick.AddListener(delegate { End(); });
    }

    private void Priority(GameObject obj, TextMeshProUGUI text)
    {
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

        if (priorityCounter == 5) submitButton.gameObject.SetActive(true);
        else submitButton.gameObject.SetActive(false);
    }

    private void End()
    {
        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        resultText.text = "";

        if (Convert.ToInt32(priorityText[0].text) <= 2) 
        { 
            resultText.text += "Chuveiro: Ganhou 80 reais!"; PlayerManager.pm.AddCoins(80); totalCoins += 80;

            conceitos["25"] += 5;
            conceitos["1"] += 6;
            conceitos["10"] += 6;
            conceitos["23"] += 6;
            conceitos["12"] += 7;
            conceitos["20"] += 6;
            conceitos["31"] += 8;
            conceitos["7"] += 8;
            conceitos["24"] += 7;
        }
        else if (Convert.ToInt32(priorityText[0].text) == 3) { resultText.text += "Chuveiro: Perdeu 15 reais!"; PlayerManager.pm.RemoveCoins(15); totalCoins -= 15; }
        else { resultText.text += "Chuveiro: Perdeu 30 reais!"; PlayerManager.pm.RemoveCoins(30); totalCoins -= 30; }

        if (Convert.ToInt32(priorityText[3].text) <= 2)
        {
            resultText.text += "\nLuz: Ganhou 80 reais!"; PlayerManager.pm.AddCoins(80); totalCoins += 80;

            conceitos["25"] += 5;
            conceitos["1"] += 6;
            conceitos["10"] += 6;
            conceitos["23"] += 6;
            conceitos["12"] += 7;
            conceitos["20"] += 6;
            conceitos["31"] += 8;
            conceitos["7"] += 8;
            conceitos["24"] += 7;
        }
        else if (Convert.ToInt32(priorityText[3].text) == 3) { resultText.text += "\nLuz: Perdeu 15 reais!"; PlayerManager.pm.RemoveCoins(15); totalCoins -= 15; }
        else { resultText.text += "\nLuz: Perdeu 30 reais!"; PlayerManager.pm.RemoveCoins(30); totalCoins -= 30; }

        if (Convert.ToInt32(priorityText[4].text) < 4) 
        {
            conceitos["25"] += 4;
            conceitos["1"] += 4;
            conceitos["10"] += 5;
            conceitos["23"] += 5;
            conceitos["12"] += 5;
            conceitos["20"] += 5;
            conceitos["31"] += 5;
            conceitos["7"] += 6;
            conceitos["24"] += 6;

            resultText.text += "\nLixo acumulado: Ganhou 60 reais!"; PlayerManager.pm.AddCoins(60); totalCoins += 60; 
        }
        else { resultText.text += "\nLixo acumulado: Perdeu 30 reais!"; PlayerManager.pm.RemoveCoins(30); totalCoins -= 30; }

        if (Convert.ToInt32(priorityText[1].text) != 5) 
        {
            conceitos["25"] += 3;
            conceitos["1"] += 2;
            conceitos["10"] += 3;
            conceitos["23"] += 4;
            conceitos["12"] += 3;
            conceitos["20"] += 4;
            conceitos["31"] += 3;
            conceitos["7"] += 5;
            conceitos["24"] += 4;

            resultText.text += "\nLouça para lavar: Ganhou 50 reais!"; PlayerManager.pm.AddCoins(50); totalCoins += 50;
        }
        else { resultText.text += "\nLouça para lavar: Perdeu 20 reais!"; PlayerManager.pm.RemoveCoins(20); totalCoins -= 20; }

        resultText.text += "\nCama para ser arrumada: Ganhou 15 reais!"; PlayerManager.pm.AddCoins(15); totalCoins += 15;

        conceitos["25"] += 2;
        conceitos["1"] += 1;
        conceitos["10"] += 2;
        conceitos["23"] += 3;
        conceitos["12"] += 2;
        conceitos["20"] += 3;
        conceitos["31"] += 2;
        conceitos["7"] += 4;
        conceitos["24"] += 2;

        resultText.text += "\n\nAté a próxima!";

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = totalCoins.ToString();


        APIManager.am.Relatorio(conceitos);
        PlayerManager.pm.AddLevel();
    }
    
    private void SetButtons(bool value)
    {
        foreach (Button b in button) b.enabled = value;
    }
}