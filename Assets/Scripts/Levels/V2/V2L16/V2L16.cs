using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V2L16 : MonoBehaviour
{
    private V2L16Helper v2l16Helper;

    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject optionPanel;
    public TextMeshProUGUI totalCoinsText;
    public Button button1, button2, button3, button4, button5, button6, button7, button8, button9;
    public Image image1, image2, image3, image4, image5, image6, image7, image8, image9;
    public Button endButton;
    private int counter = 0;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        v2l16Helper = GameObject.Find("V2L16Helper").GetComponent<V2L16Helper>();
        totalCoinsText.text = v2l16Helper.GetCoins().ToString();

        AudioManager.am.PlayVoice(AudioManager.am.v2start[15]);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Hora de montar a sua casa!\n" +
            "Você tem 1100 reais, e esta será a primeira compra para ela. Pense bem, todos os itens tem sua importância, mas escolha o que é fundamental primeiro. Use bem o dinheiro." +
            "\n\nObservação:\nO dinheiro que sobrar não será guardado no Bolodix!";
        infoButton.onClick.AddListener(delegate { Part1(); infoButton.onClick.RemoveAllListeners(); });
    }

    private void Part1()
    {
        // Declaração
        conceitos.Add("25", 0);
        conceitos.Add("44", 0);
        conceitos.Add("22", 0);
        conceitos.Add("20", 0);
        conceitos.Add("24", 0);
        conceitos.Add("7", 0);
        conceitos.Add("19", 0);
        conceitos.Add("34", 0);
        conceitos.Add("18", 0);
        conceitos.Add("64", 0);
        conceitos.Add("65", 0);

        conceitos["25"] += 0;
        conceitos["44"] += 0;
        conceitos["22"] += 0;
        conceitos["20"] += 0;
        conceitos["24"] += 0;
        conceitos["7"] += 0;
        conceitos["19"] += 0;
        conceitos["34"] += 0;
        conceitos["18"] += 0;
        conceitos["64"] += 0;
        conceitos["65"] += 0;

        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        button1.onClick.AddListener(delegate { Part1Choice(40, image1, 0); });
        button2.onClick.AddListener(delegate { Part1Choice(100, image2, 1); });
        button3.onClick.AddListener(delegate { Part1Choice(400, image3, 2); });
        button4.onClick.AddListener(delegate { Part1Choice(100, image4, 3); });
        button5.onClick.AddListener(delegate { Part1Choice(200, image5, 4); });
        button6.onClick.AddListener(delegate { Part1Choice(400, image6, 5); });
        button7.onClick.AddListener(delegate { Part1Choice(600, image7, 6); });
        button8.onClick.AddListener(delegate { Part1Choice(50, image8, 7); });
        button9.onClick.AddListener(delegate { Part1Choice(400, image9, 8); });
        endButton.onClick.AddListener(delegate { End(); endButton.onClick.RemoveAllListeners(); });
    }

    private void Part1Choice(int price, Image image, int id)
    {
        if (!image.IsActive())
        {
            if (v2l16Helper.GetCoins() >= price)
            {
                v2l16Helper.RemoveCoins(price);
                totalCoinsText.text = v2l16Helper.GetCoins().ToString();
                counter++;
                image.enabled = true;
                v2l16Helper.AddChoice(id);
            }
            else
            {
                FlavorManager.fm.ShowHidePanel(infoPanel, true);
                infoText.text = "Você não tem dinheiro suficiente pra comprar esse item, remova algum item pra poder comprá-lo";
                infoButton.onClick.AddListener(delegate
                {
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    FlavorManager.fm.ShowHidePanel(infoPanel, false);
                    infoButton.onClick.RemoveAllListeners();
                });
            }
        }
        else
        {
            v2l16Helper.AddCoins(price);
            totalCoinsText.text = v2l16Helper.GetCoins().ToString();
            counter--;
            image.enabled = false;
            v2l16Helper.RemoveChoice(id);
        }

        if (counter >= 4) endButton.gameObject.SetActive(true);
        else endButton.gameObject.SetActive(false);
    }

    private void End()
    {
        //string bonus = "";

        if (v2l16Helper.GetCoins() > 0)
        {
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            //PlayerManager.pm.AddCoins(v2l16Helper.GetCoins());
            //bonus = " Ainda ganhou mais " + v2l16Helper.GetCoins() + " reais!";
        }

        foreach (int i in v2l16Helper.choices)
        {
            if (i == 1 || i == 5 || i == 7 || i == 8)
            {
                conceitos["25"] += 5;
                conceitos["44"] += 3;
                conceitos["22"] += 4;
                conceitos["20"] += 3;
                conceitos["24"] += 3;
                conceitos["7"] += 4;
                conceitos["19"] += 6;
                conceitos["34"] += 3;
                conceitos["18"] += 3;
                conceitos["64"] += 6;
                conceitos["65"] += 7;
            }
            if (i == 2 || i == 6 || i == 9)
            {
                conceitos["25"] += 3;
                conceitos["44"] += 2;
                conceitos["22"] += 2;
                conceitos["20"] += 2;
                conceitos["24"] += 2;
                conceitos["7"] += 2;
                conceitos["19"] += 3;
                conceitos["34"] += 3;
                conceitos["18"] += 2;
                conceitos["64"] += 5;
                conceitos["65"] += 5;
            }
            if (i == 3 || i == 4)
            {
                conceitos["25"] += 2;
                conceitos["44"] += 1;
                conceitos["22"] += 0;
                conceitos["20"] += 0;
                conceitos["24"] += 1;
                conceitos["7"] += 1;
                conceitos["19"] += 2;
                conceitos["34"] += 3;
                conceitos["18"] += 0;
                conceitos["64"] += 4;
                conceitos["65"] += 4;
            }
        }

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Parabéns! Vamos para a próxima!";
        infoButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L16b"); });

        APIManager.am.Relatorio(conceitos);
    }
}