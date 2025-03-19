using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V2L25 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button resultButton;

    private int totalCoins = 150;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button p1Button1, p1Button2, p1Button3;
    public Button p2Button1, p2Button2, p2Button3;
    public Button p3Button1, p3Button2, p3Button3;
    public GameObject p1Price1, p1Price2, p1Price3;
    public GameObject p2Price1, p2Price2, p2Price3;
    public GameObject p3Price1, p3Price2, p3Price3;
    public GameObject p1Selected1, p1Selected2, p1Selected3;
    public GameObject p2Selected1, p2Selected2, p2Selected3;
    public GameObject p3Selected1, p3Selected2, p3Selected3;
    public Button submitButton;

    public int salgado, doce, suco;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        totalCoinsText.text = totalCoins.ToString();

       // AudioManager.am.PlayVoice(AudioManager.am.v2start[24]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Dia do consumo consciente aliado às necessidades. \n \n  Você precisa fazer as compras para sua casa e deve economizar analisando financeiramente. \n \n Compare os três supermercados e escolha aquele que oferece a melhor condição.";
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part1()
    {
        conceitos.Add("31", 0);
        conceitos.Add("2", 0);
        conceitos.Add("1", 0);
        conceitos.Add("20", 0);
        conceitos.Add("19", 0);
        conceitos.Add("55", 0);
        conceitos.Add("23", 0);
        conceitos.Add("75", 0);

        conceitos["31"] += 0;
        conceitos["2"] += 0;
        conceitos["1"] += 0;
        conceitos["20"] += 0;
        conceitos["19"] += 0;
        conceitos["55"] += 0;
        conceitos["23"] += 0;
        conceitos["75"] += 0;

        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        p1Button1.onClick.AddListener(delegate { PanelButton(this, 10, p1Price1, p1Selected1, 1, 0, 0); });
        p1Button2.onClick.AddListener(delegate { PanelButton(this, 26, p1Price2, p1Selected2, 0, 3, 0); });
        p1Button3.onClick.AddListener(delegate { PanelButton(this, 22, p1Price3, p1Selected3, 0, 0, 2); });

        p2Button1.onClick.AddListener(delegate { PanelButton(this, 18, p2Price1, p2Selected1, 2, 0, 0); });
        p2Button2.onClick.AddListener(delegate { PanelButton(this, 18, p2Price2, p2Selected2, 0, 2, 0); });
        p2Button3.onClick.AddListener(delegate { PanelButton(this, 14, p2Price3, p2Selected3, 0, 0, 1); });


        p3Button1.onClick.AddListener(delegate { PanelButton(this, 9, p3Price1, p3Selected1, 1, 0, 0); });
        p3Button2.onClick.AddListener(delegate { PanelButton(this, 9, p3Price2, p3Selected2, 0, 1, 0); });
        p3Button3.onClick.AddListener(delegate { PanelButton(this, 24, p3Price3, p3Selected3, 0, 0, 3); });

        submitButton.onClick.AddListener(delegate { End(); submitButton.onClick.RemoveAllListeners(); });
    }

    private void End()
    {
        if (totalCoins == 20)
        {
            conceitos["31"] += 4;
            conceitos["2"] += 4;
            conceitos["1"] += 5;
            conceitos["20"] += 6;
            conceitos["19"] += 5;
            conceitos["55"] += 7;
            conceitos["23"] += 8;
            conceitos["75"] += 10;
        }
        else if (totalCoins == 10)
        {
            conceitos["31"] += 2;
            conceitos["2"] += 2;
            conceitos["1"] += 3;
            conceitos["20"] += 5;
            conceitos["19"] += 4;
            conceitos["55"] += 5;
            conceitos["23"] += 5;
            conceitos["75"] += 8;
        }
        else if (totalCoins == 0)
        {
            conceitos["31"] += 0;
            conceitos["2"] += 0;
            conceitos["1"] += 1;
            conceitos["20"] += 2;
            conceitos["19"] += 2;
            conceitos["55"] += 2;
            conceitos["23"] += 3;
            conceitos["75"] += 2;
        }

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Tudo comprado para a festa!\n\nSobraram " + totalCoins + " reais!\n\nVamos para a festa!";
        resultButton.onClick.RemoveAllListeners();
        resultButton.onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            GameManager.gm.LoadScene("V2L25a");
        });

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = totalCoins.ToString();

        StartCoroutine(FlavorManager.fm.SpawnBucks(5)); 
        PlayerManager.pm.AddCoins(totalCoins);

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }

    static void PanelButton(V2L25 instance, int price, GameObject priceObject, GameObject selectedObject, int salgado, int doce, int suco)
    {
        if (priceObject.activeSelf)
        {
            if (instance.totalCoins >= price)
            {
                instance.totalCoins -= price;
                instance.totalCoinsText.text = instance.totalCoins.ToString();
                priceObject.SetActive(false);
                selectedObject.SetActive(true);
                instance.salgado += salgado;
                instance.doce += doce;
                instance.suco += suco;
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
            instance.salgado -= salgado;
            instance.doce -= doce;
            instance.suco -= suco;
        }

        if (instance.salgado >= 3 && instance.doce >= 3 && instance.suco >= 2) instance.submitButton.gameObject.SetActive(true);
        else instance.submitButton.gameObject.SetActive(false);
    }
}