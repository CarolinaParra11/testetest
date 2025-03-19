using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class V2L23 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject optionPanel1, optionPanel2, optionPanel3;
    public Button p1Button1, p1Button2, p1Button3;
    public Button p2Button1, p2Button2, p2Button3;
    private int itemCounter;
    private int bonus;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        // Declaração
        conceitos.Add("25", 0);
        conceitos.Add("31", 0);
        conceitos.Add("7", 0);
        conceitos.Add("20", 0);
        conceitos.Add("2", 0);
        conceitos.Add("1", 0);
        conceitos.Add("19", 0);
        conceitos.Add("18", 0);
        conceitos.Add("55", 0);
        conceitos.Add("9", 0);
        conceitos.Add("26", 0);

        conceitos["25"] += 0;
        conceitos["31"] += 0;
        conceitos["7"] += 0;
        conceitos["20"] += 0;
        conceitos["2"] += 0;
        conceitos["1"] += 0;
        conceitos["19"] += 0;
        conceitos["18"] += 0;
        conceitos["55"] += 0;
        conceitos["9"] += 0;
        conceitos["26"] += 0;



        StartCoroutine(SpanishPanel());
    }

 private IEnumerator SpanishPanel()
    {
        //Fazer Verificação
        yield return new WaitForSeconds(1);
        if (PlayerManager.pm.spanishCourse == true) 
        
        {
            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoText.text = "Parabéns!!! \n\n Você se planejou, esperou e foi recompensado!\n\n Agora, com seu espanhol, poderá ir ao Chile e ainda ganhou um aumento de 250 reais para seu Bolodix! \n\nContinue estudando!";
            PlayerManager.pm.AddCoins(250);
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            infoButton.onClick.AddListener(delegate { StartCoroutine(Part1Start()); infoButton.onClick.RemoveAllListeners(); });

        }
        else
        {
            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoText.text = "Todos os idiomas são importantes \n \n  Mas, nessa etapa, surgiu uma oportunidade de ir para o Chile. \n \n Para isso,era necessário o curso de espanhol. \n \n Continue estudando, pois será recompensado.";
            infoButton.onClick.AddListener(delegate { StartCoroutine(Part1Start()); infoButton.onClick.RemoveAllListeners(); });

        }


    }


    private IEnumerator Part1Start()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(1);
        //AudioManager.am.PlayVoice(AudioManager.am.v2start[22]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Vamos à praia, agora como empreendedor!\n \n Você já aprendeu bastante sobre como organizar o essencial para o lazer. Como empreendedor, saber definir os itens primordiais para o negócio e diferenciá-los dos secundários ajudará você a tomar decisões estratégicas no futuro.\n \n Isso não significa que os itens secundários não sejam importantes, mas alguns têm mais relevância para o sucesso do negócio do que outros.";
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); infoButton.onClick.RemoveAllListeners(); });
    }
    private IEnumerator Part1()
    {
        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel1, true);
        p1Button1.onClick.AddListener(delegate { StartCoroutine(Part1End(false)); });
        p1Button2.onClick.AddListener(delegate { StartCoroutine(Part1End(false)); });
        p1Button3.onClick.AddListener(delegate { StartCoroutine(Part1End(true)); });
    }

    private IEnumerator Part1End(bool right)
    {
        p1Button1.onClick.RemoveAllListeners();
        p1Button2.onClick.RemoveAllListeners();
        p1Button3.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        if (right)
        {
            infoText.text = "Parabéns você escolheu a melhor opção! Ganhou 30 reais para o Bolodix de bônus!";
            
            bonus += 30;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));

            conceitos["9"] += 4;
            conceitos["26"] += 9;
        }
        else infoText.text = "A melhor escolha era a 3, pois teria um produto substituto ao coco, e poderia atrair clientes da concorrência com a promoção.";

        infoText.text += "\n\nAgora, suponha que você é dono de uma barraca de coco, como em etapas anteriores. \n \n Separe os itens primordiais dos secundários, conforme explicado. \n \nPara cada item correto, você ganha 15 reais!";

       infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part3()); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part2()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel2, true);
        p2Button1.onClick.AddListener(delegate { StartCoroutine(Part2End(true)); });
        p2Button2.onClick.AddListener(delegate { StartCoroutine(Part2End(false)); });
        p2Button3.onClick.AddListener(delegate { StartCoroutine(Part2End(false)); });
    }

    private IEnumerator Part2End(bool right)
    {
        p2Button1.onClick.RemoveAllListeners();
        p2Button2.onClick.RemoveAllListeners();
        p2Button3.onClick.RemoveAllListeners();


        FlavorManager.fm.ShowHidePanel(optionPanel2, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        if (right)
        {
            infoText.text = "Certo, ganhou 10 reais!";
            bonus += 10;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));

            conceitos["9"] += 4;
            conceitos["26"] += 10;
        }
        else infoText.text = "Errado!";

        infoText.text += "\n\nAgora vamos escolher o que é mais importante para levar!";
        infoText.text += "\nCada objeto no lugar certo vale 15 reais!";

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part3()); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
    }

    private IEnumerator Part3()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel3, true);
        infoText.text = "";
    }

    public void AddItem(string essential, bool right, string title)
    {
        if (right)
        {
            infoText.text += title + ": 15 reais! (" + essential + ")\n";
            bonus += 15;
        }
        else infoText.text += title + ": 0 reais. (" + essential + ")\n";

        itemCounter++;

        if (itemCounter == 12) StartCoroutine(Part3End());
    }

    private IEnumerator Part3End()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel3, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { End(); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
    }

    private void End()
    {
        if (bonus/15 == 12)
        {
            conceitos["25"] += 12;
            conceitos["31"] += 12;
            conceitos["7"] += 18;
            conceitos["20"] += 12;
            conceitos["2"] += 18;
            conceitos["1"] += 16;
            conceitos["19"] += 12;
            conceitos["18"] += 24;
            conceitos["55"] += 16;
        }
        else if (bonus / 15 == 11)
        {
            conceitos["25"] += 11;
            conceitos["31"] += 11;
            conceitos["7"] += 16;
            conceitos["20"] += 11;
            conceitos["2"] += 16;
            conceitos["1"] += 14.5;
            conceitos["19"] += 11;
            conceitos["18"] += 22;
            conceitos["55"] += 14.5;
        }
        else if (bonus / 15 == 10)
        {
            conceitos["25"] += 10;
            conceitos["31"] += 10;
            conceitos["7"] += 14;
            conceitos["20"] += 10;
            conceitos["2"] += 14;
            conceitos["1"] += 13;
            conceitos["19"] += 10;
            conceitos["18"] += 20;
            conceitos["55"] += 13;
        }
        else if (bonus / 15 == 9)
        {
            conceitos["25"] += 9;
            conceitos["31"] += 9;
            conceitos["7"] += 12;
            conceitos["20"] += 9;
            conceitos["2"] += 12;
            conceitos["1"] += 12;
            conceitos["19"] += 9;
            conceitos["18"] += 18;
            conceitos["55"] += 11.5;
        }
        else if (bonus / 15 == 8)
        {
            conceitos["25"] += 8;
            conceitos["31"] += 8;
            conceitos["7"] += 10;
            conceitos["20"] += 8;
            conceitos["2"] += 10;
            conceitos["1"] += 10;
            conceitos["19"] += 8;
            conceitos["18"] += 16;
            conceitos["55"] += 10;
        }
        else if (bonus / 15 == 7)
        {
            conceitos["25"] += 7;
            conceitos["31"] += 7;
            conceitos["7"] += 8;
            conceitos["20"] += 7;
            conceitos["2"] += 8;
            conceitos["1"] += 8;
            conceitos["19"] += 7;
            conceitos["18"] += 14;
            conceitos["55"] += 8;
        }
        else if (bonus / 15 == 6)
        {
            conceitos["25"] += 6;
            conceitos["31"] += 6;
            conceitos["7"] += 6;
            conceitos["20"] += 6;
            conceitos["2"] += 6;
            conceitos["1"] += 6;
            conceitos["19"] += 6;
            conceitos["18"] += 12;
            conceitos["55"] += 6;
        }
        else if (bonus / 15 == 5)
        {
            conceitos["25"] += 5;
            conceitos["31"] += 5;
            conceitos["7"] += 5;
            conceitos["20"] += 5;
            conceitos["2"] += 5;
            conceitos["1"] += 5;
            conceitos["19"] += 5;
            conceitos["18"] += 10;
            conceitos["55"] += 5;
        }
        else if (bonus / 15 == 4)
        {
            conceitos["25"] += 4;
            conceitos["31"] += 4;
            conceitos["7"] += 4;
            conceitos["20"] += 4;
            conceitos["2"] += 4;
            conceitos["1"] += 4;
            conceitos["19"] += 4;
            conceitos["18"] += 8;
            conceitos["55"] += 4;
        }
        else if (bonus / 15 == 3)
        {
            conceitos["25"] += 3;
            conceitos["31"] += 3;
            conceitos["7"] += 3;
            conceitos["20"] += 3;
            conceitos["2"] += 3;
            conceitos["1"] += 3;
            conceitos["19"] += 3;
            conceitos["18"] += 6;
            conceitos["55"] += 3;
        }
        else if (bonus / 15 == 2)
        {
            conceitos["25"] += 2;
            conceitos["31"] += 2;
            conceitos["7"] += 2;
            conceitos["20"] += 2;
            conceitos["2"] += 2;
            conceitos["1"] += 2;
            conceitos["19"] += 2;
            conceitos["18"] += 4;
            conceitos["55"] += 2;
        }
        else if (bonus / 15 == 1)
        {
            conceitos["25"] += 1;
            conceitos["31"] += 1;
            conceitos["7"] += 1;
            conceitos["20"] += 1;
            conceitos["2"] += 1;
            conceitos["1"] += 1;
            conceitos["19"] += 1;
            conceitos["18"] += 2;
            conceitos["55"] += 1;
        }

        FlavorManager.fm.ShowHidePanel(infoPanel, false);

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = bonus.ToString();

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Tudo feito por hoje! Ganhou " + bonus + " reais! Até a próxima!";
        PlayerManager.pm.AddCoins(bonus);

        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }
}