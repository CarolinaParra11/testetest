using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V1L1 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button resultButton;

    private int totalCoins = 3;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public GameObject avatar1, avatar2, avatar3;
    public Image image1, image2, image3, secImage;
    public Animator animator2;
    public string animator2TriggerName;
    public AnimatorTrigger animatorTrigger1, animatorTrigger3;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        conceitos.Add("31", 0);
        conceitos.Add("1", 0);
        conceitos.Add("32", 0);

        conceitos["31"] += 0;
        conceitos["1"] += 0;
        conceitos["32"] += 0;

        AudioManager.am.PlayVoice(AudioManager.am.v1start[0]);
        totalCoinsText.text = totalCoins.ToString();

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Escolha o melhor lugar para construir a sua casa com segurança e maior ganho.";
        infoButton.onClick.AddListener(delegate 
        { 
            Part1(); 
            AudioManager.am.voiceChannel.Stop();
        });
    }

    private void Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);
    }

    public void Part1OnDrop(int id)
    {
        AudioManager.am.PlaySFX(AudioManager.am.drop);
        FlavorManager.fm.ShowHidePanel(optionPanel, false);

        if (id == 1) StartCoroutine(Part1Choice(id, avatar1, image1, null, 0, null, null, animatorTrigger1));
        if (id == 2) StartCoroutine(Part1Choice(id, avatar2, image2, secImage, 1, animator2, animator2TriggerName, null));
        if (id == 3) StartCoroutine(Part1Choice(id, null, image3, null, 3, null, null, animatorTrigger3));
    }

    public IEnumerator Part1Choice(int id, GameObject avatar, Image image, Image secImage, int price, Animator animator, string trigger, AnimatorTrigger animatorTrigger)
    {
        totalCoins -= price;
        totalCoinsText.text = totalCoins.ToString();
        StartCoroutine(FlavorManager.fm.SpawnCoinPosition(price, coinSpawn, coinWaypoint));

        image.enabled = true;
        if (secImage != null) secImage.enabled = true;

        if (animator != null) animator.SetTrigger(trigger);
        else if (animatorTrigger != null) animatorTrigger.Trigger();

        if(avatar != null) avatar.SetActive(true);

        yield return new WaitForSeconds(3);

        End(id);
    }

    private void End(int id)
    {
        if (id == 1)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l1[0]);
            resultText.text = "Parabéns, você ganhou 3 moedas por escolher um lugar seguro, e poderá plantar milho, por exemplo.";

            conceitos["31"] += 3;
            conceitos["1"] += 2;
            conceitos["32"] += 3;
        }
        if (id == 2)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l1[1]);
            resultText.text = "Parabéns, você ganhou 3 moedas por escolher um lugar seguro e poder plantar. Mas para isso, usará 1 moeda para construir o tubo que trará água do rio.";

            conceitos["31"] += 3;
            conceitos["1"] += 2;
            conceitos["32"] += 1;
        }
        if (id == 3)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l1[2]);
            resultText.text = "Entrou água em sua casa porque o rio transbordou. Você usará 3 moedas para recuperar a sua casa.";
        }

        APIManager.am.Relatorio(conceitos);

        /*public GameObject incomePanel;
        public Transform coinsPanel;*/

        if (totalCoins > 0)
        {
            FlavorManager.fm.ShowHidePanel(incomePanel, true);
            
            int counter = 0;

            while(counter < totalCoins)
            {
                coinsPanel.GetChild(counter).gameObject.SetActive(true);
                counter++;
            }
        }

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        StartCoroutine(FlavorManager.fm.SpawnCoin(totalCoins));
        PlayerManager.pm.AddCoins(totalCoins);
        PlayerManager.pm.AddLevel();
    }
}