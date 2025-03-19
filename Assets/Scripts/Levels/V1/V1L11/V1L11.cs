using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L11 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject giftPanel;
    public Button giftButton;
    public GameObject gift1;

    public GameObject optionPanel;
    public Button button1, button2, button3;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("26", 0);
        conceitos.Add("3", 0);
        conceitos.Add("31", 0);
        conceitos.Add("10", 0);
        conceitos.Add("25", 0);

        conceitos["26"] += 0;
        conceitos["3"] += 0;
        conceitos["31"] += 0;
        conceitos["10"] += 0;
        conceitos["25"] += 0;

        AudioManager.am.PlayVoice(AudioManager.am.v1start[10]);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Selecione o melhor quadro para uma sociedade ideal.";
        infoButton.onClick.AddListener(delegate 
        { 
            Part1();
            AudioManager.am.voiceChannel.Stop();
        });
    }

    private void Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        button1.onClick.AddListener(delegate { Part1End(1); });
        button2.onClick.AddListener(delegate { OptionalGift(2); });
        button3.onClick.AddListener(delegate { Part1End(3); });
    }

    private void OptionalGift(int choice)
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.ShowHidePanel(giftPanel, true);

        AudioManager.am.PlaySFX(AudioManager.am.end);
        
        gift1.SetActive(true);
        PlayerManager.pm.bonus1 = true;

        giftButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            Part1End(choice);
            giftButton.onClick.RemoveAllListeners();
            FlavorManager.fm.ShowHidePanel(giftPanel, false);
        });
    }

    private void Part1End(int choice)
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        AudioManager.am.PlaySFX(AudioManager.am.button);

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        if (choice == 1)
        {
            conceitos["26"] += 1;
            conceitos["3"] += 2;
            conceitos["31"] += 1;
            conceitos["10"] += 0;
            conceitos["25"] += 3;

            resultText.text = "Ninguém ajudou o idoso a atravessar a rua, por isso esse não é o melhor cenário.";
        }
        if (choice == 2)
        {
            conceitos["26"] += 2;
            conceitos["3"] += 2;
            conceitos["31"] += 2;
            conceitos["10"] += 1;
            conceitos["25"] += 4;

            resultText.text = "Parabéns! Você escolheu o melhor cenário! Você ganhará um presente que será revelado na última aula!";
        }
        if (choice == 3)
        {
            conceitos["26"] += 0;
            conceitos["3"] += 2;
            conceitos["31"] += 0;
            conceitos["10"] += 0;
            conceitos["25"] += 2;

            resultText.text = "O carro está sobre um pedaço da faixa, e o casal está atravessando fora da faixa. Por isso, este não é o melhor cenário.";
        }

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }
}