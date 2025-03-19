using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class NPCSelector : MonoBehaviour
{
    public string avatarString;

    private void Start()
    {
        foreach (Transform child in transform)
            if (child.tag == "AvatarSprite")
                child.GetComponent<SpriteResolver>().SetCategoryAndLabel(child.name, avatarString);
    }
}