using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L15 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject hintButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject optionPanel1, optionPanel2;
    public Button p1Button1, p1Button2, p1Button3;
    public Button p2Button1, p2Button2, p2Button3;
    private int coins;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        hintButton.SetActive(false);

        conceitos.Add("22", 0);
        conceitos.Add("7", 0);
        conceitos.Add("1", 0);
        conceitos.Add("25", 0);
        conceitos.Add("23", 0);
        conceitos.Add("10", 0);
        conceitos.Add("17", 0);

        conceitos["22"] += 0;
        conceitos["7"] += 0;
        conceitos["1"] += 0;
        conceitos["25"] += 0;
        conceitos["23"] += 0;
        conceitos["10"] += 0;
        conceitos["17"] += 0;

        StartCoroutine(Part1Start());
    }

    private IEnumerator Part1Start()
    {
        yield return new WaitForSeconds(3);

        AudioManager.am.PlayVoice(AudioManager.am.v1start[14]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Dia do shopping! Ajude o pai e a mãe do seu amigo a resolverem a situação!\n\n" +
            "Seu amigo quer um brinquedo que vale 8 moedas," +
            " mas o pai dele diz que tem apenas 3 moedas no bolso.";
        infoButton.onClick.AddListener(delegate
        {
            hintButton.SetActive(true);
            StartCoroutine(Part1());
            AudioManager.am.voiceChannel.Stop();
        });
    }

    private IEnumerator Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);

        yield return new WaitForSeconds(3);

        AudioManager.am.PlayVoice(AudioManager.am.v1l15[0]);
        FlavorManager.fm.ShowHidePanel(optionPanel1, true);

        p1Button1.onClick.AddListener(delegate 
        {
            

            Part1End();
            AudioManager.am.PlayVoice(AudioManager.am.v1l15[2]);
            infoText.text = "A resposta certa era: 5 moedas."; 
        });
        p1Button2.onClick.AddListener(delegate 
        {
            conceitos["17"] += 1;

            Part1End();
            AudioManager.am.PlayVoice(AudioManager.am.v1l15[1]);
            infoText.text = "Parabéns! Você ganhou 2 moedas bônus.";
            coins += 2;
            StartCoroutine(FlavorManager.fm.SpawnCoin(2));
        });
        p1Button3.onClick.AddListener(delegate 
        {
            

            Part1End();
            AudioManager.am.PlayVoice(AudioManager.am.v1l15[2]);
            infoText.text = "A resposta certa era: 5 moedas.";
        });
    }

    private void Part1End()
    {
        p1Button1.onClick.RemoveAllListeners();
        p1Button2.onClick.RemoveAllListeners();
        p1Button3.onClick.RemoveAllListeners();

        AudioManager.am.PlaySFX(AudioManager.am.button);

        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate 
        { 
            Part2();
            AudioManager.am.PlaySFX(AudioManager.am.button);
            infoButton.onClick.RemoveAllListeners();
        });
    }

    private void Part2()
    {
        AudioManager.am.PlayVoice(AudioManager.am.v1l15[3]);

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel2, true);

        p2Button1.onClick.AddListener(delegate { End(1); });
        p2Button2.onClick.AddListener(delegate { End(2); });
        p2Button3.onClick.AddListener(delegate { End(3); });
    }

    private void End(int bonus)
    {
        hintButton.SetActive(false);
        p2Button1.onClick.RemoveAllListeners();
        p2Button2.onClick.RemoveAllListeners();
        p2Button3.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel2, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        if (bonus == 1)
        {
            conceitos["22"] += 1;
            conceitos["7"] += 1;
            conceitos["1"] += 1;
            conceitos["25"] += 1;
            conceitos["23"] += 1;
            conceitos["10"] += 1;

            AudioManager.am.PlayVoice(AudioManager.am.v1l15[4]);
            resultText.text = "Muito bem! Você ajudou o seu amigo a conversar com o pai e escolher outro brinquedo com 3 moedas." +
                " Você ganha 1 moeda extra para o seu Bolodix!";
        }
        else if (bonus == 2)
        {
            conceitos["22"] += 1;
            conceitos["7"] += 1;
            conceitos["1"] += 1;
            conceitos["25"] += 1;
            conceitos["23"] += 2;
            conceitos["10"] += 1;

            AudioManager.am.PlayVoice(AudioManager.am.v1l15[5]);
            resultText.text = "Muito bem! Você ajudou o pai do seu amigo a comprar um brinquedo mais barato." +
                " Ainda sobrou 1 moeda para o pai dele. Você ganhou 2 moedas extras para o seu Bolodix!";
        }
        else if (bonus == 3)
        {
            conceitos["22"] += 2;
            conceitos["7"] += 2;
            conceitos["1"] += 2;
            conceitos["25"] += 2;
            conceitos["23"] += 3;
            conceitos["10"] += 2;

            AudioManager.am.PlayVoice(AudioManager.am.v1l15[6]);
            resultText.text = "Parabéns! Você ajudou seu amigo a combinar com o pai dizendo que prefere esperar para juntar dinheiro e comprar o brinquedo no momento certo," +
                " já que é esse trator que deseja. Você ganha 3 moedas extras para o seu Bolodix!";
        }

        FlavorManager.fm.ShowHidePanel(incomePanel, true);

        int counter = 0;

        while (counter < bonus + coins)
        {
            coinsPanel.GetChild(counter).gameObject.SetActive(true);
            counter++;
        }

        PlayerManager.pm.AddCoins(bonus + coins);
        StartCoroutine(FlavorManager.fm.SpawnCoin(bonus));
        APIManager.am.Relatorio(conceitos);
        PlayerManager.pm.AddLevel();
    }
}