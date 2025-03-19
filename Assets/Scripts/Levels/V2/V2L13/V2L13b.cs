using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class V2L13b : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject giftA, giftB, giftC;
    public TextMeshProUGUI counterText;
    public GameObject lidClosed, lidOpen;

    private void Start()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Vamos ver se você conseguiu ganhar seu presente do segundo sonho!!!";
        infoButton.onClick.AddListener(delegate { StartCoroutine(Part1()); infoButton.onClick.RemoveAllListeners(); });
    }

    public IEnumerator Part1()
    {
        Animator textAnim = counterText.GetComponent<Animator>();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);

        float currentTime = 4;

        while (currentTime >= 0)
        {
            currentTime -= Time.deltaTime;

            textAnim.SetTrigger("Start");

            counterText.text = ((int)currentTime).ToString();
            yield return null;
        }

        FlavorManager.fm.BigPuff();
        if (lidClosed.activeInHierarchy)
        {
            lidClosed.SetActive(false);
            lidOpen.SetActive(true);
        }

        if (PlayerManager.pm.safe2 == 30)
        {
            resultText.text = "Você não alcançou o preço do sonho! Os 30 reais que sobraram serão colocados no cofre 3!";
            PlayerManager.pm.safe3 += 30;
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
        }
        else if (PlayerManager.pm.safe2 == 40)
        {
            resultText.text = "Você alcançou o preço do sonho!";
            if (PlayerManager.pm.gift2 == 1) FlavorManager.fm.ShowHidePanel(giftA, true);
            if (PlayerManager.pm.gift2 == 2) FlavorManager.fm.ShowHidePanel(giftB, true);
            if (PlayerManager.pm.gift2 == 3) FlavorManager.fm.ShowHidePanel(giftC, true);
        }
        else if (PlayerManager.pm.safe2 == 55)
        {
            resultText.text = "Você alcançou o preço do sonho! Os 15 reais que sobraram serão colocados no cofre 3!";
            if (PlayerManager.pm.gift2 == 1) FlavorManager.fm.ShowHidePanel(giftA, true);
            if (PlayerManager.pm.gift2 == 2) FlavorManager.fm.ShowHidePanel(giftB, true);
            if (PlayerManager.pm.gift2 == 3) FlavorManager.fm.ShowHidePanel(giftC, true);
            PlayerManager.pm.safe3 += 15;
        }
        else if (PlayerManager.pm.safe2 == 100)
        {
            resultText.text = "Você alcançou o preço do sonho! Os 60 reais que sobraram serão colocados no cofre 3!";
            if (PlayerManager.pm.gift2 == 1) FlavorManager.fm.ShowHidePanel(giftA, true);
            if (PlayerManager.pm.gift2 == 2) FlavorManager.fm.ShowHidePanel(giftB, true);
            if (PlayerManager.pm.gift2 == 3) FlavorManager.fm.ShowHidePanel(giftC, true);
            PlayerManager.pm.safe3 += 60;
        }
        else if (PlayerManager.pm.safe2 == 115)
        {
            resultText.text = "Você alcançou o preço do sonho! Os 75 reais que sobraram serão colocados no cofre 3!";
            if (PlayerManager.pm.gift2 == 1) FlavorManager.fm.ShowHidePanel(giftA, true);
            if (PlayerManager.pm.gift2 == 2) FlavorManager.fm.ShowHidePanel(giftB, true);
            if (PlayerManager.pm.gift2 == 3) FlavorManager.fm.ShowHidePanel(giftC, true);
            PlayerManager.pm.safe3 += 75;
        }
        else if (PlayerManager.pm.safe2 == 125)
        {
            resultText.text = "Você alcançou o preço do sonho! Os 85 reais que sobraram serão colocados no cofre 3!";
            if (PlayerManager.pm.gift2 == 1) FlavorManager.fm.ShowHidePanel(giftA, true);
            if (PlayerManager.pm.gift2 == 2) FlavorManager.fm.ShowHidePanel(giftB, true);
            if (PlayerManager.pm.gift2 == 3) FlavorManager.fm.ShowHidePanel(giftC, true);
            PlayerManager.pm.safe3 += 85;
        }

        counterText.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);

        PlayerManager.pm.AddLevel();
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
    }
}