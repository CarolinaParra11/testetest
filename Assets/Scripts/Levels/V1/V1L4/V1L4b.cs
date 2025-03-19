using TMPro;
using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

public class V1L4b : MonoBehaviour
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
            AudioManager.am.PlayVoice(AudioManager.am.v1l4[6]);
            infoText.text = "Que ótimo! Os produtos estão em promoção. Aproveite para comprar um brinquedo.";
        }
        else
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l4[5]);
            infoText.text = "Escolha um brinquedo para comprar.";
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
            price = 1;
            button1.onClick.AddListener(delegate { Part1Choice(this, price, price1, selected1, "Parabéns, você completou a missão! Veja quantas moedas sobraram para o Bolodix!"); });
            button2.onClick.AddListener(delegate { Part1Choice(this, price, price2, selected2, "Parabéns, você completou a missão! Veja quantas moedas sobraram para o Bolodix!"); });
        }
        else
        {
            price = 2;
            button1.onClick.AddListener(delegate { Part1Choice(this, price, price1, selected1, "Pronto, você comprou seu brinquedo. Agora vamos para o mercado."); });
            button2.onClick.AddListener(delegate { Part1Choice(this, price, price2, selected2, "Pronto, você comprou seu brinquedo. Agora vamos para o mercado."); });
        }
        price1Text.text = price.ToString();
        price2Text.text = price.ToString();

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);
    }

    static void Part1Choice(V1L4b instance, int price, GameObject priceObject, GameObject selectedObject, string text)
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

            FlavorManager.fm.ShowHidePanel(resultPanel, true);
            resultText.text = text;
            AudioManager.am.PlayVoice(AudioManager.am.v1l4[8]);

            StartCoroutine(FlavorManager.fm.SpawnCoinPosition(1, coinSpawn, coinWaypoint));
            StartCoroutine(FlavorManager.fm.SpawnCoin(3));
            PlayerManager.pm.AddCoins(3);
            APIManager.am.Relatorio(conceitos);

            PlayerManager.pm.AddLevel();

            FlavorManager.fm.ShowHidePanel(incomePanel, true);

            int counter = 0;

            while (counter < 3)
            {
                coinsPanel.GetChild(counter).gameObject.SetActive(true);
                counter++;
            }
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

            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoText.text = text;
            infoButton.onClick.AddListener(delegate 
            {
                AudioManager.am.voiceChannel.Stop();
                AudioManager.am.PlaySFX(AudioManager.am.button);
                GameManager.gm.LoadScene("V1L4a");
                infoButton.onClick.RemoveAllListeners();
            });

            APIManager.am.Relatorio(conceitos);
            AudioManager.am.PlayVoice(AudioManager.am.v1l4[7]);
            StartCoroutine(FlavorManager.fm.SpawnCoinPosition(2, coinSpawn, coinWaypoint));
        }
    }
}