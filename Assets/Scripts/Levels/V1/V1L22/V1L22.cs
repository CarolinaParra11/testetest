using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V1L22 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject optionPanel;
    private int itemCounter;
    private int bonus;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("25", 0);
        conceitos.Add("31", 0);
        conceitos.Add("7", 0);
        conceitos.Add("20", 0);
        conceitos.Add("2", 0);

        conceitos["25"] += 0;
        conceitos["31"] += 0;
        conceitos["7"] += 0;
        conceitos["20"] += 0;
        conceitos["2"] += 0;

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[21]);
        infoText.text = "Vamos à praia! Escolha os itens que são essenciais para ir à praia." +
            " Essenciais são os itens que não podem faltar. Cada objeto certo ganha uma moeda!";
        infoButton.onClick.AddListener(delegate
        {
            Part1();
            AudioManager.am.voiceChannel.Stop();
        });
    }

    private void Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);
        infoText.text = "";
    }

    public void AddItem(string essential, bool right, string title)
    {
        AudioManager.am.PlaySFX(AudioManager.am.drop);
        if (right)
        {
            infoText.text += title + ": 1 moeda! (" + essential + ")\n";
            bonus ++;
        }
        else infoText.text += title + ": 0 moedas. (" + essential + ")\n";

        itemCounter++;

        if (itemCounter == 8) Part1End();
    }

    private void Part1End()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            End();
            infoButton.onClick.RemoveAllListeners();
        });
    }

    private void End()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Tudo feito por hoje! Ganhou " + bonus + " moedas! Até a próxima!";

        if (bonus > 0)
        {
            if (bonus > 1)
            {
                conceitos["25"] += 1;
                conceitos["31"] += 1;
                conceitos["7"] += 1;
                conceitos["20"] += 1;
                conceitos["2"] += 2;
            }
            else if (bonus > 2)
            {
                conceitos["25"] += 2;
                conceitos["31"] += 2;
                conceitos["7"] += 2;
                conceitos["20"] += 2;
                conceitos["2"] += 2;
            }
            else if (bonus > 3)
            {
                conceitos["25"] += 3;
                conceitos["31"] += 3;
                conceitos["7"] += 3;
                conceitos["20"] += 3;
                conceitos["2"] += 3;
            }
            else if (bonus > 4)
            {
                conceitos["25"] += 4;
                conceitos["31"] += 4;
                conceitos["7"] += 4;
                conceitos["20"] += 4;
                conceitos["2"] += 3;
            }
            else if (bonus > 5)
            {
                conceitos["25"] += 5;
                conceitos["31"] += 5;
                conceitos["7"] += 5;
                conceitos["20"] += 5;
                conceitos["2"] += 4;
            }
            else if (bonus > 6)
            {
                conceitos["25"] += 6;
                conceitos["31"] += 6;
                conceitos["7"] += 6;
                conceitos["20"] += 6;
                conceitos["2"] += 7;
            }
            else if (bonus > 7)
            {
                conceitos["25"] += 7;
                conceitos["31"] += 7;
                conceitos["7"] += 7;
                conceitos["20"] += 7;
                conceitos["2"] += 5;
            }

            FlavorManager.fm.ShowHidePanel(incomePanel, true);

            int counter = 0;

            while (counter < bonus)
            {
                coinsPanel.GetChild(counter).gameObject.SetActive(true);
                counter++;
            }
        }

        APIManager.am.Relatorio(conceitos);

        StartCoroutine(FlavorManager.fm.SpawnCoin(bonus));
        PlayerManager.pm.AddCoins(bonus);
        PlayerManager.pm.AddLevel();
    }
}
