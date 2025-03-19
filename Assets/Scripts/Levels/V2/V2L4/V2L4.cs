using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L4 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 30;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button button1, button2, button3, button4;

    private V2L4Helper v2l4Helper;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        v2l4Helper = GameObject.Find("V2L4Helper").GetComponent<V2L4Helper>();

        totalCoinsText.text = totalCoins.ToString();
        AudioManager.am.PlayVoice(AudioManager.am.v2start[3]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você recebeu trinta reais. Esse dinheiro será usado para doação. Escolha o local para onde deseja doar.";
        infoButton.onClick.AddListener(delegate { Part1(); });
    }

    private void Part1()
    {
        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        button1.onClick.AddListener(delegate{ StartCoroutine(End(1)); });
        button2.onClick.AddListener(delegate{ StartCoroutine(End(2)); });
        button3.onClick.AddListener(delegate{ StartCoroutine(End(3)); });
        button4.onClick.AddListener(delegate{ StartCoroutine(End(4)); });
    }

    private IEnumerator End(int id)
    {
        conceitos.Add("2", 1);
        conceitos.Add("3", 1);
        conceitos.Add("10", 2);
        conceitos.Add("42", 2);

        v2l4Helper.choice = id;

        totalCoins -= 30;
        totalCoinsText.text = totalCoins.ToString();
        StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));

        if (id == 1) infoText.text = "Você doou 30 reais para o Hospital Comunitário!";
        else if (id == 2) infoText.text = "Você doou 30 reais para o Orfanato!";
        else if (id == 3) infoText.text = "Você doou 30 reais para o Parque!";
        else if (id == 4) infoText.text = "Você doou 30 reais para o Asilo!";

        FlavorManager.fm.ShowHidePanel(optionPanel, false);

        yield return new WaitForSeconds(0.5f);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            GameManager.gm.LoadScene("V2L4a");
        });

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
        PlayerManager.pm.blue = true;
    }
}