using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L20 : MonoBehaviour
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

    public GameObject baloon;
    public GameObject optionPanel;
    public Button p1Button1, p1Button2, p1Button3;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        StartCoroutine(Part1Start());
    }

    private IEnumerator Part1Start()
    {
        conceitos.Add("2", 0);
        conceitos.Add("20", 0);
        conceitos.Add("25", 0);
        conceitos.Add("10", 0);

        conceitos["2"] += 0;
        conceitos["20"] += 0;
        conceitos["25"] += 0;
        conceitos["10"] += 0;

        yield return new WaitForSeconds(2);
        FlavorManager.fm.ShowHidePanel(baloon, true);
        yield return new WaitForSeconds(3);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[19]);
        infoText.text = "Seu amigo Léo está doente." +
            " Ele está precisando de remédios e está sem o dinheiro." +
            " O que você pode fazer para ajudar o Léo?";
        infoButton.onClick.AddListener(delegate { Part1(); });
    }
    private void Part1()
    {
        AudioManager.am.PlayVoice(AudioManager.am.v1l20[0]);
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        p1Button1.onClick.AddListener(delegate { OptionalGift(1); });
        p1Button2.onClick.AddListener(delegate { End(2); });
        p1Button3.onClick.AddListener(delegate { End(3); });
    }

    private void OptionalGift(int choice)
    {
        AudioManager.am.voiceChannel.Stop();

        p1Button1.onClick.RemoveAllListeners();
        p1Button2.onClick.RemoveAllListeners();
        p1Button3.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.ShowHidePanel(giftPanel, true);

        AudioManager.am.PlaySFX(AudioManager.am.end);

        gift2.SetActive(true);
        PlayerManager.pm.bonus2 = true;

        giftButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            End(choice);
            giftButton.onClick.RemoveAllListeners();
            FlavorManager.fm.ShowHidePanel(giftPanel, false);
        });
    }

    private void End(int choice)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);

        p1Button1.onClick.RemoveAllListeners();
        p1Button2.onClick.RemoveAllListeners();
        p1Button3.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(baloon, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        if (choice == 1)
        {
            conceitos["2"] += 4;
            conceitos["20"] += 3;
            conceitos["25"] += 3;
            conceitos["10"] += 3;

            AudioManager.am.PlayVoice(AudioManager.am.v1l20[1]);
            resultText.text = "Parabéns, você ajudou seu amigo a se curar mais rápido." +
                " Você comprou o remédio de que ele precisava, e serão usadas 4 moedas do Bolodix para o remédio. Mas," +
                " em troca, você ganhará um presente extra do jogo, o qual será usado na última fase! Parabéns pelo presente e pela sua ação!";
            
            PlayerManager.pm.RemoveCoins(4);
            StartCoroutine(FlavorManager.fm.SpawnCoinPosition(4, coinSpawn, coinWaypoint));
        }
        else if (choice == 2)
        {
            conceitos["2"] += 2;
            conceitos["20"] += 2;
            conceitos["25"] += 2;
            conceitos["10"] += 2;

            AudioManager.am.PlayVoice(AudioManager.am.v1l20[2]);
            resultText.text = "Você procurou um outro amigo, ele tinha o remédio," +
                " mas você demorou um pouco para chegar até o seu amigo que estava doente. Ele está bem, mas demorou um pouco mais para se recuperar." +
                " Continue se preocupando em ajudar as pessoas que estão precisando!";
        }
        else
        {
            conceitos["2"] += 1;
            conceitos["20"] += 0;
            conceitos["25"] += 0;
            conceitos["10"] += 1;

            AudioManager.am.PlayVoice(AudioManager.am.v1l20[3]);
            resultText.text = "Seu amigo se recuperou, mas sua escolha não foi a melhor, pois ele precisava do remédio com urgência." +
                " Você demorou para voltar com o remédio. Ele conseguiu até que uma outra pessoa o ajudasse." +
                " Fique feliz, pois ele se recuperou. Mas, da próxima vez, seja ligeiro!";
        }

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }
}