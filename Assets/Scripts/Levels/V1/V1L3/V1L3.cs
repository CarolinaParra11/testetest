using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L3 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject confirmPanel;
    public TextMeshProUGUI confirmText;
    public Button yesButton, noButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 6;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button button1, button2, button3;
    public Animator wallAnimator;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("10", 0);
        conceitos.Add("23", 0);
        conceitos.Add("25", 0);
        conceitos.Add("12", 0);
        conceitos.Add("1", 0);
        conceitos.Add("6", 0);
        conceitos.Add("34", 0);

        conceitos["10"] += 0;
        conceitos["23"] += 0;
        conceitos["25"] += 0;
        conceitos["12"] += 0;
        conceitos["1"] += 0;
        conceitos["6"] += 0;
        conceitos["34"] += 0;

        AudioManager.am.PlayVoice(AudioManager.am.v1start[2]);
        totalCoinsText.text = totalCoins.ToString();

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = " Você tem 6 moedas para usar na pintura da sua casa. Gaste com sabedoria e escolha a melhor tinta.";
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
        Part1Assign();
    }

    private void Part1Assign()
    {
        button1.onClick.AddListener(delegate
        {
            StartCoroutine(Part1Confirm(1, 1, 0, "wall1", "Esta é a tinta mais barata, mas só tem uma lata disponível para comprar. Gostaria de comprar?",
                "Como só tinha uma lata disponível, você pintou metade da casa. Você usará mais 2 moedas para a tinta verde que está disponível."));
        });
        button2.onClick.AddListener(delegate
        {
            StartCoroutine(Part1Confirm(2, 4, 6, "wall2", "Você precisa de duas latas desta tinta para pintar toda a sua casa. Gostaria de comprar as duas latas?",
                "Você pintou a sua casa. Agora ela está pronta para morar. Por ter completado a missão, você receberá as 4 moedas usadas de volta e mais as 2 que sobraram. Veja seu Bolodix crescendo!"));
        });
        button3.onClick.AddListener(delegate
        {
            StartCoroutine(Part1Confirm(3, 6, 8, "wall3", "Você pode pintar toda a sua casa com esta lata de tinta. Ela é mais durável e eco sustentável. Gostaria de comprar?",
                "Parabéns! Você completou a missão escolhendo a tinta que dura mais tempo e que também é sustentável. Você receberá de volta as 6 moedas que usou e mais 2 de bônus pela melhor opção. Veja seu Bolodix crescendo!"));
        });
    }

    private IEnumerator Part1Confirm(int id, int cost, int bonus, string trigger, string text1, string text2)
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        AudioManager.am.PlaySFX(AudioManager.am.button);

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(confirmPanel, true);

        confirmText.text = text1;

        yesButton.onClick.AddListener(delegate
        {
            if (id == 1) StartCoroutine(Part1Choice1(cost, 1, text2, trigger));
            else if (id == 2) StartCoroutine(End(2, cost, bonus, text2, trigger));
            else if (id == 3) StartCoroutine(End(3, cost, bonus, text2, trigger));
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
        });
        noButton.onClick.AddListener(delegate
        {
            StartCoroutine(Part1Restart());
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
        });

        if (id == 1) AudioManager.am.PlayVoice(AudioManager.am.v1l3[0]);
        if (id == 2) AudioManager.am.PlayVoice(AudioManager.am.v1l3[1]);
        if (id == 3) AudioManager.am.PlayVoice(AudioManager.am.v1l3[2]);
    }

    private IEnumerator Part1Restart()
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        AudioManager.am.voiceChannel.Stop();

        FlavorManager.fm.ShowHidePanel(confirmPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        Part1Assign();
    }

    private IEnumerator Part1Choice1(int cost, int part, string text, string trigger)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        infoButton.onClick.RemoveAllListeners();
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        AudioManager.am.voiceChannel.Stop();

        totalCoins -= cost;
        totalCoinsText.text = totalCoins.ToString();

        StartCoroutine(FlavorManager.fm.SpawnCoinPosition(cost, coinSpawn, coinWaypoint));
        wallAnimator.SetTrigger(trigger);

        if(infoPanel.activeSelf) FlavorManager.fm.ShowHidePanel(infoPanel, false);
        else FlavorManager.fm.ShowHidePanel(confirmPanel, false);
        yield return new WaitForSeconds(3);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = text;
        infoButton.onClick.AddListener(delegate 
        {
            if (part == 1) StartCoroutine(Part1Choice1(2, 2, "A casa está com cores diferentes. Você terá que usar 2 moedas para completar com a tinta verde disponível.", "wall1a"));
            else StartCoroutine(End(1, 2, 1, "Sobrou 1 moeda para o seu Bolodix. Lembre-se: o que tem menor preço, nem sempre será a melhor escolha.", "wall1b"));
        });

        if (part == 1) AudioManager.am.PlayVoice(AudioManager.am.v1l3[3]);
        else AudioManager.am.PlayVoice(AudioManager.am.v1l3[4]);
    }

    private IEnumerator End(int id, int cost, int bonus, string text, string trigger)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        infoButton.onClick.RemoveAllListeners();
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        AudioManager.am.voiceChannel.Stop();

        totalCoins -= cost;
        totalCoinsText.text = totalCoins.ToString();

        StartCoroutine(FlavorManager.fm.SpawnCoinPosition(cost, coinSpawn, coinWaypoint));
        wallAnimator.SetTrigger(trigger);

        if (infoPanel.activeSelf) FlavorManager.fm.ShowHidePanel(infoPanel, false);
        else FlavorManager.fm.ShowHidePanel(confirmPanel, false);
        yield return new WaitForSeconds(3);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = text;

        if (id == 1)
        {
            conceitos["10"] += 3;
            conceitos["23"] += 2;
            conceitos["25"] += 3;
            conceitos["12"] += 3;
            conceitos["1"] += 2;
            conceitos["6"] += 3;
            conceitos["34"] += 3;

            AudioManager.am.PlayVoice(AudioManager.am.v1l3[5]);
        }
        if (id == 2)
        {
            conceitos["10"] += 3;
            conceitos["23"] += 1;
            conceitos["25"] += 3;
            conceitos["12"] += 0;
            conceitos["1"] += 1;
            conceitos["6"] += 3;
            conceitos["34"] += 2;

            AudioManager.am.PlayVoice(AudioManager.am.v1l3[6]);
        }
        if (id == 3)
        {
            conceitos["10"] += 0;
            conceitos["23"] += 1;
            conceitos["25"] += 0;
            conceitos["12"] += 0;
            conceitos["1"] += 0;
            conceitos["6"] += 2;
            conceitos["34"] += 1;

            AudioManager.am.PlayVoice(AudioManager.am.v1l3[7]);
        }

        if (bonus > 0)
        {
            FlavorManager.fm.ShowHidePanel(incomePanel, true);

            int counter = 0;

            while (counter < bonus)
            {
                coinsPanel.GetChild(counter).gameObject.SetActive(true);
                counter++;
            }
        }

        StartCoroutine(FlavorManager.fm.SpawnCoin(bonus));
        PlayerManager.pm.AddCoins(bonus);

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }
}