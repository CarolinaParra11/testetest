using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V1L9 : MonoBehaviour
{
    public TextMeshProUGUI boardText;
    public TextMeshProUGUI coins;
    public Button boardButton;

    public GameObject giftPanel;
    public Button giftButton;
    public GameObject blue;

    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;

    private V1L9Helper v1l9Helper;

    private void Start()
    {
        v1l9Helper = GameObject.Find("V1L9Helper").GetComponent<V1L9Helper>();
        coins.text = v1l9Helper.coins.ToString();

        boardButton.onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            boardButton.onClick.RemoveAllListeners();
        });

        if (v1l9Helper.level == 1)
        {
            AudioManager.am.PlaySFX(AudioManager.am.end);
            FlavorManager.fm.BigFirework();
            FlavorManager.fm.ShowHidePanel(giftPanel, true);
            blue.SetActive(true);

            giftButton.onClick.AddListener(delegate
            {
                FlavorManager.fm.ShowHidePanel(giftPanel, false);
                FlavorManager.fm.ShowHidePanel(infoPanel, true);

                AudioManager.am.PlayVoice(AudioManager.am.v1start[8]);
                infoText.text = "Você ganhou 7 moedas!\n\nGaste da melhor maneira na doceria, no supermercado e na padaria.";
                infoButton.onClick.AddListener(delegate { End(); });

                AudioManager.am.PlaySFX(AudioManager.am.button);
                giftButton.onClick.RemoveAllListeners();
            });
            
        }
        if (v1l9Helper.level == 2)
        {
            boardText.text = "Supermercado do Zé";
            boardButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V1L9b"); });
        }
        if (v1l9Helper.level == 3)
        {
            boardText.text = "Padaria Marcos";
            boardButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V1L9c"); });
        }
    }

    private void End()
    {
        AudioManager.am.voiceChannel.Stop();
        FlavorManager.fm.ShowHidePanel(infoPanel, false);
        
        boardText.text = "Doceria da Lu";
        boardButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V1L9a"); });
    }
}