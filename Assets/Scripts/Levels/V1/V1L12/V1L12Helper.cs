using UnityEngine;

public class V1L12Helper : MonoBehaviour
{
    public bool correct;
    public int coins = 3;

    private void Awake() { DontDestroyOnLoad(gameObject); }
}
