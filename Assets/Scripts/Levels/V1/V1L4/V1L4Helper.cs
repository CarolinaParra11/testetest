using UnityEngine;

public class V1L4Helper : MonoBehaviour
{
    public bool rightChoice;
    public int coins = 6;

    private void Awake() { DontDestroyOnLoad(gameObject); }
}
