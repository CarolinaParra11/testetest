using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L11 : MonoBehaviour
{
    public GameObject infoPanel, infoPanel2;
    public TextMeshProUGUI infoText, infoText2;
    public Button infoButton, infoButton2;

    public GameObject optionPanel,optionPanel2;
    public Button button1, button2, button3, button4;
    public Button pn2button1, pn2button2, pn2button3, pn2button4;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        StartCoroutine(Part1());
    }

    private IEnumerator Part1()
    {
        // Declaração
        conceitos.Add("23", 0);
        conceitos.Add("22", 0);
        conceitos.Add("55", 0);
        conceitos.Add("25", 0);
        conceitos.Add("6", 0);
        conceitos.Add("7", 0);
        conceitos.Add("56", 0);

        yield return new WaitForSeconds(5);

       // AudioManager.am.PlayVoice(AudioManager.am.v2start[10]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "O pai do seu amigo Leo está querendo investir em uma cadeira, uma mesa e uma segunda cama. \n A cama é a menor prioridade.Veja qual a melhor opção entre as alternativas:";

        infoButton.onClick.AddListener(delegate
        {
            AudioManager.am.voiceChannel.Stop();
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
            FlavorManager.fm.ShowHidePanel(optionPanel, true);
            infoButton.onClick.RemoveAllListeners();
        });

        button1.onClick.AddListener(delegate
        {
            conceitos["23"] += 1;
            conceitos["22"] += 1;
            conceitos["55"] += 2;
            conceitos["25"] += 1;
            conceitos["6"] += 1;
            conceitos["7"] += 1;

            StartCoroutine(End(0, "Infelizmente, essa não é a melhor opção. \n" +
            "Depende. Primeiro, é preciso avaliar a necessidade da compra e comparar preços em outras lojas."));
        });

        button2.onClick.AddListener(delegate
        {
            conceitos["23"] += 2;
            conceitos["22"] += 2;
            conceitos["55"] += 2;
            conceitos["25"] += 2;
            conceitos["6"] += 1;
            conceitos["7"] += 3;

            StartCoroutine(End(20, "Muito bem! Você escolheu a melhor opção.\n" +
                "Você ganhou 20 reais extras para o Bolodix!"));
        });

        button3.onClick.AddListener(delegate
        {
            conceitos["23"] += 4;
            conceitos["22"] += 3;
            conceitos["55"] += 5;
            conceitos["25"] += 3;
            conceitos["6"] += 1;
            conceitos["7"] += 6;

            StartCoroutine(End(0, "Infelizmente, essa não é a melhor opção.\n" +
            "Depende. Primeiro, é preciso avaliar a necessidade da compra e comparar preços em outras lojas."));
        });

        button4.onClick.AddListener(delegate
        {
            conceitos["23"] += 4;
            conceitos["22"] += 3;
            conceitos["55"] += 5;
            conceitos["25"] += 3;
            conceitos["6"] += 1;
            conceitos["7"] += 6;

            StartCoroutine(End(0, "Infelizmente, essa não é a melhor opção.\n" +
            "Depende. Primeiro, é preciso avaliar a necessidade da compra e comparar preços em outras lojas."));
        });

    } 

    private IEnumerator End(int coins, string text)
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        button4.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoText.text = text;

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = coins.ToString();

        if (coins > 0)
        {
            PlayerManager.pm.AddCoins(coins);
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        }

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate
        {
            APIManager.am.Relatorio(conceitos);

            AudioManager.am.PlaySFX(AudioManager.am.button);
            StartCoroutine(Part2());

            //GameManager.gm.LoadScene("V2L11b");
        });
    }

    private IEnumerator Part2()
    {
         FlavorManager.fm.ShowHidePanel(incomePanel, false);
          FlavorManager.fm.ShowHidePanel(infoPanel, false);

        yield return new WaitForSeconds(1);

        FlavorManager.fm.ShowHidePanel(infoPanel2, true);
        infoText2.text = "Promoções oferecidas pelas lojas são sempre boas? \n"+ "Escolha a opção que melhor responde essa pergunta" ;

        infoButton2.onClick.AddListener(delegate
        {
            FlavorManager.fm.ShowHidePanel(infoPanel2, false);
            FlavorManager.fm.ShowHidePanel(optionPanel2, true);
            infoButton2.onClick.RemoveAllListeners();
        });

        pn2button1.onClick.AddListener(delegate
        {

            StartCoroutine(End2(0, "Infelizmente, essa não é a melhor opção. \n" +
            "A opção correta era: Mesa + Cadeira = 350"));
        });

        pn2button2.onClick.AddListener(delegate
        {
            StartCoroutine(End2(30, "Muito bem! Você escolheu a melhor opção.\n" +
                "Você ganhou 30 reais extras para o Bolodix!"));
        });

        pn2button3.onClick.AddListener(delegate
        {
            StartCoroutine(End2(0, "Infelizmente, essa não é a melhor opção.\n" +
            "A opção correta era: Mesa + Cadeira = 350"));
        });

        pn2button4.onClick.AddListener(delegate
        {
            StartCoroutine(End2(0, "Infelizmente, essa não é a melhor opção.\n" +
            "A opção correta era: Mesa + Cadeira = 350"));
        });

    } 
    
    private IEnumerator End2(int coins, string text)
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        button4.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel2, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel2, true);

        infoText2.text = text;

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = coins.ToString();

        if (coins > 0)
        {
            PlayerManager.pm.AddCoins(coins);
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        }

        infoButton2.onClick.RemoveAllListeners();
        infoButton2.onClick.AddListener(delegate
        {
            //APIManager.am.Relatorio(conceitos);

            AudioManager.am.PlaySFX(AudioManager.am.button);
          
            GameManager.gm.LoadScene("V2L12");

        });

    }
} 
