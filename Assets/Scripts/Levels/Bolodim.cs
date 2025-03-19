using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bolodim : MonoBehaviour
{
    public float delay = 0.5f;
    public float maxCoinsV1 = 200;
    public float maxCoinsV2 = 2000;
    public Slider slider;
    public float durationV1 = 0.5f;
    public float durationV2 = 1.5f;

    private float percentage;
    private float lerp = 0;

    private int startCoins;
    private int endCoins;
    private int score;

    public GameObject bucksIcon, coinsIcon;

    public RectTransform puffPos;
    public Animator bolodixAnimator, scoreAnimator;
    public TextMeshProUGUI coins;
    public Button button, invButton, mainMenuButton;
    public bool lockButton;

    void Start()
    {
        startCoins = PlayerManager.pm.GetStartingCoins();
        endCoins = PlayerManager.pm.GetCoins();
        score = startCoins;
        button.gameObject.SetActive(false);
        invButton.enabled = false;

        coins.text = startCoins.ToString();

        if (PlayerManager.pm.v2 || PlayerManager.pm.vA2) bucksIcon.SetActive(true);
        else coinsIcon.SetActive(true);
        StartCoroutine(ScoreCounter());

        Invoke("RefreshPlayerData", 1f);
        InvokeRepeating("DisplayNextStageButton", 5f, 5f);
    }

    void RefreshPlayerData()
    {
        APIManager.am.LoadPlayerData();
    }

    void DisplayNextStageButton()
    {
        Debug.Log("Starting DisplayNextStageButton...");
        if (!button.IsActive())
        {
            Debug.Log("Calling Load API...");
            APIManager.am.LoadPlayerData();
            Debug.Log("Player blocked: " + PlayerManager.pm.bloqueado);
            if (!PlayerManager.pm.bloqueado) button.gameObject.SetActive(true);
        }
    }

    public IEnumerator ScoreCounter()
    {
        yield return new WaitForSeconds(delay);

        if (endCoins > startCoins)
        {
            FlavorManager.fm.Fireworks(coins.gameObject.GetComponent<RectTransform>());
            FlavorManager.fm.PuffRect(puffPos);
        }

        while (score < endCoins)
        {
            if (!PlayerManager.pm.v2 || !PlayerManager.pm.vA2)
            {
                lerp += Time.deltaTime / durationV1;
                score = (int)Mathf.Lerp(score, endCoins, lerp);
            }
            else
            {
                lerp += Time.deltaTime / durationV2;
                score = (int)Mathf.Lerp(score, endCoins, lerp);
            }

            scoreAnimator.SetBool("scoreUpdating", true);
            coins.text = score.ToString();

            bolodixAnimator.SetBool("isGrowing", true);

            if (PlayerManager.pm.v2 || PlayerManager.pm.vA2) percentage = score / maxCoinsV2;
            else percentage = score / maxCoinsV1;

            slider.value = percentage;
            yield return null;
        }

        bolodixAnimator.SetBool("isGrowing", false);
        scoreAnimator.SetBool("scoreUpdating", false);
        UnlockButton();
    }

    void UnlockButton()
    {
        if (PlayerManager.pm.v2 || (!PlayerManager.pm.v2 && !PlayerManager.pm.vA1 && !PlayerManager.pm.vA2))
        {
            if (PlayerManager.pm.level > 0 && PlayerManager.pm.level < 28)
            {
                button.gameObject.SetActive(true);
                button.onClick.AddListener(delegate
                {
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    if (!PlayerManager.pm.v2) GameManager.gm.LoadScene("V1L" + PlayerManager.pm.GetLevel());
                    else GameManager.gm.LoadScene("V2L" + PlayerManager.pm.GetLevel());
                    button.onClick.RemoveAllListeners();
                });
            }
            else if (!PlayerManager.pm.ended && PlayerManager.pm.level > 27)
            {
                PlayerManager.pm.ended = true;
                if (!PlayerManager.pm.v2) AudioManager.am.PlayVoice(AudioManager.am.ending[4]);
            }
        }
        else
        {
            if (PlayerManager.pm.level > 0 && PlayerManager.pm.level < 10)
            {
                button.gameObject.SetActive(true);
                button.onClick.AddListener(delegate
                {
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    if (PlayerManager.pm.vA2 && !PlayerManager.pm.vA1) GameManager.gm.LoadScene("A2L" + PlayerManager.pm.GetLevel());
                    else GameManager.gm.LoadScene("A1L" + PlayerManager.pm.GetLevel());
                });
            }
            else if (!PlayerManager.pm.ended && PlayerManager.pm.level > 9)
            {
                PlayerManager.pm.ended = true;
                if (!PlayerManager.pm.v2 || !PlayerManager.pm.vA2) AudioManager.am.PlayVoice(AudioManager.am.ending[4]);
            }
        }


        invButton.enabled = true;
        invButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            GameManager.gm.LoadScene("Inventario");
            invButton.onClick.RemoveAllListeners();
        });

        mainMenuButton.onClick.AddListener(delegate
        {
            AudioManager.am.PlaySFX(AudioManager.am.button);
            GameManager.gm.LoadScene("Menu");
            mainMenuButton.onClick.RemoveAllListeners();
        });
    }
}