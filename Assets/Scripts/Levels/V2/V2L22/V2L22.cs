using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V2L22 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 120;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button button1, button2, button3, button4;
    public GameObject cart1, cart2, cart3, cart4;
    public GameObject adult1, adult2, adult3, adult4;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("13", 0);
        conceitos.Add("31", 0);
        conceitos.Add("10", 0);
        conceitos.Add("2", 0);
        conceitos.Add("1", 0);
        conceitos.Add("20", 0);
        conceitos.Add("19", 0);
        conceitos.Add("55", 0);

        conceitos["13"] += 0;
        conceitos["31"] += 0;
        conceitos["10"] += 0;
        conceitos["2"] += 0;
        conceitos["1"] += 0;
        conceitos["20"] += 0;
        conceitos["19"] += 0;
        conceitos["55"] += 0;

        StartCoroutine(Part1Start());
    }

    private IEnumerator Part1Start()
    {
        yield return new WaitForSeconds(4);
       // AudioManager.am.PlayVoice(AudioManager.am.v2start[21]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Férias de julho! Você recebeu 120 reais das férias do seu trabalho escolhido.\n\n Agora, escolha a melhor maneira de usar o dinheiro e ganhar mais, pois a concorrência aumentou.\n\n Pense em uma estratégia para aumentar as vendas!";
        infoButton.onClick.AddListener(delegate { Part1(); });
    }

    private void Part1()
    {
        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        button1.onClick.AddListener(delegate 
        {
            conceitos["13"] += 2;
            conceitos["31"] += 1;
            conceitos["10"] += 3;
            conceitos["2"] += 3;
            conceitos["1"] += 3;
            conceitos["20"] += 3;
            conceitos["19"] += 2;
            conceitos["55"] += 3;

            StartCoroutine(End(120, 240, "Parabéns! apesar de ter procura no verão, a pizza também é muito consumida no inverno!\n " + "\n O milho também seria uma boa opção, mas, desta vez, a pizza foi a melhor escolha, pois você dobrou suas vendas! Com dois carrinhos, você faturou 240 reais."));
        });
        button2.onClick.AddListener(delegate 
        {
            conceitos["13"] += 5;
            conceitos["31"] += 6;
            conceitos["10"] += 8;
            conceitos["2"] += 10;
            conceitos["1"] += 8;
            conceitos["20"] += 8;
            conceitos["19"] += 6;
            conceitos["55"] += 7;

            StartCoroutine(End(110, 160, "Parabéns! Essa foi a terceira melhor opção.\n \n Apesar de a demanda aumentar bastante no inverno, a pizza ainda era a melhor escolha, pois você dobraria suas vendas com dois carrinhos.\n \n O lanche teria sido uma opção um pouco melhor que o milho, não pelas vendas, mas pelo custo mais baixo. \n\n Mesmo assim, por ser um alimento popular, você conseguiu vender bem e faturou 160 reais!")); 
        });
        button3.onClick.AddListener(delegate 
        {
            conceitos["13"] += 3;
            conceitos["31"] += 2;
            conceitos["10"] += 5;
            conceitos["2"] += 5;
            conceitos["1"] += 4;
            conceitos["20"] += 4;
            conceitos["19"] += 4;
            conceitos["55"] += 4;

            StartCoroutine(End(90, 110, "Você vendeu alguns algodões-doces! Porém, desta vez, a concorrência estava forte.\n \nA pizza teria sido a melhor escolha, pois você poderia dobrar as vendas com dois carrinhos.\n \n Além disso, a barraca de lanches era uma opção mais acessível em comparação à sua. \n\nMesmo assim, você conseguiu vender e faturou 110 reais!")); 
        });
        button4.onClick.AddListener(delegate 
        {
            conceitos["13"] += 4;
            conceitos["31"] += 4;
            conceitos["10"] += 6;
            conceitos["2"] += 7;
            conceitos["1"] += 7;
            conceitos["20"] += 5;
            conceitos["19"] += 5;
            conceitos["55"] += 4;

            StartCoroutine(End(70, 135, " Parabéns! Essa foi a segunda melhor opção.\n \n A barraca de lanches não era tão cara, custando 70 reais, mas, com a pizza, você poderia dobrar as vendas e lucrar mais. \n \nMesmo assim, você vendeu bem, faturou 135 reais e ainda economizou parte do dinheiro que usaria para comprar a barraca!")); 
        });
    }

    private IEnumerator End(int price, int bonus, string resutText)
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        button4.onClick.RemoveAllListeners();

        totalCoins -= price;
        totalCoinsText.text = totalCoins.ToString();

       // slotImage.sprite = image.sprite; slotImage.gameObject.SetActive(true);

        if (bonus == 30) { cart1.SetActive(true); adult1.SetActive(true);  }
        else if (bonus == 160) { cart2.SetActive(true); adult1.SetActive(true); adult2.SetActive(true); adult3.SetActive(true); adult4.SetActive(true); }
        else if (bonus == 110) { cart3.SetActive(true); adult1.SetActive(true); adult2.SetActive(true); }
        else if (bonus == 130) { cart4.SetActive(true); adult1.SetActive(true); adult2.SetActive(true); adult3.SetActive(true);}

        StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));
        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        yield return new WaitForSeconds(4);

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = bonus.ToString();

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        StartCoroutine(FlavorManager.fm.SpawnBucks(5));

        resultText.text = resutText;
        resultText.text += "\n\nTudo feito por hoje! Até a próxima!";

        PlayerManager.pm.AddCoins(bonus);
        PlayerManager.pm.AddLevel();

        APIManager.am.Relatorio(conceitos);
    }
}