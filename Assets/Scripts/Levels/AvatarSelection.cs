using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AvatarSelection : MonoBehaviour
{
    public Button button1, button2, button3, button4;
    public Animator animator1, animator2, animator3, animator4;

    private void Start()
    {
        button1.onClick.AddListener(delegate { Part1(animator1, AvatarType.Kid01); });
        button2.onClick.AddListener(delegate { Part1(animator2, AvatarType.Kid02); });
        button3.onClick.AddListener(delegate { Part1(animator3, AvatarType.Kid03); });
        button4.onClick.AddListener(delegate { Part1(animator4, AvatarType.Kid04); });
    }

    public void Part1(Animator anim, AvatarType type)
    {
        AudioManager.am.PlaySFX(AudioManager.am.button);
        
        anim.SetTrigger("Hi");

        PlayerManager.pm.type = type;
        PlayerManager.pm.AddLevel();

        StartCoroutine(End());
    }

    IEnumerator End()
    {
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
        button3.onClick.RemoveAllListeners();
        button4.onClick.RemoveAllListeners();

        yield return new WaitForSeconds(2f);
        GameManager.gm.LoadScene("Bolodim");
    }
}