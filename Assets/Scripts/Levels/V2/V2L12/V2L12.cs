using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L12 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public DragDrop dragDrop;
    public Image[] slotBgs;

    public GameObject optionPanel;
    public Button button1, button2, button3, button4, button5, button6, button7, button8;
    public GameObject selected1, selected2, selected3, selected4, selected5, selected6, selected7, selected8;
    public Button submit;
    private int counter;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;
    private int totalMoney;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        dragDrop.enabled = false;

       // AudioManager.am.PlayVoice(AudioManager.am.v2start[11]);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Escolha o melhor local para o carrinho de lanches. \n  Você precisa vender seus lanches e ganhar mais dinheiro. \n Pense bem na escolha certa.";
        infoButton.onClick.AddListener(delegate
        {
            AudioManager.am.voiceChannel.Stop();
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
            dragDrop.enabled = true;
            infoButton.onClick.RemoveAllListeners();
        });
    }

    public void Part1(int id)
    {
        // Declaração
        conceitos.Add("1", 0);
        conceitos.Add("2", 0);
        conceitos.Add("25", 0);
        conceitos.Add("10", 0);
        conceitos.Add("5", 0);
        conceitos.Add("57", 0);
        conceitos.Add("58", 0);
        conceitos.Add("9", 0);
        conceitos.Add("20", 0);
        conceitos.Add("19", 0);
        conceitos.Add("59", 0);

        conceitos["1"] += 0;
        conceitos["2"] += 0;
        conceitos["25"] += 0;
        conceitos["10"] += 0;
        conceitos["5"] += 0;
        conceitos["57"] += 0;
        conceitos["58"] += 0;
        conceitos["9"] += 0;
        conceitos["20"] += 0;
        conceitos["19"] += 0;
        conceitos["59"] += 0;

        foreach (Image img in slotBgs) img.enabled = false;
        StartCoroutine(FlavorManager.fm.SpawnBucks(5));

        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        if (id == 1)
        {
            conceitos["1"] += 1;
            conceitos["2"] += 1;
            conceitos["25"] += 2;
            conceitos["10"] += 2;
            conceitos["5"] += 1;
            conceitos["57"] += 0;
            conceitos["58"] += 3;
            conceitos["9"] += 4;
            conceitos["20"] += 1;
            conceitos["19"] += 0;

            infoText.text = "Parabéns! Você escolheu um ótimo local com alta demanda por lanches.\n O sorvete, nesse caso, atua como um produto complementar, pois as duas pessoas tomando picolé podem sentir fome e decidir comprar um lanche. \n" + "Suas vendas renderam R$ 62, com um total de 8 pedidos concluídos.\n Excelente trabalho!";
            PlayerManager.pm.AddCoins(62);
            totalMoney += 62;
        }
        else if (id == 2)
        {
            conceitos["1"] += 3;
            conceitos["2"] += 3;
            conceitos["25"] += 4;
            conceitos["10"] += 3;
            conceitos["5"] += 4;
            conceitos["57"] += 3;
            conceitos["58"] += 5;
            conceitos["9"] += 6;
            conceitos["20"] += 3;
            conceitos["19"] += 2;

            infoText.text = "Essa não foi a melhor escolha. \n Estar próximo à barraca de sorvetes seria mais vantajoso, já que o sorvete complementa o lanche e poderia atrair mais clientes. \n A segunda melhor opção seria um local sem ninguém consumindo, pois há mais chances de conquistar novos clientes. \n " + "Mesmo assim, você conseguiu vender! Seu total foi de R$ 48, com 6 vendas. \n Bom trabalho!";
            PlayerManager.pm.AddCoins(48);
            totalMoney += 48;
        }
        else if(id == 3)
        {
            conceitos["1"] += 1;
            conceitos["2"] += 1;
            conceitos["25"] += 2;
            conceitos["10"] += 2;
            conceitos["5"] += 3;
            conceitos["57"] += 1;
            conceitos["58"] += 3;
            conceitos["9"] += 4;
            conceitos["20"] += 2;
            conceitos["19"] += 1;

            infoText.text = "Você escolheu a segunda melhor opção! \n  Ficar perto da barraca de sorvetes seria mais estratégico, já que o sorvete complementa o lanche. \n" +  "Além disso, no centro já havia alguém comendo, e escolher um local com mais pessoas que ainda não consumiram nada poderia gerar ainda mais vendas. \n Mesmo assim, seu desempenho foi ótimo! Você faturou R$ 56 com 7 vendas. \n Parabéns!";
            PlayerManager.pm.AddCoins(56);
            totalMoney += 56;
        }

       // infoText.text += "\n\nAgora escolha as comidas mais saudáveis a seguir!";

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners();  End();  });
    }
    private IEnumerator Part2()
    {

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);

        button1.onClick.AddListener(delegate { Part2Choice(selected1); });
        button2.onClick.AddListener(delegate { Part2Choice(selected2); });
        button3.onClick.AddListener(delegate { Part2Choice(selected3); });
        button4.onClick.AddListener(delegate { Part2Choice(selected4); });
        button5.onClick.AddListener(delegate { Part2Choice(selected5); });
        button6.onClick.AddListener(delegate { Part2Choice(selected6); });
        button7.onClick.AddListener(delegate { Part2Choice(selected7); });
        button8.onClick.AddListener(delegate { Part2Choice(selected8); });
        submit.onClick.AddListener(delegate { End(); submit.onClick.RemoveAllListeners(); });

    }
    private void Part2Choice(GameObject selected)
    {
        if (counter < 4)
        {
            if (!selected.activeSelf)
            {
                selected.SetActive(true);
                counter++;
            }
            else
            {
                selected.SetActive(false);
                counter--;
            }
        }
        else if (selected.activeSelf)
        {
            selected.SetActive(false);
            counter--;
        }

        if (counter == 4) submit.gameObject.SetActive(true);
        else submit.gameObject.SetActive(false);
    }

    private void End()
    {
        /*
        int bonus = 0;
        int counter = 0;

        if (selected1.activeSelf)
        {
            bonus += 5;
            counter++;
        }
        if (selected3.activeSelf) 
        { 
            bonus += 5;
            counter++;
        }
        if (selected5.activeSelf) 
        { 
            bonus += 5;
            counter++;
        }
        if (selected7.activeSelf) 
        { 
            bonus += 5;
            counter++;
        }


        if (counter == 3)
        {
            conceitos["1"] += 1;
            conceitos["2"] += 1;
            conceitos["25"] += 1;
            conceitos["10"] += 1;
            conceitos["59"] += 1;
        }
        else if(counter == 4)
        {
            conceitos["1"] += 2;
            conceitos["2"] += 2;
            conceitos["25"] += 3;
            conceitos["10"] += 2;
            conceitos["59"] += 2;
        }

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        resultText.text = "Você conseguiu " + bonus + " reais! Vamos para a próxima.";

        totalMoney += bonus;

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = totalMoney.ToString();

        if (bonus > 0) StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        PlayerManager.pm.AddCoins(bonus);

        */

        APIManager.am.Relatorio(conceitos);
        PlayerManager.pm.AddLevel();
    }
}