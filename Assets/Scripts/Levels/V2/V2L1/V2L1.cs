using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V2L1 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 10;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel1, optionPanel2, optionPanel3;
    public Button p2Button1, p2Button2;
    public TextMeshProUGUI p2Button1Text, p2Button2Text;
    public Button p3Button1, p3Button2;
    public TextMeshProUGUI p3Button1Text, p3Button2Text;

    public Image house1, house2;
    public Image pump1, pump2;
    public Image water1, water2;
    public Image corn1, corn2;
    public Image cereal1, cereal2;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public bool water;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        AudioManager.am.PlayVoice(AudioManager.am.v2start[0]);
        totalCoinsText.text = totalCoins.ToString();

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você tem dez reais para começar o seu percurso. Serão três etapas para seguir: Escolher o local da casa," +
            " escolher a melhor forma de ter água nesta casa e decidir a melhor forma de ter arroz.";
        infoButton.onClick.AddListener(delegate 
        { 
            FlavorManager.fm.ShowHidePanel(infoPanel, false); 
            FlavorManager.fm.ShowHidePanel(optionPanel1, true);
            AudioManager.am.voiceChannel.Stop();
        });
    }

    public void Part1(int id)
    {
        Choice(id, house1, house2, 5, 4);
        totalCoinsText.text = totalCoins.ToString();
        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        StartCoroutine(Part2(id)); 
    }

    private IEnumerator Part2(int id)
    {
        yield return new WaitForSeconds(3);

        if (id == 1)
        {
            conceitos.Add("31", 3);
            conceitos.Add("1", 2);
            conceitos.Add("32", 3);
            conceitos.Add("19", 1);
            conceitos.Add("25", 3);
            conceitos.Add("23", 2);
            conceitos.Add("34", 1);

            p2Button1Text.text = "2";
            p2Button2Text.text = "3";
        }
        else if (id == 2)
        {
            conceitos.Add("31", 1);
            conceitos.Add("1", 1);
            conceitos.Add("32", 2);
            conceitos.Add("19", 0);
            conceitos.Add("25", 2);
            conceitos.Add("23", 1);
            conceitos.Add("34", 1);

            p2Button1Text.text = "4";
            p2Button2Text.text = "3";
        }

        FlavorManager.fm.ShowHidePanel(optionPanel2, true);

        p2Button1.onClick.AddListener(delegate
        {
            conceitos["31"] += 3;
            conceitos["1"] += 2;
            conceitos["32"] += 3;
            conceitos["19"] += 2;
            conceitos["25"] += 3;
            conceitos["23"] += 2;
            conceitos["34"] += 1;

            water = true;
            Choice(id, pump1, pump2, 2, 4);
            StartCoroutine(Part3(id));
        });
        p2Button2.onClick.AddListener(delegate
        {
            conceitos["31"] += 0;
            conceitos["1"] += 0;
            conceitos["32"] += 1;
            conceitos["19"] += 0;
            conceitos["25"] += 1;
            conceitos["23"] += 1;
            conceitos["34"] += 1;

            Choice(id, water1, water2, 3, 3);
            StartCoroutine(Part3(id));
        });
    }

    private IEnumerator Part3(int id)
    {
        totalCoinsText.text = totalCoins.ToString();

        if (id == 1)
        {
            p3Button1Text.text = "1";
            p3Button2Text.text = "2";
        }
        else if (id == 2)
        {
            p3Button1Text.text = "1";
            p3Button2Text.text = "2";
        }

        FlavorManager.fm.ShowHidePanel(optionPanel2, false);
        yield return new WaitForSeconds(3);
        FlavorManager.fm.ShowHidePanel(optionPanel3, true);

        p3Button1.onClick.AddListener(delegate
        {
            conceitos["31"] += 2;
            conceitos["1"] += 2;
            conceitos["32"] += 2;
            conceitos["19"] += 2;
            conceitos["25"] += 3;
            conceitos["23"] += 2;
            conceitos["34"] += 2;

            Choice(id, corn1, corn2, 1, 1);
            StartCoroutine(End());
        });
        p3Button2.onClick.AddListener(delegate
        {
            conceitos["31"] += 1;
            conceitos["1"] += 0;
            conceitos["32"] += 1;
            conceitos["19"] += 0;
            conceitos["25"] += 1;
            conceitos["23"] += 1;
            conceitos["34"] += 1;

            Choice(id, cereal1, cereal2, 2, 2);
            StartCoroutine(End());
        });
    }

    private void Choice(int id, Image image1, Image image2, int price1, int price2)
    {
        if (id == 1)
        {
            image1.enabled = true;
            totalCoins -= price1;
            StartCoroutine(FlavorManager.fm.SpawnBucksPosition(price1, coinSpawn, coinWaypoint));
        }
        else if (id == 2)
        {
            image2.enabled = true;
            totalCoins -= price2;
            StartCoroutine(FlavorManager.fm.SpawnBucksPosition(price2, coinSpawn, coinWaypoint));
        }
    }

    private IEnumerator End()
    {
        APIManager.am.Relatorio(conceitos);

        totalCoinsText.text = totalCoins.ToString();
        FlavorManager.fm.ShowHidePanel(optionPanel3, false);

        yield return new WaitForSeconds(3);

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        if (totalCoins > 1) resultText.text = "Restaram " + totalCoins + " reais!";
        else if (totalCoins == 1) resultText.text = "Restou 1 real!";
        else resultText.text = "Restaram 0 reais!";

        if (water)
        {
            totalCoins += 4;
            resultText.text += "\n\nBomba d'água: Bônus de 4 reais.";
        }

        resultText.text += "\n\nAté a próxima!";

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = totalCoins.ToString();

        StartCoroutine(FlavorManager.fm.SpawnBucks(totalCoins));
        PlayerManager.pm.AddCoins(totalCoins);
        PlayerManager.pm.AddLevel();
    }
}