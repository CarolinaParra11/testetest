using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V2L8 : MonoBehaviour
{
    public TextMeshProUGUI boardText;
    public TextMeshProUGUI coins;
    public Button boardButton;
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public Button infoButton;


    private V2L8Helper v2l8Helper;

    private void Start()
    {
        v2l8Helper = GameObject.Find("V2L8Helper").GetComponent<V2L8Helper>();
        coins.text = v2l8Helper.coins.ToString();

        if(v2l8Helper.firstTime)
        {
            AudioManager.am.PlayVoice(AudioManager.am.v2start[7]);
            FlavorManager.fm.ShowHidePanel(infoPanel, true);
            infoText.text = "Você ganhou 25 reais. Gaste da melhor maneira na doceria, no supermercado e na padaria!";

            infoButton.onClick.AddListener(delegate
            {
                AudioManager.am.voiceChannel.Stop();
                infoButton.onClick.RemoveAllListeners();
                FlavorManager.fm.ShowHidePanel(infoPanel, false);
                v2l8Helper.firstTime = false;
            });

        }

        if (v2l8Helper.level == 1)
        {
            boardText.text = "Doceria da Lu";
            boardButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L8a"); boardButton.onClick.RemoveAllListeners(); });
        }
        if (v2l8Helper.level == 2)
        {
            boardText.text = "Supermercado do Zé";
            boardButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L8b"); boardButton.onClick.RemoveAllListeners(); });
        }
        if (v2l8Helper.level == 3)
        {
            boardText.text = "Padaria Marcos";
            boardButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("V2L8c"); boardButton.onClick.RemoveAllListeners(); });
        }
    }
}