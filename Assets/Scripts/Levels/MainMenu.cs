using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject logov1;
    public GameObject logov2;

    public Button playButton;
    public Button quitButton;

    void Start()
    {

        if(PlayerManager.pm.v2 || PlayerManager.pm.vA2)
        {
            logov2.SetActive(true);
            logov1.SetActive(false);
        }
        else
        {
            logov1.SetActive(true);
            logov2.SetActive(false);
        }

        playButton.onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            GameManager.gm.LoadScene("Login");
            playButton.onClick.RemoveAllListeners();
        });
        quitButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            Application.Quit();
            quitButton.onClick.RemoveAllListeners();
        });
    }
}
