using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L7 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button resultButton;

    public GameObject hintButton;

    public TextMeshProUGUI instructionT;
    public GameObject nextButtonObject;
    public Button nextButton;

    public Button button1, button2, button3;
    public Image imageA1, imageA2, imageA3;
    public Image imageB1, imageB2, imageB3;
    public Image imageC1, imageC2, imageC3;
    public Image borderA1, borderA2, borderA3;
    public Image borderB1, borderB2, borderB3;
    public Image borderC1, borderC2, borderC3;

    public Button safeAddButton, safeRemoveButton;
    public Image safe1, safe2, safe3;
    public Image toy1A, toy1B, toy1C, toy2A, toy2B, toy2C, toy3A, toy3B, toy3C; 
    public TextMeshProUGUI safeT;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private int totalCounter;

    private void Start()
    {
        hintButton.SetActive(false);

        conceitos.Add("23", 1);
        conceitos.Add("20", 1);
        conceitos.Add("2", 1);
        conceitos.Add("24", 1);
        conceitos.Add("25", 1);
        conceitos.Add("7", 1);
        conceitos.Add("30", 2);

        AudioManager.am.PlayVoice(AudioManager.am.v1start[6]);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Escolha os sonhos que quer realizar.";

        SetItemsButtons(false);
        SetSafeButtons(false);

        infoButton.onClick.AddListener(delegate
        {
            AudioManager.am.voiceChannel.Stop();
            PartA();
            infoButton.onClick.RemoveAllListeners();
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
        });

        APIManager.am.Relatorio(conceitos);
    }

    private void PartA()
    {   
        hintButton.SetActive(true);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        SetItemsButtons(true);
        instructionT.text = "Escolha o sonho 1!";
        imageA1.enabled = true; imageA2.enabled = true; imageA3.enabled = true;

        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();

        button1.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            borderA1.enabled = true; borderA2.enabled = false; borderA3.enabled = false;

            nextButtonObject.SetActive(true);
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(delegate
            {
                AudioManager.am.PlaySFX(AudioManager.am.button);
                imageA1.enabled = false; imageA2.enabled = false; imageA3.enabled = false;
                borderA1.enabled = false; borderA2.enabled = false; borderA3.enabled = false;
                PlayerManager.pm.gift1 = 1;
                PartB();
            });
        });
        button2.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            borderA1.enabled = false; borderA2.enabled = true; borderA3.enabled = false;

            nextButtonObject.SetActive(true);
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(delegate
            {
                AudioManager.am.PlaySFX(AudioManager.am.button);
                imageA1.enabled = false; imageA2.enabled = false; imageA3.enabled = false;
                borderA1.enabled = false; borderA2.enabled = false; borderA3.enabled = false;
                PlayerManager.pm.gift1 = 2;
                PartB();
            });
        });
        button3.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            borderA1.enabled = false; borderA2.enabled = false; borderA3.enabled = true;

            nextButtonObject.SetActive(true);
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(delegate
            {
                AudioManager.am.PlaySFX(AudioManager.am.button);
                imageA1.enabled = false; imageA2.enabled = false; imageA3.enabled = false;
                borderA1.enabled = false; borderA2.enabled = false; borderA3.enabled = false;
                PlayerManager.pm.gift1 = 3;
                PartB();
            });
        });
    }

    private void PartB()
    {
        nextButtonObject.SetActive(false);
        instructionT.text = "Escolha o sonho 2!";
        imageB1.enabled = true; imageB2.enabled = true; imageB3.enabled = true;

        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            borderB1.enabled = true; borderB2.enabled = false; borderB3.enabled = false;

            nextButtonObject.SetActive(true);
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(delegate
            {
                AudioManager.am.PlaySFX(AudioManager.am.button);
                imageB1.enabled = false; imageB2.enabled = false; imageB3.enabled = false;
                borderB1.enabled = false; borderB2.enabled = false; borderB3.enabled = false;
                PlayerManager.pm.gift2 = 1;
                PartC();
            });
        });
        button2.onClick.RemoveAllListeners();
        button2.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            borderB1.enabled = false; borderB2.enabled = true; borderB3.enabled = false;

            nextButtonObject.SetActive(true);
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(delegate
            {
                AudioManager.am.PlaySFX(AudioManager.am.button);
                imageB1.enabled = false; imageB2.enabled = false; imageB3.enabled = false;
                borderB1.enabled = false; borderB2.enabled = false; borderB3.enabled = false;
                PlayerManager.pm.gift2 = 2;
                PartC();
            });
        });
        button3.onClick.RemoveAllListeners();
        button3.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            borderB1.enabled = false; borderB2.enabled = false; borderB3.enabled = true;

            nextButtonObject.SetActive(true);
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(delegate
            {
                AudioManager.am.PlaySFX(AudioManager.am.button);
                imageB1.enabled = false; imageB2.enabled = false; imageB3.enabled = false;
                borderB1.enabled = false; borderB2.enabled = false; borderB3.enabled = false;
                PlayerManager.pm.gift2 = 3;
                PartC();
            });
        });
    }

    private void PartC()
    {
        nextButtonObject.SetActive(false);
        instructionT.text = "Escolha o sonho 3!";
        imageC1.enabled = true; imageC2.enabled = true; imageC3.enabled = true;

        button1.onClick.RemoveAllListeners();
        button1.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            borderC1.enabled = true; borderC2.enabled = false; borderC3.enabled = false;

            nextButtonObject.SetActive(true);
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(delegate
            {
                AudioManager.am.PlaySFX(AudioManager.am.button);
                imageC1.enabled = false; imageC2.enabled = false; imageC3.enabled = false;
                borderC1.enabled = false; borderC2.enabled = false; borderC3.enabled = false;
                PlayerManager.pm.gift3 = 1;
                Swap();
            });
        });
        button2.onClick.RemoveAllListeners();
        button2.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            borderC1.enabled = false; borderC2.enabled = true; borderC3.enabled = false;

            nextButtonObject.SetActive(true);
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(delegate
            {
                AudioManager.am.PlaySFX(AudioManager.am.button);
                imageC1.enabled = false; imageC2.enabled = false; imageC3.enabled = false;
                borderC1.enabled = false; borderC2.enabled = false; borderC3.enabled = false;
                PlayerManager.pm.gift3 = 2;
                Swap();
            });
        });
        button3.onClick.RemoveAllListeners();
        button3.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            borderC1.enabled = false; borderC2.enabled = false; borderC3.enabled = true;

            nextButtonObject.SetActive(true);
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(delegate
            {
                AudioManager.am.PlaySFX(AudioManager.am.button);
                imageC1.enabled = false; imageC2.enabled = false; imageC3.enabled = false;
                borderC1.enabled = false; borderC2.enabled = false; borderC3.enabled = false;
                PlayerManager.pm.gift3 = 3;
                Swap();
            });
        });
    }

    private void Swap()
    {
        instructionT.text = "";
        nextButtonObject.SetActive(false);

        AudioManager.am.PlayVoice(AudioManager.am.v1l7[0]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Vamos depositar pelo menos 1 moeda em cada cofre, e no máximo, 3 moedas.";

        SetItemsButtons(false);
        SetSafeButtons(false);

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            AudioManager.am.voiceChannel.Stop();
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
            SetSafeButtons(true);
            PartD();
        });
    }

    private void PartD()
    {
        safeT.transform.parent.gameObject.SetActive(true);

        instructionT.text = "Você tem 5 moedas! Escolha quanto quer depositar no cofrinho 1!";
        safe1.enabled = true;
        
        float giftNumber = PlayerManager.pm.gift1;
        if(giftNumber != 0)
        {
            if (giftNumber == 1) { toy1A.enabled = true; }
            if (giftNumber == 2) { toy1B.enabled = true; }
            if (giftNumber == 3) { toy1C.enabled = true; }
        }

        int localCounter = 0;
        safeT.text = localCounter.ToString();

        safeAddButton.onClick.RemoveAllListeners();
        safeAddButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            if (localCounter == 0) 
            {
                nextButtonObject.SetActive(true);
                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(delegate
                {
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    safe1.enabled = false;
                    nextButtonObject.SetActive(false);
                    PlayerManager.pm.safe1 = localCounter;
                    PartE();
                });
            }
            if(localCounter < 3)
            {
                localCounter++;
                totalCounter++;
                safeT.text = localCounter.ToString();
            }

            if (totalCounter < 4)
                instructionT.text = "Você ainda tem " + (5 - totalCounter).ToString() + " moedas!";
            else
                instructionT.text = "Você ainda tem " + (5 - totalCounter).ToString() + " moeda!";
        });
        safeRemoveButton.onClick.RemoveAllListeners();
        safeRemoveButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            if (localCounter > 0)
            {
                localCounter--;
                totalCounter--;
                safeT.text = localCounter.ToString();

                if (localCounter == 0) nextButtonObject.SetActive(false);
            }

            if (totalCounter < 4)
                instructionT.text = "Você ainda tem " + (5 - totalCounter).ToString() + " moedas!";
            else
                instructionT.text = "Você ainda tem " + (5 - totalCounter).ToString() + " moeda!";
        });
    }

    private void PartE()
    {
        instructionT.text = "Você tem " + (5 - totalCounter).ToString() + " moedas! Escolha quanto quer depositar no cofrinho 2!";
        safe2.enabled = true;

        float giftNumber = PlayerManager.pm.gift2;
        if (giftNumber != 0)
        {
            if (giftNumber == 1) { toy2A.enabled = true; }
            if (giftNumber == 2) { toy2B.enabled = true; }
            if (giftNumber == 3) { toy2C.enabled = true; }
        }

        int localCounter = 0;
        safeT.text = localCounter.ToString();

        safeAddButton.onClick.RemoveAllListeners();
        safeAddButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            if (localCounter == 0)
            {
                nextButtonObject.SetActive(true);
                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(delegate
                {
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    safe2.enabled = false;
                    nextButtonObject.SetActive(false);
                    PlayerManager.pm.safe2 = localCounter;
                    PartF();
                });
            }
            if ((5 - (totalCounter + 1)) > 0) 
            {
                if (localCounter < 3)
                {
                    localCounter++;
                    totalCounter++;
                    safeT.text = localCounter.ToString();
                }
            }

            if (totalCounter < 4)
                instructionT.text = "Você ainda tem " + (5 - totalCounter).ToString() + " moedas!";
            else
                instructionT.text = "Você ainda tem " + (5 - totalCounter).ToString() + " moeda!";
        });
        safeRemoveButton.onClick.RemoveAllListeners();
        safeRemoveButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            if (localCounter > 0)
            {
                localCounter--;
                totalCounter--;
                safeT.text = localCounter.ToString();

                if (localCounter == 0) nextButtonObject.SetActive(false);
            }
            if (totalCounter < 4)
                instructionT.text = "Você ainda tem " + (5 - totalCounter).ToString() + " moedas!";
            else
                instructionT.text = "Você ainda tem " + (5 - totalCounter).ToString() + " moeda!";
        });
    }

    private void PartF()
    {
        instructionT.text = "Total de moedas: " + (5 - totalCounter).ToString() + ". Escolha quanto quer depositar no cofrinho 3!";
        safe3.enabled = true;

        float giftNumber = PlayerManager.pm.gift3;
        if (giftNumber != 0)
        {
            if (giftNumber == 1) { toy3A.enabled = true; }
            if (giftNumber == 2) { toy3B.enabled = true; }
            if (giftNumber == 3) { toy3C.enabled = true; }
        }

        int localCounter = 0;
        safeT.text = localCounter.ToString();

        safeAddButton.onClick.RemoveAllListeners();
        safeAddButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            if (totalCounter + 1 == 5)
            {
                nextButtonObject.SetActive(true);
                nextButton.onClick.RemoveAllListeners();
                nextButton.onClick.AddListener(delegate
                {
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    safe3.enabled = false;
                    safeT.transform.parent.gameObject.SetActive(false);
                    SetSafeButtons(false);
                    nextButtonObject.SetActive(false);
                    instructionT.transform.parent.gameObject.SetActive(false);
                    PlayerManager.pm.safe3 = localCounter;
                    End();
                });
            }
            if (totalCounter < 5)
            {
                if (localCounter < 3)
                {
                    localCounter++;
                    totalCounter++;
                    safeT.text = localCounter.ToString();
                }
            }

            if (totalCounter < 5)
            {
                if (totalCounter < 4)
                    instructionT.text = "Você ainda tem " + (5 - totalCounter).ToString() + " moedas!";
                else
                    instructionT.text = "Você ainda tem " + (5 - totalCounter).ToString() + " moeda!";
            }
            else
                instructionT.text = "Você não tem mais moedas!";
        });
        safeRemoveButton.onClick.RemoveAllListeners();
        safeRemoveButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            if (localCounter > 0)
            {
                localCounter--;
                totalCounter--;
                safeT.text = localCounter.ToString();

                if (totalCounter != 5) nextButtonObject.SetActive(false);
            }

            if (totalCounter < 4)
                instructionT.text = "Você ainda tem " + (5 - totalCounter).ToString() + " moedas!";
            else
                instructionT.text = "Você ainda tem " + (5 - totalCounter).ToString() + " moeda!";
        });
    }
    
    private void End()
    {
              hintButton.SetActive(false );

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1l7[1]);
        resultText.text = "Veja o seu Bolodix!";
        PlayerManager.pm.AddLevel();
    }

    private void SetItemsButtons(bool value)
    {
        button1.gameObject.SetActive(value);
        button1.enabled = value;
        button2.gameObject.SetActive(value);
        button2.enabled = value;
        button3.gameObject.SetActive(value);
        button3.enabled = value;
    }

    private void SetSafeButtons(bool value)
    {
        safeAddButton.gameObject.SetActive(value);
        safeAddButton.enabled = value;
        safeRemoveButton.gameObject.SetActive(value);
        safeRemoveButton.enabled = value;
    }
}