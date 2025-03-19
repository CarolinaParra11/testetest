using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L13 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject giftA, giftB, giftC;
    public TextMeshProUGUI counterText;
    public GameObject lidClosed, lidOpen;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("34", 1);
        conceitos.Add("23", 1);
        conceitos.Add("20", 1);
        conceitos.Add("28", 1);
        conceitos.Add("33", 1);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[12]);
        infoText.text = "Hoje é o dia do cofre dois. Vamos ver se você conseguiu realizar o segundo sonho.";
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

        counterText.gameObject.SetActive(false);
        FlavorManager.fm.BigPuff();
        if (lidClosed.activeInHierarchy)
        {
            lidClosed.SetActive(false);
            lidOpen.SetActive(true);
        }

        if (PlayerManager.pm.safe2 > 1)
        {
            if (PlayerManager.pm.gift2 == 1) FlavorManager.fm.ShowHidePanel(giftA, true);
            if (PlayerManager.pm.gift2 == 2) FlavorManager.fm.ShowHidePanel(giftB, true);
            if (PlayerManager.pm.gift2 == 3) FlavorManager.fm.ShowHidePanel(giftC, true);
        }
        
        yield return new WaitForSeconds(3);

        if (PlayerManager.pm.safe2 == 1)
        {
            StartCoroutine(FlavorManager.fm.SpawnCoin(1));
            PlayerManager.pm.AddCoins(1);
            AudioManager.am.PlayVoice(AudioManager.am.v1l13[0]);
            resultText.text = "O presente que você queria tem um valor de 2 moedas," +
                " mas a moeda que você guardou irá aumentar o seu Bolodix.";
        }
        else if (PlayerManager.pm.safe2 == 2)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l13[1]);
            resultText.text = "Parabéns, você ganhou seu presente!";
        }
        else if (PlayerManager.pm.safe2 == 3)
        {
            StartCoroutine(FlavorManager.fm.SpawnCoin(1));
            PlayerManager.pm.AddCoins(1);
            AudioManager.am.PlayVoice(AudioManager.am.v1l13[2]);
            resultText.text = "Parabéns, você ganhou seu presente! Sobrou 1 moeda," +
                " já que seu presente vale duas moedas. Seu Bolodix também irá aumentar.";
        }

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
    }
}
