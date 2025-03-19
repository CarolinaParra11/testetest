using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class A2L4 : MonoBehaviour
{
    public GameObject LoadTime;
    public GameObject BG1;
    public GameObject BG2;
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

    private int totalCoins = 100;
    private int bonusCoins;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinSpawn1, coinWaypoint, coinWaypoint1;

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

    private void Start()
    {
        BG1.SetActive(true);
        BG2.SetActive(false);
        totalCoinsText.text = totalCoins.ToString();
        //  AudioManager.am.PlayVoice(AudioManager.am.v2start[13]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "O zoológico de sua cidade anunciou que ficará 3 meses fechados para reformas a partir do próximo mês e você viu uma boa oportunidade de finalmente fazer um passeio que há tempos desejava. Você então convidou outros 6 amigos para irem juntos e eles aceitaram.";
       
        infoButton.onClick.AddListener(delegate { FlavorManager.fm.ShowHidePanel(infoPanel, false); StartCoroutine(Part1()); infoButton.onClick.RemoveAllListeners(); });
    }
    private IEnumerator TimePanel()
    {

        FlavorManager.fm.ShowHidePanel(LoadTime, true);

        yield return new WaitForSeconds(2.0f);
        FlavorManager.fm.ShowHidePanel(LoadTime, false);

    }
    private IEnumerator Part1()
    {
        infoTextC.text = "Pacote indisponível nesta data, você pode comprar o unitário ou escolher outra opção.";
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(TimePanel());

        infoButtonC.onClick.AddListener(delegate { FlavorManager.fm.ShowHidePanel(infoPanelC, false); });
        conceitos.Add("22", 22);
        infoTextB.text = "Como sempre, vocês tem recursos limitados, economizaram bastante e precisam escolher dentre algumas opções. Decidiram que cada um iria disponibilizar R$100,00. Vc e Seus amigos, então, devem tomar as decisões em relação ao que será escolhido.";
        yield return new WaitForSeconds(3);
        FlavorManager.fm.ShowHidePanel(infoPanelB, true);
        infoButtonB.onClick.AddListener(delegate { FlavorManager.fm.ShowHidePanel(infoPanelB, false); FlavorManager.fm.ShowHidePanel(optionPanel, true); infoButton.onClick.RemoveAllListeners(); });

        button[0].onClick.AddListener(delegate { Part1Choice(this, 33, 00, price[0], selected[0]); });
        button[1].onClick.AddListener(delegate { Part1Choice(this, 181, 00, price[1], selected[1]); });
        button[2].onClick.AddListener(delegate { Part1Choice(this, 37, 00, price[2], selected[2]); });
        button[3].onClick.AddListener(delegate { Part1Choice(this, 200, 00, price[3], selected[3]); });
        button[4].onClick.AddListener(delegate { Part1Choice(this, 55, 00, price[4], selected[4]); });
        button[5].onClick.AddListener(delegate { FlavorManager.fm.ShowHidePanel(infoPanelC, true); });
        button[6].onClick.AddListener(delegate { Part1Choice(this, 47, 00, price[6], selected[6]); });
        button[7].onClick.AddListener(delegate { Part1Choice(this, 240, 00, price[7], selected[7]); });
        button[8].onClick.AddListener(delegate { Part1Choice(this, 55, 00, price[8], selected[8]); });
        button[9].onClick.AddListener(delegate { Part1Choice(this, 130, 00, price[9], selected[9]); });
        button[10].onClick.AddListener(delegate { Part1Choice(this, 35, 00, price[10], selected[10]); });
        button[11].onClick.AddListener(delegate { Part1Choice(this, 120, 00, price[11], selected[11]); });
        button[12].onClick.AddListener(delegate { Part1Choice(this, -600, 00, price[12], selected[12]); });
        button[13].onClick.AddListener(delegate { Part1Choice(this, 2, 00, price[13], selected[13]); });
        button[14].onClick.AddListener(delegate { Part1Choice(this, 9, 00, price[14], selected[14]); });
        button[15].onClick.AddListener(delegate { Part1Choice(this, 12, 00, price[15], selected[15]); });
        button[16].onClick.AddListener(delegate { Part1Choice(this, 7, 00, price[16], selected[16]); });
        button[17].onClick.AddListener(delegate { Part1Choice(this, 13, 00, price[17], selected[17]); });
        button[18].onClick.AddListener(delegate { Part1Choice(this, 33, 00, price[18], selected[18]); });
        button[19].onClick.AddListener(delegate { Part1Choice(this, 75, 00, price[19], selected[19]); });
        button[20].onClick.AddListener(delegate { Part1Choice(this, 60, 00, price[20], selected[20]); });
        button[21].onClick.AddListener(delegate { Part1Choice(this, 100, 00, price[21], selected[21]); });
        button[22].onClick.AddListener(delegate { Part1Choice(this, 80, 00, price[22], selected[22]); });



        submitButton.onClick.AddListener(delegate { StartCoroutine(Part2()); submitButton.onClick.RemoveAllListeners(); });

        

    }

    private IEnumerator Part2()
    {
        StartCoroutine(TimePanel());
        yield return new WaitForSeconds(1);
        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        BG1.SetActive(false);
        BG2.SetActive(true);
        yield return new WaitForSeconds(3);


        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "No parque, vocês decidem fazer suas paradas para refeições, uma às 11h30 e outra às 15h30.";
        infoTextB.text = "Dentre seus amigos e as opções disponíveis, você precisa decidir se irá realizar uma compra conjunta ou se deseja devolver para cada um o que sobrou da compra dos ingressos, para que cada um escolha o que deseja.";

        infoButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
            FlavorManager.fm.ShowHidePanel(infoPanelB, true);



        });

        infoButtonB.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanelB, false);

            infoText.text = "Vocês ainda têm " + totalCoins + " reais! Estude as opções a seguir:";
            this.infoButton.onClick.RemoveAllListeners();
            FlavorManager.fm.ShowHidePanel(optionPanel2, true);



        });

        //extra bucks animation
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
            else if(isExtra)
            {
               
                isExtra = false;
                AudioManager.am.PlaySFX(AudioManager.am.button);
                StartCoroutine(FlavorManager.fm.SpawnBucksPosition(1, coinWaypoint1, coinSpawn1));
            }

        });


        submitButton2.onClick.AddListener(delegate { StartCoroutine(End()); submitButton2.onClick.RemoveAllListeners(); AudioManager.am.PlaySFX(AudioManager.am.button); });


    }


    private static void Part1Choice(A2L4 instance, int price, int bonus, GameObject priceObject, GameObject selectedObject)
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

        if (selected[22].activeSelf) bonusCoins += 20;

        resultText.text = "Sobraram " + totalCoins + " reais e você conseguiu mais " + bonusCoins + " de bônus ! Vamos para a próxima!";

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = (totalCoins + bonusCoins).ToString();

        if (bonusCoins > 0) StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));
        PlayerManager.pm.AddCoins(totalCoins + bonusCoins);
        //APIManager.am.Relatorio(conceitos); // to do -> novos conceitos 

        PlayerManager.pm.AddLevel();
    }
}