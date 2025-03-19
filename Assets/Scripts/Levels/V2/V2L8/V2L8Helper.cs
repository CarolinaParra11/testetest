using UnityEngine;

public class V2L8Helper : MonoBehaviour
{
    public int level = 1;
    public int coins = 25;
    public bool firstTime;
    private void Awake() { DontDestroyOnLoad(gameObject); firstTime = true; }
}