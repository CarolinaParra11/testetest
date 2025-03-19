using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V2L27 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button resultButton;

    public GameObject giftA, giftB, giftC;
    public TextMeshProUGUI counterText;
    public GameObject lidClosed, lidOpen;

    public GameObject bankPanel;
    public TextMeshProUGUI bankText;
    public Button bankButton;

    public int income;
    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("23", 5);
        conceitos.Add("20", 5);
        conceitos.Add("44", 5);
        conceitos.Add("1", 5);
        conceitos.Add("32", 6);
        conceitos.Add("34", 5);
        conceitos.Add("76", 2);

        AudioManager.am.PlayVoice(AudioManager.am.v2start[26]);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Hoje é dia de retornar ao banco após 1 mês. Além disso, você terá outras revelações."; 
        infoButton.onClick.AddListener(delegate { Part1(); AudioManager.am.voiceChannel.Stop(); });
    }

    private void Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(bankPanel, true);
        if (PlayerManager.pm.v2l26choice == 50) income += 65;
        else if (PlayerManager.pm.v2l26choice == 75) income += 95;
        else if (PlayerManager.pm.v2l26choice == 100) income += 125;

        StartCoroutine(FlavorManager.fm.SpawnBucks(5));

        bankText.text = "Você ganhou " + income + " reais para o Bolodix!\n\n" +
            "Agora vamos ver se você conseguiu ganhar seu presente do terceiro sonho!!!";

        bankButton.onClick.AddListener(delegate { StartCoroutine(Part2()); });
    }

    public IEnumerator Part2()
    {
        Animator textAnim = counterText.GetComponent<Animator>();
        FlavorManager.fm.ShowHidePanel(bankPanel, false);

        float currentTime = 4;

        while (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;

            textAnim.SetTrigger("Start");

            counterText.text = ((int)currentTime).ToString();
            yield return null;
        }

        FlavorManager.fm.BigPuff();
        if (lidClosed.activeInHierarchy)
        {
            lidClosed.SetActive(false);
            lidOpen.SetActive(true);
        }

        if (PlayerManager.pm.safe3 == 15)
        {
            resultText.text = "Você não alcançou o preço do sonho! Mas os 15 reais irão voltar ao Bolodix!";
            income += 15;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        }
        else if (PlayerManager.pm.safe3 == 30)
        {
            resultText.text = "Você não alcançou o preço do sonho! Mas os 30 reais irão voltar ao Bolodix!";
            income += 30;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        }
        else if (PlayerManager.pm.safe3 == 40)
        {
            resultText.text = "Você não alcançou o preço do sonho! Mas os 40 reais irão voltar ao Bolodix!";
            income += 40;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        }
        else if (PlayerManager.pm.safe3 == 100)
        {
            resultText.text = "Você alcançou o preço do sonho!";
            if (PlayerManager.pm.gift3 == 1) FlavorManager.fm.ShowHidePanel(giftA, true);
            if (PlayerManager.pm.gift3 == 2) FlavorManager.fm.ShowHidePanel(giftB, true);
            if (PlayerManager.pm.gift3 == 3) FlavorManager.fm.ShowHidePanel(giftC, true);
        }
        else if (PlayerManager.pm.safe3 == 115)
        {
            resultText.text = "Você alcançou o preço do sonho! Os 15 reais irão voltar ao Bolodix!";
            income += 15;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            if (PlayerManager.pm.gift3 == 1) FlavorManager.fm.ShowHidePanel(giftA, true);
            if (PlayerManager.pm.gift3 == 2) FlavorManager.fm.ShowHidePanel(giftB, true);
            if (PlayerManager.pm.gift3 == 3) FlavorManager.fm.ShowHidePanel(giftC, true);
        }
        else if (PlayerManager.pm.safe3 == 125)
        {
            resultText.text = "Você alcançou o preço do sonho! Os 25 reais irão voltar ao Bolodix!";
            income += 25;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            if (PlayerManager.pm.gift3 == 1) FlavorManager.fm.ShowHidePanel(giftA, true);
            if (PlayerManager.pm.gift3 == 2) FlavorManager.fm.ShowHidePanel(giftB, true);
            if (PlayerManager.pm.gift3 == 3) FlavorManager.fm.ShowHidePanel(giftC, true);
        }
        else if (PlayerManager.pm.safe3 == 155)
        {
            resultText.text = "Você alcançou o preço do sonho! Os 55 reais irão voltar ao Bolodix!";
            income += 55;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            if (PlayerManager.pm.gift3 == 1) FlavorManager.fm.ShowHidePanel(giftA, true);
            if (PlayerManager.pm.gift3 == 2) FlavorManager.fm.ShowHidePanel(giftB, true);
            if (PlayerManager.pm.gift3 == 3) FlavorManager.fm.ShowHidePanel(giftC, true);
        }

        counterText.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);

        PlayerManager.pm.AddCoins(income);
        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = income.ToString();

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultButton.onClick.RemoveAllListeners();
        APIManager.am.Relatorio(conceitos);

        resultButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("EndLevel"); AudioManager.am.PlaySFX(AudioManager.am.button); });
    }
}
