using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L14 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    private int totalCoins = 150;
    private int bonusCoins;
    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button submitButton;
    public Button[] button;
    public GameObject[] price;
    public GameObject[] selected;
    private int totalPrice;
    public TextMeshProUGUI totalPriceText;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        totalCoinsText.text = totalCoins.ToString();
       // AudioManager.am.PlayVoice(AudioManager.am.v2start[13]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Com o fim das férias de julho se aproximando, você percebe que precisa ajustar seu planejamento financeiro para a compra do material escolar.\n \nAlguns itens precisam ser repostos, e é essencial lembrar que todo planejamento financeiro inicial deve considerar etapas futuras igualmente importantes, garantindo um consumo consciente aliado a uma estratégia bem definida.";
        
        infoButton.onClick.AddListener(delegate { StartCoroutine(InfoTextAtt()); });
    }

    private IEnumerator InfoTextAtt()
    {
        infoText.text = "Você tem 150 reais e precisa repor alguns materiais escolares. \n \n Seu lápis e borracha estão desgastados, seu apontador quebrou ao cair no chão, e você também precisa de um novo caderno, com aproximadamente 60 a 65 páginas. \n \n Compare os preços e avalie a qualidade antes de fazer sua escolha.";
        yield return new WaitForSeconds(1);
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); infoButton.onClick.RemoveAllListeners(); });
         
    }

    

    private IEnumerator Part1()
    {

        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(1);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        Debug.Log("Part1");

        button[0].onClick.AddListener(delegate { Part1Choice(this, 40, 0, price[0], selected[0]); });
        button[1].onClick.AddListener(delegate { Part1Choice(this, 40, 0, price[1], selected[1]); });
        button[2].onClick.AddListener(delegate { Part1Choice(this, 7, 12, price[2], selected[2]); });
        button[3].onClick.AddListener(delegate { Part1Choice(this, 6, 0, price[3], selected[3]); });
        button[4].onClick.AddListener(delegate { Part1Choice(this, 12, 0, price[4], selected[4]); });
        button[5].onClick.AddListener(delegate { Part1Choice(this, 13, 12, price[5], selected[5]); });
        button[6].onClick.AddListener(delegate { Part1Choice(this, 3, 12, price[6], selected[6]); });
        button[7].onClick.AddListener(delegate { Part1Choice(this, 10, 0, price[7], selected[7]); });
        button[8].onClick.AddListener(delegate { Part1Choice(this, 4, 0, price[8], selected[8]); });
        button[9].onClick.AddListener(delegate { Part1Choice(this, 6, 12, price[9], selected[9]); });
        button[10].onClick.AddListener(delegate { Part1Choice(this, 5, 0, price[10], selected[10]); });
        button[11].onClick.AddListener(delegate { Part1Choice(this, 7, 0, price[11], selected[11]); });
        button[12].onClick.AddListener(delegate { Part1Choice(this, 12, 0, price[12], selected[12]); });
        submitButton.onClick.AddListener(delegate { StartCoroutine(End()); submitButton.onClick.RemoveAllListeners(); });
    }

    private static void Part1Choice(V2L14 instance, int price, int bonus, GameObject priceObject, GameObject selectedObject)
    {
        if (priceObject.activeSelf)
        {
            if (instance.totalCoins >= price)
            {
                instance.totalCoins -= price;
                instance.totalCoinsText.text = instance.totalCoins.ToString();

                instance.bonusCoins += bonus;
                instance.totalPrice += price;
                instance.totalPriceText.text = instance.totalPrice.ToString();

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
                });
            }
        }
        else
        {
            instance.totalCoins += price;
            instance.totalCoinsText.text = instance.totalCoins.ToString();

            instance.bonusCoins -= bonus;
            instance.totalPrice -= price;
            instance.totalPriceText.text = instance.totalPrice.ToString();

            priceObject.SetActive(true);
            selectedObject.SetActive(false);
        }
    }

    private IEnumerator End()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        if (selected[3].activeSelf && selected[4].activeSelf) bonusCoins -= 10;

        resultText.text = "Sobraram " + totalCoins + " reais e você conseguiu mais " + bonusCoins + " de bônus (veja os detalhes na lista)!\n \n Vamos para a próxima! \n";

        bool rubber = false;

        for (int i = 0; i < selected.Length; i++)
        {
            if (selected[i].activeSelf)
            {
                if (i == 0) resultText.text += "\n - Mochila Azul: 0 reais.";
                else if (i == 1) resultText.text += "\n - Mochila Rosa: 0 reais.";
                else if (i == 2) resultText.text += "\n - Borracha Branca (R$7): 12 reais!";
                else if (i == 3)
                {
                    resultText.text += "\n - Borracha Branca (R$6): 0 reais.";
                    rubber = true;

                   /* conceitos["14"] += 4;
                    conceitos["22"] += 3;
                    conceitos["19"] += 5;
                    conceitos["1"] += 6;
                    conceitos["7"] += 3;
                    conceitos["34"] += 2;
                    conceitos["31"] += 3;
                    conceitos["55"] += 4;
                   */ conceitos["2"] += 3;
                }
                else if (i == 4 && rubber) resultText.text += "\n - Caderno (R$12): 0 reais.";
                else if (i == 4 && !rubber)
                {
                   /* conceitos["14"] += 4;
                    conceitos["22"] += 3;
                    conceitos["19"] += 5;
                    conceitos["1"] += 6;
                    conceitos["7"] += 3;
                    conceitos["34"] += 2;
                    conceitos["31"] += 3;
                    conceitos["55"] += 4;
                    conceitos["2"] += 3;
                    */

                    resultText.text += "\n - Borracha colorida: 0 reais.";
                }
                else if (i == 5) resultText.text += "\n - Caderno (R$13): 12 reais!";
                else if (i == 6) resultText.text += "\n - Lápis (R$3): 12 reais!";
                else if (i == 7) resultText.text += "\n - Estojo: 0 reais.";
                else if (i == 8) resultText.text += "\n - Lápis (R$4): 0 reais.";
                else if (i == 9) resultText.text += "\n - Apontador (R$6): 12 reais!";
                else if (i == 10) resultText.text += "\n - Apontador (R$5): 0 reais.";
                else if (i == 11) resultText.text += "\n - Régua: 0 reais.";
                else if (i == 12) resultText.text += "\n - Garrafa d'água: 0 reais.";
                else resultText.text += "ERROR";

                if(i == 0 || i == 1 || i == 5 || i == 6 || i == 7 || i == 8 || i == 10 || i == 11)
                {
                    /*
                    conceitos["14"] += 4;
                    conceitos["22"] += 3;
                    conceitos["19"] += 5;
                    conceitos["1"] += 6;
                    conceitos["7"] += 3;
                    conceitos["34"] += 2;
                    conceitos["31"] += 3;
                    conceitos["55"] += 4;
                    conceitos["2"] += 3;

                    */
                }
                else
                {
                    /*
                    conceitos["14"] += 0;
                    conceitos["22"] += 0;
                    conceitos["19"] += 1;
                    conceitos["1"] += 1;
                    conceitos["7"] += 0;
                    conceitos["34"] += 2;
                    conceitos["31"] += 1;
                    conceitos["55"] += 1;
                    conceitos["2"] += 1;

                    */
                }
            }
        }

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = (totalCoins + bonusCoins).ToString();

      //  if (bonusCoins > 0) StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawn, coinWaypoint));
       // StartCoroutine(FlavorManager.fm.SpawnBucks(5));

        PlayerManager.pm.AddCoins(totalCoins + bonusCoins);
        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }
}
