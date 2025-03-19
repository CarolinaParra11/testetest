using UnityEngine;

public class V1L9Helper : MonoBehaviour
{
    public int level = 1;
    public int coins = 7;

    private void Awake() { DontDestroyOnLoad(gameObject); }
}
