using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V1L4 : MonoBehaviour
{
    private V1L4Helper v1l4Helper;

    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject confirmPanel;
    public TextMeshProUGUI confirmText;
    public Button yesButton, noButton;

    public TextMeshProUGUI totalCoinsText;

    public GameObject optionPanel;
    public Button leftButton, rightButton;

    private void Start()
    {
        v1l4Helper = GameObject.Find("V1L4Helper").GetComponent<V1L4Helper>();
        totalCoinsText.text = v1l4Helper.coins.ToString();

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1start[3]);
        infoText.text = "Você recebeu 6 moedas para usar. Veja aonde quer ir primeiro: à Loja de Brinquedos ou ao Mercado.";
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
        Part1Assign();
    }

    private void Part1Assign()
    {
        leftButton.onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            AudioManager.am.PlayVoice(AudioManager.am.v1l4[9]);
            StartCoroutine(Part1Confirm("Deseja ir para o mercado?", "V1L4a", true));
            leftButton.onClick.RemoveAllListeners();
        });
        rightButton.onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            AudioManager.am.PlayVoice(AudioManager.am.v1l4[10]);
            StartCoroutine(Part1Confirm("Deseja ir para a loja de brinquedos?", "V1L4b", false));
            rightButton.onClick.RemoveAllListeners();
        });
    }

    private IEnumerator Part1Confirm(string text, string name, bool choice)
    {
        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(confirmPanel, true);
        confirmText.text = text;

        yesButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(confirmPanel, false);
            v1l4Helper.rightChoice = choice;
            GameManager.gm.LoadScene(name);
            AudioManager.am.voiceChannel.Stop();
            yesButton.onClick.RemoveAllListeners();
        });
        noButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(confirmPanel, false);
            FlavorManager.fm.ShowHidePanel(optionPanel, true);
            Part1Assign();
            AudioManager.am.voiceChannel.Stop();
            noButton.onClick.RemoveAllListeners();
        });
    }
}