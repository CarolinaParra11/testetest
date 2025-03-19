using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L26a : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    private V2L26Helper v2l26Helper;
    public TextMeshProUGUI totalMoneyText;
    public RectTransform coinSpawnStart, coinSpawnEnd;

    public Button button1, button2;
    public GameObject price1, price2;
    public GameObject selected1, selected2;
    public Button goBackButton;
    public Button buyButton;
    private int buyCount = 0;
    private int maxBuy = 1;

    private void Start()
    {
        v2l26Helper = GameObject.Find("V2L26Helper").GetComponent<V2L26Helper>();

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        if (!v2l26Helper.item3)
        {
            infoText.text = "Você ainda tem " + v2l26Helper.coins + " reais!";
            infoButton.onClick.AddListener(delegate { Part1(); infoButton.onClick.RemoveAllListeners(); });
        }
        else
        {
            infoText.text = "Você já comprou o copo! Vamos voltar!";
            infoButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L26"); infoButton.onClick.RemoveAllListeners(); });
        }

        totalMoneyText.text = v2l26Helper.coins.ToString();
        SetButtons(false);
    }

    private void Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        goBackButton.gameObject.SetActive(true);

        button1.onClick.AddListener(delegate { PanelButton(v2l26Helper.item3, this, 25, price1, selected1);  });
        button2.onClick.AddListener(delegate { PanelButton(v2l26Helper.item3, this, 15, price2, selected2);  });
        buyButton.onClick.AddListener(delegate { StartCoroutine(End(true)); });
        goBackButton.onClick.AddListener(delegate { StartCoroutine(End(false)); });

        SetButtons(true);
    }

    private IEnumerator End(bool bought)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        goBackButton.gameObject.SetActive(false);
        buyButton.gameObject.SetActive(false);
        SetButtons(false);

        yield return new WaitForSeconds(0.5f);

        FlavorManager.fm.ShowHidePanel(infoPanel, true);

        if (!bought)
        {
            infoText.text = "Vamos voltar!";
        }
        else
        {
            infoText.text = "Vamos voltar!";
            StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawnStart, coinSpawnEnd));
            v2l26Helper.item3 = true;
        }

        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L26"); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });
        
    }

    static void PanelButton(bool itemBought, V2L26a instance, int price, GameObject priceObject, GameObject selectedObject)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        instance.goBackButton.gameObject.SetActive(false);

        if (!itemBought)
        {
            if (instance.buyCount < instance.maxBuy)
            {
                if (instance.v2l26Helper.coins >= price)
                {
                    instance.v2l26Helper.coins -= price;
                    instance.totalMoneyText.text = instance.v2l26Helper.coins.ToString();

                    priceObject.SetActive(false);
                    selectedObject.SetActive(true);

                    instance.buyButton.gameObject.SetActive(true);
                    instance.buyCount++;
                }
                else instance.StartCoroutine(instance.NotEnoughMoney());
            }
            else if (selectedObject.activeSelf)
            {
                instance.v2l26Helper.coins += price;
                instance.totalMoneyText.text = instance.v2l26Helper.coins.ToString();

                priceObject.SetActive(true);
                selectedObject.SetActive(false);

                instance.goBackButton.gameObject.SetActive(true);
                instance.buyButton.gameObject.SetActive(false);
                instance.buyCount--;
            }
        }
        else instance.StartCoroutine(instance.AlreadyBought());
    }

    private IEnumerator NotEnoughMoney()
    {
        SetButtons(false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você não tem dinheiro suficiente!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate
        {
            SetButtons(true);
            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
            infoButton.onClick.RemoveAllListeners();
            goBackButton.gameObject.SetActive(true);
        });
    }

    private IEnumerator AlreadyBought()
    {
        SetButtons(false);
        yield return new WaitForSeconds(0.5f);
        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        infoText.text = "Você já comprou esse item!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate
        {
            SetButtons(true);
            AudioManager.am.PlaySFX(AudioManager.am.button);
            FlavorManager.fm.ShowHidePanel(infoPanel, false);
            infoButton.onClick.RemoveAllListeners();
            if (!buyButton.gameObject.activeSelf)
                goBackButton.gameObject.SetActive(true);
        });
    }

    private void SetButtons(bool value)
    {
        button1.enabled = value;
        button2.enabled = value;
        buyButton.enabled = value;
        goBackButton.enabled = value;
    }
}