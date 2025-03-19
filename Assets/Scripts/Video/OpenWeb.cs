using UnityEngine;

public class OpenWeb : MonoBehaviour
{
    public bool windowsPC;
    public GameObject web;
    public string url;
    public bool v1;
    public bool intro;
    public int level;
    public SampleWebView sampleWeb;

    private void Awake()
    {
        gameObject.SetActive(!APIManager.UserIsB2b);
    }

    private void Start()
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
      if(_url != null)
      {
        url = _url;
      }
      else
      url = null;
    }

    public void OpenWebView()
    {
      if(windowsPC)
      {
        Application.OpenURL(url);
      }

      else
      {
        if(web.active)
        {
          Time.timeScale = 1;
          GameObject webObject = GameObject.Find("WebViewObject");
          Destroy(webObject);
          web.SetActive(false);
        }

        else
        {
          Time.timeScale = 0;
          web.SetActive(true);
          sampleWeb.Url = url;
        }
        print(Time.timeScale);
      }
    }
}
