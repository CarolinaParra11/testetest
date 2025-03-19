using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L19 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 50;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject cutscene, gameplay;
 //   public Button cutsceneEnd;
    public Button cutEnd;

    public Button submitButton;
    public Button[] button;
    public GameObject[] price;
    public GameObject[] selected;

    public int income;
    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        // Declaração
        conceitos.Add("1", 0);
        conceitos.Add("7", 0);
        conceitos.Add("10", 0);
        conceitos.Add("23", 0);
        conceitos.Add("44", 0);
        conceitos.Add("20", 0);
        conceitos.Add("24", 0);
        conceitos.Add("31", 0);
        conceitos.Add("16", 0);
        conceitos.Add("22", 0);

        conceitos["1"] += 0;
        conceitos["7"] += 0;
        conceitos["10"] += 0;
        conceitos["23"] += 0;
        conceitos["44"] += 0;
        conceitos["20"] += 0;
        conceitos["24"] += 0;
        conceitos["31"] += 0;
        conceitos["16"] += 0;
        conceitos["22"] += 0;

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você ganhou um novo cachorro!\n\n" +
            "Você foi até o pet shop comprar o que precisava e encontrou o seu amigo Léo!\n" +
            "Ele também tem um cachorro, olha só o que ele já tem:";
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); });

        cutEnd.gameObject.SetActive(false);

    }

    private IEnumerator Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        cutscene.SetActive(true);

        yield return new WaitForSeconds(5.5f);

    //    cutsceneEnd.gameObject.SetActive(true);
      //  cutsceneEnd.onClick.AddListener(delegate { StartCoroutine(Part2Start()); cutsceneEnd.onClick.RemoveAllListeners();  cutsceneEnd.gameObject.SetActive(false); });
    
        cutEnd.gameObject.SetActive(true);
        cutEnd.onClick.AddListener(delegate { StartCoroutine(Part2Start()); cutEnd.onClick.RemoveAllListeners();  cutEnd.gameObject.SetActive(false); });
    
    
    }

    private IEnumerator Part2Start()
    {
        cutscene.GetComponent<Animator>().SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        cutscene.SetActive(false);
        SetButtons(false);
        totalCoinsText.text = totalCoins.ToString();
        AudioManager.am.PlayVoice(AudioManager.am.v2start[18]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Dia do pet shop: Escolha as melhores opções para a primeira compra do seu pet.";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { Part2(); AudioManager.am.PlaySFX(AudioManager.am.button); AudioManager.am.voiceChannel.Stop(); infoButton.onClick.RemoveAllListeners(); });
    }

    private void Part2()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        gameplay.SetActive(true);
        SetButtons(true);
        button[0].onClick.AddListener(delegate { PanelButton(this, 20, price[0], selected[0]); });
        button[1].onClick.AddListener(delegate { PanelButton(this, 10, price[1], selected[1]); });
        button[2].onClick.AddListener(delegate { PanelButton(this, 10, price[2], selected[2]); });
        button[3].onClick.AddListener(delegate { PanelButton(this, 10, price[3], selected[3]); });
        button[4].onClick.AddListener(delegate { PanelButton(this, 20, price[4], selected[4]); });
        button[5].onClick.AddListener(delegate { PanelButton(this, 10, price[5], selected[5]); });
        button[6].onClick.AddListener(delegate { PanelButton(this, 20, price[6], selected[6]); });
        submitButton.onClick.AddListener(delegate { End(); });
    }

    private void End()
    {
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        if (selected[0].activeSelf) { resultText.text += "Ração: Ganhou 20 reais!\n"; income += 20; }
        if (selected[1].activeSelf) { resultText.text += "Lenço: Ganhou 0 reais!\n"; }
        if (selected[2].activeSelf) { resultText.text += "Prato: Ganhou 20 reais!\n"; income += 20; }
        if (selected[3].activeSelf) { resultText.text += "Roupa: Ganhou 0 reais!\n"; }
        if (selected[4].activeSelf) { resultText.text += "Escova: Ganhou 10 reais!\n"; income += 10; }
        if (selected[5].activeSelf) { resultText.text += "Coleira: Ganhou 20 reais!\n"; income += 20; }
        if (selected[6].activeSelf) { resultText.text += "Casa: Ganhou 20 reais!\n"; income += 20; }
        resultText.text += "\nTudo feito por hoje! Até a próxima!";

        if (income >= 60)
        {
            conceitos["1"] += 6;
            conceitos["7"] += 6;
            conceitos["10"] += 6;
            conceitos["23"] += 1;
            conceitos["44"] += 5;
            conceitos["20"] += 5;
            conceitos["24"] += 5;
            conceitos["31"] += 6;
            conceitos["16"] += 1;
            conceitos["22"] += 6;
        }
        else if (income == 50)
        {
            conceitos["1"] += 5;
            conceitos["7"] += 5;
            conceitos["10"] += 5;
            conceitos["23"] += 1;
            conceitos["44"] += 4;
            conceitos["20"] += 4;
            conceitos["24"] += 4;
            conceitos["31"] += 5;
            conceitos["16"] += 1;
            conceitos["22"] += 5;
        }
        else if (income == 40)
        {
            conceitos["1"] += 4;
            conceitos["7"] += 4;
            conceitos["10"] += 4;
            conceitos["23"] += 1;
            conceitos["44"] += 3;
            conceitos["20"] += 3;
            conceitos["24"] += 3;
            conceitos["31"] += 4;
            conceitos["16"] += 1;
            conceitos["22"] += 4;
        }
        else if (income == 30)
        {
            conceitos["1"] += 3;
            conceitos["7"] += 3;
            conceitos["10"] += 3;
            conceitos["23"] += 0;
            conceitos["44"] += 2;
            conceitos["20"] += 2;
            conceitos["24"] += 2;
            conceitos["31"] += 3;
            conceitos["16"] += 1;
            conceitos["22"] += 3;
        }
        else if (income == 20)
        {
            conceitos["1"] += 2;
            conceitos["7"] += 2;
            conceitos["10"] += 2;
            conceitos["23"] += 0;
            conceitos["44"] += 1;
            conceitos["20"] += 2;
            conceitos["24"] += 2;
            conceitos["31"] += 2;
            conceitos["16"] += 1;
            conceitos["22"] += 2;
        }
        else if (income == 10)
        {
            conceitos["1"] += 1;
            conceitos["7"] += 1;
            conceitos["10"] += 1;
            conceitos["23"] += 0;
            conceitos["44"] += 1;
            conceitos["20"] += 1;
            conceitos["24"] += 1;
            conceitos["31"] += 1;
            conceitos["16"] += 1;
            conceitos["22"] += 1;
        }
        else
        {
            conceitos["1"] += 0;
            conceitos["7"] += 0;
            conceitos["10"] += 0;
            conceitos["23"] += 0;
            conceitos["44"] += 0;
            conceitos["20"] += 0;
            conceitos["24"] += 0;
            conceitos["31"] += 0;
            conceitos["16"] += 1;
            conceitos["22"] += 0;
        }

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = income.ToString();

        PlayerManager.pm.AddCoins(income);
        StartCoroutine(FlavorManager.fm.SpawnBucks(5));

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
        SetButtons(false);
    }

    static void PanelButton(V2L19 instance, int price, GameObject priceObject, GameObject selectedObject)
    {
        if (priceObject.activeSelf)
        {
            if (instance.totalCoins >= price)
            {
                instance.totalCoins -= price;
                instance.totalCoinsText.text = instance.totalCoins.ToString();
                priceObject.SetActive(false);
                selectedObject.SetActive(true);
            }
            else
            {
                FlavorManager.fm.ShowHidePanel(instance.infoPanel, true);
                instance.infoText.text = "Você não tem dinheiro suficiente, escolha outro ou remova algum já escolhido!";
                instance.infoButton.onClick.RemoveAllListeners();
                instance.infoButton.onClick.AddListener(delegate { AudioManager.am.PlaySFX(AudioManager.am.button); FlavorManager.fm.ShowHidePanel(instance.infoPanel, false); });
            }
        }
        else
        {
            instance.totalCoins += price;
            instance.totalCoinsText.text = instance.totalCoins.ToString();
            priceObject.SetActive(true);
            selectedObject.SetActive(false);
        }

        if (instance.totalCoins == 0) instance.submitButton.gameObject.SetActive(true);
        else instance.submitButton.gameObject.SetActive(false);
    }

    private void SetButtons(bool value)
    {
        foreach (Button b in button) b.enabled = value;
    }
}