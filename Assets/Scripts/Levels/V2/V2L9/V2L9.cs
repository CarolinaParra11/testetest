using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V2L9 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    //public GameObject hintButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject giftPanel;
    public Button giftButton;
    public GameObject gift1;

    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel1, optionPanel2;
    public Button p1Button1, p1Button2, p1Button3;
    public Button p2Button1, p2Button2;
    public TextMeshProUGUI p2Text;
    public Animator animator;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        //hintButton.SetActive(false);
        AudioManager.am.PlayVoice(AudioManager.am.v2start[8]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Selecione o melhor quadro para uma sociedade ideal.";
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part1()
    {
        // Declaração
        conceitos.Add("26", 0);
        conceitos.Add("50", 0);
        conceitos.Add("31", 0);
        conceitos.Add("10", 0);
        conceitos.Add("25", 0);
        conceitos.Add("28", 0);
        conceitos.Add("52", 0);
        conceitos.Add("23", 0);

        conceitos["26"] += 0;
        conceitos["50"] += 0;
        conceitos["31"] += 0;
        conceitos["10"] += 0;
        conceitos["25"] += 0;
        conceitos["28"] += 0;
        conceitos["52"] += 0;
        conceitos["23"] += 0;

             //hintButton.SetActive(true);

        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel1, true);

        p1Button1.onClick.AddListener(delegate { StartCoroutine(Part1End(1)); p1Button1.onClick.RemoveAllListeners(); });
        p1Button2.onClick.AddListener(delegate { OptionalGift(2); p1Button2.onClick.RemoveAllListeners(); });
        p1Button3.onClick.AddListener(delegate { StartCoroutine(Part1End(3)); p1Button3.onClick.RemoveAllListeners(); });
    }

    private void OptionalGift(int choice)
    {
        p1Button1.onClick.RemoveAllListeners();
        p1Button2.onClick.RemoveAllListeners();
        p1Button3.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        FlavorManager.fm.BigFirework();
        FlavorManager.fm.ShowHidePanel(giftPanel, true);

        AudioManager.am.PlaySFX(AudioManager.am.end);
                FlavorManager.fm.BigFirework();
        gift1.SetActive(true);
        PlayerManager.pm.bonus1 = true;

        giftButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            StartCoroutine(Part1End(choice));
            giftButton.onClick.RemoveAllListeners();
            FlavorManager.fm.ShowHidePanel(giftPanel, false);
        });
    }

    private IEnumerator Part1End(int choice)
    {
        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        if (choice == 1)
        {
            conceitos["26"] += 1;
            conceitos["50"] += 2;
            conceitos["31"] += 1;
            conceitos["25"] += 3;

            infoText.text = "Ninguém ajudou o idoso a atravessar a rua, por isso este não é o melhor cenário.";
        }
        if (choice == 2)
        {
            conceitos["26"] += 2;
            conceitos["50"] += 2;
            conceitos["31"] += 2;
            conceitos["10"] += 1;
            conceitos["25"] += 4;
            conceitos["28"] += 1;

            infoText.text = "Parabéns! Você escolheu o melhor cenário! Você ganhará um presente que será revelado na última aula!";
        }
        if (choice == 3)
        {
            conceitos["50"] += 2;
            conceitos["25"] += 2;

            infoText.text = "O carro está sobre um pedaço da faixa e a mulher está atravessando fora da faixa, por isso este não é o melhor cenário.";
        }

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { AudioManager.am.PlaySFX(AudioManager.am.button); StartCoroutine(Part2Start()); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part2Start()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        animator.SetTrigger("cutscene");
        yield return new WaitForSeconds(5);
        Part2();
    }

    private void Part2()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel2, true);
        if (PlayerManager.pm.vault > 0)
        {
            conceitos["52"] += 3;
            conceitos["23"] += 2;

            p2Text.text = "Como você guardou no pote de segurança, foram economizados 15 reais. Agora será necessário gastar apenas 5 reais do Bolodix!";
            p2Button1.gameObject.SetActive(true);
            p2Button1.onClick.AddListener(delegate { StartCoroutine(End(1)); p2Button1.onClick.RemoveAllListeners(); });
        }
        else
        {
            p2Text.text = "Como você não guardou no pote de segurança, agora será necessário gastar 20 reais do Bolodix!";
            p2Button2.gameObject.SetActive(true);
            p2Button2.onClick.AddListener(delegate { StartCoroutine(End(2)); p2Button2.onClick.RemoveAllListeners(); });
        }
    }

    private IEnumerator End(int choice)
    {
        //hintButton.SetActive(false);

        FlavorManager.fm.ShowHidePanel(optionPanel2, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        resultText.text = "Tudo acabou bem! Você conseguiu comprar o remédio de que precisa e vai melhorar! Até a próxima!";

        if (choice == 1)
        {
            PlayerManager.pm.vault -= 15;
            PlayerManager.pm.RemoveCoins(5);
        }
        else if (choice == 2) PlayerManager.pm.RemoveCoins(20);

        StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }
}