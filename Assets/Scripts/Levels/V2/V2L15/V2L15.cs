using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L15 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject objectPanel;
    public Button button1, button2, button3, button4;

    public TextMeshProUGUI nome;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        nome.text = PlayerManager.pm.nome;
        
        //AudioManager.am.PlayVoice(AudioManager.am.v2start[14]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Seus clientes querem 4 pepinos e 3 acerolas, mas não estão dispostos a negociar.\n \n Se você não ajudá-los, sairão de mãos vazias!\n \n Descubra qual cliente oferece o melhor negócio.";
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part1()
    {
        conceitos.Add("20", 0);
        conceitos.Add("58", 0);
        conceitos.Add("1", 0);
        conceitos.Add("62", 0);
        conceitos.Add("23", 0);
        conceitos.Add("10", 0);
        conceitos.Add("2", 0);
        conceitos.Add("25", 0);
        conceitos.Add("13", 0);
        conceitos.Add("26", 0);
        conceitos.Add("63", 0);

        conceitos["20"] += 0;
        conceitos["58"] += 0;
        conceitos["1"] += 0;
        conceitos["62"] += 0;
        conceitos["23"] += 0;
        conceitos["10"] += 0;
        conceitos["2"] += 0;
        conceitos["25"] += 0;
        conceitos["13"] += 0;
        conceitos["26"] += 0;
        conceitos["63"] += 0;

        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(4);
        FlavorManager.fm.ShowHidePanel(objectPanel, true);

        button1.onClick.AddListener(delegate
        {
            conceitos["20"] += 2;
            conceitos["58"] += 0;
            conceitos["1"] += 1;
            conceitos["62"] += 4;
            conceitos["23"] += 1;
            conceitos["10"] += 0;
            conceitos["2"] += 1;
            conceitos["25"] += 1;
            conceitos["13"] += 5;
            conceitos["26"] += 3;
            conceitos["63"] += 1;

            StartCoroutine(End(0, 0, "Infelizmente, essa não é a melhor opção.\n  \n A opção 3 gera um maior lucro para o seu negócio.\n \n Veja o cálculo:\n \nPasso 1: Some o valor da venda → 20 + 18 = 38 reais.\n \nPasso 2: Aplique o desconto de 3 reais → 38 - 3 = 35reais.\n \nEsse é o maior valor em comparação com as outras opções"));
        });
        button2.onClick.AddListener(delegate
        {
            conceitos["20"] += 1;
            conceitos["58"] += 0;
            conceitos["1"] += 0;
            conceitos["62"] += 4;
            conceitos["23"] += 1;
            conceitos["10"] += 0;
            conceitos["2"] += 1;
            conceitos["25"] += 1;
            conceitos["13"] += 5;
            conceitos["26"] += 3;
            conceitos["63"] += 1;

            StartCoroutine(End(0, 0, "Infelizmente, essa não é a melhor opção.\n  \n A opção 3 gera um maior lucro para o seu negócio.\n \n Veja o cálculo:\n \nPasso 1: Some o valor da venda → 20 + 18 = 38 reais.\n \nPasso 2: Aplique o desconto de 3 reais → 38 - 3 = 35reais.\n \nEsse é o maior valor em comparação com as outras opções"));
        });
        button3.onClick.AddListener(delegate
        {
            conceitos["20"] += 4;
            conceitos["58"] += 9;
            conceitos["1"] += 5;
            conceitos["62"] += 5;
            conceitos["23"] += 5;
            conceitos["10"] += 4;
            conceitos["2"] += 4;
            conceitos["25"] += 4;
            conceitos["13"] += 9;
            conceitos["26"] += 10;
            conceitos["63"] += 8;

            StartCoroutine(End(12, 5, "Parabéns você fez o melhor negócio! Ganhou 12 reais pro seu Bolodix!"));
        });
        button4.onClick.AddListener(delegate
        {
            conceitos["20"] += 3;
            conceitos["58"] += 6;
            conceitos["1"] += 3;
            conceitos["62"] += 3;
            conceitos["23"] += 1;
            conceitos["10"] += 2;
            conceitos["2"] += 3;
            conceitos["25"] += 3;
            conceitos["13"] += 8;
            conceitos["26"] += 7;
            conceitos["63"] += 6;

            StartCoroutine(End(0, 0, "Infelizmente, essa não é a melhor opção.\n  \n A opção 3 gera um maior lucro para o seu negócio.\n \n Veja o cálculo:\n \nPasso 1: Some o valor da venda → 20 + 18 = 38 reais.\n \nPasso 2: Aplique o desconto de 3 reais → 38 - 3 = 35reais.\n \nEsse é o maior valor em comparação com as outras opções"));
        });
    }

    private IEnumerator End(int coins, int bucks, string text)
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        button4.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(objectPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = text;

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = coins.ToString();

        StartCoroutine(FlavorManager.fm.SpawnBucks(bucks));
        PlayerManager.pm.AddCoins(coins);

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel(); 
    }
}