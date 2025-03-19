using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class A2L1 : MonoBehaviour
{

    public GameObject infoPanel;
    public GameObject BGA;
    public GameObject BGB;
    public GameObject BGC;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button resultButton;


    private int totalCoins = 1100;
    private int bonusCoins;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

   
    public TextMeshProUGUI textCounter;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI titleTextB;
    public TextMeshProUGUI titleTextC;

    private int coins;
    public Button nextButton;
    public int counter = 1;
    public Button safeButton1, safeButton2, safeButton3;
    public TextMeshProUGUI safeButton1ValueText;
    public Image safeButton1Selected, safeButton2Selected, safeButton3Selected;
    public Image safe1, safe2, safe3;
    public Image safeToy;

    private int itemCounter;
    private int bonus;


    public GameObject optionPanel1, optionPanel2, optionPanel3, optionPanel4, optionPanel5;

    public Button p1SubmitButton, p2SubmitButton, p3SubmitButton, p4SubmitButton, p5SubmitButton;
    public GameObject p1Submit, p2Submit, p3Submit, p4Submit, p5Submit;

    public Button p1Button1, p1Button2, p1Button3;
    public GameObject p1b1Price, p1b2Price, p1b3Price;
    public GameObject p1b1Selected, p1b2Selected, p1b3Selected;

    public Button p3Button1, p3Button2, p3Button3;
    public GameObject p3b1Price, p3b2Price, p3b3Price;
    public GameObject p3b1Selected, p3b2Selected, p3b3Selected;

    public Button p4Button1, p4Button2, p4Button3;
    public GameObject p4b1Price, p4b2Price, p4b3Price;
    public GameObject p4b1Selected, p4b2Selected, p4b3Selected;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;


    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    //Em desenvolvimento ..

    private void Start()
    {
        totalCoinsText.text = totalCoins.ToString();
        BGA.SetActive(true);
        
    //AudioManager.am.PlayVoice(AudioManager.am.v2start[x]);
    FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você gosta muito de esportes e está se planejando para ir a um dos mais aguardados eventos esportivos do mundo, que será realizado na Coréia do Sul. Para isso, você deve, primeiro, programar sua viagem de avião.";
        infoButton.onClick.AddListener(delegate
        {
            StartCoroutine(Part1());
        });
    }
    private void Update()
    {
        textCounter.text = counter.ToString();
        if (counter == 1)
        {
            titleTextB.text = "LAZER";
            titleTextC.text = "Você poderá passear em locais ir à um estádio, etc.";

            safe1.enabled = true;
            safe2.enabled = false;
            safe3.enabled = false;

        }
        else if (counter == 2)
        {
            titleTextB.text = "FUTURO";
            titleTextC.text = "Cursos de idiomas: JAPONÊS, ESPANHOL, ou aperfeiçoar seu INGLÊS ficando bom.";
            safe2.enabled = true;
            safe1.enabled = false;
            safe3.enabled = false;

        }
        else if (counter == 3)
        {
            titleTextB.text = "IMPREVISTOS";
            titleTextC.text = "nunca se sabe, imprevistos acontecem...";
            safe3.enabled = true;
            safe1.enabled = false;
            safe2.enabled = false;

        }else if (counter == 4) StartCoroutine(Part2End());
    }
    private IEnumerator Part1()
    {

        PlayerManager.pm.vault = 220;

        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel1, true);
        BGA.SetActive(false);
        BGC.SetActive(false);
        BGB.SetActive(true);
        
        p1Button1.onClick.AddListener(delegate { PanelButtonOneOption(this, 430, p1b1Price, p1b1Selected, p1b2Selected, p1b3Selected, p1Submit); });
        p1Button2.onClick.AddListener(delegate { PanelButtonOneOption(this, 620, p1b2Price, p1b2Selected, p1b1Selected, p1b3Selected, p1Submit); });
        p1Button3.onClick.AddListener(delegate { PanelButtonOneOption(this, 750, p1b3Price, p1b3Selected, p1b1Selected, p1b2Selected, p1Submit); });
        p1SubmitButton.onClick.AddListener(delegate { StartCoroutine(Part1End()); });
    }

    private IEnumerator Part1End()
    {

        StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));
        
        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        BGA.SetActive(true);
        BGC.SetActive(false);
        BGB.SetActive(false);
        PlayerManager.pm.AddCoins(totalCoins + bonusCoins);
        

        infoText.text = "Agora você pode escolher como irá se programar para curtir sua viagem e assim, determinar como deseja separar seu orçamento para utilizar com diferentes finalidades. Você tem um total de 220 reais! Escolha como quer separar este valor em cada uma das opções apresentadas:";
        counter = 1;
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { AudioManager.am.PlaySFX(AudioManager.am.button); FlavorManager.fm.ShowHidePanel(infoPanel, false); FlavorManager.fm.ShowHidePanel(optionPanel2, true); Part2Start(); infoButton.onClick.RemoveAllListeners(); });

    }

    private void Part2Start()
    {
        
        safeButton1.gameObject.SetActive(true);
        safeButton2.gameObject.SetActive(true);
        safeButton3.gameObject.SetActive(true);
        StartCoroutine(Part2());

    }

    private IEnumerator Part2()
    {
        

        if (counter < 4)
        {
            yield return new WaitForSeconds(0.5f);
   
            BGA.SetActive(false);
            BGC.SetActive(true);
            BGB.SetActive(false);
            titleText.text = "Quantos reais quer depositar no cofre? " ;

            safeButton1.onClick.AddListener(delegate { Part2Choice(this, safeButton1Selected, safeButton1.gameObject, 70); PlayerManager.pm.charger = (true); });
            safeButton2.onClick.AddListener(delegate { Part2Choice(this, safeButton2Selected, safeButton2.gameObject, 50); PlayerManager.pm.charger = (false); });
            safeButton3.onClick.AddListener(delegate { Part2Choice(this, safeButton3Selected, safeButton3.gameObject, 100); PlayerManager.pm.charger = (true); });

            safe1.enabled = false;
            safe2.enabled = false;
            safe3.enabled = false;
 
        } else titleText.text = "término da etapa... " + counter;
        //else StartCoroutine(Part2End());

        //p2SubmitButton.onClick.AddListener(delegate { StartCoroutine(Part2End()); });
    }

    private static void Part2Choice(A2L1 instance, Image selected, GameObject coin, int price)
    {
        //instance.totalCoins = 220;
        instance.totalCoinsText.text = instance.totalCoins.ToString();
        instance.coins = instance.totalCoins;

        instance.safeButton1Selected.enabled = false;
        instance.safeButton2Selected.enabled = false;
        instance.safeButton3Selected.enabled = false;

        instance.coins -= price;
        instance.totalCoinsText.text = instance.coins.ToString();
        selected.enabled = true;

        instance.nextButton.gameObject.SetActive(true);
        instance.nextButton.onClick.RemoveAllListeners();
        instance.nextButton.onClick.AddListener(delegate
        {
            instance.nextButton.onClick.RemoveAllListeners();
            instance.nextButton.gameObject.SetActive(false);
            AudioManager.am.PlaySFX(AudioManager.am.button);

            if (instance.counter == 1) PlayerManager.pm.safe1 = price;
            if (instance.counter == 2) PlayerManager.pm.safe2 = price;
            if (instance.counter == 3) PlayerManager.pm.safe3 = price; 

            instance.totalCoins -= price;
            instance.counter++;
            coin.SetActive(false);
            instance.titleText.text = "Quantos reais quer depositar no cofre? " ;

            Image safeImage = null;
            
            
            if (instance.counter == 2)
            {
                safeImage = instance.safeButton2Selected;
                
            }
            else if (instance.counter == 3) safeImage = instance.safeButton3Selected;
            safeImage = instance.safeButton3Selected;
            

            // instance.Part2();
        });
    }

    private IEnumerator Part2End()
    {
        counter = 1;
       
        StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));

        infoText.text = "Você guardou uma reserva para futuros imprevistos!";
        FlavorManager.fm.ShowHidePanel(optionPanel2, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        
        yield return new WaitForSeconds(3f);
        infoText.text = "Passou uma semana e você irá decidir o LAZER e o FUTURO:";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { AudioManager.am.PlaySFX(AudioManager.am.button); StartCoroutine(Part3()); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part3()
    {
        
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel3, true);

        p3Button1.onClick.AddListener(delegate { PlayerManager.pm.basketGame = true; PanelButtonOneOption(this, 50, p3b1Price, p3b1Selected, p3b2Selected, p3b3Selected, p3Submit); });
        p3Button2.onClick.AddListener(delegate { PlayerManager.pm.tenisGame = true;  PanelButtonOneOption(this, 70, p3b2Price, p3b2Selected, p3b1Selected, p3b3Selected, p3Submit); });
        p3Button3.onClick.AddListener(delegate { PlayerManager.pm.gym = true;        PanelButtonOneOption(this, 70, p3b3Price, p3b3Selected, p3b1Selected, p3b2Selected, p3Submit); });

        p3SubmitButton.onClick.AddListener(delegate { StartCoroutine(Part4()); });


    }

    private IEnumerator Part4()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel3, false);
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel4, true);

        p4Button1.onClick.AddListener(delegate { PlayerManager.pm.koreanCourse = true; PanelButtonOneOption(this, 50, p4b1Price, p4b1Selected, p4b2Selected, p4b3Selected, p3Submit); });
        p4Button2.onClick.AddListener(delegate { PlayerManager.pm.spanishCourse = true; PanelButtonOneOption(this, 70, p4b2Price, p4b2Selected, p4b1Selected, p4b3Selected, p3Submit); });
        p4Button3.onClick.AddListener(delegate { PlayerManager.pm.englishCourse = true; PanelButtonOneOption(this, 70, p4b3Price, p4b3Selected, p4b1Selected, p4b2Selected, p3Submit); });

        p4SubmitButton.onClick.RemoveAllListeners();
        p4SubmitButton.onClick.AddListener(delegate { StartCoroutine(Part5()); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });

    }

    private IEnumerator Part5()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel4, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel5, true);
        infoText.text = "";
        p5SubmitButton.onClick.AddListener(delegate { StartCoroutine(End()); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
    }

    public void AddItem(string essential, bool right, string title)
    {
        if (right)
        {
            //to do - (aguardando dados)
            infoText.text += title + ": 15 reais! (" + essential + ")\n";
            bonus += 15;
        }
        else infoText.text += title + ": 0 reais. (" + essential + ")\n";

        itemCounter++;

        if (itemCounter == 6) StartCoroutine(End());
   


    }

 

    private IEnumerator End()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel5, false);
        resultText.text = "Sobraram " + totalCoins + " reais e você conseguiu ganhar mais " + bonusCoins + " de bônus! Até a próxima!\n";
       
        yield return new WaitForSeconds(0.5f);

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        incomeText.text = (totalCoins + bonusCoins).ToString();

        StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));
        PlayerManager.pm.AddCoins(totalCoins + bonusCoins);
        APIManager.am.Relatorio(conceitos);

        //to do - (criar novo menu ?)
        
         PlayerManager.pm.AddLevel();
        resultButton.onClick.AddListener(delegate
        {
            AudioManager.am.voiceChannel.Stop();
            GameManager.gm.LoadScene("Menu");
            AudioManager.am.PlaySFX(AudioManager.am.button);
        });
        //GameManager.gm.LoadScene("A2L2");
    }

    private static void PanelButtonOneOption(A2L1 instance, int price, GameObject priceObject, GameObject selectedObject, GameObject otherSelectedObject1, GameObject otherSelectedObject2, GameObject pSubmit)
    {
        if (!otherSelectedObject1.activeSelf && !otherSelectedObject2.activeSelf)
        {
            if (priceObject.activeSelf)
            {
                if (instance.totalCoins >= price)
                {
                    instance.totalCoins -= price;
                    instance.totalCoinsText.text = instance.totalCoins.ToString();

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
                        instance.infoButton.onClick.RemoveAllListeners();
                    });
                }
            }
            else
            {
                instance.totalCoins += price;
                instance.totalCoinsText.text = instance.totalCoins.ToString();

                priceObject.SetActive(true);
                selectedObject.SetActive(false);
            }
        }

        if (selectedObject.activeSelf || otherSelectedObject1.activeSelf || otherSelectedObject2.activeSelf) pSubmit.SetActive(true);
        else pSubmit.SetActive(false);
    }

    private static void PanelButton(A2L1 instance, int price, int bonus, ref int totalPrice, TextMeshProUGUI totalPriceText, GameObject priceObject, GameObject selectedObject)
    {
        if (priceObject.activeSelf)
        {
            if (instance.totalCoins >= price)
            {
                instance.totalCoins -= price;
                instance.totalCoinsText.text = instance.totalCoins.ToString();

                instance.bonusCoins += bonus;

                if (bonus != 0)
                {
                    //to do - (aguardando dados)
                    instance.conceitos["18"] += 1.5;
                  
                }

                totalPrice += price;
                totalPriceText.text = totalPrice.ToString();

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
                    instance.infoButton.onClick.RemoveAllListeners();
                });
            }
        }
        else
        {
            instance.totalCoins += price;
            instance.totalCoinsText.text = instance.totalCoins.ToString();

            instance.bonusCoins -= bonus;
            totalPrice -= price;
            totalPriceText.text = totalPrice.ToString();

            priceObject.SetActive(true);
            selectedObject.SetActive(false);
        }
    }




}
