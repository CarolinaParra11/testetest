using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L2 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject hintButton1;
    public GameObject hintButton2;

    public GameObject optionPanel;
    private int correctDrops;
    private int currentDrops;
    public DragDrop[] dragDrops;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        hintButton1.SetActive(false);
        hintButton2.SetActive(false);

        conceitos.Add("10", 0);
        conceitos.Add("25", 0);
        conceitos.Add("12", 0);

        conceitos["10"] += 0;
        conceitos["25"] += 0;
        conceitos["12"] += 0;

        AudioManager.am.PlayVoice(AudioManager.am.v1start[1]);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Coloque os lixos nos cestos corretos.";
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

              hintButton1.SetActive(true);
        hintButton2.SetActive(true);
    }

    public void Part1Choice(bool correct)
    {
         

        AudioManager.am.PlaySFX(AudioManager.am.drop);
        currentDrops++;
        if (correct) correctDrops++;
        if (currentDrops == 6) End();
    }

    public void End()
    {
               hintButton1.SetActive(false);
        hintButton2.SetActive(false);

        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        if (correctDrops == 0) 
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l2[0]);
            resultText.text = "Você arrastou os materiais para os cestos que não são adequados. É importante pensar na reciclagem do nosso mundo e numa sociedade sustentável.";
        }
        if (correctDrops == 1) 
        {
            conceitos["10"] += 0;
            conceitos["25"] += 1;
            conceitos["12"] += 1;

            AudioManager.am.PlayVoice(AudioManager.am.v1l2[1]);
            resultText.text = "Parabéns pelo acerto! Você colocou um material no local certo, e ganhou 1 moeda para o seu Bolodix. O seu acerto ajudou a produzir uma sociedade mais sustentável, e foi retribuído pelo ganho que produziu. Continue pensando na reciclagem do nosso mundo."; 
        }
        if (correctDrops == 2) 
        {
            conceitos["10"] += 1;
            conceitos["25"] += 1;
            conceitos["12"] += 1;

            AudioManager.am.PlayVoice(AudioManager.am.v1l2[2]);
            resultText.text = "Parabéns pelos acertos! Você colocou dois materiais no local certo, e ganhou 2 moedas para o seu Bolodix. Os seus acertos ajudaram a produzir uma sociedade mais sustentável, e você foi retribuído pelo ganho que produziu. Continue pensando na reciclagem do nosso mundo."; 
        }
        if (correctDrops == 3) 
        {
            conceitos["10"] += 1;
            conceitos["25"] += 2;
            conceitos["12"] += 1;

            AudioManager.am.PlayVoice(AudioManager.am.v1l2[3]);
            resultText.text = "Parabéns pelos acertos! Você colocou três materiais no local certo, e ganhou 3 moedas para o seu Bolodix. Os seus acertos ajudaram a produzir uma sociedade mais sustentável, e você foi retribuído pelo ganho que produziu. Continue pensando na reciclagem do nosso mundo."; 
        }
        if (correctDrops == 4) 
        {
            conceitos["10"] += 2;
            conceitos["25"] += 2;
            conceitos["12"] += 2;

            AudioManager.am.PlayVoice(AudioManager.am.v1l2[4]);
            resultText.text = "Parabéns! Você colocou quatro materiais no local certo, e ganhou 4 moedas para o seu Bolodix. Além disso, ajudou a produzir uma sociedade mais sustentável, e foi retribuído pelo ganho que produziu. Continue pensando na reciclagem do nosso mundo."; 
        }
        if (correctDrops == 5) 
        {
            conceitos["10"] += 2;
            conceitos["25"] += 2;
            conceitos["12"] += 3;

            AudioManager.am.PlayVoice(AudioManager.am.v1l2[5]);
            resultText.text = "Parabéns! Você colocou cinco materiais no local certo, e ganhou 5 moedas para o seu Bolodix. Além disso, ajudou a produzir uma sociedade mais sustentável, e foi retribuído pelo ganho que produziu. Continue pensando na reciclagem do nosso mundo."; 
        }
        if (correctDrops == 6) 
        {
            conceitos["10"] += 2;
            conceitos["25"] += 3;
            conceitos["12"] += 3;

            AudioManager.am.PlayVoice(AudioManager.am.v1l2[6]);
            resultText.text = "Parabéns! Você colocou seis materiais no local certo, e ganhou 6 moedas para o seu Bolodix. Além disso, ajudou a produzir uma sociedade mais sustentável, e foi retribuído pelo ganho que produziu. Continue pensando na reciclagem do nosso mundo."; 
        }

        if (correctDrops > 0)
        {
            FlavorManager.fm.ShowHidePanel(incomePanel, true);

            int counter = 0;

            while (counter < correctDrops)
            {
                coinsPanel.GetChild(counter).gameObject.SetActive(true);
                counter++;
            }
        }

        FlavorManager.fm.SpawnCoin(correctDrops);
        PlayerManager.pm.AddCoins(correctDrops);

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }
}