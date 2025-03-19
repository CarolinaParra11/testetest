using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L9Buttons : MonoBehaviour
{
    private V1L9Helper v1l9Helper;

    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins;
    private int value1, value2, value3;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button button1, button2, button3;
    public GameObject price1, price2, price3;
    public GameObject selected1, selected2, selected3;
    public Button submitButton;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        conceitos.Add("23", 0);
        conceitos.Add("34", 0);
        conceitos.Add("2", 0);
        conceitos.Add("20", 0);
        conceitos.Add("1", 0);
        conceitos.Add("7", 0);
        conceitos.Add("25", 0);
        conceitos.Add("9", 0);
        conceitos.Add("5", 0);

        conceitos["23"] += 0;
        conceitos["34"] += 0;
        conceitos["2"] += 0;
        conceitos["20"] += 0;
        conceitos["1"] += 0;
        conceitos["7"] += 0;
        conceitos["25"] += 0;
        conceitos["9"] += 0;
        conceitos["5"] += 0;

        v1l9Helper = GameObject.Find("V1L9Helper").GetComponent<V1L9Helper>();
        totalCoins = v1l9Helper.coins;
        totalCoinsText.text = v1l9Helper.coins.ToString();

        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        if (v1l9Helper.level == 1)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l9[0]);
            infoText.text = "Você chegou à doceria. Escolha um produto.";
        }
        if (v1l9Helper.level == 2)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l9[1]);
            infoText.text = "Você chegou ao supermercado. Escolha um produto para comprar.";
        }
        if (v1l9Helper.level == 3)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l9[2]);
            infoText.text = "Você chegou à padaria. Escolha um produto para comprar.";
        }
        
        infoButton.onClick.AddListener(delegate 
        { 
            Part1();
            AudioManager.am.voiceChannel.Stop();
        });
    }

    private void Part1()
    {
        if (v1l9Helper.level == 1) { value1 = 2; value2 = 2; value3 = 3; }
        if (v1l9Helper.level == 2) { value1 = 3; value2 = 2; value3 = 2; }
        if (v1l9Helper.level == 3) { value1 = 3; value2 = 1; value3 = 3; }

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        button1.onClick.AddListener(delegate { PanelButtonOneOption(this, value1, price1, selected1, selected2, selected3, submitButton.gameObject); });
        button2.onClick.AddListener(delegate { PanelButtonOneOption(this, value2, price2, selected2, selected1, selected3, submitButton.gameObject); });
        button3.onClick.AddListener(delegate { PanelButtonOneOption(this, value3, price3, selected3, selected1, selected2, submitButton.gameObject); });
    }

    private void End(int price)
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        v1l9Helper.level++;
        if (selected1.activeSelf) v1l9Helper.coins -= value1;
        if (selected2.activeSelf) v1l9Helper.coins -= value2;
        if (selected3.activeSelf) v1l9Helper.coins -= value3;

        if (v1l9Helper.level < 4)
        {
            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoText.text = "Vamos para as próximas compras!";
            infoButton.onClick.AddListener(delegate { FlavorManager.fm.ShowHidePanel(infoPanel, false); GameManager.gm.LoadScene("V1L9"); });
        }
        else
        {
            FlavorManager.fm.ShowHidePanel(resultPanel, true);

            if (totalCoins > 0)
            {
                if(totalCoins == 2)
                {
                    conceitos["23"] += 2;
                    conceitos["34"] += 2;
                    conceitos["2"] += 2;
                    conceitos["20"] += 3;
                    conceitos["1"] += 2;
                    conceitos["7"] += 3;
                    conceitos["25"] += 1;
                    conceitos["9"] += 1;
                    conceitos["5"] += 2;
                }
                else if (totalCoins == 1)
                {
                    conceitos["23"] += 1;
                    conceitos["34"] += 1;
                    conceitos["2"] += 1;
                    conceitos["20"] += 2;
                    conceitos["1"] += 1;
                    conceitos["7"] += 2;
                    conceitos["25"] += 1;
                    conceitos["9"] += 1;
                    conceitos["5"] += 1;
                }

                FlavorManager.fm.ShowHidePanel(incomePanel, true);

                int counter = 0;

                while (counter < totalCoins)
                {
                    coinsPanel.GetChild(counter).gameObject.SetActive(true);
                    counter++;
                }
            }
            else
            {
                if (totalCoins == 0)
                {
                    conceitos["23"] += 0;
                    conceitos["34"] += 0;
                    conceitos["2"] += 0;
                    conceitos["20"] += 1;
                    conceitos["1"] += 0;
                    conceitos["7"] += 1;
                    conceitos["25"] += 1;
                    conceitos["9"] += 1;
                    conceitos["5"] += 0;
                }
            }

            AudioManager.am.PlayVoice(AudioManager.am.v1l9[3]);
            resultText.text = "Parabéns, você passou por todas as lojas. Veja se fez boas compras e quanto sobrou para o Bolodix.";

            PlayerManager.pm.AddCoins(totalCoins);
            StartCoroutine(FlavorManager.fm.SpawnCoin(totalCoins));
            APIManager.am.Relatorio(conceitos);

            PlayerManager.pm.AddLevel();
        }

        StartCoroutine(FlavorManager.fm.SpawnCoinPosition(price, coinSpawn, coinWaypoint));
    }

    private static void PanelButtonOneOption(V1L9Buttons instance, int price, GameObject priceObject, GameObject selectedObject, GameObject otherSelectedObject1, GameObject otherSelectedObject2, GameObject pSubmit)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        if (!otherSelectedObject1.activeSelf && !otherSelectedObject2.activeSelf)
        {
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
                    instance.infoText.text = "Você não tem dinheiro suficiente, escolha outro!";
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
        }

        if (selectedObject.activeSelf || otherSelectedObject1.activeSelf || otherSelectedObject2.activeSelf) pSubmit.SetActive(true);
        else pSubmit.SetActive(false);

        instance.submitButton.onClick.RemoveAllListeners();
        instance.submitButton.onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            instance.End(price);
            instance.submitButton.onClick.RemoveAllListeners();
        });
    }
}