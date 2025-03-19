using UnityEngine;
using UnityEngine.UI;

public class ResultButton : MonoBehaviour
{
    private void Start()
    {
        AudioManager.am.PlaySFX(AudioManager.am.end);
        GetComponent<Button>().onClick.AddListener(delegate 
        {
            AudioManager.am.voiceChannel.Stop();
            GameManager.gm.LoadScene("Bolodim");
        });
    }
}
