using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L8 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    //public GameObject hintButton;

    private int totalCoins = 6;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button button1, button2, button3, button4;

    private V1L8Helper v1l8Helper;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        //hintButton.SetActive(false);

        conceitos.Add("37", 1);
        conceitos.Add("10", 2);

        v1l8Helper = GameObject.Find("V1L8Helper").GetComponent<V1L8Helper>();

        totalCoinsText.text = totalCoins.ToString();
        AudioManager.am.PlayVoice(AudioManager.am.v1start[7]);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você recebeu 6 moedas.\nEssas moedas serão usadas para doação.\n\nEscolha o local para onde você quer doar!!!";
        infoButton.onClick.AddListener(delegate 
        { 
            Part1();
            AudioManager.am.voiceChannel.Stop();
        });
    }

    private void Part1()
    {
        //hintButton.SetActive(true);

        APIManager.am.Relatorio(conceitos);

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        button1.onClick.AddListener(delegate { End(1); });
        button2.onClick.AddListener(delegate { End(2); });
        button3.onClick.AddListener(delegate { End(3); });
        button4.onClick.AddListener(delegate { End(4); });
    }

    private void End(int id)
    {
        //hintButton.SetActive(false);
        v1l8Helper.choice = id;

        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        button4.onClick.RemoveAllListeners();

        AudioManager.am.PlaySFX(AudioManager.am.button);
        totalCoins -= 6;
        totalCoinsText.text = totalCoins.ToString();
        StartCoroutine(FlavorManager.fm.SpawnCoinPosition(6, coinSpawn, coinWaypoint));

        if (id == 1)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l8[0]);
            infoText.text = "Parabéns! Você doou 6 moedas para o hospital comunitário, e ajudou.";
        }
        else if (id == 2)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l8[1]);
            infoText.text = "Parabéns! Você doou 6 moedas para o orfanato e ajudou as crianças.";
        }
        else if (id == 3)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l8[2]);
            infoText.text = "Parabéns! Você doou 6 moedas para o parque e ajudou a restaurar as árvores.";
        }
        else if (id == 4)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l8[3]);
            infoText.text = "Parabéns! Você doou 6 moedas para o asilo, e ajudou.";
        }

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            GameManager.gm.LoadScene("V1L8a");
        });

        PlayerManager.pm.AddLevel();
        PlayerManager.pm.blue = true;
    }
}
