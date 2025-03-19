using UnityEngine;
using UnityEngine.UI;

public class ButtonUnassigner : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate 
        { 
            GetComponent<Button>().onClick.RemoveAllListeners(); 
        });
    }
}
