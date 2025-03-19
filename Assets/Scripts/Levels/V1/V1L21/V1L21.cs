using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L21 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 6;
    public TextMeshProUGUI totalCoinsText;

    private int totalBar = 1;
    public TextMeshProUGUI totalBarText;

    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button p1Button1, p1Button2, p1Button3;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("20", 0);
        conceitos.Add("23", 0);
        conceitos.Add("31", 0);
        conceitos.Add("2", 0);
        conceitos.Add("1", 0);
        conceitos.Add("7", 0);
        conceitos.Add("14", 0);
        conceitos.Add("34", 0);
        conceitos.Add("27", 0);

        conceitos["20"] += 0;
        conceitos["23"] += 0;
        conceitos["31"] += 0;
        conceitos["2"] += 0;
        conceitos["1"] += 0;
        conceitos["7"] += 0;
        conceitos["14"] += 0;
        conceitos["34"] += 0;
        conceitos["27"] += 0;

        totalCoinsText.text = "1";
        totalBarText.text = "1";

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[20]);
        infoText.text = "Você precisa comprar dois pãezinhos e uma garrafa de leite para sua casa." +
            " Veja a melhor opção, já que tem 6 moedas disponíveis para usar.";
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

        p1Button1.onClick.AddListener(delegate { End(1); });
        p1Button2.onClick.AddListener(delegate { End(2); });
        p1Button3.onClick.AddListener(delegate { End(3); });
    }

    private void End(int choice)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);

        p1Button1.onClick.RemoveAllListeners();
        p1Button2.onClick.RemoveAllListeners();
        p1Button3.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        if (choice == 1)
        {
            conceitos["20"] += 1;
            conceitos["23"] += 1;
            conceitos["31"] += 1;
            conceitos["2"] += 3;
            conceitos["1"] += 2;
            conceitos["7"] += 3;
            conceitos["14"] += 2;
            conceitos["34"] += 1;
            conceitos["27"] += 1;

            AudioManager.am.PlayVoice(AudioManager.am.v1l21[0]);
            resultText.text = "Você gastou 5 moedas e comprou o necessário." +
                " Parabéns! Você conseguiu guardar 1 moeda para o seu Bolodix.";
            StartCoroutine(FlavorManager.fm.SpawnCoinPosition(5, coinSpawn, coinWaypoint));
            StartCoroutine(FlavorManager.fm.SpawnCoin(1));
            PlayerManager.pm.AddCoins(1);
            FlavorManager.fm.ShowHidePanel(incomePanel, true);
            coinsPanel.GetChild(0).gameObject.SetActive(true);
            totalCoinsText.text = "1";
            totalBarText.text = "0";
        }
        else if (choice == 2)
        {
            conceitos["20"] += 0;
            conceitos["23"] += 0;
            conceitos["31"] += 0;
            conceitos["2"] += 2;
            conceitos["1"] += 0;
            conceitos["7"] += 0;
            conceitos["14"] += 2;
            conceitos["34"] += 1;
            conceitos["27"] += 0;

            AudioManager.am.PlayVoice(AudioManager.am.v1l21[1]);
            resultText.text = "Você usou todas as suas moedas e conseguiu comprar o que precisava." +
                " Sobraram ainda dois pães. Da próxima vez, escolha a opção que guarde mais moedas.";
            StartCoroutine(FlavorManager.fm.SpawnCoinPosition(6, coinSpawn, coinWaypoint));
            totalCoinsText.text = "0";
            totalBarText.text = "0";
        }
        else
        {
            conceitos["20"] += 3;
            conceitos["23"] += 2;
            conceitos["31"] += 3;
            conceitos["2"] += 5;
            conceitos["1"] += 4;
            conceitos["7"] += 45;
            conceitos["14"] += 2;
            conceitos["34"] += 2;
            conceitos["27"] += 2;

            AudioManager.am.PlayVoice(AudioManager.am.v1l21[2]);
            resultText.text = "Parabéns! Você escolheu a melhor opção!" +
                " Mesmo com o leite custando mais moedas, você aproveitou a promoção dos dois pãezinhos " +
                " sem precisar usar as suas moedas. Continue guardando e poupando, seu Bolodix está crescendo!";
            StartCoroutine(FlavorManager.fm.SpawnCoinPosition(4, coinSpawn, coinWaypoint));
            StartCoroutine(FlavorManager.fm.SpawnCoin(2));
            PlayerManager.pm.AddCoins(2);
            FlavorManager.fm.ShowHidePanel(incomePanel, true);
            coinsPanel.GetChild(0).gameObject.SetActive(true);
            coinsPanel.GetChild(1).gameObject.SetActive(true);
            totalCoinsText.text = "3";
            totalBarText.text = "0";
        }

        APIManager.am.Relatorio(conceitos);
        PlayerManager.pm.AddLevel();
    }
}