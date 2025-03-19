using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V1L16 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 9;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button button1, button2, button3, button4, button5, button6, button7, button8, button9;
    public Image image1, image2, image3, image4, image5, image6, image7, image8, image9;
    public Button endButton;
    private int counter = 0;

    public GameObject incomePanel;
    public Transform coinsPanel;

    private void Start()
    {
        totalCoinsText.text = totalCoins.ToString();
        AudioManager.am.PlayVoice(AudioManager.am.v1start[15]);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Monte a sua casa: Você tem 9 moedas," +
            " e esta será sua primeira compra pra ela.\n\nTodos os itens tem sua importância," +
            " mas escolha o que é fundamental primeiro, e compre no mínimo três itens!\n\nUse bem suas moedas.";
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

        button1.onClick.AddListener(delegate { Part1Choice(2, image1, 0); });
        button2.onClick.AddListener(delegate { Part1Choice(3, image2, 1); });
        button3.onClick.AddListener(delegate { Part1Choice(5, image3, 2); });
        button4.onClick.AddListener(delegate { Part1Choice(3, image4, 3); });
        button5.onClick.AddListener(delegate { Part1Choice(2, image5, 4); });
        button6.onClick.AddListener(delegate { Part1Choice(2, image6, 5); });
        button7.onClick.AddListener(delegate { Part1Choice(4, image7, 6); });
        button8.onClick.AddListener(delegate { Part1Choice(3, image8, 7); });
        button9.onClick.AddListener(delegate { Part1Choice(3, image9, 8); });
        endButton.onClick.AddListener(delegate { End(); });
    }

    private void Part1Choice(int price, Image image, int id)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        if (!image.IsActive())
        {
            if (totalCoins >= price)
            {
                totalCoins -=price;
                totalCoinsText.text = totalCoins.ToString();
                counter++;
                image.enabled = true;
                PlayerManager.pm.AddChoice(id);
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
            totalCoins += price;
            totalCoinsText.text = totalCoins.ToString();
            counter--;
            image.enabled = false;
            PlayerManager.pm.RemoveChoice(id);
        }

        if (counter >= 3) endButton.gameObject.SetActive(true);
        else endButton.gameObject.SetActive(false);
    }

    private void End()
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        button4.onClick.RemoveAllListeners();
        button5.onClick.RemoveAllListeners();
        button6.onClick.RemoveAllListeners();
        button7.onClick.RemoveAllListeners();
        button8.onClick.RemoveAllListeners();
        button9.onClick.RemoveAllListeners();
        endButton.onClick.RemoveAllListeners();

        AudioManager.am.PlaySFX(AudioManager.am.button);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Parabéns! Vamos para a próxima!";
        if (totalCoins > 0)
        {
            FlavorManager.fm.ShowHidePanel(incomePanel, true);

            int counter = 0;

            while (counter < totalCoins)
            {
                coinsPanel.GetChild(counter).gameObject.SetActive(true);
                counter++;
            }

            if (totalCoins == 1) resultText.text += "\n\nAinda sobrou " + totalCoins + " moeda!";
            else resultText.text += "\n\nAinda sobraram " + totalCoins + " moedas!";
            StartCoroutine(FlavorManager.fm.SpawnCoin(totalCoins));
            PlayerManager.pm.AddCoins(totalCoins);  
        }
        PlayerManager.pm.AddLevel();
    }
}