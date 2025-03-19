using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V2L21 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public RectTransform coinStart, coinWaypoint;

    public Animator animatorAdult;
    public TweenCharacters animatorPlayer;
    public GameObject optionPanel1, optionPanel2;
    public TextMeshProUGUI p1Text1, p1Text2;
    public Button p1SubmitButton;
    public Button p2Button1, p2Button2;

    public int income;
    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI incomeTitle;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        // Declaração
        conceitos.Add("25", 0);
        conceitos.Add("72", 0);
        conceitos.Add("33", 0);
        conceitos.Add("31", 0);
        conceitos.Add("22", 0);
        conceitos.Add("71", 0);
        conceitos.Add("73", 0);
        conceitos.Add("74", 0);

        conceitos["25"] += 0;
        conceitos["72"] += 0;
        conceitos["33"] += 0;
        conceitos["31"] += 0;
        conceitos["22"] += 0;
        conceitos["71"] += 0;
        conceitos["73"] += 0;
        conceitos["74"] += 0;

        //help
        AudioManager.am.PlayVoice(AudioManager.am.v2start[20]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Um mês se passou, e você está de volta ao banco.";
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part1()
    {
        AudioManager.am.voiceChannel.Stop();
        animatorPlayer.GoPath();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);

        yield return new WaitForSeconds(2);
        animatorAdult.SetTrigger("ShowCard");
        yield return new WaitForSeconds(2);

        FlavorManager.fm.ShowHidePanel(optionPanel1, true);

        int choice = PlayerManager.pm.v2l20choice;
        if (choice == 1)
        {
            p1Text1.text = "Você: Eu vou devolver os 35 reais que emprestou, acabei não usando. Quanto falta para completar? ";
            p1Text2.text = "Gerente: São mais 14 reais pelo empréstimo. Com os 35, totaliza 49 reais!";
            PlayerManager.pm.RemoveCoins(49); income -= 49;
            StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinStart, coinWaypoint));
        }
        else if (choice == 2)
        {
            p1Text1.text = "Você: Eu vim buscar os 35 reais, mais os 7 reais rendidos por ter guardado";
            p1Text2.text = "Gerente: Com certeza, senhor. Aqui estão seus 42 reais, faça bom proveito.";
            PlayerManager.pm.AddCoins(42); income += 42;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        }
        else if (choice == 3)
        {
            p1Text1.text = "Você: Obrigado pelo seguro, ele foi útil quando tive problemas!";
            p1Text2.text = "Gerente: Ele durará 11 meses ainda! Use à vontade por esse período!";
        }
        else
        {
            p1Text1.text = "Você: Vim buscar os meus 35 reais e verificar, também, se ganhei algum prêmio!";
            p1Text2.text = "Gerente: Infelizmente, o sorteio já ocorreu e você não ganhou o prêmio. Estão aqui seus 35 reais!";
            PlayerManager.pm.AddCoins(35); income += 35;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        }

        p1SubmitButton.onClick.AddListener(delegate { StartCoroutine(Part1End()); p1SubmitButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part1End()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoText.text = "Ótimo, vamos para a pergunta que vale 20 reais!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part2()); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part2()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel2, true);

        p2Button1.onClick.AddListener(delegate 
        {
            conceitos["25"] += 3;
            conceitos["72"] += 2;
            conceitos["33"] += 4;
            conceitos["31"] += 3;
            conceitos["22"] += 2;
            conceitos["71"] += 2;
            conceitos["73"] += 2;
            conceitos["74"] += 2;

            infoText.text = "Certo! Ganhou 20 reais!";
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            PlayerManager.pm.AddCoins(20); income += 20;
            StartCoroutine(Part2End());
            p2Button1.onClick.RemoveAllListeners();
        });
        p2Button2.onClick.AddListener(delegate
        {
            conceitos["25"] += 3;
            conceitos["72"] += 0;
            conceitos["33"] += 0;
            conceitos["31"] += 2;
            conceitos["22"] += 2;
            conceitos["71"] += 1;
            conceitos["73"] += 0;
            conceitos["74"] += 1;

            infoText.text = "Errado! Você paga uma taxa em dinheiro ao escolher retirar dinheiro na função crédito.\n\n" +
            "A função débito é gratuíta!";
            StartCoroutine(Part2End());
            p2Button2.onClick.RemoveAllListeners();
        });
    }

    private IEnumerator Part2End()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel2, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { StartCoroutine(End()); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator End()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);

        if (income < 0)
        {
            income *= -1;
            incomeTitle.text = "Retirou do Bolodix";
        }

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = income.ToString();

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Tudo feito por hoje! Até a próxima!";

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }
}
