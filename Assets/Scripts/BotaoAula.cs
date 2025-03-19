using UnityEngine;

public class BotaoAula : MonoBehaviour
{
    public string url;
    public bool v1;
    public bool intro;
    public int level;

    private void Awake()
    {
        gameObject.SetActive(!APIManager.UserIsB2b);
    }

    void Start()
    {
        gameObject.SetActive(!APIManager.UserIsB2b);
        GetLink();
    }

    void GetLink()
    {
        StartCoroutine(APIManager.am.AuthOpenAula(v1, level, intro, URLOnComplete));
    }

    public void URLOnComplete(string _url)
    {
        if (_url != null)
        {
            url = _url;
        }
        else
            url = null;
    }
    public void OpenAulaWebSite()
    {
        Application.OpenURL(url);
    }
}
