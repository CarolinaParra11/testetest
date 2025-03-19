using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V2L18 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject hintButton;

    public GameObject optionPanel1, optionPanel2, optionPanel3;
    public Button p1Button1, p1Button2, p1Button3, p1Button4;
    public Button p2Button1, p2Button2, p2Button3, p2Button4;
    public Button p3Button1, p3Button2, p3Button3;
    public TextMeshProUGUI nome;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;
    public int totalCoins;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        hintButton.SetActive(false);
        nome.text = PlayerManager.pm.nome;

        StartCoroutine(Part1Start());
    }

    private IEnumerator Part1Start()
    {
        conceitos.Add("66", 0);
        conceitos.Add("67", 0);
        conceitos.Add("68", 0);
        conceitos.Add("64", 0);
        conceitos.Add("10", 0);
        conceitos.Add("13", 0);
        conceitos.Add("62", 0);
        conceitos.Add("25", 0);
        conceitos.Add("44", 0);
        conceitos.Add("1", 0);
        conceitos.Add("27", 0);

        conceitos["66"] += 0;
        conceitos["67"] += 0;
        conceitos["68"] += 0;
        conceitos["64"] += 0;
        conceitos["10"] += 0;
        conceitos["13"] += 0;
        conceitos["62"] += 0;
        conceitos["25"] += 0;
        conceitos["44"] += 0;
        conceitos["1"] += 0;
        conceitos["27"] += 0;

        yield return new WaitForSeconds(4);
        AudioManager.am.PlayVoice(AudioManager.am.v2start[17]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Dono do Mercado 2: Agora você venderá acerolas e pepinos juntos. Escolha a melhor opção. Antes, teremos duas perguntas bônus para tentar fazer crescer o Bolodix.";
        infoButton.onClick.AddListener(delegate { Part1(); });
    }

    private void Part1()
    {
        hintButton.SetActive(true);
        Debug.Log("part 1 start");
        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel1, true);

        p1Button1.onClick.AddListener(delegate { StartCoroutine(Part1End("Opção errada! Tente mais uma vez!")); });
        p1Button2.onClick.AddListener(delegate { StartCoroutine(Part1End("Opção errada! Tente mais uma vez!"));  });
        p1Button3.onClick.AddListener(delegate 
        {
            conceitos["66"] += 4;
            conceitos["67"] += 5;

            StartCoroutine(Part1End("Acertou! Vamos ver se consegue a próxima!")); 
            PlayerManager.pm.AddCoins(10); 
            totalCoins += 10; 
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        });
        p1Button4.onClick.AddListener(delegate { StartCoroutine(Part1End("Opção errada! Tente mais uma vez!"));  });
    }

    private IEnumerator Part1End(string str)
    {
        p1Button1.onClick.RemoveAllListeners();
        p1Button2.onClick.RemoveAllListeners();
        p1Button3.onClick.RemoveAllListeners();
        p1Button4.onClick.RemoveAllListeners();

        Debug.Log("spawnei part 1 end");
        yield return new WaitForSeconds(0.5f);
  
        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = str;
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { Part2(); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });

    }

    private void Part2()
    {

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel2, true);

        p2Button1.onClick.AddListener(delegate { StartCoroutine(Part2End("Opção errada! Tente mais uma vez!")); });
        p2Button2.onClick.AddListener(delegate 
        {
            conceitos["66"] += 4;
            conceitos["67"] += 5;

            StartCoroutine(Part2End("Acertou!"));
            PlayerManager.pm.AddCoins(10); totalCoins += 10;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        });
        p2Button3.onClick.AddListener(delegate { StartCoroutine(Part2End("Opção errada! Tente mais uma vez!")); });
        p2Button4.onClick.AddListener(delegate { StartCoroutine(Part2End("Opção errada! Tente mais uma vez!")); });
    }

    private IEnumerator Part2End(string str)
    {
        p2Button1.onClick.RemoveAllListeners();
        p2Button2.onClick.RemoveAllListeners();
        p2Button3.onClick.RemoveAllListeners();
        p2Button4.onClick.RemoveAllListeners();

        Debug.Log("spawnei part 2 end");
        yield return new WaitForSeconds(0.5f);

        FlavorManager.fm.ShowHidePanel(optionPanel2, false);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = str;
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { Part3Start(); AudioManager.am.PlaySFX(AudioManager.am.button);});
    }

    private void Part3Start()
    {
        infoText.text = "Hoje é o último dia para a venda das acerolas, elas vão estragar logo!\n\n" +
                        "Não vale a pena ganhar 12 reais por fruta. Você pode vender por 15 reais cada para os seus clientes.\n\n\n" +
                        "Veja! Um cliente está interessado em comprar! Escolha qual a melhor opção para a venda!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { Part3(); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
    }

    private void Part3()
    {
        Debug.Log("pei");

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel3, true);

        p3Button1.onClick.AddListener(delegate 
        {
            conceitos["66"] += 4;
            conceitos["67"] += 4;
            conceitos["68"] += 3;
            conceitos["64"] += 5;
            conceitos["10"] += 3;
            conceitos["13"] += 5;
            conceitos["62"] += 4;
            conceitos["25"] += 4;
            conceitos["44"] += 5;
            conceitos["1"] += 5;
            conceitos["27"] += 7;

            StartCoroutine(Part3End("Parabéns! Você escolheu a melhor opção: agradou o seu cliente aceitando o valor oferecido, e conseguiu vender todas as acerolas que tinha por um ótimo valor!\n\nGanhou 120 reais!"));
            PlayerManager.pm.AddCoins(120);
            totalCoins += 120;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        });
        p3Button2.onClick.AddListener(delegate 
        {
            conceitos["66"] += 3;
            conceitos["67"] += 3;
            conceitos["68"] += 2;
            conceitos["64"] += 4;
            conceitos["10"] += 2;
            conceitos["13"] += 3;
            conceitos["62"] += 2;
            conceitos["25"] += 3;
            conceitos["44"] += 3;
            conceitos["1"] += 3;
            conceitos["27"] += 2;

            StartCoroutine(Part3End("Apesar de ter vendido no preço que estipulou, 15 reais cada, vão sobrar duas acerolas, que irão estragar e perder seu valor.\n\nGanhou 90 reais!")); 
            PlayerManager.pm.AddCoins(90);
            totalCoins += 90;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        });
        p3Button3.onClick.AddListener(delegate 
        {
            conceitos["66"] += 1;
            conceitos["67"] += 1;
            conceitos["68"] += 1;
            conceitos["64"] += 2;
            conceitos["10"] += 1;
            conceitos["13"] += 0;
            conceitos["62"] += 0;
            conceitos["25"] += 1;
            conceitos["44"] += 1;
            conceitos["1"] += 1;
            conceitos["27"] += 1;

            StartCoroutine(Part3End("As acerolas estavam estragadas quando o cliente veio buscar! Lembre que os produtos têm validade. Você devolveu o valor ao cliente."));
        });
    }

    private IEnumerator Part3End(string str)
    {
        p3Button1.onClick.RemoveAllListeners();
        p3Button2.onClick.RemoveAllListeners();
        p3Button3.onClick.RemoveAllListeners();

        Debug.Log("spawnei part 3 end");
        yield return new WaitForSeconds(0.5f);

        FlavorManager.fm.ShowHidePanel(optionPanel3, false);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = str;
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate {  End(); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
    }

    private void End()
    {
        hintButton.SetActive(false);
        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = totalCoins.ToString();

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Tudo feito por hoje! Até a proxima!";

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }
}