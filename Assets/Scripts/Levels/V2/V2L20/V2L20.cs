using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V2L20 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public RectTransform coinStart, coinWaypoint;

    public Animator animatorAdult;
    public TweenCharacters animatorPlayer;
    public GameObject optionPanel1, optionPanel2, optionPanel3;
    public Button p1Button1, p1Button2, p1Button3, p1Button4;
    public TextMeshProUGUI p2Text;
    public Button p2Button1, p2Button2;
    public Button p3Button1, p3Button2, p3Button3, p3Button4;

    public int income;
    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        // Declaração
        conceitos.Add("44", 0);
        conceitos.Add("20", 0);
        conceitos.Add("25", 0);
        conceitos.Add("10", 0);
        conceitos.Add("33", 0);
        conceitos.Add("31", 0);
        conceitos.Add("22", 0);
        conceitos.Add("71", 0);

        conceitos["44"] += 0;
        conceitos["20"] += 0;
        conceitos["25"] += 0;
        conceitos["10"] += 0;
        conceitos["33"] += 0;
        conceitos["31"] += 0;
        conceitos["22"] += 0;
        conceitos["71"] += 0;

        //AudioManager.am.PlayVoice(AudioManager.am.v2start[19]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você juntou parte do salário do seu trabalho. \n \n Agora tem 200 reais para guardar no banco!";
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); });
    }

    private IEnumerator Part1()
    {
        AudioManager.am.voiceChannel.Stop();
        animatorPlayer.GoPath();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);

        yield return new WaitForSeconds(2);
        animatorAdult.SetTrigger("ShowCard");
        yield return new WaitForSeconds(2);
        Part1Start();
    }

    private void Part1Start()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel1, true);

       // p1Button1.onClick.AddListener(delegate { StartCoroutine(Part1End(1)); PlayerManager.pm.AddCoins(0); income += 210; StartCoroutine(FlavorManager.fm.SpawnBucks(5)); });
        p1Button1.onClick.AddListener(delegate { StartCoroutine(Part1End(1));  });
        p1Button2.onClick.AddListener(delegate { StartCoroutine(Part1End(2)); });
      /*  p1Button3.onClick.AddListener(delegate
        {
            if (!PlayerManager.pm.ensurance)
            {
                PlayerManager.pm.dentist = true;
                StartCoroutine(Part1End(3));
            }
            else StartCoroutine(Part1Return());
        });
        */
        p1Button3.onClick.AddListener(delegate { StartCoroutine(Part1End(3)); });
        p1Button4.onClick.AddListener(delegate { StartCoroutine(Part1End(4)); });
    }

    private IEnumerator Part1Return()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoText.text = "Você já tem um plano bucal!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate 
        { 
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
            AudioManager.am.PlaySFX(AudioManager.am.button);
            infoButton.onClick.RemoveAllListeners();
            Part1Start();
        });
    }

    private IEnumerator Part1End(int choice)
    {
        p1Button1.onClick.RemoveAllListeners();
        p1Button2.onClick.RemoveAllListeners();
        p1Button3.onClick.RemoveAllListeners();
        p1Button4.onClick.RemoveAllListeners();


        PlayerManager.pm.v2l20choice = choice;
        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoText.text = "Ótimo, feito!\n\n" +
            "Espere...\n \n Agora vem uma pergunta bônus!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part3()); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part2()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel2, true);

        if (PlayerManager.pm.v2l20choice == 3 || PlayerManager.pm.ensurance)
        {
            p2Text.text = "Você já tem um plano bucal!" +
                "\n\nVocê não precisou gastar nada a mais para realizar os procedimentos!";
            p2Button1.gameObject.SetActive(true);
            p2Button1.onClick.AddListener(delegate { StartCoroutine(Part2End()); });
        }
        else
        {
            p2Text.text = "Você precisou retirar dinheiro do seu Bolodix para o tratamento.";
            p2Button2.gameObject.SetActive(true);
            p2Button2.onClick.AddListener(delegate
            {
                StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinStart, coinWaypoint));
                PlayerManager.pm.RemoveCoins(40);
                StartCoroutine(Part2End());
            });
        }
    }

    private IEnumerator Part2End()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel2, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoText.text = "Tudo acabou bem!\n\nVamos para a última pergunta!";

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part3()); AudioManager.am.PlaySFX(AudioManager.am.button); });
    }

    private IEnumerator Part3()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel3, true);

        p3Button1.onClick.AddListener(delegate 
        {
            conceitos["44"] += 0;
            conceitos["20"] += 0;
            conceitos["25"] += 1;
            conceitos["10"] += 0;
            conceitos["33"] += 0;
            conceitos["31"] += 1;
            conceitos["22"] += 2;
            conceitos["71"] += 1;

            StartCoroutine(Part3End(false)); 
        });
        p3Button2.onClick.AddListener(delegate 
        {
            conceitos["44"] += 4;
            conceitos["20"] += 4;
            conceitos["25"] += 4;
            conceitos["10"] += 5;
            conceitos["33"] += 10;
            conceitos["31"] += 8;
            conceitos["22"] += 10;
            conceitos["71"] += 10;

            StartCoroutine(Part3End(false)); 
        });
        p3Button3.onClick.AddListener(delegate 
        {
            conceitos["44"] += 2;
            conceitos["20"] += 1;
            conceitos["25"] += 1;
            conceitos["10"] += 1;
            conceitos["33"] += 2;
            conceitos["31"] += 3;
            conceitos["22"] += 3;
            conceitos["71"] += 3;

            StartCoroutine(Part3End(true)); 
        });
        p3Button4.onClick.AddListener(delegate 
        {
            conceitos["44"] += 2;
            conceitos["20"] += 2;
            conceitos["25"] += 2;
            conceitos["10"] += 4;
            conceitos["33"] += 6;
            conceitos["31"] += 3;
            conceitos["22"] += 5;
            conceitos["71"] += 5;

            StartCoroutine(Part3End(false)); 
        });
    }

    private IEnumerator Part3End(bool right)
    {
        p3Button1.onClick.RemoveAllListeners();
        p3Button2.onClick.RemoveAllListeners();
        p3Button3.onClick.RemoveAllListeners();
        p3Button4.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel3, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        if (right)
        {
            conceitos["71"] += 1;
            infoText.text = "“Parabéns!!! Você acertou a pergunta bônus muito difícil. Ganhou mais 100 reais de bônus!!! \n \nVai crescer bastante o seu Bolodix!”";
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            income += 100;
            PlayerManager.pm.AddCoins(30);
        }
        else infoText.text = "Essa não era a melhor escolha.\n\n A melhor alternativa era a 3 que diz:\n \n Depende. Bancos menores oferecem mais retorno, mas há risco de perder o dinheiro";

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { StartCoroutine(End()); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator End()
    {
        APIManager.am.Relatorio(conceitos);

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = income.ToString();

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Tudo feito por hoje! Na próxima aula, você verá o resultado de suas escolhas!";
        PlayerManager.pm.AddLevel();
    }
}