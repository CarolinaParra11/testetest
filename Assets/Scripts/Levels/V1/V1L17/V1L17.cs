using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V1L17 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject[] panelsArray;
    private List<GameObject> panelsList;
    public GameObject[] itemsArray;
    private List<GameObject> itemsList;
    public int[] priceArray;
    private List<int> priceList;
    private int counter = 0;

    public int income;
    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("25", 0);
        conceitos.Add("1", 0);
        conceitos.Add("2", 0);
        conceitos.Add("22", 0);
        conceitos.Add("20", 0);
        conceitos.Add("24", 0);
        conceitos.Add("7", 0);
        conceitos.Add("31", 0);
        conceitos.Add("34", 0);
        conceitos.Add("18", 0);

        conceitos["25"] += 0;
        conceitos["1"] += 0;
        conceitos["2"] += 0;
        conceitos["22"] += 0;
        conceitos["20"] += 0;
        conceitos["24"] += 0;
        conceitos["7"] += 0;
        conceitos["31"] += 0;
        conceitos["34"] += 0;
        conceitos["18"] += 0;

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[16]);
        infoText.text = "Oh, não! Está chovendo muito forte, e você não irá conseguir sair de casa por alguns dias. Veja se os itens que comprou para a sua casa te mantêm bem durante os dias que tiver que ficar sem sair!";
        infoButton.onClick.AddListener(delegate 
        { 
            Part1(); 
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
        });

        panelsList = new List<GameObject>();
        panelsList.AddRange(panelsArray);
        itemsList = new List<GameObject>();
        itemsList.AddRange(itemsArray);
        priceList = new List<int>();
        priceList.AddRange(priceArray);
    }

    private void Part1()
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        if (counter < PlayerManager.pm.v1l16choices.Count)
        {
            if (priceList[PlayerManager.pm.v1l16choices[counter]] > 0) StartCoroutine(FlavorManager.fm.SpawnCoin(priceList[PlayerManager.pm.v1l16choices[counter]]));
            income += priceList[PlayerManager.pm.v1l16choices[counter]];
            PlayerManager.pm.AddCoins(priceList[PlayerManager.pm.v1l16choices[counter]]);

            FlavorManager.fm.ShowHidePanel(panelsList[PlayerManager.pm.v1l16choices[counter]], true);
            FlavorManager.fm.ShowHidePanel(itemsList[PlayerManager.pm.v1l16choices[counter]], true);
            AudioManager.am.PlayVoice(AudioManager.am.v1l17[PlayerManager.pm.v1l16choices[counter]]);

            Button button = panelsList[PlayerManager.pm.v1l16choices[counter]].transform.Find("NextButton").GetComponent<Button>();
            button.onClick.AddListener(delegate
            {
                FlavorManager.fm.ShowHidePanel(panelsList[PlayerManager.pm.v1l16choices[counter]], false);
                counter++;
                Part1();
            });
        }
        else End();
    }

    private void End()
    {
        foreach(int i in PlayerManager.pm.v1l16choices)
        {
            if (i == 1 || i == 5 || i == 7 || i == 8)
            {
                conceitos["25"] += 5;
                conceitos["1"] += 4;
                conceitos["2"] += 3;
                conceitos["22"] += 4;
                conceitos["20"] += 3;
                conceitos["24"] += 3;
                conceitos["7"] += 4;
                conceitos["31"] += 5;
                conceitos["34"] += 3;
                conceitos["18"] += 3;
            }
            else if (i == 2 || i == 6 || i == 9)
            {
                conceitos["25"] += 3;
                conceitos["1"] += 3;
                conceitos["2"] += 2;
                conceitos["22"] += 2;
                conceitos["20"] += 2;
                conceitos["24"] += 2;
                conceitos["7"] += 2;
                conceitos["31"] += 2;
                conceitos["34"] += 3;
                conceitos["18"] += 2;
            }
            else if (i == 3 || i == 4)
            {
                conceitos["25"] += 2;
                conceitos["1"] += 1;
                conceitos["2"] += 1;
                conceitos["22"] += 0;
                conceitos["20"] += 0;
                conceitos["24"] += 1;
                conceitos["7"] += 1;
                conceitos["31"] += 0;
                conceitos["34"] += 3;
                conceitos["18"] += 0;
            }
        }

        APIManager.am.Relatorio(conceitos);

        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Parabéns! Vamos para a próxima!";
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