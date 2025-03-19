using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V1L26 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 5;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button button1, button2, button3;
    public GameObject cart1, cart2, cart3 ;
    public GameObject adult1, adult2, adult3, adult4;

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

        conceitos["13"] += 0;
        conceitos["31"] += 0;
        conceitos["10"] += 0;
        conceitos["2"] += 0;
        conceitos["1"] += 0;
        conceitos["20"] += 0;

        StartCoroutine(Part1Start());
    }

    private IEnumerator Part1Start()
    {
        totalCoinsText.text = totalCoins.ToString();
        yield return new WaitForSeconds(3);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[25]);
        infoText.text = "Férias de Julho! Você recebeu 5 moedas. Escolha a melhor maneira de usá-las e ganhar mais.";
        infoButton.onClick.AddListener(delegate { Part1(); AudioManager.am.voiceChannel.Stop(); });
    }

    private void Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        button1.onClick.AddListener(delegate { StartCoroutine(End(5, 2, AudioManager.am.v1l26[0],
            "Você ganhou só 2 moedas," +
            " pois estamos no inverno e a venda de picolés não foi boa." +
            " Por isso, alguns deles estragaram."));

            conceitos["13"] += 2;
            conceitos["31"] += 2;
            conceitos["10"] += 1;
            conceitos["2"] += 3;
            conceitos["1"] += 1;
            conceitos["20"] += 2;

        });
        button2.onClick.AddListener(delegate { StartCoroutine(End(5, 9, AudioManager.am.v1l26[1],
            "Você ganhou 9 moedas. O milho foi um sucesso!" +
            " Estamos no inverno, é uma época de colheita de milho," +
            " e as pessoas gostam de comer um alimento mais quente." +
            " Você conseguiu vender bastante, e gerou muitas moedas. Veja seu Bolodix!"));

            conceitos["13"] += 6;
            conceitos["31"] += 6;
            conceitos["10"] += 5;
            conceitos["2"] += 6;
            conceitos["1"] += 7;
            conceitos["20"] += 6;

        });
        button3.onClick.AddListener(delegate { StartCoroutine(End(5, 7, AudioManager.am.v1l26[2],
            "Você ganhou 7 moedas! Estamos no inverno," +
            " as pessoas gostam de comer pizza nesta época!\n\n" +
            " Outras opções, como bebidas quentes, pão de queijo e milho," +
            " poderiam ser melhores para este momento." +
            " Mas você conseguiu recuperar o que usou e ainda ganhou 2 moedas a mais!"));

            conceitos["13"] += 4;
            conceitos["31"] += 5;
            conceitos["10"] += 3;
            conceitos["2"] += 5;
            conceitos["1"] += 4;
            conceitos["20"] += 4;

        });
    }

    private IEnumerator End(int price, int bonus, AudioClip clip , string resutText)
    {
        APIManager.am.Relatorio(conceitos);
        AudioManager.am.PlaySFX(AudioManager.am.button);
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        totalCoins -= price;
        totalCoinsText.text = totalCoins.ToString();

        if (bonus == 2) { cart1.SetActive(true); adult1.SetActive(true); }
        else if (bonus == 9) { cart2.SetActive(true); adult1.SetActive(true); adult2.SetActive(true); adult3.SetActive(true); adult4.SetActive(true); }
        else if (bonus == 7) { cart3.SetActive(true); adult1.SetActive(true); adult2.SetActive(true); }

        StartCoroutine(FlavorManager.fm.SpawnCoinPosition(price, coinSpawn, coinWaypoint));

        FlavorManager.fm.ShowHidePanel(optionPanel, false);

        yield return new WaitForSeconds(3);

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        AudioManager.am.PlayVoice(clip);
        resultText.text = resutText;

        if (bonus > 0)
        {
            FlavorManager.fm.ShowHidePanel(incomePanel, true);

            int counter = 0;

            while (counter < bonus)
            {
                coinsPanel.GetChild(counter).gameObject.SetActive(true);
                counter++;
            }
        }

        StartCoroutine(FlavorManager.fm.SpawnCoin(bonus));
        PlayerManager.pm.AddCoins(bonus);
        PlayerManager.pm.AddLevel();
    }
}