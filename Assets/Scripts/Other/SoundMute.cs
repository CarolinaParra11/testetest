using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundMute : MonoBehaviour
{
    public Image cancel;

    void Start()
    {
        if (AudioManager.am.musicChannel.mute == false)
            cancel.enabled = false;
        else
            cancel.enabled = true;
    }

    public void Mute()
    {
        if (!cancel.enabled)
        {
            AudioManager.am.ToggleMuteChannels(true);
            cancel.enabled = true;
        }
        else
        {
            AudioManager.am.ToggleMuteChannels(false);
            cancel.enabled = false;
        }
    }
}
