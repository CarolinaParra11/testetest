using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public enum AdultType
{
    Man01,
    Man02,
    Man03,
    OldMan01,
    OldMan02,
    Woman01,
    Woman02,
    Woman03,
    OldWoman01,
    OldWoman02,
}

public class AdultSpriteChanger : MonoBehaviour
{
    public AdultType adultType;
    public List<GameObject> childrenList;
    public string adultString;

    [ButtonMethod] private void ChangeSprite()
    {
        ChangeAdultSprite();
    }

    private void Start()
    {
        ChangeAdultSprite();
    }

    void ChangeAdultSprite()
    {
        childrenList.Clear();

        if (adultType == AdultType.Man01) adultString = "man1";
        if (adultType == AdultType.Man02) adultString = "man2";
        if (adultType == AdultType.Man03) adultString = "man3";
        if (adultType == AdultType.OldMan01) adultString = "oldman1";
        if (adultType == AdultType.OldMan02) adultString = "oldman2";
        if (adultType == AdultType.Woman01) adultString = "woman1";
        if (adultType == AdultType.Woman02) adultString = "woman2";
        if (adultType == AdultType.Woman03) adultString = "woman3";
        if (adultType == AdultType.OldWoman01) adultString = "oldwoman1";
        if (adultType == AdultType.OldWoman02) adultString = "oldwoman2";


        foreach (Transform child in transform)
        {

            if (child.tag == "AdultSprite")
            {
                //Debug Purposes
                childrenList.Add(child.gameObject);


                string label = adultString;
                string partName = child.name;
                // Change sprite
                child.GetComponent<SpriteResolver>().SetCategoryAndLabel(partName, label);
            }
        }
    }

    
}
