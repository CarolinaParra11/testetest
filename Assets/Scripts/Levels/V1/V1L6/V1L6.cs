using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class V1L6 : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject optionPanel1, optionPanel2;

    public TweenCharacters tween;
    public RectTransform rect1, rect2, rect3, rect4;
    public Transform transform1, transform2, transform3, transform4;
    public TextMeshProUGUI title1, title2, title3, title4;

    public Button p1Button1, p1Button2, p1Button3, p1Button4;

    private int dropCounter, correctCounter;
    public TextMeshProUGUI professionText;
    public TextMeshProUGUI nome;

    public GameObject incomePanel;
    public Transform coinsPanel;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();


    private void Start()
    {
        conceitos.Add("25", 0);
        conceitos.Add("28", 0);
        conceitos.Add("21", 0);

        conceitos["25"] += 0;
        conceitos["28"] += 0;
        conceitos["21"] += 0;

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

        AudioManager.am.PlayVoice(AudioManager.am.v1start[5]);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Escolha sua profissão preferida.";
        infoButton.onClick.AddListener(delegate 
        { 
            Part1();
            AudioManager.am.voiceChannel.Stop();
        });
    }

    private void Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel1, true);

        p1Button1.onClick.AddListener(delegate { Part1Choice(1, rect1, transform1); });
        p1Button2.onClick.AddListener(delegate { Part1Choice(2, rect2, transform2); });
        p1Button3.onClick.AddListener(delegate { Part1Choice(3, rect3, transform3); });
        p1Button4.onClick.AddListener(delegate { Part1Choice(4, rect4, transform4); });
    }

    private void Part1Choice(int id, RectTransform rect, Transform transform)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        p1Button1.onClick.RemoveAllListeners();
        p1Button2.onClick.RemoveAllListeners();
        p1Button3.onClick.RemoveAllListeners();
        p1Button4.onClick.RemoveAllListeners();

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
        AudioManager.am.PlayVoice(AudioManager.am.v1l6[0]);
        FlavorManager.fm.ShowHidePanel(optionPanel2, true);
    }

    public void Part2Choice(int id)
    {
        dropCounter++;
        if (id == PlayerManager.pm.professionId) correctCounter++;
        if (dropCounter == 2) Part2End();
    }

    private void Part2End()
    {
        professionText.text = PlayerManager.pm.GetProfessionName();
        nome.text = PlayerManager.pm.nome;

        FlavorManager.fm.ShowHidePanel(optionPanel2, false);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        if (correctCounter == 2)
        {
            conceitos["25"] += 2;
            conceitos["28"] += 2;
            conceitos["21"] += 2;

            AudioManager.am.PlayVoice(AudioManager.am.v1l6[1]);
            infoText.text = "Você ganhou 4 moedas para o seu Bolodix! Veja o profissional escolhido em ação!";
        }
        if (correctCounter == 1)
        {
            conceitos["25"] += 1;
            conceitos["28"] += 1;
            conceitos["21"] += 1;

            AudioManager.am.PlayVoice(AudioManager.am.v1l6[2]);
            infoText.text = "Você ganhou 2 moedas para o seu Bolodix! Veja o profissional escolhido em ação!";
        }
        if (correctCounter == 0)
        {
            conceitos["25"] += 0;
            conceitos["28"] += 0;
            conceitos["21"] += 1;

            AudioManager.am.PlayVoice(AudioManager.am.v1l6[3]);
            infoText.text = "Você não escolheu os objetos da sua profissão… Veja agora o profissional escolhido em ação.";
        }

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate 
        {
            End();
            infoButton.onClick.RemoveAllListeners();
        });

        if (correctCounter * 2 > 0)
        {
            FlavorManager.fm.ShowHidePanel(incomePanel, true);

            int counter = 0;

            while (counter < correctCounter * 2)
            {
                coinsPanel.GetChild(counter).gameObject.SetActive(true);
                counter++;
            }

            StartCoroutine(FlavorManager.fm.SpawnCoin(correctCounter * 2));
            PlayerManager.pm.AddCoins(correctCounter * 2);
        }
    }

    private void End()
    {
        APIManager.am.Relatorio(conceitos);
        AudioManager.am.PlaySFX(AudioManager.am.button);
        AudioManager.am.voiceChannel.Stop();
        PlayerManager.pm.AddLevel();
        GameManager.gm.LoadScene("V1L6b");
    }
}
