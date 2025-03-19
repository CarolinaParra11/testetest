using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L26b : MonoBehaviour
{
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    private V2L26Helper v2l26Helper;
    public int totalPrice;
    public TextMeshProUGUI totalPriceText, totalMoneyText;
    public RectTransform coinSpawnStart, coinSpawnEnd;

    public Button button1, button2, button3;
    public GameObject price1, price2, price3;
    public GameObject selected1, selected2, selected3;
    public Button goBackButton;
    public Button buyButton;

    private void Start()
    {
        v2l26Helper = GameObject.Find("V2L26Helper").GetComponent<V2L26Helper>();

        FlavorManager.fm.ShowHidePanel(infoPanel, true);
        if (!v2l26Helper.item1 || !v2l26Helper.item2 || !v2l26Helper.item3)
        {
            infoText.text = "Você ainda tem " + v2l26Helper.coins + " reais!";
            infoButton.onClick.AddListener(delegate { Part1(); infoButton.onClick.RemoveAllListeners(); });
        }
        else
        {
            infoText.text = "Você já comprou tudo! Vamos voltar!";
            infoButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L26"); infoButton.onClick.RemoveAllListeners(); });
        }

        totalMoneyText.text = v2l26Helper.coins.ToString();
        SetButtons(false);
    }

    private void Part1()
    {
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        goBackButton.gameObject.SetActive(true);

        button1.onClick.AddListener(delegate { PanelButton(v2l26Helper.item1, this, 10, price1, selected1); });
        button2.onClick.AddListener(delegate { PanelButton(v2l26Helper.item2, this, 5, price2, selected2); });
        button3.onClick.AddListener(delegate { PanelButton(v2l26Helper.item3, this, 10, price3, selected3); });
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
        infoText.text = "Vamos voltar!";
        infoButton.onClick.RemoveAllListeners();
        infoButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L26"); AudioManager.am.PlaySFX(AudioManager.am.button); infoButton.onClick.RemoveAllListeners(); });

        if (bought) StartCoroutine(FlavorManager.fm.SpawnBucksPosition(5, coinSpawnStart, coinSpawnEnd));
        if (selected1.activeSelf) v2l26Helper.item1 = true;
        if (selected2.activeSelf) v2l26Helper.item2 = true;
        if (selected3.activeSelf) v2l26Helper.item3 = true;
    }

    static void PanelButton(bool itemBought, V2L26b instance, int price, GameObject priceObject, GameObject selectedObject)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        instance.goBackButton.gameObject.SetActive(false);

        if (!itemBought)
        {
            if (!selectedObject.activeSelf)
            {
                if (instance.v2l26Helper.coins >= price)
                {
                    instance.v2l26Helper.coins -= price;
                    instance.totalMoneyText.text = instance.v2l26Helper.coins.ToString();

                    priceObject.SetActive(false);
                    selectedObject.SetActive(true);
                }
                else instance.StartCoroutine(instance.NotEnoughMoney());
            }
            else
            {
                instance.v2l26Helper.coins += price;
                instance.totalMoneyText.text = instance.v2l26Helper.coins.ToString();

                priceObject.SetActive(true);
                selectedObject.SetActive(false);
            }

            if (instance.totalPrice != 0) instance.buyButton.gameObject.SetActive(true);
            else
            {
                instance.goBackButton.gameObject.SetActive(true);
                instance.buyButton.gameObject.SetActive(false);
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
        button3.enabled = value;
        buyButton.enabled = value;
        goBackButton.enabled = value;
    }
}