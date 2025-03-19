using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V2L24 : MonoBehaviour
{
    public GameObject infoPanel;

    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject giftPanel;
    public Button giftButton;
    public GameObject gift2;

    public RectTransform coinSpawn, coinWaypoint;

    public Animator animator;
    public TweenCharacters tween;
    public GameObject baloon;

    public GameObject optionPanel;
    public Button p1Button1, p1Button2, p1Button3;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("24", 0);
        conceitos.Add("19", 0);
        conceitos.Add("2", 0);
        conceitos.Add("1", 0);
        conceitos.Add("7", 0);
        conceitos.Add("31", 0);
        conceitos.Add("33", 0);
        conceitos.Add("39", 0);

        conceitos["24"] += 0;
        conceitos["19"] += 0;
        conceitos["2"] += 0;
        conceitos["1"] += 0;
        conceitos["7"] += 0;
        conceitos["31"] += 0;
        conceitos["33"] += 0;
        conceitos["39"] += 0;

      //  AudioManager.am.PlayVoice(AudioManager.am.v2start[23]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você economizou o ano inteiro e gostaria de uma camiseta nova para comemorar seu aniversário.\n \n Escolha a opção que pode te recompensar por ter esperado, mas sem desperdiçar.";
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
    }


    private IEnumerator Part1Start()
    {
        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);

        animator.SetBool("isWalking", true);
        tween.GoPath();

        yield return new WaitForSeconds(4);

        animator.SetBool("isWalking", false);
        animator.SetBool("OnPhone", true);

        yield return new WaitForSeconds(1);

        FlavorManager.fm.ShowHidePanel(baloon, true);

        yield return new WaitForSeconds(5);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Parece que seu amigo não está bem e precisa de remédios!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
    }
    private IEnumerator Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);
        p1Button1.onClick.AddListener(delegate { StartCoroutine(Part1End(1)); });
        p1Button2.onClick.AddListener(delegate { OptionalGift(2); });
        p1Button3.onClick.AddListener(delegate { StartCoroutine(Part1End(3)); });
    }

    private void OptionalGift(int choice)
    {
        AudioManager.am.voiceChannel.Stop();

        p1Button1.onClick.RemoveAllListeners();
        p1Button2.onClick.RemoveAllListeners();
        p1Button3.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.BigFirework();
        FlavorManager.fm.ShowHidePanel(giftPanel, true);

        AudioManager.am.PlaySFX(AudioManager.am.end);

        gift2.SetActive(true);
        PlayerManager.pm.bonus2 = true;

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
        p1Button1.onClick.RemoveAllListeners();
        p1Button2.onClick.RemoveAllListeners();
        p1Button3.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(baloon, false);

        if (choice == 1)
        {
            conceitos["24"] += 1;
            conceitos["19"] += 0;
            conceitos["2"] += 1;
            conceitos["1"] += 3;
            conceitos["7"] += 0;
            conceitos["31"] += 1;
            conceitos["33"] += 3;
            conceitos["39"] += 1;

            infoText.text = "Parabéns!!! \n\n Você não desperdiçou a camiseta, fez o bem ao próximo, evitando desperdício e permitindo que seu colega, que precisava, não gastasse o dinheiro.\n\n Ao mesmo tempo, conquistou seu sonho.\n\n Você ganhou 20 reais de bônus e o presente vermelho, que será revelado na última fase!";
            PlayerManager.pm.AddCoins(15);
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));

        }
        else if (choice == 2)
        {
            conceitos["24"] += 4;
            conceitos["19"] += 3;
            conceitos["2"] += 5;
            conceitos["1"] += 6;
            conceitos["7"] += 4;
            conceitos["31"] += 6;
            conceitos["33"] += 6;
            conceitos["39"] += 6;

            infoText.text = "A opção 2 ou 3 não eram as melhores. \n\n Quando você doa a camiseta assim que pode, evita desperdício, a utiliza da melhor maneira (doando), faz o bem e permite que seu colega, que precisava, não gaste o dinheiro.\n\n Ao mesmo tempo, você conquista seu sonho.";
           // PlayerManager.pm.RemoveCoins(15);
           // StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));
        }
        else
        {
            conceitos["24"] += 3;
            conceitos["19"] += 1;
            conceitos["2"] += 3;
            conceitos["1"] += 4;
            conceitos["7"] += 2;
            conceitos["31"] += 3;
            conceitos["33"] += 4;
            conceitos["39"] += 4;

            infoText.text = "A opção 2 ou 3 não eram as melhores. \n\n Quando você doa a camiseta assim que pode, evita desperdício, a utiliza da melhor maneira (doando), faz o bem e permite que seu colega, que precisava, não gaste o dinheiro.\n\n Ao mesmo tempo, você conquista seu sonho.";
        }

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { End(); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
    }

    private void End()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Tudo feito por hoje! Até a próxima!";

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }
}