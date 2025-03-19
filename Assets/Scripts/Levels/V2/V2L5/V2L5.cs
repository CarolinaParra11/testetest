using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V2L5 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject giftPanel;
    public Button giftButton;
    public GameObject blue;

    public GameObject hintButton;

    private int totalCoins = 35;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button button1, button2, button3;
    public GameObject option1, option2, option3;
    public AdultWalker tween1, tween2, tween3;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        hintButton.SetActive(false);
        totalCoinsText.text = totalCoins.ToString();

        AudioManager.am.PlaySFX(AudioManager.am.end);
        FlavorManager.fm.BigFirework();
        FlavorManager.fm.ShowHidePanel(giftPanel, true);
        blue.SetActive(true);

        giftButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            Part1Start();
            giftButton.onClick.RemoveAllListeners();
        });
    }

    private void Part1Start()
    {
          hintButton.SetActive(true);
        AudioManager.am.PlayVoice(AudioManager.am.v2start[4]);
        FlavorManager.fm.ShowHidePanel(giftPanel, false);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Dia de ajudar o gerente da empresa de reciclagem. Escolha a melhor opção, pois os funcionários não estão respeitando as regras e estão jogando os lixos nos locais errados. Eles estão jogando em cestos de outras cores. A empresa é uma fábrica de janelas, e o vidro jogado fora deve ser colocado nos cestos verdes.";
        infoButton.onClick.AddListener(delegate { Part1(); });
    }

    private void Part1()
    {
        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        button1.onClick.AddListener(delegate{ Part1Choice(25, option1, tween1, 20); });
        button2.onClick.AddListener(delegate{ Part1Choice(20, option2, tween2, 0); });
        button3.onClick.AddListener(delegate{ Part1Choice(30, option3, tween3, 15); });
    }

    private void Part1Choice(int price, GameObject option, AdultWalker cutscene, int bonus)
    {
        if(option.name == "option1")
        {
            conceitos.Add("10", 3);
            conceitos.Add("13", 4);
            conceitos.Add("23", 4);
            conceitos.Add("20", 4);
            conceitos.Add("19", 4);
            conceitos.Add("43", 3);
            conceitos.Add("44", 3);
            conceitos.Add("45", 3);
        }
        else if (option.name == "option2")
        {
            conceitos.Add("10", 1);
            conceitos.Add("23", 2);
            conceitos.Add("19", 2);
            conceitos.Add("43", 1);
            conceitos.Add("44", 1);
            conceitos.Add("45", 2);
        }
        else if (option.name == "option3")
        {
            conceitos.Add("10", 2);
            conceitos.Add("13", 2);
            conceitos.Add("23", 3);
            conceitos.Add("20", 2);
            conceitos.Add("19", 2);
            conceitos.Add("43", 2);
            conceitos.Add("44", 2);
            conceitos.Add("45", 2);
        }

        StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));

        totalCoins -= price;
        totalCoinsText.text = totalCoins.ToString();

        option.SetActive(true);

        StartCoroutine(cutscene.FollowPath());

        StartCoroutine(End(bonus));
    }

    private IEnumerator End(int bonus)
    {
        hintButton.SetActive(false);
        FlavorManager.fm.ShowHidePanel(optionPanel, false);

        yield return new WaitForSeconds(7.5f);

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        if (bonus > 0) resultText.text = "Ganhou " + bonus + " reais de bônus pela escolha!";
        else resultText.text = "";
        resultText.text += "\n\nSobraram " + totalCoins + " reais! Até a próxima!";

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = (totalCoins + bonus).ToString();

        StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        PlayerManager.pm.AddCoins(totalCoins + bonus);

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }
}