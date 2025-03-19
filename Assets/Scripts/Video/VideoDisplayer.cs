using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoDisplayer : MonoBehaviour
{
    public GameObject videoDisplay;
    private VideoPlayer videoPlayer;
    public bool valorV2 = false;
    private int currentFrame;
    //private Image bgColor;
    bool oPaiTaOn = false;

    // Start is called before the first frame update
    void Start()
    {
      videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
      //bgColor = GameObject.Find("Video BG").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
      if(videoPlayer.isPlaying)
      {
        oPaiTaOn = true;
        if(valorV2 && videoPlayer.frame >= 415)
        {
          StopPlaying();
        }
      }

      if(oPaiTaOn)
      {
        if(!videoPlayer.isPlaying)
        {
            StopPlaying();
        }
      }
    }

    public void StopPlaying()
    {
      oPaiTaOn = false;
      videoDisplay.SetActive(false);
    }
}
