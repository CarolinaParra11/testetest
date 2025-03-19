using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L23 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 9;
    private int bonusCoins = 0;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawnStart, coinSpawnEnd;

    public GameObject optionPanel;
    public TextMeshProUGUI totalPriceText;
    private int totalPrice;

    public Button submitButton;
    public Button[] button;
    public GameObject[] price;
    public GameObject[] selected;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    void Start()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[22]);
        infoText.text = "Hoje você precisa montar o seu material escolar!\n\n Escolha os objetos de que precisa," +
            " vendo também como usar melhor as moedas e olhando o que é fundamental! Você terá 9 moedas." +
            " Escolha com sabedoria, e boas compras.";
        infoButton.onClick.AddListener(delegate 
        { 
            StartCoroutine(PartAStart());
            AudioManager.am.voiceChannel.Stop();
        });

        totalCoinsText.text = totalCoins.ToString();
    }

    public IEnumerator PartAStart()
    {
        conceitos.Add("24", 0);
        conceitos.Add("22", 0);
        conceitos.Add("2", 0);
        conceitos.Add("1", 0);
        conceitos.Add("7", 0);
        conceitos.Add("34", 0);
        conceitos.Add("31", 0);

        conceitos["24"] += 0;
        conceitos["22"] += 0;
        conceitos["2"] += 0;
        conceitos["1"] += 0;
        conceitos["7"] += 0;
        conceitos["34"] += 0;
        conceitos["31"] += 0;

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(2);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        button[0].onClick.AddListener(delegate { PanelButton(this, 2, 0, price[0], selected[0]); });
        button[1].onClick.AddListener(delegate { PanelButton(this, 2, 0, price[1], selected[1]); });
        button[2].onClick.AddListener(delegate { PanelButton(this, 3, 1, price[2], selected[2]); });
        button[3].onClick.AddListener(delegate { PanelButton(this, 1, 1, price[3], selected[3]); });
        button[4].onClick.AddListener(delegate { PanelButton(this, 1, 1, price[4], selected[4]); });
        button[5].onClick.AddListener(delegate { PanelButton(this, 2, 1, price[5], selected[5]); });
        button[6].onClick.AddListener(delegate { PanelButton(this, 1, 1, price[6], selected[6]); });
        button[7].onClick.AddListener(delegate { PanelButton(this, 2, 1, price[7], selected[7]); });
        button[8].onClick.AddListener(delegate { PanelButton(this, 1, 1, price[8], selected[8]); });
        button[9].onClick.AddListener(delegate { PanelButton(this, 1, 0, price[9], selected[9]); });
        submitButton.onClick.AddListener(delegate { End(); });
    }

    public void End()
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);

        button[0].onClick.RemoveAllListeners();
        button[1].onClick.RemoveAllListeners();
        button[2].onClick.RemoveAllListeners();
        button[3].onClick.RemoveAllListeners();
        button[4].onClick.RemoveAllListeners();
        button[5].onClick.RemoveAllListeners();
        button[6].onClick.RemoveAllListeners();
        button[7].onClick.RemoveAllListeners();
        button[8].onClick.RemoveAllListeners();
        button[9].onClick.RemoveAllListeners();
        submitButton.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        if (selected[3].activeSelf && selected[4].activeSelf) bonusCoins--;

        if (totalCoins == 1) resultText.text = "Sobrou " + totalCoins + " moeda, e você conseguiu mais " + bonusCoins + " de bônus. Veja os detalhes na lista!\n";
        else resultText.text = "Sobraram " + totalCoins + " moedas, e você conseguiu mais " + bonusCoins + " de bônus. Veja os detalhes na lista!\n";

        bool rubber = false;

        for (int i = 0; i < selected.Length; i++)
        {
            if (selected[i].activeSelf)
            {
                if (i == 0)
                {
                    conceitos["34"] += 1;
                    resultText.text += "\n - Mochila Azul: 0 moedas!";
                }
                else if (i == 1)
                {
                    conceitos["34"] += 1;
                    resultText.text += "\n - Mochila Rosa: 0 moedas.";
                }
                else if (i == 2)
                {
                    conceitos["24"] += 4;
                    conceitos["22"] += 3;
                    conceitos["2"] += 5;
                    conceitos["1"] += 5;
                    conceitos["7"] += 3;
                    conceitos["34"] += 1;
                    conceitos["31"] += 1;

                    resultText.text += "\n - Mochila Branca: 1 moeda!";
                }
                else if (i == 3)
                {
                    conceitos["24"] += 4;
                    conceitos["22"] += 3;
                    conceitos["2"] += 5;
                    conceitos["1"] += 5;
                    conceitos["7"] += 3;
                    conceitos["34"] += 1;
                    conceitos["31"] += 1;

                    resultText.text += "\n - Borracha: 1 moeda!";
                    rubber = true;
                }
                else if (i == 4 && rubber)
                {
                    conceitos["34"] += 1;
                    resultText.text += "\n - Borracha colorida: 0 moedas.";
                }
                else if (i == 4 && !rubber)
                {
                    conceitos["24"] += 4;
                    conceitos["22"] += 3;
                    conceitos["2"] += 5;
                    conceitos["1"] += 5;
                    conceitos["7"] += 3;
                    conceitos["34"] += 1;
                    conceitos["31"] += 1;

                    resultText.text += "\n - Borracha colorida: 1 moeda!";
                }
                else if (i == 5)
                {
                    conceitos["24"] += 4;
                    conceitos["22"] += 3;
                    conceitos["2"] += 5;
                    conceitos["1"] += 5;
                    conceitos["7"] += 3;
                    conceitos["34"] += 1;
                    conceitos["31"] += 1;

                    resultText.text += "\n - Caderno: 1 moeda.";
                }
                else if (i == 6)
                {
                    conceitos["24"] += 4;
                    conceitos["22"] += 3;
                    conceitos["2"] += 5;
                    conceitos["1"] += 5;
                    conceitos["7"] += 3;
                    conceitos["34"] += 1;
                    conceitos["31"] += 1;

                    resultText.text += "\n - Lápis: 1 moeda!";
                }
                else if (i == 7)
                {
                    conceitos["24"] += 4;
                    conceitos["22"] += 3;
                    conceitos["2"] += 5;
                    conceitos["1"] += 5;
                    conceitos["7"] += 3;
                    conceitos["34"] += 1;
                    conceitos["31"] += 1;

                    resultText.text += "\n - Estojo: 1 moeda!";
                }
                else if (i == 8)
                {
                    conceitos["24"] += 4;
                    conceitos["22"] += 3;
                    conceitos["2"] += 5;
                    conceitos["1"] += 5;
                    conceitos["7"] += 3;
                    conceitos["34"] += 1;
                    conceitos["31"] += 1;

                    resultText.text += "\n - Caixa de lápis de cor: 1 moeda!";
                }
                else if (i == 9)
                {
                    conceitos["34"] += 1;

                    resultText.text += "\n - Porta lápis: 0 moedas.";
                }
                else resultText.text += "ERROR";
            }
        }

        if (bonusCoins > 0) StartCoroutine(FlavorManager.fm.SpawnCoinPosition(5, coinSpawnStart, coinSpawnEnd));
        PlayerManager.pm.AddCoins(totalCoins + bonusCoins);
        APIManager.am.Relatorio(conceitos);
        PlayerManager.pm.AddLevel();

        if (totalCoins + bonusCoins > 0)
        {
            FlavorManager.fm.ShowHidePanel(incomePanel, true);

            int counter = 0;

            while (counter < totalCoins + bonusCoins)
            {
                coinsPanel.GetChild(counter).gameObject.SetActive(true);
                counter++;
            }
        }
    }

    static void PanelButton(V1L23 instance, int price, int bonus, GameObject priceObject, GameObject selectedObject)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        if (priceObject.activeSelf)
        {
            if (instance.totalCoins >= price)
            {
                instance.totalCoins -= price;
                instance.totalCoinsText.text = instance.totalCoins.ToString();

                instance.totalPrice += price;
                instance.totalPriceText.text = instance.totalPrice.ToString();

                instance.bonusCoins += bonus;

                priceObject.SetActive(false);
                selectedObject.SetActive(true);
            }
            else
            {
                FlavorManager.fm.ShowHidePanel(instance.infoPanel, true);
                instance.infoText.text = "Você não tem dinheiro suficiente, escolha outro!";
                instance.infoButton.onClick.RemoveAllListeners();
                instance.infoButton.onClick.AddListener(delegate
                {
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    FlavorManager.fm.ShowHidePanel(instance.infoPanel, false);
                });
            }
        }
        else
        {
            instance.totalCoins += price;
            instance.totalCoinsText.text = instance.totalCoins.ToString();

            instance.totalPrice -= price;
            instance.totalPriceText.text = instance.totalPrice.ToString();

            instance.bonusCoins -= bonus;

            priceObject.SetActive(true);
            selectedObject.SetActive(false);
        }
    }
}
