using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V2L26 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private V2L26Helper v2l26Helper;
    public Button button1, button2, button3;
    public TextMeshProUGUI totalCoinsText;

    public GameObject optionPanel;
    public Button optionButton1, optionButton2;

    public int income;
    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI incomeTitle;

    public Button endButton;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("55", 0);
        conceitos.Add("31", 0);
        conceitos.Add("10", 0);
        conceitos.Add("1", 0);
        conceitos.Add("39", 0);
        conceitos.Add("20", 0);
        conceitos.Add("5", 0);
        conceitos.Add("75", 0);
        conceitos.Add("23", 0);

        conceitos["55"] += 0;
        conceitos["31"] += 0;
        conceitos["10"] += 0;
        conceitos["1"] += 0;
        conceitos["39"] += 0;
        conceitos["20"] += 0;
        conceitos["5"] += 0;
        conceitos["75"] += 0;
        conceitos["23"] += 0;

        v2l26Helper = GameObject.Find("V2L26Helper").GetComponent<V2L26Helper>();
  
        if (!v2l26Helper.visited)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v2start[25]);

            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoText.text = "Você trabalhou no final de semana e recebeu 100 reais. Veja a melhor opção para usar de acordo com o objetivo.";
            infoButton.onClick.AddListener(delegate { Part1(); AudioManager.am.voiceChannel.Stop(); infoButton.onClick.RemoveAllListeners(); });
            v2l26Helper.visited = true;
        }
        else
        {
            if (((!v2l26Helper.item1 && v2l26Helper.coins >= 10) || (!v2l26Helper.item2 && v2l26Helper.coins >= 5) || (!v2l26Helper.item3 && v2l26Helper.coins >= 10)) || v2l26Helper.coins >= 50)
            {
                FlavorManager.fm.ShowHidePanel(infoPanel, true);
                infoText.text = "Você ainda tem " + v2l26Helper.coins + " reais!\n\nContinue comprando ou deposite algum valor no banco!";
                infoButton.onClick.AddListener(delegate { Part1(); infoButton.onClick.RemoveAllListeners(); });
            }
            else PreEnd();
        }

        if (v2l26Helper.coins < 50)
        {
            endButton.gameObject.SetActive(true);
            endButton.onClick.AddListener(delegate 
            {
                AudioManager.am.PlaySFX(AudioManager.am.button);
                PreEnd();
                endButton.gameObject.SetActive(false);
            });
        }

        totalCoinsText.text = v2l26Helper.coins.ToString();
        SetButtons(false);
    }

    private void PreEnd()
    {
        if (v2l26Helper.item1)
        {
            conceitos["55"] += 5;
            conceitos["31"] += 3;
            conceitos["10"] += 6;
            conceitos["1"] += 4;
            conceitos["39"] += 3;
            conceitos["20"] += 2;
            conceitos["5"] += 7;
            conceitos["75"] += 7;
            conceitos["23"] += 8;
        }
        if (v2l26Helper.item2)
        {
            conceitos["55"] += 5;
            conceitos["31"] += 3;
            conceitos["10"] += 6;
            conceitos["1"] += 4;
            conceitos["39"] += 3;
            conceitos["20"] += 2;
            conceitos["5"] += 7;
            conceitos["75"] += 7;
            conceitos["23"] += 8;
        }
        if (v2l26Helper.item3)
        {
            conceitos["55"] += 5;
            conceitos["31"] += 5;
            conceitos["10"] += 7;
            conceitos["1"] += 6;
            conceitos["39"] += 5;
            conceitos["20"] += 3;
            conceitos["5"] += 9;
            conceitos["75"] += 7;
            conceitos["23"] += 8;
        }

        if(PlayerManager.pm.v2l26choice == 50)
        {
            conceitos["55"] += 4;
            conceitos["31"] += 1;
            conceitos["10"] += 6;
            conceitos["1"] += 2;
            conceitos["39"] += 2;
            conceitos["20"] += 3;
            conceitos["5"] += 0;
            conceitos["75"] += 6;
            conceitos["23"] += 7.5;
        }
        else if (PlayerManager.pm.v2l26choice == 75)
        {
            conceitos["55"] += 5;
            conceitos["31"] += 4;
            conceitos["10"] += 7;
            conceitos["1"] += 4;
            conceitos["39"] += 3;
            conceitos["20"] += 5;
            conceitos["5"] += 0;
            conceitos["75"] += 7;
            conceitos["23"] += 8;
        }
        else if (PlayerManager.pm.v2l26choice == 100)
        {
            conceitos["55"] += 4;
            conceitos["31"] += 3;
            conceitos["10"] += 7;
            conceitos["1"] += 3;
            conceitos["39"] += 3;
            conceitos["20"] += 4;
            conceitos["5"] += 0;
            conceitos["75"] += 6;
            conceitos["23"] += 8;
        }

        income = v2l26Helper.coins;
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        if (!v2l26Helper.item1 || !v2l26Helper.item2 || !v2l26Helper.item3)
        {
            infoText.text = "Você não conseguiu tudo o que precisava!\n";
            if (!v2l26Helper.item1)
            {
                income -= 30;
                infoText.text += "\nFaltou o abacaxi! Você teve que pedir no delivery e estava mais caro: foram 15 reais com a taxa de entrega de 15 reais.";
                infoText.text += "\nFaltou o abacaxi! Você teve que pedir no delivery e estava mais caro: foram 15 reais mais a taxa de entrega que custa também 15 reais. Somando um total de 30 reais.";
            }
            if (!v2l26Helper.item2)
            {
                income -= 25;
                infoText.text += "\nFaltou a banana! Você teve que pedir no delivery e estava mais caro: foram 10 reais com a taxa de entrega de 15 reais.";
            }
            if (!v2l26Helper.item3)
            {
                income -= 35;
                infoText.text += "\nFaltou o copo! Você teve que pedir no delivery e estava mais caro: foram 20 reais com a taxa de entrega de 15 reais.";
            }
        }
        else infoText.text = "\n\nParabéns! Tudo comprado!";

        infoButton.onClick.AddListener(delegate { Part2(); infoButton.onClick.RemoveAllListeners(); });
    }

    private void Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        button1.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L26a"); SetButtons(false); AudioManager.am.PlaySFX(AudioManager.am.button); });
        button2.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L26b"); SetButtons(false); AudioManager.am.PlaySFX(AudioManager.am.button); });
        button3.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L26c"); SetButtons(false); AudioManager.am.PlaySFX(AudioManager.am.button); });

        SetButtons(true);
    }

    private void Part2()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        optionButton1.onClick.AddListener(delegate { Part2End(false); });
        optionButton2.onClick.AddListener(delegate { Part2End(true); });
    }

    private void Part2End(bool correct)
    {
        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        if (correct)
        {
            conceitos["55"] += 5;
            conceitos["31"] += 2;
            conceitos["10"] += 2;
            conceitos["1"] += 2;
            conceitos["39"] += 1;
            conceitos["20"] += 0;
            conceitos["5"] += 2;
            conceitos["75"] += 0;
            conceitos["23"] += 10;

            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            income += 30;
            infoText.text = "Parabéns!\n\nO copo no supermercado custa 10 reais a unidade, e se levasse 2 compensaria, pois custaria 20 reais, preço melhor que a promoção da loja.\n\nGanhou 30 reais!";
        }
        else
        {
            infoText.text = "O copo no mercado vale 10 reais por unidade! Assim, 2 copos seriam 20 reais, preço melhor que da loja, mesmo com promoção.";
        }
        infoButton.onClick.AddListener(delegate { End(); infoButton.onClick.RemoveAllListeners(); });
    }

    private void End()
    {
        //.fm.ShowHidePanel(infoPanel, false);
        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddCoins(income);
        PlayerManager.pm.AddLevel();


        if (income < 0)
        {
            income *= -1;
            incomeTitle.text = "Retirou do Bolodix";
        }

        FlavorManager.fm.ShowHidePanel(incomePanel, true);

        incomeText.text = income.ToString();

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Tudo feito! Até a próxima!";
    }

    private void SetButtons(bool value)
    {
        button1.enabled = value;
        button2.enabled = value;
        button3.enabled = value;
    }
}