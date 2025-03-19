using UnityEngine;

public class V2L4Helper : MonoBehaviour
{
    public int choice;

    private void Awake() { DontDestroyOnLoad(gameObject); }
}
