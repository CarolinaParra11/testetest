using UnityEngine;

public class OPENUP : MonoBehaviour
{
    public string url;

    public void OpenURL()
    {
      Application.OpenURL(url);
    }
}
