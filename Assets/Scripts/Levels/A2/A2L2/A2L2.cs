using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class A2L2 : MonoBehaviour
{

    public GameObject infoPanel;
    public GameObject BGA, BGB, BGC, chargerObj;
    public TextMeshProUGUI infoText, infoAvatarText, infoTaxiText, infoHotelText, infoAvatarHotelText, infoStoreText, infoAvatarStoreText, infoBankText, infoAvatarBankText, infoTicketText;
    public Button infoButton;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public GameObject infoAvatar, infoTaxi;
    public GameObject LoadTime;
    private int totalCoins = 1000;
    private int bonusCoins;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;
    public GameObject optionPanel0, optionPanel1, optionPanel2, optionPanel3, optionPanel4, optionPanel5, optionPanel6, optionPanel7, optionPanel7b, optionPanel8, optionPanel8b;
    public Button btGoStore, btGoBank;
 
    public GameObject p2Submit;
    public Button p2SubmitButton;
    public Button p2Button1, p2Button2, p2Button3;
    public GameObject p2b1Price, p2b2Price, p2b3Price;
    public GameObject p2b1Selected, p2b2Selected, p2b3Selected;

    public GameObject p1Submit;
    public Button p1SubmitButton;
    public Button p1Button1, p1Button2, p1Button3;
    public GameObject p1b1Price, p1b2Price, p1b3Price;
    public GameObject p1b1Selected, p1b2Selected, p1b3Selected;

    public Button[] p3Button;
    public GameObject[] p3bPrice;
    public GameObject[] p3bSelected;
    private int p3Total;
    public TextMeshProUGUI p3TotalText;
    public GameObject p3Submit;
    public Button p3SubmitButton;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        // BGA.SetActive(false); BGC.SetActive(false); BGC.SetActive(false);
        btGoStore.gameObject.SetActive(false);
        btGoBank.gameObject.SetActive(false);
        totalCoinsText.text = totalCoins.ToString();

        FlavorManager.fm.ShowHidePanel(optionPanel0, true);
        StartCoroutine(Part1());

    }

    private IEnumerator TimePanel()
    {

        FlavorManager.fm.ShowHidePanel(LoadTime, true);

        yield return new WaitForSeconds(2.0f);
        FlavorManager.fm.ShowHidePanel(LoadTime, false);

    }


    private IEnumerator Part1()
    {
        yield return new WaitForSeconds(5.0f);
        FlavorManager.fm.ShowHidePanel(optionPanel0, false);
        yield return new WaitForSeconds(1.0f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Chegando na Coreia do Sul seu avatar fica em dúvida de como chegar no hotel, e vai pedir ajuda ao taxista:";


        infoButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            BGA.SetActive(true);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
            infoButton.onClick.RemoveAllListeners();
            StartCoroutine(Part1b());

        });

    }

    private IEnumerator Part1b()
    {

        BGA.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel1, true);

        yield return new WaitForSeconds(1.0f);
        infoAvatarText.text = "Preciso ir para o hotel.";
        yield return new WaitForSeconds(1.5f);
        infoAvatarText.text = "";
        yield return new WaitForSeconds(0.5f);
        infoTaxiText.text = "Você fala inglês?";
        yield return new WaitForSeconds(2.0f);

        if (PlayerManager.pm.koreanCourse)
        {
            infoAvatarText.text = "Não falo inglês muito bem...e falo muito pouco coreano...";
            yield return new WaitForSeconds(1.5f);
            infoText.text = "Que pena! Seu avatar tentou falar coreano mas você fez 3 meses de curso, não teve o retorno esperado pois foi muito pouco tempo. Caso tivesse escolhida fazer o curso em inglês teria uma melhor conversa com o taxista!";
            FlavorManager.fm.ShowHidePanel(infoPanel, true);

            infoButton.onClick.AddListener(delegate
            {
                AudioManager.am.PlaySFX(AudioManager.am.button);
                FlavorManager.fm.ShowHidePanel(infoPanel, false);
                infoButton.onClick.RemoveAllListeners();
                StartCoroutine(Part1End());

            });

        }
        else if (PlayerManager.pm.spanishCourse)
        {
            infoAvatarText.text = "Não falo inglês muito bem...e sei pouco espanhol...";
            yield return new WaitForSeconds(2.5f);
            infoText.text = "Que pena! Seu avatar tentou falar espanhol mas o taxista só fala inglês e sua língua nativa!  “Língua nativa é a língua da região praticada aonde a pessoa nasceu, assim na Coréia do Sul a língua nativa é o coreano.”";
            FlavorManager.fm.ShowHidePanel(infoPanel, true);

            infoButton.onClick.AddListener(delegate
            {
                AudioManager.am.PlaySFX(AudioManager.am.button);
                FlavorManager.fm.ShowHidePanel(infoPanel, false);
                infoButton.onClick.RemoveAllListeners();
                StartCoroutine(Part1End());

            });



        }
        else if (PlayerManager.pm.englishCourse)
        {
            infoAvatarText.text = "YES! Falo muito bem inglês!";
            yield return new WaitForSeconds(1.5f);
            infoText.text = "Muito bem! Você ganhou 200 pontos extras para a bolonave, você fez o curso de inglês e vai conseguir chegar no hotel rapidamente!";
            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoButton.onClick.AddListener(delegate
            {

                AudioManager.am.PlaySFX(AudioManager.am.button);
                FlavorManager.fm.ShowHidePanel(infoPanel, false);
                infoButton.onClick.RemoveAllListeners();
                StartCoroutine(Part1End());

            });

        }


    }

    private IEnumerator Part1End()
    {
        yield return new WaitForSeconds(0.5f);
        if (PlayerManager.pm.englishCourse)
        {
            infoAvatarText.text = "GREAT! Chegarei rápido ao hotel e pegarei algumas dicas com o motorista...";
            yield return new WaitForSeconds(1.5f);
            infoTaxiText.text = "YES my friend!";
            //infoText.text = "Por causa do seu bom inglês você chegará adiantado ao hotel sua corrida custou apenas o equivalente a 25 reais e está pegando dicas com motorista!";
            yield return new WaitForSeconds(3.5f);

            StartCoroutine(taxiGo());


        }
        else if (PlayerManager.pm.spanishCourse || PlayerManager.pm.koreanCourse)
        {
            infoAvatarText.text = "Puxa vou ter tentar usar um aplicativo para tentar se comunicar com o motorista, isso vai me atrasar um pouco...";
            yield return new WaitForSeconds(1.5f);
            infoTaxiText.text = " ?... :> (< ^.";
            yield return new WaitForSeconds(3.5f);

            StartCoroutine(taxiGo());

        }

    }

    private IEnumerator taxiGo()
    {

        if (PlayerManager.pm.englishCourse)
        {
            BGA.SetActive(false); BGC.SetActive(true); BGB.SetActive(false);
            infoText.text = "Por causa do seu bom inglês você chegará adiantado ao hotel sua corrida custou apenas o equivalente a 25 reais e está pegando dicas com motorista!";
            FlavorManager.fm.ShowHidePanel(optionPanel1, false);

            StartCoroutine(TimePanel());
            yield return new WaitForSeconds(2.5f);

            FlavorManager.fm.ShowHidePanel(optionPanel2, true);
            yield return new WaitForSeconds(4.5f);


            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoButton.onClick.AddListener(delegate
            {

                AudioManager.am.PlaySFX(AudioManager.am.button);

                infoButton.onClick.RemoveAllListeners();

                AudioManager.am.PlaySFX(AudioManager.am.button);
                FlavorManager.fm.ShowHidePanel(infoPanel, false);
                infoText.text = "";
                FlavorManager.fm.ShowHidePanel(optionPanel2, false);
                infoButton.onClick.RemoveAllListeners();

                StartCoroutine(Part2());

            });


        }
        else if (PlayerManager.pm.spanishCourse || PlayerManager.pm.koreanCourse)
        {

            BGA.SetActive(false); BGC.SetActive(true); BGB.SetActive(false);
            infoText.text = "O motorista deu muitas voltas para chegar ao hotel pois não lhe entendeu muito bem e sua corrida custou 40 reais!";
            FlavorManager.fm.ShowHidePanel(optionPanel1, false);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(TimePanel());

            yield return new WaitForSeconds(2.0f);

            FlavorManager.fm.ShowHidePanel(optionPanel2, true);

            yield return new WaitForSeconds(4.5f);


            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoButton.onClick.AddListener(delegate
            {

                AudioManager.am.PlaySFX(AudioManager.am.button);
                BGA.SetActive(false); BGC.SetActive(true); BGB.SetActive(false);
                infoButton.onClick.RemoveAllListeners();

                AudioManager.am.PlaySFX(AudioManager.am.button);
                FlavorManager.fm.ShowHidePanel(infoPanel, false);
                infoText.text = "O motorista deu muitas voltas para chegar ao hotel pois não lhe entendeu muito bem e sua corrida custou 40 reais!";
                FlavorManager.fm.ShowHidePanel(optionPanel2, false);
                infoButton.onClick.RemoveAllListeners();


                StartCoroutine(Part2());

            });

        }


    }

    private IEnumerator Part2()
    {
        //yield return new WaitForSeconds(0.5f);
        StartCoroutine(TimePanel());
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(1.0f);
        FlavorManager.fm.ShowHidePanel(optionPanel3, true);

        yield return new WaitForSeconds(1.5f);
        infoAvatarHotelText.text = "Olá tudo bem? Tenho uma reserva com vocês.";
        yield return new WaitForSeconds(2.5f);

        infoAvatarHotelText.text = "";
        infoHotelText.text = "Olá! Seja bem vindo(a) ao Hotel. Já preparamos tudo para sua estadia.";
        yield return new WaitForSeconds(3.5f);

        infoAvatarHotelText.text = "Muito Obrigado(a)";
        infoHotelText.text = "";
        yield return new WaitForSeconds(3.0f);

        infoAvatarHotelText.text = "Preciso carregar meu smartphone... Mas não estou encontrando o carregador...";
        yield return new WaitForSeconds(4.0f);
        infoAvatarHotelText.text = "Acho que perdi meu carregador... melhor eu comprar um novo carregador com urgência...";
        yield return new WaitForSeconds(4.0f);
        infoHotelText.text = "Que pena! Fique tranquilo você poderá comprar um novo na loja ao lado do hotel.";
        infoAvatarHotelText.text = "";
        yield return new WaitForSeconds(4.0f);

        infoAvatarHotelText.text = "Ok. Agradeço a dica! Irei agora mesmo!";
        yield return new WaitForSeconds(3.0f);
        infoHotelText.text = "Eu que agradeço! Boa sorte e bom passeio!";
        yield return new WaitForSeconds(4.0f);


        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Agora você precisa ir a loja e encontrar um carregador para seu celular!";

        infoButton.onClick.AddListener(delegate
        {

            AudioManager.am.PlaySFX(AudioManager.am.button);
            //BGA.SetActive(false); BGC.SetActive(true); BGB.SetActive(false);
            infoButton.onClick.RemoveAllListeners();

            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);

            FlavorManager.fm.ShowHidePanel(optionPanel3, false);
            infoButton.onClick.RemoveAllListeners();

            StartCoroutine(Part2b());


        });


    }

    private IEnumerator Part2b()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel4, true); FlavorManager.fm.ShowHidePanel(optionPanel5, false); FlavorManager.fm.ShowHidePanel(optionPanel6, false);


        yield return new WaitForSeconds(1.50f);


        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Vamos lá! Escolha um destino que julgue a melhor opção. ";
        infoButton.onClick.AddListener(delegate
        {
            btGoStore.gameObject.SetActive(true);
            btGoBank.gameObject.SetActive(true);
            infoButton.onClick.RemoveAllListeners();

            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);

            infoButton.onClick.RemoveAllListeners();




        });
        btGoStore.onClick.AddListener(delegate
        {

            btGoStore.gameObject.SetActive(false);
            btGoBank.gameObject.SetActive(false);
            AudioManager.am.PlaySFX(AudioManager.am.button);
            StartCoroutine(TimePanel());
            infoButton.onClick.RemoveAllListeners();
            StartCoroutine(goStore());



        });

        btGoBank.onClick.AddListener(delegate
        {
            btGoStore.gameObject.SetActive(false);
            btGoBank.gameObject.SetActive(false);
            AudioManager.am.PlaySFX(AudioManager.am.button);
            StartCoroutine(TimePanel());
            infoButton.onClick.RemoveAllListeners();
            StartCoroutine(goBank());



        });



    }


    private IEnumerator goStore()
    {
        yield return new WaitForSeconds(1.50f);
        FlavorManager.fm.ShowHidePanel(optionPanel4, false);
        FlavorManager.fm.ShowHidePanel(optionPanel5, true);
        yield return new WaitForSeconds(1.50f);
        infoStoreText.text = "Olá jovem! Seja bem vindo(a) a nossa loja de acessórios de celulares e informática! ";
        yield return new WaitForSeconds(2.50f);
        infoStoreText.text = "Posso ajudá-lo(a) em algum produto específico? ";
        yield return new WaitForSeconds(2.50f);
        infoAvatarStoreText.text = "Olá! Que loja bacana! ";
        yield return new WaitForSeconds(2.50f);
        infoAvatarStoreText.text = "Preciso de uma carregador compatível com meu smartfone.";
        yield return new WaitForSeconds(2.50f);
        infoStoreText.text = "Sim! Compatível com seu aparelho nós temos este modelo de R$ 70,00 ";
        yield return new WaitForSeconds(2.50f);
        chargerObj.gameObject.SetActive(true);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        if (PlayerManager.pm.charger)
        {

            infoText.text = "Confirme a compra do carregador.";

        }
        else
        {
            infoText.text = "Você não tem o suficiente! Você poderá vender o ticket do jogo por 50 reais ou você vai até o banco pedir um dinheiro mas pagará 30 reais quando chegar no brasil por esse valor de 20 reais que falta.";

        }


        infoButton.onClick.AddListener(delegate
        {
            chargerObj.gameObject.SetActive(false);
            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);


            if (PlayerManager.pm.charger)
            {
                infoButton.onClick.RemoveAllListeners();
                StartCoroutine(Part2c());
            }
            else if (!PlayerManager.pm.charger)
            {
                infoButton.onClick.RemoveAllListeners();
                StartCoroutine(Part2b());
            }



        });



    }
    private IEnumerator goBank()
    {
        yield return new WaitForSeconds(1.50f);
        FlavorManager.fm.ShowHidePanel(optionPanel4, false);
        FlavorManager.fm.ShowHidePanel(optionPanel6, true);
        yield return new WaitForSeconds(.50f);
        infoBankText.text = "Seja bem vindo ao banco garoto! Eu sou o gerente. ";
        yield return new WaitForSeconds(2.50f);
        infoBankText.text = "Em que posso servi-lo? ";
        yield return new WaitForSeconds(2.50f);
        infoAvatarBankText.text = "Obrigado Sr. gerente. ";
        infoBankText.text = "";
        yield return new WaitForSeconds(1.50f);
        infoAvatarBankText.text = "Preciso de um empréstimo emergencial.";
        yield return new WaitForSeconds(2.50f);
        infoBankText.text = "Sim claro! Estou checando sua conta...";
        yield return new WaitForSeconds(3.0f);
        infoBankText.text = "Pronto! Já está na conta. Agradecemos a preferência.";
        yield return new WaitForSeconds(2.50f);
        infoAvatarBankText.text = "Muito Obrigado(a) Sr. gerente! ";
        yield return new WaitForSeconds(2.50f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você conseguiu um empréstimo para comprar o carregador, porém terá que pagar R$ 30,00 de juros quando chegar ao Brasil.";
        infoButton.onClick.AddListener(delegate
        {

            infoButton.onClick.RemoveAllListeners();
            PlayerManager.pm.charger = (true);
            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);

            infoButton.onClick.RemoveAllListeners();

            StartCoroutine(Part2b());


        });



    }



    private IEnumerator Part2c()
    {

        yield return new WaitForSeconds(1.50f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = infoText.text = "Parabéns! Você conseguiu resolver seu problema! Ufa você se preveniu e tinha o dinheiro guardado para esse momento. Sempre guarde dinheiro para esses momentos.Você ganhou 5 pontos extras para a bolonave.";
        yield return new WaitForSeconds(1.50f);


        infoButton.onClick.AddListener(delegate
        {

            infoButton.onClick.RemoveAllListeners();

            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);

            infoButton.onClick.RemoveAllListeners();
            StartCoroutine(Part3());



        });

    }

    private IEnumerator Part3()
    {

        StartCoroutine(TimePanel());
        yield return new WaitForSeconds(1.50f);
        FlavorManager.fm.ShowHidePanel(optionPanel5, false);
        FlavorManager.fm.ShowHidePanel(optionPanel7, true);
        yield return new WaitForSeconds(.50f);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Hoje é o dia dos eventos esportivos.";
        infoButton.onClick.AddListener(delegate
        {

            infoButton.onClick.RemoveAllListeners();

            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);

            infoButton.onClick.RemoveAllListeners();

            StartCoroutine(Part4());


        });




    }






    private IEnumerator Part4()
    {

        yield return new WaitForSeconds(1.0f);
        infoText.text = "Chegando próximo ao estádio você encontrou uma loja de artigos esportivos! Talvez seja uma boa ideia dar uma olhada.";
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoButton.onClick.AddListener(delegate
        {

            infoButton.onClick.RemoveAllListeners();

            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);

            infoButton.onClick.RemoveAllListeners();
            FlavorManager.fm.ShowHidePanel(optionPanel7b, true);


        });


        p2Button1.onClick.AddListener(delegate { PanelButtonOneOption(this, 10, p2b1Price, p2b1Selected, p2b2Selected, p2b3Selected, p2Submit); });
        p2Button2.onClick.AddListener(delegate { PanelButtonOneOption(this, 30, p2b2Price, p2b2Selected, p2b1Selected, p2b3Selected, p2Submit); });
        p2Button3.onClick.AddListener(delegate { PanelButtonOneOption(this, 20, p2b3Price, p2b3Selected, p2b1Selected, p2b2Selected, p2Submit); });
        p2SubmitButton.onClick.AddListener(delegate { StartCoroutine(Part5()); });

    }

    private IEnumerator Part5()
    {

        StartCoroutine(TimePanel());
        yield return new WaitForSeconds(1.0f);
        FlavorManager.fm.ShowHidePanel(optionPanel7b, false); FlavorManager.fm.ShowHidePanel(optionPanel7, false);
        yield return new WaitForSeconds(1.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel8, true);
        yield return new WaitForSeconds(1.0f);
        infoText.text = "Que legal! O evento foi sensacional! Agora bateu aquela fome! Você pode comer um lanche, ir a um restaurante ou comer no hotel";
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoButton.onClick.AddListener(delegate
        {

            infoButton.onClick.RemoveAllListeners();

            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);

            infoButton.onClick.RemoveAllListeners();
            FlavorManager.fm.ShowHidePanel(optionPanel8b, true);


        });



        p1Button1.onClick.AddListener(delegate { PanelButtonOneOption(this, 25, p1b1Price, p1b1Selected, p1b2Selected, p1b3Selected, p1Submit); });
        p1Button2.onClick.AddListener(delegate { PanelButtonOneOption(this, 20, p1b2Price, p1b2Selected, p1b1Selected, p1b3Selected, p1Submit); });
        p1Button3.onClick.AddListener(delegate { PanelButtonOneOption(this, 0, p1b3Price, p1b3Selected, p1b1Selected, p1b2Selected, p1Submit); });
        p1SubmitButton.onClick.AddListener(delegate { StartCoroutine(End()); });

    }






    private IEnumerator End()
    {
        resultText.text = "Sobraram " + totalCoins + " reais e você conseguiu ganhar mais " + bonusCoins + " de bônus! Até a próxima!\n";

        for (int i = 0; i < p3bSelected.Length; i++)
        {
            if (p3bSelected[i].activeSelf)
            {
                //to do - (aguardando dados)
                if (i == 0) resultText.text += "\n - item1 + 10 de BÔNUS!";
                else if (i == 1) resultText.text += "\n - item2: 0 reais.";
                else if (i == 2) resultText.text += "\n - item3: 25 reais + 10 de BÔNUS!";
                else if (i == 3) resultText.text += "\n - item4 + 10 de BÔNUS!";

                else resultText.text += "ERROR";
            }
        }

        FlavorManager.fm.ShowHidePanel(optionPanel3, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = (totalCoins + bonusCoins).ToString();

        if (p3Total > 0) StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));
        PlayerManager.pm.AddCoins(totalCoins + bonusCoins);
        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }

    private static void PanelButtonOneOption(A2L2 instance, int price, GameObject priceObject, GameObject selectedObject, GameObject otherSelectedObject1, GameObject otherSelectedObject2, GameObject pSubmit)
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


    private static void PanelButton(A2L2 instance, int price, int bonus, ref int totalPrice, TextMeshProUGUI totalPriceText, GameObject priceObject, GameObject selectedObject)
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
                    instance.conceitos["11"] += 1.5;
                    instance.conceitos["12"] += 1.5;

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


    private static void PanelButton2(A2L2 instance, int price, int bonus, ref int totalPrice, TextMeshProUGUI totalPriceText, GameObject priceObject, GameObject selectedObject)
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
                    instance.conceitos["11"] += 1.5;
                    instance.conceitos["12"] += 1.5;

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
