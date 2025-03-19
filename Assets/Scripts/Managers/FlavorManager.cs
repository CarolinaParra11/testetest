using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlavorManager : MonoBehaviour
{
    public static FlavorManager fm;

    [Header("Coins")]
    [SerializeField] [Range(1f, 4f)] float minDuration = 3f;
    [SerializeField] [Range(1f, 4f)] float maxDuration = 4f;
    public Ease easeType = Ease.InOutBack;

    [Header("Transitions")]
    public float TransitionTime;
    private Vector3 center;
    public float postTransitionTime = 0.2f;

    [Header("Panel")]
    public float PanelDelay;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestroy");

        if (objs.Length > 5)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        fm = this;


    }

    private void Start()
    {
        center = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);
    }


    public void Update()
    {

        #region TOUCH
        if (Input.GetMouseButtonDown(0))
        {
            GameObject particle = ObjectPooler.SharedInstance.GetPooledObject("TouchFX");
            if (particle != null)
            {
                particle.transform.position = Input.mousePosition;
                particle.SetActive(true);
            }

            GameObject particle2 = ObjectPooler.SharedInstance.GetPooledObject("Touch2FX");
            if (particle2 != null)
            {
                particle2.transform.position = Input.mousePosition;
                particle2.SetActive(true);
            }
        }
        #endregion
    
    }

    #region PUFFs
    public void Puff(Vector3 pos)
    {
        GameObject puff = ObjectPooler.SharedInstance.GetPooledObject("FX_smokepuff");
        if(puff != null)
        {
            puff.transform.position = pos;
            puff.gameObject.SetActive(true);
        }
    }

    public void PuffRect(Transform posi)
    {
        Vector3 vPosition = Camera.main.WorldToScreenPoint(posi.position);

        GameObject puff = ObjectPooler.SharedInstance.GetPooledObject("FX_smokepuff");
        if (puff != null)
        {
            puff.transform.position = vPosition;
            puff.gameObject.SetActive(true);
        }
    }

    public void BigPuff()
    {
        GameObject bPuff = ObjectPooler.SharedInstance.GetPooledObject("FX_smokepuff_big");
        if(bPuff != null)
        {
            bPuff.transform.position = center;
            bPuff.gameObject.SetActive(true);
        }
    }

    public void BigFirework()
    {
        GameObject bPuff = ObjectPooler.SharedInstance.GetPooledObject("FX_bigFireworks");
        if (bPuff != null)
        {
            bPuff.transform.position = center;
            bPuff.gameObject.SetActive(true);
        }
    }


    public void Fireworks(Transform posi)
    {
        Vector3 vPosition = Camera.main.WorldToScreenPoint(posi.position);

        GameObject fireZone = ObjectPooler.SharedInstance.GetPooledObject("FX_fireworks");
        if(fireZone != null)
        {
            fireZone.transform.position = vPosition;
            fireZone.gameObject.SetActive(true);
        }
    }

    #endregion

    #region COINS
    public IEnumerator SpawnCoin(int n)
    {
        Vector3 vStart = center;
        Vector3 vEnd = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Bolodim").transform.position);

        for (int i = 0; i < n; i++)
        {
            GameObject coin = ObjectPooler.SharedInstance.GetPooledObject("Coin");
            coin.transform.position = vStart;
            coin.gameObject.SetActive(true);

            GameObject fw = ObjectPooler.SharedInstance.GetPooledObject("FX_fireworks");
            fw.transform.position = vStart;
            fw.gameObject.SetActive(true);

            float duration = UnityEngine.Random.Range(minDuration, maxDuration);
            coin.transform.DOMove(vEnd, duration).SetEase(easeType).OnComplete(() =>
            {
                GameObject sm = ObjectPooler.SharedInstance.GetPooledObject("FX_smokepuff");
                sm.transform.position = vEnd;
                sm.gameObject.SetActive(true);
                coin.SetActive(false);
            });

            AudioManager.am.PlayCoin(AudioManager.am.coin);
            yield return new WaitForSeconds(0.25f);
        }
    }

    public IEnumerator SpawnCoinPosition(int n, Transform tStart, Transform tEnd)
    {
        Vector3 vStart = Camera.main.WorldToScreenPoint(tStart.position);
        Vector3 vEnd = Camera.main.WorldToScreenPoint(tEnd.position);

        for (int i = 0; i < n; i++)
        {
            GameObject coin = ObjectPooler.SharedInstance.GetPooledObject("Coin");
            coin.transform.position = vStart;
            coin.gameObject.SetActive(true);

            GameObject fw = ObjectPooler.SharedInstance.GetPooledObject("FX_fireworks");
            fw.transform.position = vStart;
            fw.gameObject.SetActive(true);

            float duration = UnityEngine.Random.Range(minDuration, maxDuration);
            coin.transform.DOMove(vEnd, duration).SetEase(easeType).OnComplete(() =>
            {
                GameObject sm = ObjectPooler.SharedInstance.GetPooledObject("FX_smokepuff");
                sm.transform.position = vEnd;
                sm.gameObject.SetActive(true);
                coin.SetActive(false);
            });

            AudioManager.am.PlayCoin(AudioManager.am.coin);

            yield return new WaitForSeconds(0.25f);
        }
    }

    public IEnumerator SpawnBucks(int n)
    {
        Vector3 vStart = center;
        Vector3 vEnd = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Bolodim").transform.position);

        for (int i = 0; i < n; i++)
        {
            GameObject coin = ObjectPooler.SharedInstance.GetPooledObject("Bucks");
            coin.transform.position = vStart;
            coin.gameObject.SetActive(true);

            GameObject fw = ObjectPooler.SharedInstance.GetPooledObject("FX_fireworks");
            fw.transform.position = vStart;
            fw.gameObject.SetActive(true);

            float duration = UnityEngine.Random.Range(minDuration, maxDuration);
            coin.transform.DOMove(vEnd, duration).SetEase(easeType).OnComplete(() =>
            {
                GameObject sm = ObjectPooler.SharedInstance.GetPooledObject("FX_smokepuff");
                sm.transform.position = vEnd;
                sm.gameObject.SetActive(true);
                coin.SetActive(false);
            });

            AudioManager.am.PlayCoin(AudioManager.am.coin);
            yield return new WaitForSeconds(0.25f);
        }
    }

    public IEnumerator SpawnBucksPosition(int n, Transform tStart, Transform tEnd)
    {
        Vector3 vStart = Camera.main.WorldToScreenPoint(tStart.position);
        Vector3 vEnd = Camera.main.WorldToScreenPoint(tEnd.position);

        for (int i = 0; i < n; i++)
        {
            GameObject coin = ObjectPooler.SharedInstance.GetPooledObject("Bucks");
            coin.transform.position = vStart;
            coin.gameObject.SetActive(true);

            GameObject fw = ObjectPooler.SharedInstance.GetPooledObject("FX_fireworks");
            fw.transform.position = vStart;
            fw.gameObject.SetActive(true);

            float duration = UnityEngine.Random.Range(minDuration, maxDuration);
            coin.transform.DOMove(vEnd, duration).SetEase(easeType).OnComplete(() => 
            {
                GameObject sm = ObjectPooler.SharedInstance.GetPooledObject("FX_smokepuff");
                sm.transform.position = vEnd;
                sm.gameObject.SetActive(true);

                coin.SetActive(false); 
            });

            AudioManager.am.PlayCoin(AudioManager.am.coin);

            yield return new WaitForSeconds(0.25f);
        }
    }
    #endregion

    #region PANEL
    public void ShowHidePanel(GameObject panel, bool ShowOrNotShow)
    {
        if (ShowOrNotShow) StartCoroutine(PanelDelayer(panel, true));
        else StartCoroutine(PanelDelayer(panel, false));
    }

    IEnumerator PanelDelayer(GameObject panel, bool state)
    {
        if (state)
        {
            yield return new WaitForSeconds(0.2f);
            panel.SetActive(true);
        }
        else
        {
            panel.GetComponent<Animator>().SetTrigger("PanelOff");
            yield return new WaitForSecondsRealtime(PanelDelay);
            panel.SetActive(false);
        }
    }
    #endregion

    #region TRANSITIONS
    public IEnumerator LoadTransition(string sceneName)
    {
        StartCoroutine(TransitionManager.tm.LoadTransition(sceneName, TransitionTime, postTransitionTime));
        yield return new WaitForSeconds(0.1f);
    }
    #endregion
}
