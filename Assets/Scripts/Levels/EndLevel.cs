using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    public GameObject giftPanel1, giftPanel2, giftPanel3, bluePanel, trophyPanel;
    public TextMeshProUGUI giftText1, giftText2, giftText3, blueText;
    public Button giftButton1, giftButton2, giftButton3, blueButton;
    public GameObject coin, bucks, coinRain, buckRain;

    private void Start()
    {
        if (PlayerManager.pm.bonus1) Part1();
        else if (PlayerManager.pm.bonus2) StartCoroutine(Part2());
        else StartCoroutine(Part3());
    }

    public void Part1()
    {
        FlavorManager.fm.BigFirework();
        FlavorManager.fm.ShowHidePanel(giftPanel1, true);

        if (!PlayerManager.pm.v2)
        {
            coinRain.SetActive(true);
            AudioManager.am.PlayVoice(AudioManager.am.ending[0]);
            giftText1.text = "Parabéns! Você ganhou 4 moedas por ajudar a escolher a melhor opção para a sociedade, e hoje teve retorno!!!";
        }
        else
        {
            buckRain.SetActive(true);
            giftText1.text = "Parabéns! Você ganhou trinta reais por ajudar a escolher a melhor opção para a sociedade, e hoje teve retorno!!!";
        }

        giftButton1.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            AudioManager.am.voiceChannel.Stop();

            if (PlayerManager.pm.bonus2) StartCoroutine(Part2());
            else StartCoroutine(Part3());

            giftButton1.onClick.RemoveAllListeners();
        });

        if (!PlayerManager.pm.v2)
        {
            StartCoroutine(FlavorManager.fm.SpawnCoin(4));
            PlayerManager.pm.AddCoins(4);
        }
        else
        {
            StartCoroutine(FlavorManager.fm.SpawnBucks(3));
            PlayerManager.pm.AddCoins(30);
        }
    }

    public IEnumerator Part2()
    {
        FlavorManager.fm.ShowHidePanel(giftPanel1, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.BigFirework();
        FlavorManager.fm.ShowHidePanel(giftPanel2, true);
        
        if (!PlayerManager.pm.v2)
        {
            AudioManager.am.PlayVoice(AudioManager.am.ending[1]);
            giftText2.text = "Parabéns! Você ganhou 6 moedas!!! Seu amigo está retribuindo as 4 moedas que você gastou para ele melhorar e te deu mais 2 moedas de presente no final do ano!";
        }
        else giftText2.text = "Parabéns! Você ganhou 40 reais!!! Seu amigo está retribuindo os 15 reais que gastou para ele melhorar, e te deu mais 25 reais de presente para o final do ano!";

        giftButton2.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            AudioManager.am.voiceChannel.Stop();

            StartCoroutine(Part3());
            
            giftButton2.onClick.RemoveAllListeners();
        });

        if (!PlayerManager.pm.v2)
        {
            StartCoroutine(FlavorManager.fm.SpawnCoin(6));
            PlayerManager.pm.AddCoins(6);
        }
        else
        {
            StartCoroutine(FlavorManager.fm.SpawnBucks(4));
            PlayerManager.pm.AddCoins(40);
        }
    }

    public IEnumerator Part3()
    {
        FlavorManager.fm.ShowHidePanel(giftPanel1, false);
        FlavorManager.fm.ShowHidePanel(giftPanel2, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.BigFirework();
        FlavorManager.fm.ShowHidePanel(giftPanel3, true);

        if (!PlayerManager.pm.v2)
        {
            coin.SetActive(true);
            AudioManager.am.PlayVoice(AudioManager.am.ending[2]);
            giftText3.text = "Hora da revelação: Moeda azul brilhante!!!";
        }
        else
        {
            bucks.SetActive(true);
            giftText3.text = "Hora da revelação: Nota azul brilhante!!!";
        }

        giftButton3.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            AudioManager.am.voiceChannel.Stop();

            StartCoroutine(Part4());
            
            giftButton3.onClick.RemoveAllListeners();
        });
    }

    public IEnumerator Part4()
    {
        FlavorManager.fm.ShowHidePanel(giftPanel1, false);
        FlavorManager.fm.ShowHidePanel(giftPanel2, false);
        FlavorManager.fm.ShowHidePanel(giftPanel3, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.BigPuff();
        FlavorManager.fm.ShowHidePanel(bluePanel, true);

        if (!PlayerManager.pm.v2)
        {
            AudioManager.am.PlayVoice(AudioManager.am.ending[3]);
            blueText.text = "Parabéns!!! Você fez uma cidade melhor. Continuem ajudando a todos! Por isso, você foi retribuído com 5 moedas para cada Bolodix!!! Veja ele crescendo!";
        }
        else blueText.text = "Parabéns!!! Você fez uma cidade melhor. Continuem ajudando a todos! Por isso, você foi retribuído com cinquenta reais para cada Bolodix!!! Veja ele crescendo!";

        blueButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            AudioManager.am.voiceChannel.Stop();

            GameManager.gm.LoadScene("Bolodim");

            blueButton.onClick.RemoveAllListeners();
        });

        if (!PlayerManager.pm.v2)
        {
            StartCoroutine(FlavorManager.fm.SpawnCoin(5));
            PlayerManager.pm.AddCoins(5);
        }
        else
        {
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            PlayerManager.pm.AddCoins(50);
        }

        PlayerManager.pm.AddLevel();

        yield return new WaitForSeconds(13);

        trophyPanel.SetActive(true);
    }
}