
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using MyBox;
using UnityEngine;

public class TweenCharacters : MonoBehaviour
{
    public RectTransform obj;
    public RectTransform target;

    public Transform avatar;
    public RectTransform[] pathWaypoints;
    public float duration;

    public Ease easeType = Ease.InOutBack;
    public PathType pathType = PathType.CatmullRom;
    public PathMode pathMode = PathMode.Sidescroller2D;

    [ButtonMethod] void Go() { GoPath(); }


    public void Animate() 
    {
        obj.DOMove(target.position, 1).SetEase(easeType);
    }

    public void GoPath()
    {
        Animator animAvatar;
        if(avatar != null)
        {
        animAvatar = avatar.GetComponent<Animator>();
        animAvatar.SetBool("isWalking", true);


        Vector3[] path = new Vector3[pathWaypoints.Length];
        for(int i = 0; i < pathWaypoints.Length; i++)
        {
            path[i] = pathWaypoints[i].position;
        }

        avatar.DOPath(path, duration, pathType, pathMode).OnComplete(() =>
        {
            animAvatar.SetBool("isWalking", false);
        }); ;


        }
    }

}