using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class V2L26c : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    private V2L26Helper v2l26Helper;
    public TextMeshProUGUI totalMoneyText;
    public RectTransform coinSpawnStart, coinSpawnEnd;

    public Animator animatorAdult;
    public TweenCharacters animatorPlayer;

    public GameObject optionPanel;
    public Button button1, button2, button3;
    public Button goBackButton;

    private void Start()
    {
        v2l26Helper = GameObject.Find("V2L26Helper").GetComponent<V2L26Helper>();
        
        StartCoroutine(Part1());

        goBackButton.onClick.AddListener(delegate { GoBack(); });
    }

    private IEnumerator Part1()
    {
        totalMoneyText.text = v2l26Helper.coins.ToString();
        animatorPlayer.GoPath();
        yield return new WaitForSeconds(2);
        animatorAdult.SetTrigger("ShowCard");
        yield return new WaitForSeconds(2);

        if (v2l26Helper.coins >= 50 && !v2l26Helper.deposit)
        {
            FlavorManager.fm.ShowHidePanel(optionPanel, true);

            button1.onClick.AddListener(delegate { End(50); });

            if (v2l26Helper.coins >= 75)
            {
                button2.onClick.AddListener(delegate { End(75); });
                button2.gameObject.SetActive(true);
            }
            if (v2l26Helper.coins == 100)
            {
                button3.onClick.AddListener(delegate { End(100); });
                button3.gameObject.SetActive(true);
            }
        }
        else
        {
            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoText.text = "Você já depositou ou não tem dinheiro suficiente para depositar no banco!";
            infoButton.onClick.RemoveAllListeners();
            infoButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L26"); infoButton.onClick.RemoveAllListeners(); AudioManager.am.PlaySFX(AudioManager.am.button); });
        }
    }

    private void End(int value)
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        goBackButton.onClick.RemoveAllListeners();

        AudioManager.am.PlaySFX(AudioManager.am.button);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = value + " reais depositados!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L26"); infoButton.onClick.RemoveAllListeners(); AudioManager.am.PlaySFX(AudioManager.am.button); });


        PlayerManager.pm.v2l26choice = value;
        v2l26Helper.deposit = true;
        v2l26Helper.coins -= value;
        totalMoneyText.text = v2l26Helper.coins.ToString();
        StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawnStart, coinSpawnEnd));
    }

    private void GoBack()
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        goBackButton.onClick.RemoveAllListeners();

        AudioManager.am.PlaySFX(AudioManager.am.button);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Vamos voltar!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L26"); infoButton.onClick.RemoveAllListeners(); AudioManager.am.PlaySFX(AudioManager.am.button); });
    }
}