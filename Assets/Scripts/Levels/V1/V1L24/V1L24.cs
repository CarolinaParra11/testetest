using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V1L24 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject objectPanel;
    public Button button1, button2, button3;

    public TextMeshProUGUI nome;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("31", 0);
        conceitos.Add("2", 0);
        conceitos.Add("1", 0);
        conceitos.Add("20", 0);
        conceitos.Add("19", 0);
        conceitos.Add("13", 0);

        conceitos["31"] += 0;
        conceitos["2"] += 0;
        conceitos["1"] += 0;
        conceitos["20"] += 0;
        conceitos["19"] += 0;
        conceitos["13"] += 0;

        nome.text = PlayerManager.pm.nome;

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[23]);
        infoText.text = "Você é dono de um mercado!\n\n" +
            " Você precisa comprar quatro pepinos, e seus clientes os querem até cinco dias." +
            "\n\nEscolha com calma.";
        infoButton.onClick.AddListener(delegate
        {
            StartCoroutine(PartA());
            AudioManager.am.voiceChannel.Stop();
        });
    }

    private IEnumerator PartA()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(2);
        FlavorManager.fm.ShowHidePanel(objectPanel, true);

        button1.onClick.AddListener(delegate
        {
            conceitos["31"] += 1;
            conceitos["2"] += 1;
            conceitos["1"] += 1;
            conceitos["20"] += 0;
            conceitos["19"] += 1;
            conceitos["13"] += 2;
            APIManager.am.Relatorio(conceitos);

            AudioManager.am.PlaySFX(AudioManager.am.button);
            PlayerManager.pm.AddLevel();
            FlavorManager.fm.ShowHidePanel(objectPanel, false);
            FlavorManager.fm.ShowHidePanel(resultPanel, true);
            AudioManager.am.PlayVoice(AudioManager.am.v1l24[0]);
            resultText.text = "Você comprou os pepinos a um bom preço," +
            " mas não sabe quanto vai usar de moedas para ir de carro até o sítio pegar os legumes," +
            " e também não sabe quanto tempo vai demorar. A melhor opção seria o cenário três," +
            " em que um caminhão do agricultor vem entregar diretamente até seu mercado!";
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
        });
        button2.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            PlayerManager.pm.AddLevel();
            FlavorManager.fm.ShowHidePanel(objectPanel, false);
            FlavorManager.fm.ShowHidePanel(resultPanel, true);
            AudioManager.am.PlayVoice(AudioManager.am.v1l24[1]);
            resultText.text = "Você comprou os pepinos a um bom preço," +
            " mas a demora de oito dias não atende aos seus clientes." +
            " Eles precisam dos pepinos em até cinco dias.";
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
        });
        button3.onClick.AddListener(delegate
        {
            conceitos["31"] += 4;
            conceitos["2"] += 3;
            conceitos["1"] += 3;
            conceitos["20"] += 3;
            conceitos["19"] += 3;
            conceitos["13"] += 6;
            APIManager.am.Relatorio(conceitos);

            AudioManager.am.PlaySFX(AudioManager.am.button);
            PlayerManager.pm.AddCoins(2);
            PlayerManager.pm.AddLevel();

            StartCoroutine(FlavorManager.fm.SpawnCoin(2));

            FlavorManager.fm.ShowHidePanel(objectPanel, false);
            FlavorManager.fm.ShowHidePanel(resultPanel, true);

            FlavorManager.fm.ShowHidePanel(incomePanel, true);
            coinsPanel.GetChild(0).gameObject.SetActive(true);
            coinsPanel.GetChild(1).gameObject.SetActive(true);

            AudioManager.am.PlayVoice(AudioManager.am.v1l24[2]);
            resultText.text = "Parabéns! Você escolheu a melhor opção." +
            " Comprou os pepinos a um bom preço, e ainda vai recebê-los até amanhã," +
            " o que deixará felizes os seus clientes. Por isso, você ganha 2 moedas extras para o seu Bolodix.";
            button1.onClick.RemoveAllListeners();
            button2.onClick.RemoveAllListeners();
            button3.onClick.RemoveAllListeners();
        });
    }
}