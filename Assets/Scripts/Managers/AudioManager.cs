using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager am;

    [Header("Channels")]
    public AudioSource musicChannel;
    public AudioSource sfxChannel;
    public AudioSource voiceChannel;
    public AudioSource coinChannel;
    public AudioSource transitionChannel;

    [Header("Music")]
    public AudioClip mainMenu;
    public AudioClip[] musics;

    [Header("SFX")]
    public AudioClip button;
    public AudioClip drag;
    public AudioClip drop;
    public AudioClip coin;
    public AudioClip transitionIn;
    public AudioClip transitionOut;
    public AudioClip end;

    [Header("Voice")]
    public AudioClip[] v1start;
    public AudioClip[] v2start;
    public AudioClip[] v1l1, v1l2, v1l3, v1l4, v1l5, v1l6, v1l7, v1l8, v1l9, v1l10, v1l11, v1l12, v1l13, v1l14;
    public AudioClip[] v1l15, v1l16, v1l17, v1l18, v1l19, v1l20, v1l21, v1l22, v1l23, v1l24, v1l25, v1l26, v1l27;
    public AudioClip[] ending;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestroy");
        if (objs.Length > 5) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        am = this;
    }

    public void PlayMusic(AudioClip clip)
    {
        musicChannel.clip = clip;
        musicChannel.Play();
    }

    public void PlayRandomMusic()
    {
        int randomNumber = Random.Range(0, musics.Length);

        if (musicChannel.clip != musics[randomNumber])
        {
            musicChannel.clip = musics[randomNumber];
            musicChannel.Play();
        }
        else PlayRandomMusic();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxChannel.clip = clip;
        sfxChannel.Play();
    }

    public void PlayVoice(AudioClip clip)
    {
        voiceChannel.clip = clip;
        voiceChannel.Play();
    }
    
    public void PlayCoin(AudioClip clip)
    {
        coinChannel.clip = clip;
        coinChannel.Play();
    }
    
    public void PlayTransition(AudioClip clip)
    {
        transitionChannel.clip = clip;
        transitionChannel.Play();
    }

    public void ToggleMuteChannel(AudioSource channel)
    {
        channel.mute = (channel.mute) ? false : true;
    }

    public void ToggleMuteChannels(bool value)
    {
        musicChannel.mute = value;
        sfxChannel.mute = value;
        voiceChannel.mute = value;
        coinChannel.mute = value;
        transitionChannel.mute = value;
    }
}