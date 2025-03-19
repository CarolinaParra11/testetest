using UnityEngine;
using UnityEngine.UI;

public class RegularButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate 
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
        });
    }
}
