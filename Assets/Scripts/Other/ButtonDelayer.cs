using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDelayer : MonoBehaviour
{
    private Button TheButton;
    public float ButtonReactivateDelay = 0.2f;

    private void Start()
    {
        TheButton = GetComponent<Button>();
        TheButton.interactable = true;
    }

    // Assign this as your OnClick listener from the inspector
    public void WhenClicked()
    {
        TheButton.interactable = false;
        StartCoroutine(EnableButtonAfterDelay(TheButton, ButtonReactivateDelay));

        // Do whatever else your button is supposed to do.
    }

    IEnumerator EnableButtonAfterDelay(Button button, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        button.interactable = true;
    }

}
