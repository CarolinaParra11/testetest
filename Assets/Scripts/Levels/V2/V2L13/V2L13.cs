using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V2L13 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject optionPanel;
    public Button ensuranceButton, bolodimButton;
    public TextMeshProUGUI panelText;

    public RectTransform coinSpawn, coinWaypoint;

    public GameObject cutscene;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Hoje é dia de ir até a loja de brinquedos para ver se conseguimos o Sonho 2!";
        infoButton.onClick.AddListener(delegate
        {
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
            StartCoroutine(PartA());
        });
    }

    public IEnumerator PartA()
    {
        // Declaração
        conceitos.Add("35", 0);
        conceitos.Add("23", 0);
        conceitos.Add("20", 0);
        conceitos.Add("28", 0);
        conceitos.Add("33", 0);
        conceitos.Add("60", 0);
        conceitos.Add("25", 0);

        conceitos["35"] += 2;
        conceitos["23"] += 3;
        conceitos["20"] += 3;
        conceitos["28"] += 3;
        conceitos["33"] += 2;
        conceitos["60"] += 2;
        conceitos["25"] += 2;

        cutscene.SetActive(true);
        yield return new WaitForSeconds(8);
        
        FlavorManager.fm.ShowHidePanel(optionPanel, true);
        if (PlayerManager.pm.ensurance)
        {
            conceitos["35"] += 3;
            conceitos["23"] += 4;
            conceitos["20"] += 4;
            conceitos["28"] += 5;
            conceitos["33"] += 4;
            conceitos["60"] += 3;
            conceitos["25"] += 2;

            panelText.text = "Como você tem um plano de saúde, poderá fazer o tratamento sem ter gasto algum!";
            ensuranceButton.gameObject.SetActive(true);
            ensuranceButton.onClick.AddListener(delegate { StartCoroutine(End()); ensuranceButton.onClick.RemoveAllListeners(); });
        }
        else
        {
            conceitos["35"] += 1;
            conceitos["23"] += 2;
            conceitos["20"] += 2;
            conceitos["28"] += 2;
            conceitos["33"] += 2;
            conceitos["60"] += 1;
            conceitos["25"] += 2;

            panelText.text = "Como você não tem um plano de saúde, terá que retirar dinheiro do seu Bolodix para o tratamento.";
            bolodimButton.gameObject.SetActive(true);
            bolodimButton.onClick.AddListener(delegate 
            {
                StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));
                PlayerManager.pm.RemoveCoins(120);
                StartCoroutine(End());
                bolodimButton.onClick.RemoveAllListeners();
            });
        }
    }

    public IEnumerator End()
    {
        APIManager.am.Relatorio(conceitos);
        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Tudo acabou bem! Que ótimo! Agora vamos ver se você conquistou o presente do Sonho 2!";
        infoButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L13b"); });
    }
}