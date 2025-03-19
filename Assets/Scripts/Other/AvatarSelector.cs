using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public enum AvatarType { Kid01, Kid02, Kid03, Kid04, lucas, laura }

public enum FaceType { Kid01, Kid02, Kid03, Kid04, lucas, laura, ill }

public class AvatarSelector : MonoBehaviour
{
    private AvatarType avatarPlayer = AvatarType.Kid01;
    private string avatarString;

    private void Start()
    {
        avatarPlayer = PlayerManager.pm.type;
        if (avatarPlayer == AvatarType.Kid01) avatarString = "kid_01";
        else if (avatarPlayer == AvatarType.Kid02) avatarString = "kid_02";
        else if (avatarPlayer == AvatarType.Kid03) avatarString = "kid_03";
        else if (avatarPlayer == AvatarType.Kid04) avatarString = "kid_04";
        else if (avatarPlayer == AvatarType.lucas) avatarString = "lucas";
        else if (avatarPlayer == AvatarType.laura) avatarString = "laura";

        foreach (Transform child in transform)
            if (child.tag == "AvatarSprite")
                child.GetComponent<SpriteResolver>().SetCategoryAndLabel(child.name, avatarString);
    }
}