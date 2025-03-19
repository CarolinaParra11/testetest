using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V1L27 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button resultButton;

    public GameObject giftA, giftB, giftC;
    public TextMeshProUGUI counterText;
    public GameObject lidClosed, lidOpen;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        conceitos.Add("23", 0);
        conceitos.Add("20", 0);
        conceitos.Add("2", 0);
        conceitos.Add("1", 0);
        conceitos.Add("34", 0);

        conceitos["23"] += 5;
        conceitos["20"] += 5;
        conceitos["2"] += 5;
        conceitos["1"] += 5;
        conceitos["34"] += 5;

        APIManager.am.Relatorio(conceitos);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[26]);
        infoText.text = "Vamos ver se você conseguiu ganhar o presente do sonho três.";
        infoButton.onClick.AddListener(delegate
        {
            StartCoroutine(Part1());
            AudioManager.am.voiceChannel.Stop();
        });
    }

    public IEnumerator Part1()
    {
        Animator textAnim = counterText.GetComponent<Animator>();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);

        float currentTime = 4;

        while (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;

            textAnim.SetTrigger("Start");

            counterText.text = ((int)currentTime).ToString();
            yield return null;
        }

        FlavorManager.fm.BigPuff();
        if (lidClosed.activeInHierarchy)
        {
            lidClosed.SetActive(false);
            lidOpen.SetActive(true);
        }

        if (PlayerManager.pm.safe3 == 1)
        {
            PlayerManager.pm.AddCoins(1);
            StartCoroutine(FlavorManager.fm.SpawnCoin(1));
            coinsPanel.GetChild(0).gameObject.SetActive(true);
        }
        else if (PlayerManager.pm.safe3 == 2)
        {
            PlayerManager.pm.AddCoins(2);
            StartCoroutine(FlavorManager.fm.SpawnCoin(2));
            coinsPanel.GetChild(0).gameObject.SetActive(true);
            coinsPanel.GetChild(1).gameObject.SetActive(true);
        }
        else if (PlayerManager.pm.safe3 == 3)
        {
            if (PlayerManager.pm.gift3 == 1) FlavorManager.fm.ShowHidePanel(giftA, true);
            if (PlayerManager.pm.gift3 == 2) FlavorManager.fm.ShowHidePanel(giftB, true);
            if (PlayerManager.pm.gift3 == 3) FlavorManager.fm.ShowHidePanel(giftC, true);
        }

        counterText.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);

        if (PlayerManager.pm.safe3 == 1)
        {
            FlavorManager.fm.ShowHidePanel(incomePanel, true);
            AudioManager.am.PlayVoice(AudioManager.am.v1l27[1]);
            resultText.text = "Você guardou 1 moeda no cofre e o valor do presente é de 3 moedas." +
                " Não se preocupe, a moeda que guardou no cofre ajudou a aumentar o Bolodix!";
        }
        else if (PlayerManager.pm.safe3 == 2)
        {
            FlavorManager.fm.ShowHidePanel(incomePanel, true);
            AudioManager.am.PlayVoice(AudioManager.am.v1l27[2]);
            resultText.text = "Você guardou 2 moedas no cofre e o valor do presente é de 3 moedas." +
                " Não se preocupe, as 2 moedas que você guardou no cofre ajudaram a aumentar o Bolodix!";
        }
        else if (PlayerManager.pm.safe3 == 3)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l27[0]);
            resultText.text = "Parabéns! Você escolheu a quantidade de moedas certa para comprar o presente." +
                " Veja a abertura do cofre.";
        }
        
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultButton.onClick.RemoveAllListeners();
        resultButton.onClick.AddListener(delegate 
        {
            AudioManager.am.voiceChannel.Stop();
            GameManager.gm.LoadScene("EndLevel"); 
            AudioManager.am.PlaySFX(AudioManager.am.button); 
        });
    }
}
