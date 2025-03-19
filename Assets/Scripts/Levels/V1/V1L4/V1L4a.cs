using TMPro;
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

public class V1L4a : MonoBehaviour
{
    private V1L4Helper v1l4Helper;

    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button button1, button2;
    public GameObject price1, price2;
    public GameObject selected1, selected2;
    public TextMeshProUGUI price1Text, price2Text;
    public Button buyButton;
    private int buyCount = 0;
    private int maxBuy = 1;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        v1l4Helper = GameObject.Find("V1L4Helper").GetComponent<V1L4Helper>();
        totalCoinsText.text = v1l4Helper.coins.ToString();

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        if (v1l4Helper.rightChoice)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l4[1]);
            infoText.text = "Escolha um alimento para comprar.";
        }
        else
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l4[2]);
            infoText.text = "Que pena, os produtos estão mais caros agora. Escolha o alimento que quer comprar.";
        }
        infoButton.onClick.AddListener(delegate 
        {
            Part1();
            AudioManager.am.voiceChannel.Stop();
        }); 
    }

    private void Part1()
    {
        int price;
        if (v1l4Helper.rightChoice)
        {
            price = 2;
            button1.onClick.AddListener(delegate { Part1Choice(this, price, price1, selected1, "Legal, você comprou seu alimento. Vamos para a loja de brinquedos!"); });
            button2.onClick.AddListener(delegate { Part1Choice(this, price, price2, selected2, "Legal, você comprou seu alimento. Vamos para a loja de brinquedos!"); });
        }
        else
        {
            price = 3;
            button1.onClick.AddListener(delegate { Part1Choice(this, price, price1, selected1, "Alimento comprado. Veja o quanto sobrou para o Bolodix!"); });
            button2.onClick.AddListener(delegate { Part1Choice(this, price, price2, selected2, "Alimento comprado. Veja o quanto sobrou para o Bolodix!"); });
        }
        price1Text.text = price.ToString();
        price2Text.text = price.ToString();

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);
    }

    static void Part1Choice(V1L4a instance, int price, GameObject priceObject, GameObject selectedObject, string text)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        if (instance.buyCount < instance.maxBuy)
        {
            instance.v1l4Helper.coins -= price;
            instance.totalCoinsText.text = instance.v1l4Helper.coins.ToString();

            priceObject.SetActive(false);
            selectedObject.SetActive(true);

            instance.buyButton.gameObject.SetActive(true);
            instance.buyCount++;
        }
        else if (selectedObject.activeSelf)
        {
            instance.v1l4Helper.coins += price;
            instance.totalCoinsText.text = instance.v1l4Helper.coins.ToString();

            priceObject.SetActive(true);
            selectedObject.SetActive(false);

            instance.buyButton.gameObject.SetActive(false);
            instance.buyCount--;
        }

        instance.buyButton.onClick.RemoveAllListeners();
        instance.buyButton.onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            instance.End(text); 
            instance.buyButton.onClick.RemoveAllListeners();
        });
    }

    private void End(string text)
    {
        if (v1l4Helper.rightChoice)
        {
            conceitos.Add("23", 0.5);
            conceitos.Add("20", 0.5);
            conceitos.Add("16", 0.5);
            conceitos.Add("6", 0.5);
            conceitos.Add("35", 0.5);
            conceitos.Add("24", 0.5);
            conceitos.Add("9", 0.5);

            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoText.text = text;
            infoButton.onClick.AddListener(delegate 
            {
                AudioManager.am.voiceChannel.Stop();
                AudioManager.am.PlaySFX(AudioManager.am.button);
                GameManager.gm.LoadScene("V1L4b");
                infoButton.onClick.RemoveAllListeners();
            });

            APIManager.am.Relatorio(conceitos);
            AudioManager.am.PlayVoice(AudioManager.am.v1l4[3]);
            StartCoroutine(FlavorManager.fm.SpawnCoinPosition(2, coinSpawn, coinWaypoint));
        }
        else
        {
            conceitos.Add("23", 0);
            conceitos.Add("20", 0);
            conceitos.Add("16", 0);
            conceitos.Add("6", 0.5);
            conceitos.Add("35", 0);
            conceitos.Add("24", 0);
            conceitos.Add("9", 0);

            FlavorManager.fm.ShowHidePanel(resultPanel, true);
            resultText.text = text;
            AudioManager.am.PlayVoice(AudioManager.am.v1l4[4]);

            StartCoroutine(FlavorManager.fm.SpawnCoinPosition(3, coinSpawn, coinWaypoint));
            StartCoroutine(FlavorManager.fm.SpawnCoin(1));
            PlayerManager.pm.AddCoins(1);
            APIManager.am.Relatorio(conceitos);

            PlayerManager.pm.AddLevel();

            int counter = 0;

            while (counter < 1)
            {
                coinsPanel.GetChild(counter).gameObject.SetActive(true);
                counter++;
            }
        }
    }
}