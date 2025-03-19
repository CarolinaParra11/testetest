using UnityEngine;
using UnityEngine.UI;

public class ButtonsUnassigner : MonoBehaviour
{
    public Button[] buttons;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            foreach (Button button in buttons) button?.onClick.RemoveAllListeners();
        });
    }
}
