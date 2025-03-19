using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L14 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject optionPanel;
    public Image image1, image2, image3;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("1", 0);
        conceitos.Add("2", 0);
        conceitos.Add("4", 0);
        conceitos.Add("8", 0);
        conceitos.Add("10", 0);
        conceitos.Add("13", 0);
        conceitos.Add("17", 0);

        conceitos["1"] += 0;
        conceitos["2"] += 0;
        conceitos["4"] += 0;
        conceitos["8"] += 0;
        conceitos["10"] += 0;
        conceitos["13"] += 0;
        conceitos["17"] += 0;

        AudioManager.am.PlayVoice(AudioManager.am.v2start[11]);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        //infoText.text = "Escolha o melhor local para empreender e colocar seu carrinho de coco!";
        infoText.text = "Escolha o melhor local para o carrinho de coco.";
        infoButton.onClick.AddListener(delegate { Part1(); AudioManager.am.voiceChannel.Stop(); });
    }

    private void Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);
    }

    public void Part1Choice(int id)
    {
        AudioManager.am.PlaySFX(AudioManager.am.drop);

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        FlavorManager.fm.ShowHidePanel(incomePanel, true);

        if (id == 1)
        {
            conceitos["1"] += 2;
            conceitos["2"] += 2;
            conceitos["4"] += 0;
            conceitos["8"] += 2;
            conceitos["10"] += 0;
            conceitos["13"] += 2;

            image1.enabled = true;
            AudioManager.am.PlayVoice(AudioManager.am.v1l14[2]);
            resultText.text = "Você conseguiu vender para duas pessoas e ganhou 2 moedas." +
                " Tinha duas pessoas perto tomando sorvete, mas não quiseram comprar água de coco.";
            StartCoroutine(FlavorManager.fm.SpawnCoin(2));
            PlayerManager.pm.AddCoins(2);

            coinsPanel.GetChild(0).gameObject.SetActive(true);
            coinsPanel.GetChild(1).gameObject.SetActive(true);

        }
        else if (id == 2)
        {
            conceitos["1"] += 1;
            conceitos["2"] += 2;
            conceitos["4"] += 1;
            conceitos["8"] += 2;
            conceitos["10"] += 1;
            conceitos["13"] += 2;

            image2.enabled = true;
            AudioManager.am.PlayVoice(AudioManager.am.v1l14[0]);
            resultText.text = "Você vendeu para duas pessoas e assim ganhou 2 moedas.";
            StartCoroutine(FlavorManager.fm.SpawnCoin(2));
            PlayerManager.pm.AddCoins(2);

            coinsPanel.GetChild(0).gameObject.SetActive(true);
            coinsPanel.GetChild(1).gameObject.SetActive(true);
        }
        else if (id == 3)
        {
            conceitos["1"] += 3;
            conceitos["2"] += 3;
            conceitos["4"] += 2;
            conceitos["8"] += 3;
            conceitos["10"] += 2;
            conceitos["13"] += 3;

            image3.enabled = true;
            AudioManager.am.PlayVoice(AudioManager.am.v1l14[1]);
            resultText.text = "Parabéns! Você fez a melhor opção." +
                " Escolheu o melhor local e vendeu para quatro pessoas." +
                " Você ganhou 4 moedas para o Bolodix.";
            StartCoroutine(FlavorManager.fm.SpawnCoin(4));
            PlayerManager.pm.AddCoins(4);

            coinsPanel.GetChild(0).gameObject.SetActive(true);
            coinsPanel.GetChild(1).gameObject.SetActive(true);
            coinsPanel.GetChild(2).gameObject.SetActive(true);
            coinsPanel.GetChild(3).gameObject.SetActive(true);
        }

        PlayerManager.pm.AddLevel();

        APIManager.am.Relatorio(conceitos);
    }
}