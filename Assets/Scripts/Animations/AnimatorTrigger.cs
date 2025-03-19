using UnityEngine;

public class AnimatorTrigger : MonoBehaviour
{
    public string triggerWord;
    public Animator[] animators;

    public void Trigger()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger(triggerWord);
        }
    }
}