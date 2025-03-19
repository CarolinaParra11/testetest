using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestroy");
        if (objs.Length > 5) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        gm = this;
    }

    public void LoadScene(string name)
    {
        StartCoroutine(FlavorManager.fm.LoadTransition(name));
    }

}