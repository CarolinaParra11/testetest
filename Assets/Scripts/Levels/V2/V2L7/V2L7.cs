using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L7 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject hintButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 170;
    private int coins;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public TextMeshProUGUI titleText;
    public Button nextButton;

    public int counter = 1;

    public Button giftButton1, giftButton2, giftButton3;
    public Image imageA1, imageA2, imageA3;
    public Image imageB1, imageB2, imageB3;
    public Image imageC1, imageC2, imageC3;
    public Image borderA1, borderA2, borderA3;
    public Image borderB1, borderB2, borderB3;
    public Image borderC1, borderC2, borderC3;
    
    public Button vaultButton1, vaultButton2;
    public TextMeshProUGUI vaultText;
    public Image vault;
        
    public Button safeButton1, safeButton2, safeButton3;
    public TextMeshProUGUI safeButton1ValueText;
    public Image safeButton1Selected, safeButton2Selected, safeButton3Selected;
    public Image safe1, safe2, safe3;

    public Image safeToy;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        hintButton.SetActive(false);
       // AudioManager.am.PlayVoice(AudioManager.am.v2start[6]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você terá 170 reais hoje para guardar no pote da segurança e em três cofres dos sonhos que quer realizar.";
        infoButton.onClick.AddListener(delegate { Part1(); });

        totalCoinsText.text = totalCoins.ToString();

        conceitos.Add("23", 0);
        conceitos.Add("20", 0);
        conceitos.Add("2", 0);
        conceitos.Add("24", 0);
        conceitos.Add("25", 0);
        conceitos.Add("7", 0);
        conceitos.Add("30", 0);
        conceitos.Add("44", 0);
        conceitos.Add("41", 0);
        conceitos.Add("49", 0);

        conceitos["23"] += 0;
        conceitos["20"] += 0;
        conceitos["2"] += 0;
        conceitos["24"] += 0;
        conceitos["25"] += 0;
        conceitos["7"] += 0;
        conceitos["30"] += 0;
        conceitos["44"] += 0;
        conceitos["41"] += 0;
        conceitos["49"] += 0;
    }

    private void Part1()
    {
        hintButton.SetActive(true);
        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        titleText.text = "Decisão para o futuro!";
        vault.enabled = true;
        vaultText.text = "Você quer depositar 15 reais no Cofre da Segurança? Isso pode ajudá-lo em alguma emergência.";

        vaultButton1.onClick.AddListener(delegate
        {
            conceitos["23"] += 1;
            conceitos["20"] += 1;
            conceitos["2"] += 2;
            conceitos["24"] += 1;
            conceitos["25"] += 1;
            conceitos["7"] += 2;
            conceitos["30"] += 2;
            conceitos["44"] += 2;
            conceitos["41"] += 2;
            conceitos["49"] += 1;

            vaultButton1.gameObject.SetActive(false);
            vaultButton2.gameObject.SetActive(false);

            vaultText.gameObject.SetActive(false);
            vault.enabled = false;

            totalCoins -= 15;
            totalCoinsText.text = totalCoins.ToString();
            PlayerManager.pm.vault = 15;
            Part2();

            
        });
        vaultButton2.onClick.AddListener(delegate
        {
            conceitos["30"] += 1;
            conceitos["44"] += 1;
            conceitos["41"] += 2;
            conceitos["49"] += 1;

            vaultButton1.gameObject.SetActive(false);
            vaultButton2.gameObject.SetActive(false);

            vaultText.gameObject.SetActive(false);
            vault.enabled = false;
            Part2();
        });

        vaultButton1.gameObject.SetActive(true);
        vaultButton2.gameObject.SetActive(true);
    }

    private void Part2()
    {
        if (counter < 4)
        {
            titleText.text = "Escolha o presente que quer para o Sonho " + counter;

            imageA1.enabled = false; imageA2.enabled = false; imageA3.enabled = false;
            imageB1.enabled = false; imageB2.enabled = false; imageB3.enabled = false;
            imageC1.enabled = false; imageC2.enabled = false; imageC3.enabled = false;

            borderA1.enabled = false; borderA2.enabled = false; borderA3.enabled = false;
            borderB1.enabled = false; borderB2.enabled = false; borderB3.enabled = false;
            borderC1.enabled = false; borderC2.enabled = false; borderC3.enabled = false;

            if (counter == 1) { imageA1.enabled = true; imageA2.enabled = true; imageA3.enabled = true; }
            else if (counter == 2) { imageB1.enabled = true; imageB2.enabled = true; imageB3.enabled = true; }
            else if (counter == 3) { imageC1.enabled = true; imageC2.enabled = true; imageC3.enabled = true; }

            giftButton1.onClick.RemoveAllListeners();
            giftButton2.onClick.RemoveAllListeners();
            giftButton3.onClick.RemoveAllListeners();

            giftButton1.onClick.AddListener(delegate { Part2Choice(1); });
            giftButton2.onClick.AddListener(delegate { Part2Choice(2); });
            giftButton3.onClick.AddListener(delegate { Part2Choice(3); });

            giftButton1.gameObject.SetActive(true);
            giftButton2.gameObject.SetActive(true);
            giftButton3.gameObject.SetActive(true);
        }
        else Part2End();
    }

    private void Part2Choice(int id)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);

        if (id == 1)
        {
            if (counter == 1) { borderA1.enabled = true; borderA2.enabled = false; borderA3.enabled = false; }
            else if (counter == 2) { borderB1.enabled = true; borderB2.enabled = false; borderB3.enabled = false; }
            else if (counter == 3) { borderC1.enabled = true; borderC2.enabled = false; borderC3.enabled = false; }
        }
        else if (id == 2)
        {
            if (counter == 1) { borderA1.enabled = false; borderA2.enabled = true; borderA3.enabled = false; }
            else if (counter == 2) { borderB1.enabled = false; borderB2.enabled = true; borderB3.enabled = false; }
            else if (counter == 3) { borderC1.enabled = false; borderC2.enabled = true; borderC3.enabled = false; }
        }
        else if (id == 3)
        {
            if (counter == 1) { borderA1.enabled = false; borderA2.enabled = false; borderA3.enabled = true; }
            else if (counter == 2) { borderB1.enabled = false; borderB2.enabled = false; borderB3.enabled = true; }
            else if (counter == 3) { borderC1.enabled = false; borderC2.enabled = false; borderC3.enabled = true; }
        }
    
        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(delegate
        {
            nextButton.gameObject.SetActive(false);
            AudioManager.am.PlaySFX(AudioManager.am.button);

            imageA1.enabled = false; imageA2.enabled = false; imageA3.enabled = false;
            imageB1.enabled = false; imageB2.enabled = false; imageB3.enabled = false;
            imageC1.enabled = false; imageC2.enabled = false; imageC3.enabled = false;

            borderA1.enabled = false; borderA2.enabled = false; borderA3.enabled = false;
            borderB1.enabled = false; borderB2.enabled = false; borderB3.enabled = false;
            borderC1.enabled = false; borderC2.enabled = false; borderC3.enabled = false;

            if (counter == 1) PlayerManager.pm.gift1 = id;
            if(counter == 2) PlayerManager.pm.gift2 = id;
            if(counter == 3) PlayerManager.pm.gift3 = id;
            counter++;

            Part2();
        });
        nextButton.gameObject.SetActive(true);
    }

    private void Part2End()
    {
        titleText.text = "";

        giftButton1.gameObject.SetActive(false);
        giftButton2.gameObject.SetActive(false);
        giftButton3.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Agora vamos escolher quantos reais depositar em cada sonho!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
            counter = 1;

            Part3Start();
        });
    }

    private void Part3Start()
    {
        safeButton1.gameObject.SetActive(true);
        safeButton2.gameObject.SetActive(true);
        safeButton3.gameObject.SetActive(true);
        Part3();
    }

    private void Part3()
    {
        if (counter < 4)
        {
            titleText.text = "Escolha quantos reais quer depositar no Pote " + counter;

            if (PlayerManager.pm.vault == 15) safeButton1ValueText.text = "15";
            else safeButton1ValueText.text = "30";

            int i = 0;
            if (PlayerManager.pm.vault == 15) i = 15;
            else i = 30;

            safeButton1.onClick.AddListener(delegate { Part3Choice(this, safeButton1Selected, safeButton1.gameObject, i); });
            safeButton2.onClick.AddListener(delegate { Part3Choice(this, safeButton2Selected, safeButton2.gameObject, 40); });
            safeButton3.onClick.AddListener(delegate { Part3Choice(this, safeButton3Selected, safeButton3.gameObject, 100); });

            safe1.enabled = false;
            safe2.enabled = false;
            safe3.enabled = false;

            safeToy.enabled = true;

            if (counter == 1)
            {
                safe1.enabled = true;
                if (PlayerManager.pm.gift1 == 1) safeToy.sprite = imageA1.sprite;
                if (PlayerManager.pm.gift1 == 2) safeToy.sprite = imageA2.sprite;
                if (PlayerManager.pm.gift1 == 3) safeToy.sprite = imageA3.sprite;
            }
            else if (counter == 2)
            {
                safe2.enabled = true;
                if (PlayerManager.pm.gift2 == 1) safeToy.sprite = imageB1.sprite;
                if (PlayerManager.pm.gift2 == 2) safeToy.sprite = imageB2.sprite;
                if (PlayerManager.pm.gift2 == 3) safeToy.sprite = imageB3.sprite;
            }
            else if (counter == 3)
            {
                safe3.enabled = true;
                if (PlayerManager.pm.gift3 == 1) safeToy.sprite = imageC1.sprite;
                if (PlayerManager.pm.gift3 == 2) safeToy.sprite = imageC2.sprite;
                if (PlayerManager.pm.gift3 == 3) safeToy.sprite = imageC3.sprite;
            }
        }
        else End();
    }

    private static void Part3Choice(V2L7 instance, Image selected, GameObject coin, int price)
    {
        instance.coins = instance.totalCoins;

        instance.safeButton1Selected.enabled = false;
        instance.safeButton2Selected.enabled = false;
        instance.safeButton3Selected.enabled = false;

        instance.coins -= price;
        instance.totalCoinsText.text = instance.coins.ToString();
        selected.enabled = true;

        instance.nextButton.gameObject.SetActive(true);
        instance.nextButton.onClick.RemoveAllListeners();
        instance.nextButton.onClick.AddListener(delegate
        {
            instance.nextButton.onClick.RemoveAllListeners();
            instance.nextButton.gameObject.SetActive(false);
            AudioManager.am.PlaySFX(AudioManager.am.button);

            if (instance.counter == 1) PlayerManager.pm.safe1 = price;
            if (instance.counter == 2) PlayerManager.pm.safe2 = price;
            if (instance.counter == 3) PlayerManager.pm.safe3 = price;

            instance.totalCoins -= price;
            instance.counter++;
            coin.SetActive(false);

            Image safeImage = null;
            if (instance.counter == 2) safeImage = instance.safeButton2Selected;
            else if (instance.counter == 3) safeImage = instance.safeButton3Selected;
            instance.Part3();
        });
    }

    private void End()
    {
        hintButton.SetActive(false);

        conceitos["23"] += 2;
        conceitos["20"] += 2;
        conceitos["2"] += 2;
        conceitos["24"] += 2;
        conceitos["25"] += 2;
        conceitos["7"] += 2;
        conceitos["30"] += 2;
        conceitos["44"] += 2;
        conceitos["41"] += 2;
        conceitos["49"] += 1;

        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        resultText.text = "Tudo foi feito. Até a próxima!";

        if (totalCoins > 0) StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }
}