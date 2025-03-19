using UnityEngine;

public class V2L26Helper : MonoBehaviour
{
    public static V2L26Helper v2l26h;
    public int coins = 100;
    public bool visited, item1, item2, item3, deposit;

    private void Awake()
    {
        if (v2l26h == null)
        {
            v2l26h = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}