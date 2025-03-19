using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class V1L12a : MonoBehaviour
{
    private V1L12Helper v1l12helper;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public TextMeshProUGUI totalCoinsText;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {

        conceitos.Add("1", 0);
        conceitos.Add("2", 0);
        conceitos.Add("25", 0);
        conceitos.Add("10", 0);
        conceitos.Add("34", 0);
        conceitos.Add("24", 0);
        conceitos.Add("28", 0);

        conceitos["1"] += 0;
        conceitos["2"] += 0;
        conceitos["25"] += 0;
        conceitos["10"] += 0;
        conceitos["34"] += 0;
        conceitos["24"] += 0;
        conceitos["28"] += 0;

        v1l12helper = GameObject.Find("V1L12Helper").GetComponent<V1L12Helper>();
        totalCoinsText.text = v1l12helper.coins.ToString();

        FlavorManager.fm.ShowHidePanel(resultPanel, true);

        if (v1l12helper.correct)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l12[3]);
            PlayerManager.pm.AddCoins(3);
            StartCoroutine(FlavorManager.fm.SpawnCoin(3));
            resultText.text = "Parabéns!" +
                " Você soube esperar," +
                " comeu o que queria na sua casa e agora pode guardar as três moedas no seu Bolodix." +
                " Veja como ele está maior agora!";

            FlavorManager.fm.ShowHidePanel(incomePanel, true);

            int counter = 0;

            while (counter < 3)
            {
                coinsPanel.GetChild(counter).gameObject.SetActive(true);
                counter++;
            }

            conceitos["1"] += 2;
            conceitos["2"] += 2;
            conceitos["25"] += 1;
            conceitos["10"] += 2;
            conceitos["34"] += 1;
            conceitos["24"] += 1;
            conceitos["28"] += 1;
        }
        else
        {
            conceitos["1"] += 1;
            conceitos["2"] += 1;
            conceitos["25"] += 0;
            conceitos["10"] += 1;
            conceitos["34"] += 0;
            conceitos["24"] += 0;
            conceitos["28"] += 0;

            AudioManager.am.PlayVoice(AudioManager.am.v1l12[2]);
            resultText.text = "Você se alimentou e não tem mais fome!" +
                " Se esperasse chegar a sua casa," +
                " não precisaria usar as 3 moedas e ainda poderia comer um sanduíche e beber água.";
        }

        APIManager.am.Relatorio(conceitos);
        PlayerManager.pm.AddLevel();
    }
}