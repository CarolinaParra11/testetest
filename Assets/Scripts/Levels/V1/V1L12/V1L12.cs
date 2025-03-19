using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V1L12 : MonoBehaviour
{
    private V1L12Helper v1l12helper;

    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject confirmPanel;
    public TextMeshProUGUI confirmText;
    public Button yesButton, noButton;

    public TextMeshProUGUI totalCoinsText;
    public RectTransform coinSpawn, coinWaypoint;

    public GameObject optionPanel;
    public Button leftButton, rightButton;
    
    private void Start()
    {
        AudioManager.am.PlayVoice(AudioManager.am.v1start[11]);
        v1l12helper = GameObject.Find("V1L12Helper").GetComponent<V1L12Helper>();
        totalCoinsText.text = v1l12helper.coins.ToString();

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você recebeu 3 moedas." +
            " Hoje você está voltando da escola e precisa decidir se quer parar no caminho para comer um sanduíche ou se quer ir direto para casa e comer lá." +
            " Lembre-se de que está com um pouco de fome, mas aguenta chegar até sua casa.";
        infoButton.onClick.AddListener(delegate 
        { 
            Part1();
            AudioManager.am.voiceChannel.Stop();
        });
    }

    public void Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        FlavorManager.fm.ShowHidePanel(optionPanel, true);
        Part1Assign();
    }

    public void Part1Assign()
    {
        leftButton.onClick.AddListener(delegate 
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l12[0]);
            StartCoroutine(Part1Confirm(0, true, "Deseja guardar suas moedas e se alimentar em sua casa?"));
        });
        rightButton.onClick.AddListener(delegate 
        {
            AudioManager.am.PlayVoice(AudioManager.am.v1l12[1]);
            StartCoroutine(Part1Confirm(3, false, "Deseja gastar suas 3 moedas e comprar um sanduiche e água?")); 
        });
    }

    private IEnumerator Part1Confirm(int cost, bool correct, string text)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        leftButton.onClick.RemoveAllListeners();
        rightButton.onClick.RemoveAllListeners();

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(confirmPanel, true);
        confirmText.text = text;

        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            End(cost, correct);
        });

        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(confirmPanel, false);
            FlavorManager.fm.ShowHidePanel(optionPanel, true);
            Part1Assign();
            noButton.onClick.RemoveAllListeners();
            AudioManager.am.voiceChannel.Stop();
        });
    }

    private void End(int cost, bool correct)
    {
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        AudioManager.am.voiceChannel.Stop();

        v1l12helper.coins -= cost;
        v1l12helper.correct = correct;

        totalCoinsText.text = v1l12helper.coins.ToString();
        StartCoroutine(FlavorManager.fm.SpawnCoinPosition(cost, coinSpawn, coinWaypoint));

        FlavorManager.fm.ShowHidePanel(optionPanel, false);
        GameManager.gm.LoadScene("V1L12a");
    }
}