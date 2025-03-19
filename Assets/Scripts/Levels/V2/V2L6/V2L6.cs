using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L6 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 300;
    private int bonusCoins;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel1, optionPanel2, optionPanel3;
    
    public Button p1SubmitButton, p2SubmitButton, p3SubmitButton;
    public GameObject p1Submit, p2Submit, p3Submit;

    public Button p1Button1, p1Button2, p1Button3;
    public GameObject p1b1Price, p1b2Price, p1b3Price;
    public GameObject p1b1Selected, p1b2Selected, p1b3Selected;
    
    public Button p2Button1, p2Button2, p2Button3;
    public GameObject p2b1Price, p2b2Price, p2b3Price;
    public GameObject p2b1Selected, p2b2Selected, p2b3Selected;

    public Button[] p3Button;
    public GameObject[] p3bPrice;
    public GameObject[] p3bSelected;
    private int p3Total;
    public TextMeshProUGUI p3TotalText;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        totalCoinsText.text = totalCoins.ToString();

        AudioManager.am.PlayVoice(AudioManager.am.v2start[5]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Dia do Acampamento! Você irá acampar e precisará escolher quando deseja ir, como irá e quais os itens essenciais que não podem faltar no acampamento. Você tem trezentos reais para isso.";
        infoButton.onClick.AddListener(delegate 
        { 
            StartCoroutine(Part1()); 
        });
    }

    private IEnumerator Part1()
    {
        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel1, true);

        p1Button1.onClick.AddListener(delegate { PanelButtonOneOption(this, 60, p1b1Price, p1b1Selected, p1b2Selected, p1b3Selected, p1Submit); });
        p1Button2.onClick.AddListener(delegate { PanelButtonOneOption(this, 50, p1b2Price, p1b2Selected, p1b1Selected, p1b3Selected, p1Submit); });
        p1Button3.onClick.AddListener(delegate { PanelButtonOneOption(this, 40, p1b3Price, p1b3Selected, p1b1Selected, p1b2Selected, p1Submit); });
        p1SubmitButton.onClick.AddListener(delegate { StartCoroutine(Part1End()); });
    }

    private IEnumerator Part1End()
    {
        // Declaração
        conceitos.Add("18", 0);
        conceitos.Add("10", 0);
        conceitos.Add("23", 0);
        conceitos.Add("46", 0);
        conceitos.Add("19", 0);
        conceitos.Add("47", 0);
        conceitos.Add("48", 0);
        conceitos.Add("44", 0);
        conceitos.Add("25", 0);
        conceitos.Add("31", 0);
        conceitos.Add("34", 0);

        conceitos["18"] += 0;
        conceitos["10"] += 0;
        conceitos["23"] += 0;
        conceitos["46"] += 0;
        conceitos["19"] += 0;
        conceitos["47"] += 0;
        conceitos["48"] += 0;
        conceitos["44"] += 0;
        conceitos["25"] += 0;
        conceitos["31"] += 0;
        conceitos["34"] += 0;

        if (p1b1Selected.activeSelf)
        {
            conceitos["18"] += 1;
            conceitos["10"] += 1;
            conceitos["23"] += 1;
            conceitos["46"] += 1;
            conceitos["19"] += 1;
            conceitos["47"] += 1;
            conceitos["48"] += 1;
            conceitos["44"] += 1;
            conceitos["25"] += 1;
            conceitos["31"] += 1;
            conceitos["34"] += 1;
        }
        else if (p1b2Selected.activeSelf)
        {
            conceitos["18"] += 2;
            conceitos["10"] += 2;
            conceitos["23"] += 2;
            conceitos["46"] += 3;
            conceitos["19"] += 2;
            conceitos["47"] += 2;
            conceitos["48"] += 2;
            conceitos["44"] += 2;
            conceitos["25"] += 2;
            conceitos["31"] += 3;
            conceitos["34"] += 2;
        }
        else if (p1b3Selected.activeSelf)
        {
            conceitos["18"] += 3;
            conceitos["10"] += 4;
            conceitos["23"] += 4;
            conceitos["46"] += 5;
            conceitos["19"] += 4;
            conceitos["47"] += 4;
            conceitos["48"] += 4;
            conceitos["44"] += 2;
            conceitos["25"] += 2;
            conceitos["31"] += 5;
            conceitos["34"] += 2;
        }

        StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));

        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoText.text = "Legal! Você ainda tem " + totalCoins + " reais! Agora vamos continuar nossas compras!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { AudioManager.am.PlaySFX(AudioManager.am.button); StartCoroutine(Part2()); infoButton.onClick.RemoveAllListeners(); });
        
    }

    private IEnumerator Part2()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel2, true);

        p2Button1.onClick.AddListener(delegate { PanelButtonOneOption(this, 150, p2b1Price, p2b1Selected, p2b2Selected, p2b3Selected, p2Submit); });
        p2Button2.onClick.AddListener(delegate { PanelButtonOneOption(this, 80, p2b2Price, p2b2Selected, p2b1Selected, p2b3Selected, p2Submit); });
        p2Button3.onClick.AddListener(delegate { PanelButtonOneOption(this, 70, p2b3Price, p2b3Selected, p2b1Selected, p2b2Selected, p2Submit); });
        p2SubmitButton.onClick.AddListener(delegate { StartCoroutine(Part2End()); });
    }

    private IEnumerator Part2End()
    {
        if (p2b1Selected.activeSelf)
        {
            conceitos["18"] += 1;
            conceitos["10"] += 1;
            conceitos["23"] += 1;
            conceitos["46"] += 1;
            conceitos["19"] += 1;
            conceitos["47"] += 1;
            conceitos["48"] += 1;
            conceitos["44"] += 1;
            conceitos["25"] += 1;
            conceitos["31"] += 1;
            conceitos["34"] += 1;
        }
        else if (p2b2Selected.activeSelf)
        {
            conceitos["18"] += 2;
            conceitos["10"] += 2;
            conceitos["23"] += 2;
            conceitos["46"] += 2;
            conceitos["19"] += 2;
            conceitos["47"] += 2;
            conceitos["48"] += 2;
            conceitos["44"] += 2;
            conceitos["25"] += 1;
            conceitos["31"] += 2;
            conceitos["34"] += 4;
        }
        else if (p2b3Selected.activeSelf)
        {
            conceitos["18"] += 3;
            conceitos["10"] += 2;
            conceitos["23"] += 3;
            conceitos["46"] += 2;
            conceitos["19"] += 3;
            conceitos["47"] += 3;
            conceitos["48"] += 2;
            conceitos["44"] += 3;
            conceitos["25"] += 1;
            conceitos["31"] += 4;
            conceitos["34"] += 4;
        }

        StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));

        FlavorManager.fm.ShowHidePanel(optionPanel2, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoText.text = "Legal! Você ainda tem " + totalCoins + " reais! Agora vamos continuar nossas compras!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { AudioManager.am.PlaySFX(AudioManager.am.button); StartCoroutine(Part3()); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part3()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel3, true);

        p3Button[0].onClick.AddListener(delegate { PanelButton(this, 3, 13, ref p3Total, p3TotalText, p3bPrice[0], p3bSelected[0]); });
        p3Button[1].onClick.AddListener(delegate { PanelButton(this, 10, 0, ref p3Total, p3TotalText, p3bPrice[1], p3bSelected[1]); });
        p3Button[2].onClick.AddListener(delegate { PanelButton(this, 25, 35, ref p3Total, p3TotalText, p3bPrice[2], p3bSelected[2]); });
        p3Button[3].onClick.AddListener(delegate { PanelButton(this, 25, 35, ref p3Total, p3TotalText, p3bPrice[3], p3bSelected[3]); });
        p3Button[4].onClick.AddListener(delegate { PanelButton(this, 12, 22, ref p3Total, p3TotalText, p3bPrice[4], p3bSelected[4]); });
        p3Button[5].onClick.AddListener(delegate { PanelButton(this, 8, 0, ref p3Total, p3TotalText, p3bPrice[5], p3bSelected[5]); });
        p3Button[6].onClick.AddListener(delegate { PanelButton(this, 10, 20, ref p3Total, p3TotalText, p3bPrice[6], p3bSelected[6]); });
        p3Button[7].onClick.AddListener(delegate { PanelButton(this, 4, 14, ref p3Total, p3TotalText, p3bPrice[7], p3bSelected[7]); });
        p3Button[8].onClick.AddListener(delegate { PanelButton(this, 4, 14, ref p3Total, p3TotalText, p3bPrice[8], p3bSelected[8]); });
        p3Button[9].onClick.AddListener(delegate { PanelButton(this, 12, 0, ref p3Total, p3TotalText, p3bPrice[9], p3bSelected[9]); });
        p3Button[10].onClick.AddListener(delegate { PanelButton(this, 10, 0, ref p3Total, p3TotalText, p3bPrice[10], p3bSelected[10]); });
        p3Button[11].onClick.AddListener(delegate { PanelButton(this, 20, 30, ref p3Total, p3TotalText, p3bPrice[11], p3bSelected[11]); });
        p3Button[12].onClick.AddListener(delegate { PanelButton(this, 20, 0, ref p3Total, p3TotalText, p3bPrice[12], p3bSelected[12]); });
        p3SubmitButton.onClick.AddListener(delegate { StartCoroutine(End()); });
    }

    private IEnumerator End()
    {
        resultText.text = "Sobraram " + totalCoins + " reais e você conseguiu ganhar mais " + bonusCoins + " de bônus! Até a próxima!\n";

        for (int i = 0; i < p3bSelected.Length; i++)
        {
            if (p3bSelected[i].activeSelf)
            {
                if (i == 0) resultText.text += "\n - Garrafa de água: 3 reais + 10 de BÔNUS!";
                else if (i == 1) resultText.text += "\n - Bola de futebol: 0 reais.";
                else if (i == 2) resultText.text += "\n - Mochila: 25 reais + 10 de BÔNUS!";
                else if (i == 3) resultText.text += "\n - Bota de acampar: 25 reais + 10 de BÔNUS!";
                else if (i == 4) resultText.text += "\n - Protetor solar: 12 reais + 10 de BÔNUS!";
                else if (i == 5) resultText.text += "\n - Colchonete: 0 reais.";
                else if (i == 6) resultText.text += "\n - Repelente: 10 reais + 10 de BÔNUS!";
                else if (i == 7) resultText.text += "\n - Escova de dente: 4 reais + 10 de BÔNUS!";
                else if (i == 8) resultText.text += "\n - Pasta de dente: 4 reais + 10 de BÔNUS!";
                else if (i == 9) resultText.text += "\n - Kit de canetas coloridas: 0 reais.";
                else if (i == 10) resultText.text += "\n - Suco de garrafa: 0 reais.";
                else if (i == 11) resultText.text += "\n - Kit de remédios: 20 reais + 10 de BÔNUS!";
                else if (i == 12) resultText.text += "\n - Jogo de montar: 0 reais.";
                else resultText.text += "ERROR";
            }
        }

        FlavorManager.fm.ShowHidePanel(optionPanel3, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = (totalCoins + bonusCoins).ToString();

        if (p3Total > 0) StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));
        PlayerManager.pm.AddCoins(totalCoins + bonusCoins);
        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }

    private static void PanelButtonOneOption(V2L6 instance, int price, GameObject priceObject, GameObject selectedObject, GameObject otherSelectedObject1, GameObject otherSelectedObject2, GameObject pSubmit)
    {
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
                    instance.infoButton.onClick.AddListener(delegate
                    {
                        AudioManager.am.PlaySFX(AudioManager.am.button);
                        FlavorManager.fm.ShowHidePanel(instance.infoPanel, false);
                        instance.infoButton.onClick.RemoveAllListeners();
                    });
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
    }

    private static void PanelButton(V2L6 instance, int price, int bonus, ref int totalPrice, TextMeshProUGUI totalPriceText, GameObject priceObject, GameObject selectedObject)
    {
        if (priceObject.activeSelf)
        {
            if (instance.totalCoins >= price)
            {
                instance.totalCoins -= price;
                instance.totalCoinsText.text = instance.totalCoins.ToString();

                instance.bonusCoins += bonus;

                if(bonus != 0)
                {
                    instance.conceitos["18"] += 1.5;
                    instance.conceitos["10"] += 1;
                    instance.conceitos["23"] += 1.5;
                    instance.conceitos["46"] += 1;
                    instance.conceitos["19"] += 2;
                    instance.conceitos["47"] += 2;
                    instance.conceitos["48"] += 1;
                    instance.conceitos["44"] += 1;
                    instance.conceitos["25"] += 1;
                    instance.conceitos["31"] += 1;
                    instance.conceitos["34"] += 1;
                }

                totalPrice += price;
                totalPriceText.text = totalPrice.ToString();

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
                    instance.infoButton.onClick.RemoveAllListeners();
                });
            }
        }
        else
        {
            instance.totalCoins += price;
            instance.totalCoinsText.text = instance.totalCoins.ToString();

            instance.bonusCoins -= bonus;
            totalPrice -= price;
            totalPriceText.text = totalPrice.ToString();

            priceObject.SetActive(true);
            selectedObject.SetActive(false);
        }
    }
}