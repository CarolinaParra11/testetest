using UnityEngine;
using DG.Tweening;
using System.Collections;

public class AdultWalker : MonoBehaviour
{
    public Transform[] avatar;
    public RectTransform[] waypoint;
    public string walktrigger;
    public float duration;
    public float delayTime;
    public bool delay;
    public bool startAtStart = false;
    public bool placeItem = false;

    private void Start()
    {
        if (startAtStart) StartCoroutine(FollowPath());
    }


    public IEnumerator FollowPath()
    {
        for(int i = 0; i < avatar.Length; i++)
        {
            Animator animAvatar;
            animAvatar = avatar[i].GetComponent<Animator>();
            animAvatar.SetBool(walktrigger, true);

            avatar[i].DOMove(waypoint[i].position, duration).OnComplete(() =>
            {
                animAvatar.SetBool(walktrigger, false);
             if(placeItem == true) animAvatar.SetTrigger("placeItem");
            }); ;

            if (delay) yield return new WaitForSeconds(delayTime);            
        }
    }

}
