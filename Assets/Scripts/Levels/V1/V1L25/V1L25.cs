using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V1L25 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject optionPanel;
    public Button button1, button2, button3;

    public TextMeshProUGUI nome;

    public int income;
    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("13", 0);
        conceitos.Add("31", 0);
        conceitos.Add("10", 0);
        conceitos.Add("2", 0);
        conceitos.Add("1", 0);
        conceitos.Add("20", 0);
        conceitos.Add("25", 0);

        conceitos["13"] += 0;
        conceitos["31"] += 0;
        conceitos["10"] += 0;
        conceitos["2"] += 0;
        conceitos["1"] += 0;
        conceitos["20"] += 0;
        conceitos["25"] += 0;

        nome.text = PlayerManager.pm.nome;
        StartCoroutine(Part1Start());
    }

    private IEnumerator Part1Start()
    {
        yield return new WaitForSeconds(2);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[24]);
        infoText.text = "Continue sendo um bom empreendedor!\n\n Venda os pepinos no mercado." +
            " Hoje é o último dia antes que eles estraguem. Aproveite para fazer uma boa venda!" +
            " Lembrando que você tem um cliente interessado." +
            " Como gastou 4 moedas para comprar e trazer os quatro pepinos, pense na melhor opção.";
        infoButton.onClick.AddListener(delegate { Part3(); AudioManager.am.voiceChannel.Stop(); });
    }

    private void Part3()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        button1.onClick.AddListener(delegate
        {
            conceitos["13"] += 5;
            conceitos["31"] += 5;
            conceitos["10"] += 4;
            conceitos["2"] += 5;
            conceitos["1"] += 5;
            conceitos["20"] += 3;
            conceitos["25"] += 3;

            AudioManager.am.PlayVoice(AudioManager.am.v1l25[0]);
            StartCoroutine(Part3End("Parabéns! Você escolheu a melhor opção, agradou o seu cliente," +
                " vendeu todos os pepinos e ganhou 2 moedas a mais do que gastou para trazê-los." +
                " Você irá ganhar 2 moedas extras para o seu Bolodix!"));
            PlayerManager.pm.AddCoins(2);
            income += 2;
            StartCoroutine(FlavorManager.fm.SpawnCoin(2));
        });
        button2.onClick.AddListener(delegate 
        {
            conceitos["13"] += 3;
            conceitos["31"] += 3;
            conceitos["10"] += 3;
            conceitos["2"] += 3;
            conceitos["1"] += 3;
            conceitos["20"] += 2;
            conceitos["25"] += 2;

            AudioManager.am.PlayVoice(AudioManager.am.v1l25[1]);
            StartCoroutine(Part3End("Apesar de ter vendido a um preço bom de 2 moedas pelo pepino," +
                " os outros três pepinos ficaram sobrando, perderam qualidade e não podem ser vendidos." +
                " A melhor opção seria vender a maior quantidade de pepinos" +
                " por um valor maior do que as 4 moedas que usou para comprá-los.")); 
        });
        button3.onClick.AddListener(delegate 
        {
            conceitos["13"] += 1;
            conceitos["31"] += 2;
            conceitos["10"] += 2;
            conceitos["2"] += 2;
            conceitos["1"] += 2;
            conceitos["20"] += 1;
            conceitos["25"] += 1;

            AudioManager.am.PlayVoice(AudioManager.am.v1l25[2]);
            StartCoroutine(Part3End("Apesar de ter vendido a um preço bom," +
                " igual à quantidade de 4 moedas que você usou para trazê-los do mercado," +
                " o pepino que sobrou não é aproveitado. Assim, da próxima vez, considere vender todos os legumes," +
                " mesmo que por 6 moedas, e ainda agrade seu cliente.")); 
        });
    }

    private IEnumerator Part3End(string str)
    {
        APIManager.am.Relatorio(conceitos);
        AudioManager.am.PlaySFX(AudioManager.am.button);
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = str;
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { End(); AudioManager.am.PlaySFX(AudioManager.am.button); });
    }

    private void End()
    {
        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Tudo feito por hoje! Até a próxima!";
        PlayerManager.pm.AddLevel();

        if (income > 0)
        {
            FlavorManager.fm.ShowHidePanel(incomePanel, true);

            int counter = 0;

            while (counter < income)
            {
                coinsPanel.GetChild(counter).gameObject.SetActive(true);
                counter++;
            }
        }
    }
}
