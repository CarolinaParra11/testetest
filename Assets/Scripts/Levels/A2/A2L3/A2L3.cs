using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class A2L3 : MonoBehaviour
{
    public GameObject infoPanel;
    public GameObject infoPanelB;
    public GameObject infoPanelC;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI infoTextB;
    public TextMeshProUGUI infoTextC;
    public Button infoButton;
    public Button infoButtonB;
    public Button infoButtonC;

    public Button ExtraCashButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 50;
    private int bonusCoins;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn,coinSpawn1, coinWaypoint, coinWaypoint1;

    public GameObject optionPanel;
    public GameObject optionPanel2;
    public Button submitButton;
    public Button submitButton2;
    public Button[] button;
    public GameObject[] price;
    public GameObject[] selected;
    private int totalPrice;

    public TextMeshProUGUI totalPriceText;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    //Em desenvolvimento ..

    private void Start()
    {
        totalCoinsText.text = totalCoins.ToString();
      //  AudioManager.am.PlayVoice(AudioManager.am.v2start[??]); // verificar musicas e audios/voz das fases
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = " .";
        infoTextB.text = " !";
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part1()
    {
        infoTextC.text = "Indisponível nesta data, escolha outra opção.";
        conceitos.Add("", 0);
        conceitos[""] += 0;
        infoButtonC.onClick.AddListener(delegate { FlavorManager.fm.ShowHidePanel(infoPanelC, false); });
        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(2);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        button[0].onClick.AddListener(delegate { FlavorManager.fm.ShowHidePanel(infoPanelC, true); });
        button[1].onClick.AddListener(delegate { Part1Choice(this, 28, 10, price[1], selected[1]); });
        button[2].onClick.AddListener(delegate { Part1Choice(this, 33, 10, price[2], selected[2]); });
        button[3].onClick.AddListener(delegate { Part1Choice(this, 28, 10, price[3], selected[3]); });
        button[4].onClick.AddListener(delegate { Part1Choice(this, 18, 10, price[4], selected[4]); });
        button[5].onClick.AddListener(delegate { Part1Choice(this, 12, 10, price[5], selected[5]); });
        button[6].onClick.AddListener(delegate { Part1Choice(this, 12, 10, price[6], selected[6]); });
        button[7].onClick.AddListener(delegate { Part1Choice(this, 22, 10, price[7], selected[7]); });
        button[8].onClick.AddListener(delegate { Part1Choice(this, 10, 10, price[8], selected[8]); });
        button[9].onClick.AddListener(delegate { Part1Choice(this, 15, 10, price[9], selected[9]); });
        button[10].onClick.AddListener(delegate { Part1Choice(this, 24, 10, price[10], selected[10]); });
        button[11].onClick.AddListener(delegate { Part1Choice(this, 26, 10, price[11], selected[12]); });
        button[12].onClick.AddListener(delegate { Part1Choice(this, 29, 10, price[12], selected[12]); });
        button[13].onClick.AddListener(delegate { Part1Choice(this, -10, 10, price[13], selected[13]); });


        submitButton.onClick.AddListener(delegate { StartCoroutine(Part2()); submitButton.onClick.RemoveAllListeners(); });
        

    }

    private IEnumerator Part2()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        yield return new WaitForSeconds(2);
        FlavorManager.fm.ShowHidePanel(infoPanelB, true);

        infoButtonB.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanelB, false);
            FlavorManager.fm.ShowHidePanel(infoPanelC, true);
            infoTextC.text = "Você ainda tem " + totalCoins + " reais!                                 O Léo ainda tem R$10,00!                                  Caso queira, você pode propor ao Léo uma compra conjunta se achar interessante. Assim, vocês podem unificar os valores de vocês.";
            this.infoButton.onClick.RemoveAllListeners();


        });
        infoButtonC.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanelC, false);
            infoTextC.text = "... ";


            FlavorManager.fm.ShowHidePanel(optionPanel2, true);
            this.infoButton.onClick.RemoveAllListeners();


        });


        bool isExtra = false;
        
            ExtraCashButton.onClick.AddListener(delegate
            {
                
                if (!isExtra)
                {

                    isExtra = true;
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    StartCoroutine(FlavorManager.fm.SpawnBucksPosition(1, coinSpawn1, coinWaypoint1));
                    this.infoButton.onClick.RemoveAllListeners();
                }
                else
                {
                    isExtra = false;
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    StartCoroutine(FlavorManager.fm.SpawnBucksPosition(1, coinWaypoint1, coinSpawn1));
                }

            });
               


        submitButton2.onClick.AddListener(delegate { StartCoroutine(End()); submitButton2.onClick.RemoveAllListeners(); AudioManager.am.PlaySFX(AudioManager.am.button); });
        

    }
    

    private static void Part1Choice(A2L3 instance, int price, int bonus, GameObject priceObject, GameObject selectedObject)
    {
        if (priceObject.activeSelf)
        {
            if (instance.totalCoins >= price)
            {
                instance.totalCoins -= price;
                instance.totalCoinsText.text = instance.totalCoins.ToString();

                instance.bonusCoins += bonus;
                instance.totalPrice += price;
                instance.totalPriceText.text = instance.totalPrice.ToString();

                priceObject.SetActive(false);
                selectedObject.SetActive(true);
            }
            else
            {
                FlavorManager.fm.ShowHidePanel(instance.infoPanel, true);
                instance.infoText.text = "Você não tem dinheiro suficiente, escolha outro!";
                instance.infoButton.onClick.RemoveAllListeners();
                instance.infoButton.onClick.AddListener(delegate
                {
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    FlavorManager.fm.ShowHidePanel(instance.infoPanel, false);
                });
            }
        }
        else
        {
            instance.totalCoins += price;
            instance.totalCoinsText.text = instance.totalCoins.ToString();

            instance.bonusCoins -= bonus;
            instance.totalPrice -= price;
            instance.totalPriceText.text = instance.totalPrice.ToString();

            priceObject.SetActive(true);
            selectedObject.SetActive(false);
        }
    }

    private IEnumerator End()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        
        if (selected[3].activeSelf && selected[4].activeSelf) bonusCoins-= 100;

        resultText.text = "Sobraram " + totalCoins + " reais e você conseguiu mais " + bonusCoins + " de bônus ! Vamos para a próxima!";
       

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = (totalCoins + bonusCoins).ToString();

        if (bonusCoins > 0) StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));
        PlayerManager.pm.AddCoins(totalCoins + bonusCoins);
        //APIManager.am.Relatorio(conceitosA2); // to do -> novos conceitos 


        PlayerManager.pm.AddLevel();
    }
}