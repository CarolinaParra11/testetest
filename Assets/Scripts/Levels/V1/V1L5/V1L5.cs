using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L5 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject optionPanel1, optionPanel2;
    public Button[] button;
    public GameObject[] priority;
    public TextMeshProUGUI[] priorityText;
    public Button submitButton;
    private int currentPriority = 0;
    private int correctDrops = 0;
    private int currentDrops = 0;
    private int coins;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("24", 0);
        conceitos.Add("18", 0);
        conceitos.Add("10", 0);

        conceitos["24"] += 0;
        conceitos["18"] += 0;
        conceitos["10"] += 0;

        AudioManager.am.PlayVoice(AudioManager.am.v1start[4]);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Primeira missão: Toque nos objetos por ordem de importância, ou prioridade.";
        infoButton.onClick.AddListener(delegate
        {
            Part1();
            AudioManager.am.voiceChannel.Stop();
        });
    }

    private void Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel1, true);
        Part1Assign();
    }

    private void Part1Assign()
    {
        button[0].onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            Part1Choice(priority[0], priorityText[0]); 
        });
        button[1].onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            Part1Choice(priority[1], priorityText[1]); 
        });
        button[2].onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            Part1Choice(priority[2], priorityText[2]); 
        });
        submitButton.onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            Part1End();
            submitButton.onClick.RemoveAllListeners();
        });
    }

    private void Part1Choice(GameObject obj, TextMeshProUGUI text)
    {
        if (!obj.activeSelf)
        {
            obj.SetActive(true);
            currentPriority++;
            text.text = currentPriority.ToString();
        }
        else
        {
            obj.SetActive(false);
            currentPriority--;

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

        if (currentPriority == 3) submitButton.gameObject.SetActive(true);
        else submitButton.gameObject.SetActive(false);
    }
    
    private void Part1End()
    {
        button[0].onClick.RemoveAllListeners();
        button[1].onClick.RemoveAllListeners();
        button[2].onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        if (priorityText[0].text == "1" && priorityText[1].text == "2" && priorityText[2].text == "3")
        {
            conceitos["24"] += 2;
            conceitos["18"] += 2;

            coins += 4;
            StartCoroutine(FlavorManager.fm.SpawnCoin(coins));
            AudioManager.am.PlayVoice(AudioManager.am.v1l5[1]);
            infoText.text = "Parabéns! Você ganhou 4 moedas pelo acerto.";
        }
        else
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l5[0]);
            infoText.text = "Não foi desta vez... Vamos para a próxima missão!!!";
        }
        
        infoButton.onClick.AddListener(delegate 
        {
            AudioManager.am.voiceChannel.Stop();
            AudioManager.am.PlayVoice(AudioManager.am.v1l5[2]);
            AudioManager.am.PlaySFX(AudioManager.am.button);
            infoText.text = "Segunda missão: Agora arraste cada objeto para o local correto.";
            
            infoButton.onClick.AddListener(delegate
            {
                Part2();
                AudioManager.am.voiceChannel.Stop();
                infoButton.onClick.RemoveAllListeners();
            });
        });
    }

    private void Part2()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel2, true);
    }

    public void Part2Choice(bool correct)
    {
        AudioManager.am.PlaySFX(AudioManager.am.drop);
        currentDrops++;
        if (correct) correctDrops++;
        if (currentDrops == 3) End();
    }

    private void End()
    {
        coins += correctDrops * 2;

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Parabéns! " + "Você ganhou " + correctDrops * 2 + " moedas!";

        if (correctDrops == 1)
        {
            conceitos["10"] += 1;
            AudioManager.am.PlayVoice(AudioManager.am.v1l5[3]);
        }
        else if (correctDrops == 2)
        {
            conceitos["18"] += 1;
            conceitos["10"] += 1;
            AudioManager.am.PlayVoice(AudioManager.am.v1l5[4]);
        }
        else if (correctDrops == 3)
        {
            conceitos["18"] += 2;
            conceitos["10"] += 2;
            AudioManager.am.PlayVoice(AudioManager.am.v1l5[5]);
        }
        else if (correctDrops == 0)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l5[6]);
            resultText.text = "Os objetos não foram arrastados para os lugares certos...";
        }

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

        StartCoroutine(FlavorManager.fm.SpawnCoin(coins));
        PlayerManager.pm.AddCoins(coins);
        APIManager.am.Relatorio(conceitos);
        PlayerManager.pm.AddLevel();
    }
}