using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L19 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 5;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject cutscene, gameplay;
   // public Button cutsceneEnd;
    public Button cutEnd;

    public Button submitButton;
    public Button[] button;
    public GameObject[] price;
    public GameObject[] selected;
    private int coins;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("1", 0);
        conceitos.Add("7", 0);
        conceitos.Add("10", 0);
        conceitos.Add("23", 0);
        conceitos.Add("2", 0);
        conceitos.Add("20", 0);
        conceitos.Add("24", 0);
        conceitos.Add("31", 0);

        conceitos["1"] += 0;
        conceitos["7"] += 0;
        conceitos["10"] += 0;
        conceitos["23"] += 0;
        conceitos["2"] += 0;
        conceitos["20"] += 0;
        conceitos["24"] += 0;
        conceitos["31"] += 0;

        AudioManager.am.PlayVoice(AudioManager.am.v1start[18]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você foi conversar com seu amigo Léo para saber sobre o pet dele.";
        infoButton.onClick.AddListener(delegate { Part1(); });
    }

    private void Part1()
    {
        AudioManager.am.PlayVoice(AudioManager.am.v1l19[0]);
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        cutscene.SetActive(true);

        cutEnd.gameObject.SetActive(true);
        cutEnd.onClick.AddListener(delegate{Part2Start(); cutEnd.onClick.RemoveAllListeners(); AudioManager.am.PlaySFX(AudioManager.am.button); cutEnd.gameObject.SetActive(false);});

    }

    private void Part2Start()
    {
        cutscene.SetActive(false);
        SetButtons(false);
        totalCoinsText.text = totalCoins.ToString();
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1l19[1]);
        infoText.text = "Quais produtos você quer? Você tem apenas 5 moedas para usar.";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate 
        { 
            Part2(); 
            AudioManager.am.PlaySFX(AudioManager.am.button);
            AudioManager.am.voiceChannel.Stop();
        });
    }

    private void Part2()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        gameplay.SetActive(true);
        SetButtons(true);
        button[0].onClick.AddListener(delegate { PanelButton(this, 2, price[0], selected[0]); });
        button[1].onClick.AddListener(delegate { PanelButton(this, 1, price[1], selected[1]); });
        button[2].onClick.AddListener(delegate { PanelButton(this, 1, price[2], selected[2]); });
        button[3].onClick.AddListener(delegate { PanelButton(this, 1, price[3], selected[3]); });
        button[4].onClick.AddListener(delegate { PanelButton(this, 2, price[4], selected[4]); });
        button[5].onClick.AddListener(delegate { PanelButton(this, 2, price[5], selected[5]); });
        button[6].onClick.AddListener(delegate { PanelButton(this, 2, price[6], selected[6]); });
        submitButton.onClick.AddListener(delegate { End(); });
    }

    private void End()
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        if (selected[0].activeSelf) { resultText.text += "Ração: Ganhou 2 moedas!\n"; coins += 2; }
        if (selected[1].activeSelf) { resultText.text += "Lenço: Ganhou 0 moeda!\n"; }
        if (selected[2].activeSelf) { resultText.text += "Prato: Ganhou 2 moedas!\n"; coins += 2; }
        if (selected[3].activeSelf) { resultText.text += "Roupa: Ganhou 0 moeda!\n"; }
        if (selected[4].activeSelf) { resultText.text += "Escova: Ganhou 1 moeda!\n"; coins += 1; }
        if (selected[5].activeSelf) { resultText.text += "Coleira: Ganhou 2 moedas!\n"; coins += 2; }
        if (selected[6].activeSelf) { resultText.text += "Casa: Ganhou 2 moedas!\n"; coins += 2; }
        resultText.text += "\nTudo feito por hoje! Até a proxima!";

        if(coins == 1)
        {
            conceitos["1"] += 1;
            conceitos["7"] += 1;
            conceitos["10"] += 1;
            conceitos["23"] += 0;
            conceitos["2"] += 1;
            conceitos["20"] += 1;
            conceitos["24"] += 1;
            conceitos["31"] += 1;
        }
        else if (coins == 2)
        {
            conceitos["1"] += 2;
            conceitos["7"] += 2;
            conceitos["10"] += 2;
            conceitos["23"] += 0;
            conceitos["2"] += 1;
            conceitos["20"] += 2;
            conceitos["24"] += 2;
            conceitos["31"] += 2;
        }
        else if (coins == 3)
        {
            conceitos["1"] += 3;
            conceitos["7"] += 3;
            conceitos["10"] += 3;
            conceitos["23"] += 0;
            conceitos["2"] += 2;
            conceitos["20"] += 2;
            conceitos["24"] += 2;
            conceitos["31"] += 3;
        }
        else if (coins == 4)
        {
            conceitos["1"] += 4;
            conceitos["7"] += 4;
            conceitos["10"] += 4;
            conceitos["23"] += 1;
            conceitos["2"] += 3;
            conceitos["20"] += 3;
            conceitos["24"] += 3;
            conceitos["31"] += 4;
        }
        else if (coins == 5)
        {
            conceitos["1"] += 5;
            conceitos["7"] += 5;
            conceitos["10"] += 5;
            conceitos["23"] += 1;
            conceitos["2"] += 4;
            conceitos["20"] += 4;
            conceitos["24"] += 4;
            conceitos["31"] += 5;
        }
        else if (coins == 6)
        {
            conceitos["1"] += 6;
            conceitos["7"] += 6;
            conceitos["10"] += 6;
            conceitos["23"] += 1;
            conceitos["2"] += 5;
            conceitos["20"] += 5;
            conceitos["24"] += 5;
            conceitos["31"] += 6;
        }

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddCoins(coins);
        StartCoroutine(FlavorManager.fm.SpawnCoin(coins));
        PlayerManager.pm.AddLevel();
        SetButtons(false);

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

    static void PanelButton(V1L19 instance, int price, GameObject priceObject, GameObject selectedObject)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        if (priceObject.activeSelf)
        {
            if (instance.totalCoins >= price)
            {
                instance.totalCoins -= price;
                instance.totalCoinsText.text = instance.totalCoins.ToString();
                priceObject.SetActive(false);
                selectedObject.SetActive(true);
            }
            else
            {
                FlavorManager.fm.ShowHidePanel(instance.infoPanel, true);
                instance.infoText.text = "Você não tem dinheiro suficiente, escolha outro ou remova algum já escolhido!";
                instance.infoButton.onClick.RemoveAllListeners();
                instance.infoButton.onClick.AddListener(delegate { AudioManager.am.PlaySFX(AudioManager.am.button); FlavorManager.fm.ShowHidePanel(instance.infoPanel, false); });
            }
        }
        else
        {
            instance.totalCoins += price;
            instance.totalCoinsText.text = instance.totalCoins.ToString();
            priceObject.SetActive(true);
            selectedObject.SetActive(false);
        }

        if (instance.totalCoins == 0) instance.submitButton.gameObject.SetActive(true);
        else instance.submitButton.gameObject.SetActive(false);
    }

    private void SetButtons(bool value)
    {
        foreach (Button b in button) b.enabled = value;
    }
}