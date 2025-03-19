using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L16b : MonoBehaviour
{
    private V2L16Helper v2l16Helper;

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

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;
    public int totalCoins;

    private void Start()
    {
        v2l16Helper = GameObject.Find("V2L16Helper").GetComponent<V2L16Helper>();

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[16]);
        infoText.text = "Oh, não! Está chovendo muito forte, e você não irá conseguir sair de casa por alguns dias. Veja se os itens que comprou para a sua casa te mantêm bem durante os dias que tiver que ficar sem sair!";
        infoButton.onClick.AddListener(delegate { Part1(); FlavorManager.fm.ShowHidePanel(infoPanel, false); });

        panelsList = new List<GameObject>();
        panelsList.AddRange(panelsArray);
        itemsList = new List<GameObject>();
        itemsList.AddRange(itemsArray);
        priceList = new List<int>();
        priceList.AddRange(priceArray);   
    }

    private void Part1()
    {
        AudioManager.am.voiceChannel.Stop();
        if (counter < v2l16Helper.choices.Count)
        {
            if (priceList[v2l16Helper.choices[counter]] > 0) StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            PlayerManager.pm.AddCoins(priceList[v2l16Helper.choices[counter]]);
            totalCoins += priceList[v2l16Helper.choices[counter]];

            FlavorManager.fm.ShowHidePanel(panelsList[v2l16Helper.choices[counter]], true);
            FlavorManager.fm.ShowHidePanel(itemsList[v2l16Helper.choices[counter]], true);

            Button button = panelsList[v2l16Helper.choices[counter]].transform.Find("NextButton").GetComponent<Button>();
            button.onClick.AddListener(delegate 
            { 
                FlavorManager.fm.ShowHidePanel(panelsList[v2l16Helper.choices[counter]], false); 
                counter++; 
                Part1(); 
            });
        }
        else End();
    }

    private void End()
    {
        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = totalCoins.ToString();

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Parabéns! Vamos para a próxima!";
        PlayerManager.pm.AddLevel();
    }
}
