using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L3 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 220;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel1, optionPanel2, optionPanel3;
    private int p1Total, p2Total, p3Total;
    public TextMeshProUGUI p1TotalText, p2TotalText, p3TotalText;
    
    public Button p1SubmitButton, p2SubmitButton, p3SubmitButton;
    public GameObject p1Submit, p2Submit, p3Submit;


    public Button p1Button1, p1Button2;
    public GameObject p1b1Price, p1b1Selected, p1b2Price, p1b2Selected;

    public Button p2Button1, p2Button2, p2Button3, p2Button4;
    public GameObject p2b1Price, p2b1Selected, p2b2Price, p2b2Selected, p2b3Price, p2b3Selected, p2b4Price, p2b4Selected;

    public Button p3Button1, p3Button2, p3Button3;
    public GameObject p3b1Price, p3b1Selected, p3b2Price, p3b2Selected, p3b3Price, p3b3Selected;

    public int bonus;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        totalCoinsText.text = totalCoins.ToString();
       // AudioManager.am.PlayVoice(AudioManager.am.v2start[2]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Seu salário aumentou para R$220 por semana! Aprender um segundo idioma pode garantir outro aumento e até transferência para o Chile. Veja as opções de plano de saúde e escolha a melhor para você!";
        infoButton.onClick.AddListener(delegate { Part1(); });
    }

    private void Part1()
    {
        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel1, true);

        p1Button1.onClick.AddListener(delegate { PanelButtonOneOption(this, 70, p1b1Price, p1b1Selected, p1b2Selected, p1Submit); });
        p1Button2.onClick.AddListener(delegate { PanelButtonOneOption(this, 85, p1b2Price, p1b2Selected, p1b1Selected, p1Submit); });
       // p1Button1.onClick.AddListener(delegate { PanelButton(this, 70, ref p1Total, p1TotalText, p1b1Price, p1b1Selected); });
      //  p1Button2.onClick.AddListener(delegate { PanelButton(this, 85, ref p1Total, p1TotalText, p1b2Price, p1b2Selected); });
        p1SubmitButton.onClick.AddListener(delegate { StartCoroutine(Part1End()); });
    }

    private IEnumerator Part1End()
    {
        // Declaração
        conceitos.Add("10", 0);
        conceitos.Add("20", 0);
        conceitos.Add("28", 0);
        conceitos.Add("25", 0);
        conceitos.Add("1", 0);
        conceitos.Add("7", 0);
        conceitos.Add("41", 0);
        conceitos.Add("23", 0);

        conceitos["10"] += 0;
        conceitos["20"] += 0;
        conceitos["28"] += 0;
        conceitos["25"] += 0;
        conceitos["1"] += 0;
        conceitos["7"] += 0;
        conceitos["41"] += 0;
        conceitos["23"] += 0;

        if (p1b1Selected.activeSelf)
        {
            conceitos["10"] += 3;
            conceitos["20"] += 4;
            conceitos["28"] += 5;
            conceitos["25"] += 4;
            conceitos["1"] += 5;
            conceitos["7"] += 5;
            conceitos["41"] += 6;

            PlayerManager.pm.ensurance = true;
            PlayerManager.pm.dentist = true;
        }
        if (p1b2Selected.activeSelf)
        {
            conceitos["10"] += 3;
            conceitos["20"] += 4;
            conceitos["28"] += 5;
            conceitos["25"] += 4;
            conceitos["1"] += 5;
            conceitos["7"] += 5;
            conceitos["41"] += 6;

            PlayerManager.pm.ensurance = true;

            // PlayerManager.pm.english = true;
        }
        if (p1Total > 0) FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint);

        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoText.text = "Legal! Você ainda tem " + totalCoins + " reais! Agora vamos continuar nossas compras!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { AudioManager.am.PlaySFX(AudioManager.am.button); StartCoroutine(Part2()); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part2()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel2, true);
        

        p2Button1.onClick.AddListener(delegate { PanelButton(this, 90, ref p2Total, p2TotalText, p2b1Price, p2b1Selected); });
        p2Button2.onClick.AddListener(delegate { PanelButton(this, 85, ref p2Total, p2TotalText, p2b2Price, p2b2Selected); });
        p2Button3.onClick.AddListener(delegate { PanelButton(this, 70, ref p2Total, p2TotalText, p2b3Price, p2b3Selected); });
        p2Button4.onClick.AddListener(delegate { PanelButton(this, 85, ref p2Total, p2TotalText, p2b4Price, p2b4Selected); });
        p2SubmitButton.onClick.AddListener(delegate { StartCoroutine(Part2End()); });
    }

    private IEnumerator Part2End()
    {
        if (p2b1Selected.activeSelf)
        {
            conceitos["25"] += 1;
            conceitos["1"] += 1;
            conceitos["7"] += 1;
            conceitos["41"] += 1;

            PlayerManager.pm.english = true;
        }

        if (p2b2Selected.activeSelf)
        {
            conceitos["10"] += 1;
            conceitos["20"] += 2;
            conceitos["28"] += 1;
            conceitos["25"] += 2;
            conceitos["1"] += 3;
            conceitos["7"] += 3;
            conceitos["41"] += 2;   
            conceitos["23"] += 2;

            PlayerManager.pm.koreanCourse = true;
        }

        if (p2b3Selected.activeSelf)
        {
            conceitos["10"] += 1;
            conceitos["20"] += 2;
            conceitos["28"] += 1;
            conceitos["25"] += 2;
            conceitos["1"] += 3;
            conceitos["7"] += 3;
            conceitos["41"] += 2;
            conceitos["23"] += 2;

            PlayerManager.pm.spanishCourse = true;
        }
    
        if (p2b4Selected.activeSelf)
        {
            conceitos["10"] += 1;
            conceitos["20"] += 2;
            conceitos["28"] += 1;
            conceitos["25"] += 2;
            conceitos["1"] += 3;
            conceitos["7"] += 3;
            conceitos["41"] += 2;
            conceitos["23"] += 2;
        }

        if (p2Total > 0) FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint);

        FlavorManager.fm.ShowHidePanel(optionPanel2, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoText.text = "Legal! Você ainda tem " + totalCoins + " reais!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { AudioManager.am.PlaySFX(AudioManager.am.button); StartCoroutine(Part3()); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part3()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel3, true);

        p3Button1.onClick.AddListener(delegate { Panel3ButtonOneOption(this, p3b1Price, p3b1Selected, p3b2Selected, p3b3Selected, p3Submit); });
        p3Button2.onClick.AddListener(delegate { Panel3ButtonOneOption(this, p3b2Price, p3b2Selected, p3b1Selected, p3b3Selected, p3Submit); });
        p3Button3.onClick.AddListener(delegate { Panel3ButtonOneOption(this, p3b3Price, p3b3Selected, p3b2Selected, p3b1Selected, p3Submit); });

        //p3Button1.onClick.AddListener(delegate { PanelButton(this, 15, ref p3Total, p3TotalText, p3b1Price, p3b1Selected); });
       // p3Button2.onClick.AddListener(delegate { PanelButton(this, 15, ref p3Total, p3TotalText, p3b2Price, p3b2Selected); });
       // p3Button3.onClick.AddListener(delegate { PanelButton(this, 12, ref p3Total, p3TotalText, p3b3Price, p3b3Selected); });
        p3SubmitButton.onClick.AddListener(delegate { StartCoroutine(Part3End()); });
    }

    private IEnumerator Part3End()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel3, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);


        if (p3b1Selected.activeSelf)
        {
            conceitos["10"] += 1;
            conceitos["20"] += 2;
            conceitos["28"] += 2;
            conceitos["25"] += 2;
            conceitos["1"] += 2;
            conceitos["7"] += 2;
            conceitos["41"] += 2;
            conceitos["23"] += 2;

            infoText.text += "“Parabéns!!! Você acertou! Ganhou 15 reais de bônus!";
            bonus += 15;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        }
        if (p3b2Selected.activeSelf)
        {
            conceitos["10"] += 1;
            conceitos["20"] += 2;
            conceitos["28"] += 2;
            conceitos["25"] += 2;
            conceitos["1"] += 2;
            conceitos["7"] += 2;
            conceitos["41"] += 2;
            conceitos["23"] += 2;

            infoText.text += "A opção correta era a 1! 3 x 25 = 75 reais!";
           
        }
        if(!p3b1Selected.activeSelf && !p3b2Selected.activeSelf && !p3b3Selected.activeSelf)
        {
            conceitos["10"] += 1;
            conceitos["28"] += 1;
            conceitos["25"] += 1;
            conceitos["1"] += 1;
            conceitos["7"] += 1;
            conceitos["41"] += 2;
            conceitos["23"] += 1;

            infoText.text += "A opção correta era a 1! 3 x 25 = 75 reais!";
        }

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { AudioManager.am.PlaySFX(AudioManager.am.button); StartCoroutine(End()); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator End()
    {
        if (p3Total > 0) FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint);

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        resultText.text = "Sobraram " + totalCoins + " reais! Até a próxima!";

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = (totalCoins + bonus).ToString();

        if (totalCoins > 0)
        {
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            PlayerManager.pm.AddCoins(totalCoins + bonus);
        }

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }

    private static void PanelButton(V2L3 instance, int price, ref int totalPrice, TextMeshProUGUI totalPriceText, GameObject priceObject, GameObject selectedObject)
    {
        if (priceObject.activeSelf)
        {
            if (instance.totalCoins >= price)
            {
                instance.totalCoins -= price;
                instance.totalCoinsText.text = instance.totalCoins.ToString();

                totalPrice += price;
                totalPriceText.text = totalPrice.ToString();

                priceObject.SetActive(false);
                selectedObject.SetActive(true);
            }
            else
            {
                FlavorManager.fm.ShowHidePanel(instance.infoPanel, true);
                instance.infoText.text = "Você não tem dinheiro suficiente, escolha outro!";
                instance.infoButton.onClick.RemoveAllListeners();
                instance.infoButton.onClick.AddListener(delegate
                {
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    FlavorManager.fm.ShowHidePanel(instance. infoPanel, false);
                });
            }
        }
        else
        {
            instance.totalCoins += price;
            instance.totalCoinsText.text = instance.totalCoins.ToString();

            totalPrice -= price;
            totalPriceText.text = totalPrice.ToString();
            
            priceObject.SetActive(true);
            selectedObject.SetActive(false);
        }
    }

    private static void PanelButtonOneOption(V2L3 instance, int price, GameObject priceObject, GameObject selectedObject, GameObject otherSelectedObject, GameObject pSubmit)
{
    if (!otherSelectedObject.activeSelf) // Apenas um outro botão para verificar
    {
        if (priceObject.activeSelf)
        {
            if (instance.totalCoins >= price)
            {
                instance.totalCoins -= price;
                instance.totalCoinsText.text = instance.totalCoins.ToString();

                priceObject.SetActive(false);
                selectedObject.SetActive(true);
            }
            else
            {
                FlavorManager.fm.ShowHidePanel(instance.infoPanel, true);
                instance.infoText.text = "Você não tem dinheiro suficiente, escolha outro!";
                instance.infoButton.onClick.RemoveAllListeners();
                instance.infoButton.onClick.AddListener(delegate
                {
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    FlavorManager.fm.ShowHidePanel(instance.infoPanel, false);
                    instance.infoButton.onClick.RemoveAllListeners();
                });
            }
        }
        else
        {
            instance.totalCoins += price;
            instance.totalCoinsText.text = instance.totalCoins.ToString();

            priceObject.SetActive(true);
            selectedObject.SetActive(false);
        }
    }

    // Ajustando a verificação do botão de submissão para apenas duas opções
    if (selectedObject.activeSelf || otherSelectedObject.activeSelf) 
        pSubmit.SetActive(true);
    else 
        pSubmit.SetActive(false);
}

private static void Panel3ButtonOneOption(V2L3 instance, GameObject priceObject, GameObject selectedObject, GameObject otherSelectedObject1, GameObject otherSelectedObject2, GameObject pSubmit)
{
    // Verifica se não há outras opções selecionadas
    if (!otherSelectedObject1.activeSelf && !otherSelectedObject2.activeSelf)
    {
        // Alterna entre selecionar e desmarcar a opção
        if (priceObject.activeSelf)
        {
            priceObject.SetActive(false);
            selectedObject.SetActive(true);
        }
        else
        {
            priceObject.SetActive(true);
            selectedObject.SetActive(false);
        }
    }

    // Ajusta o botão de submissão para ser ativado se qualquer uma das opções estiver selecionada
    pSubmit.SetActive(selectedObject.activeSelf || otherSelectedObject1.activeSelf || otherSelectedObject2.activeSelf);
}



}