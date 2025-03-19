using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L2 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject optionPanel1, optionPanel2, optionPanel3, optionPanel4;

    public TweenCharacters tween;
    public RectTransform rect1, rect2, rect3, rect4;
    public Transform transform1, transform2, transform3, transform4;
    public TextMeshProUGUI title1, title2, title3, title4;

    public Button p1Button1, p1Button2, p1Button3, p1Button4;
    public Button p2Button1, p2Button2, p2Button3, p2Button4;
    public Button p4Button;

    private int dropCounter, correctCounter;
    public TextMeshProUGUI professionText;
    public TextMeshProUGUI nome;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;
    public int totalIncome;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start()
    {
        if (PlayerManager.pm.type == AvatarType.Kid01 || PlayerManager.pm.type == AvatarType.Kid03)
        {
            title1.text = "Dentista";
            title2.text = "Bombeiro";
            title3.text = "Médico";
            title4.text = "Piloto de avião";
        }
        else
        {
            title1.text = "Dentista";
            title2.text = "Bombeira";
            title3.text = "Médica";
            title4.text = "Piloto de avião";
        }
        AudioManager.am.PlayVoice(AudioManager.am.v2start[1]);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Escolha sua profissão preferida e curso que você deve fazer para que se torne um bom profissional.";
        infoButton.onClick.AddListener(delegate { Part1(); }); 
    }

    private void Part1()
    {
        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel1, true);

        p1Button1.onClick.AddListener(delegate{ Part1Choice(1, rect1, transform1); });
        p1Button2.onClick.AddListener(delegate{ Part1Choice(2, rect2, transform2); });
        p1Button3.onClick.AddListener(delegate{ Part1Choice(3, rect3, transform3); });
        p1Button4.onClick.AddListener(delegate{ Part1Choice(4, rect4, transform4); });
    }

    private void Part1Choice(int id, RectTransform rect, Transform transform)
    {
        PlayerManager.pm.professionId = id;

        rect1.gameObject.SetActive(false);
        rect2.gameObject.SetActive(false);
        rect3.gameObject.SetActive(false);
        rect4.gameObject.SetActive(false);
        rect.gameObject.SetActive(true);

        tween.obj = rect;
        tween.avatar = transform;
        tween.Animate();

        Part2();
    }

    private void Part2()
    {
        FlavorManager.fm.ShowHidePanel(optionPanel1, false);
        FlavorManager.fm.ShowHidePanel(optionPanel2, true);

        p2Button1.onClick.AddListener(delegate { Part2Choice(4); });
        p2Button2.onClick.AddListener(delegate { Part2Choice(3); });
        p2Button3.onClick.AddListener(delegate { Part2Choice(2); });
        p2Button4.onClick.AddListener(delegate { Part2Choice(1); });
    }

    private void Part2Choice(int id)
    {
        if (id == PlayerManager.pm.professionId) Part2End(true);
        else Part2End( false);
    }

    private void Part2End(bool correct)
    {
        FlavorManager.fm.ShowHidePanel(optionPanel2, false);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        if (correct)
        {
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            PlayerManager.pm.AddCoins(20);
            totalIncome += 20;
            infoText.text = "Parabéns, curso correto! Ganhou 20 reais!";

            conceitos.Add("10", 2);
            conceitos.Add("25", 2);
            conceitos.Add("38", 3);
            conceitos.Add("39", 1);
            conceitos.Add("28", 1);
            conceitos.Add("1", 1);
            conceitos.Add("2", 4);
        }
        else
        {
            conceitos.Add("10", 1);
            conceitos.Add("25", 0);
            conceitos.Add("38", 2);
            conceitos.Add("39", 1);
            conceitos.Add("28", 0);
            conceitos.Add("1", 0);
            conceitos.Add("2", 1);

            infoText.text = "Não foi dessa vez";
        }

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { AudioManager.am.PlaySFX(AudioManager.am.button); Part3(); infoButton.onClick.RemoveAllListeners(); });
    }

    private void Part3()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel3, true);
    }

    public void Part3Choice(int id)
    {
        dropCounter++;
        if (id == PlayerManager.pm.professionId) correctCounter++;
        if (dropCounter == 2) Part3End();
    }

    private void Part3End()
    {
        professionText.text = PlayerManager.pm.GetProfessionName();

        if(correctCounter == 0)
        {
            conceitos["10"] += 2;
            conceitos["25"] += 3;
            conceitos["28"] += 2;
            conceitos["1"] += 1;
            conceitos["2"] += 3;
        }
        else if (correctCounter == 1)
        {
            conceitos["10"] += 1;
            conceitos["25"] += 1;
            conceitos["28"] += 1;
            conceitos["1"] += 0;
            conceitos["2"] += 2;
        }
        else if (correctCounter == 2)
        {
            conceitos["10"] += 0;
            conceitos["25"] += 0;
            conceitos["28"] += 0;
            conceitos["1"] += 0;
            conceitos["2"] += 1;
        }


        FlavorManager.fm.ShowHidePanel(optionPanel3, false);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você conseguiu " + correctCounter * 15 + " reais!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { AudioManager.am.PlaySFX(AudioManager.am.button); Part4(); infoButton.onClick.RemoveAllListeners(); });

        if (correctCounter > 0)
        {
            StartCoroutine(FlavorManager.fm.SpawnBucks(correctCounter * 3));
            PlayerManager.pm.AddCoins(correctCounter * 15);
            totalIncome += correctCounter * 15;
        }
    }

    private void Part4()
    {
        professionText.text = PlayerManager.pm.GetProfessionName();
        nome.text = PlayerManager.pm.nome;

        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel4, true);

        p4Button.onClick.AddListener(delegate { StartCoroutine(End()); p4Button.onClick.RemoveAllListeners(); });
    }

    private IEnumerator End()
    {
        APIManager.am.Relatorio(conceitos);

        FlavorManager.fm.ShowHidePanel(optionPanel4, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Agora vamos ver o profissonal em ação!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate
        {

            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
            GameManager.gm.LoadScene("V2L2b");
            infoButton.onClick.RemoveAllListeners();
        });

        FlavorManager.fm.ShowHidePanel(incomePanel, true);
        incomeText.text = totalIncome.ToString();

        PlayerManager.pm.AddLevel();
    }
}