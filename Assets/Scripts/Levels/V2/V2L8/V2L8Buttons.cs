using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class V2L8Buttons : MonoBehaviour
{
    private V2L8Helper v2l8Helper;

    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins;
    private int value1, value2, value3;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button button1, button2, button3;
    public GameObject price1, price2, price3;
    public GameObject selected1, selected2, selected3;
    public Button submitButton;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        v2l8Helper = GameObject.Find("V2L8Helper").GetComponent<V2L8Helper>();
        totalCoins = v2l8Helper.coins;
        totalCoinsText.text = v2l8Helper.coins.ToString();

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você tem " + totalCoins + " reais para comprar nos três mercados!";
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); infoButton.onClick.RemoveAllListeners(); });

        // Declaração
        conceitos.Add("23", 0);
        conceitos.Add("34", 0);
        conceitos.Add("2", 0);
        conceitos.Add("20", 0);
        conceitos.Add("1", 0);
        conceitos.Add("7", 0);
        conceitos.Add("24", 0);
        conceitos.Add("9", 0);
        conceitos.Add("5", 0);
        conceitos.Add("49", 0);
        conceitos.Add("44", 0);

        conceitos["23"] += 0;
        conceitos["34"] += 0;
        conceitos["2"] += 0;
        conceitos["20"] += 0;
        conceitos["1"] += 0;
        conceitos["7"] += 0;
        conceitos["24"] += 0;
        conceitos["9"] += 0;
        conceitos["5"] += 0;
        conceitos["49"] += 0;
        conceitos["44"] += 0;

    }

    private IEnumerator Part1()
    {
        if (v2l8Helper.level == 1) { value1 = 7; value2 = 7; value3 = 9; }
        if (v2l8Helper.level == 2) { value1 = 9; value2 = 7; value3 = 7; }
        if (v2l8Helper.level == 3) { value1 = 9; value2 = 6; value3 = 9; }

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        button1.onClick.AddListener(delegate { PanelButtonOneOption(this, value1, price1, selected1, selected2, selected3, submitButton.gameObject); });
        button2.onClick.AddListener(delegate { PanelButtonOneOption(this, value2, price2, selected2, selected1, selected3, submitButton.gameObject); });
        button3.onClick.AddListener(delegate { PanelButtonOneOption(this, value3, price3, selected3, selected1, selected2, submitButton.gameObject); });

        submitButton.onClick.AddListener(delegate { End(); submitButton.onClick.RemoveAllListeners(); });
    }

    private void End()
    {
        v2l8Helper.level++;
        if (selected1.activeSelf) v2l8Helper.coins -= value1;
        if (selected2.activeSelf) v2l8Helper.coins -= value2;
        if (selected3.activeSelf) v2l8Helper.coins -= value3;

        if (v2l8Helper.level < 4)
        {
            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoText.text = "Vamos para as próximas compras!";
            infoButton.onClick.AddListener(delegate { FlavorManager.fm.ShowHidePanel(infoPanel, false); GameManager.gm.LoadScene("V2L8"); infoButton.onClick.RemoveAllListeners(); });
        }
        else
        {
            FlavorManager.fm.ShowHidePanel(resultPanel, true);

            if (totalCoins > 1)
            {
                if(totalCoins == 5)
                {
                    conceitos["23"] += 5;
                    conceitos["34"] += 5;
                    conceitos["2"] += 5;
                    conceitos["20"] += 4;
                    conceitos["1"] += 6;
                    conceitos["7"] += 3;
                    conceitos["24"] += 1;
                    conceitos["9"] += 1;
                    conceitos["5"] += 3;
                    conceitos["49"] += 3;
                    conceitos["44"] += 4;
                }
                else if (totalCoins == 3)
                {
                    conceitos["23"] += 3;
                    conceitos["34"] += 3;
                    conceitos["2"] += 3;
                    conceitos["20"] += 2;
                    conceitos["1"] += 4;
                    conceitos["7"] += 2;
                    conceitos["24"] += 1;
                    conceitos["9"] += 1;
                    conceitos["5"] += 2;
                    conceitos["49"] += 2;
                    conceitos["44"] += 2;
                }
                else if(totalCoins == 2)
                {
                    conceitos["23"] += 2;
                    conceitos["34"] += 2;
                    conceitos["2"] += 2;
                    conceitos["20"] += 2;
                    conceitos["1"] += 3;
                    conceitos["7"] += 1;
                    conceitos["24"] += 1;
                    conceitos["9"] += 1;
                    conceitos["5"] += 1;
                    conceitos["49"] += 1;
                    conceitos["44"] += 1;
                }


                resultText.text = "Sobraram" + totalCoins + "reais! Tudo feito! Até a próxima!";
                PlayerManager.pm.AddCoins(totalCoins);
                StartCoroutine(FlavorManager.fm.SpawnBucks(totalCoins));
            }
            if (totalCoins == 1)
            {
                conceitos["23"] += 1;
                conceitos["34"] += 1;
                conceitos["2"] += 1;
                conceitos["20"] += 1;
                conceitos["1"] += 2;
                conceitos["7"] += 1;
                conceitos["24"] += 1;
                conceitos["9"] += 1;
                conceitos["5"] += 1;
                conceitos["49"] += 1;
                conceitos["44"] += 1;

                resultText.text = "Sobrou 1 real! Tudo feito! Até a próxima!";
                PlayerManager.pm.AddCoins(1);
                StartCoroutine(FlavorManager.fm.SpawnBucks(1));
            }
            else
            {
                conceitos["24"] += 1;
                conceitos["9"] += 1;
                conceitos["5"] += 1;
                conceitos["49"] += 1;
                conceitos["44"] += 1;

                resultText.text = "Tudo feito! Até a próxima!";
            }
            APIManager.am.Relatorio(conceitos);

            PlayerManager.pm.AddLevel();
        }

        StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));
    }

    private static void PanelButtonOneOption(V2L8Buttons instance, int price, GameObject priceObject, GameObject selectedObject, GameObject otherSelectedObject1, GameObject otherSelectedObject2, GameObject pSubmit)
    {
        if (!otherSelectedObject1.activeSelf && !otherSelectedObject2.activeSelf)
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
                    instance.infoText.text = "Você não tem dinheiro suficiente, escolha outro!";
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
        }

        if (selectedObject.activeSelf || otherSelectedObject1.activeSelf || otherSelectedObject2.activeSelf) pSubmit.SetActive(true);
        else pSubmit.SetActive(false);
    }
}