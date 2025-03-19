using MyBox;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class ProfessionScript : MonoBehaviour
{
    public AvatarType avatarType;
    private string jobString;

#if UNITY_EDITOR
    [ButtonMethod] private void ChangeJob()
    {
        ChangeJobSprites();
    }
#endif

    private void Start()
    {
        if (PlayerManager.pm != null)
        {
            avatarType = PlayerManager.pm.type;
            ChangeJobSprites();
        }
    }

    void ChangeJobSprites()
    {
        if (avatarType == AvatarType.Kid01) jobString = "kid01";
        if (avatarType == AvatarType.Kid02) jobString = "kid02";
        if (avatarType == AvatarType.Kid03) jobString = "kid03";
        if (avatarType == AvatarType.Kid04) jobString = "kid04";

        foreach (Transform child in transform)
        {
            if(child.tag == "JobsSprite")
            {
                string label = jobString;
                string partName = child.name;
                child.GetComponent<SpriteResolver>().SetCategoryAndLabel(partName, label);
            }
        }
    }
}