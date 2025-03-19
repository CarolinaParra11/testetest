using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dica : MonoBehaviour
{
  public GameObject video;

  public void PlayHint()
  {
    //video = GameObject.Find("Video Display");
    video.SetActive(true);
  }
}
