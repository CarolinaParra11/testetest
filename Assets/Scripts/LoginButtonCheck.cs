using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginButtonCheck : MonoBehaviour
{
    public TMP_InputField i_login;
    public TMP_InputField i_senha;

    public Button checkButton;
    public Button tryAgainButton;
    public Button backToMenuButton;
    public Button backBtn;

    public GameObject shadow;
    public TextMeshProUGUI mensagem;
    private Animator shadowAnimator;

    private void Start()
    {
        shadowAnimator = shadow.GetComponent<Animator>();

        AddListenersToButtons();

    }

    void AddListenersToButtons()
    {
        checkButton.onClick.AddListener(delegate { GetInfo(); AudioManager.am.PlaySFX(AudioManager.am.button); checkButton.onClick.RemoveAllListeners(); });
        tryAgainButton.onClick.AddListener(delegate { CloseDialog(); AudioManager.am.PlaySFX(AudioManager.am.button); tryAgainButton.onClick.RemoveAllListeners(); });
        backToMenuButton.onClick.AddListener(delegate { GoToMenu(); AudioManager.am.PlaySFX(AudioManager.am.button); backToMenuButton.onClick.RemoveAllListeners(); });
        backBtn.onClick.AddListener(delegate { GoToMenu(); AudioManager.am.PlaySFX(AudioManager.am.button); backBtn.onClick.RemoveAllListeners(); });
    }

    public void GetInfo()
    {
        string versao = "V1";

        if (PlayerManager.pm.vA1)
            versao = "VA1";
        else if (PlayerManager.pm.vA2)
            versao = "VA2";
        else if (PlayerManager.pm.v2)
            versao = "V2";

        StartCoroutine(APIManager.am.PostCredentials(i_login.text, i_senha.text, SystemInfo.deviceUniqueIdentifier, versao, DialogFailLog, DialogTrueLog));
    }

    void DialogFailLog()
    {
        OpenDialog("Aluno não cadastrado ou senha incorreta, tente novamente!", false);
    }

    void DialogTrueLog()
    {
        APIManager.am.LoadPlayerData();
        OpenDialog("Login feito com sucesso!", true);
    }

    private void OpenDialog(string message, bool success)
    {
        shadow.SetActive(true);
        shadowAnimator.SetTrigger("openPanel");
        mensagem.text = message;

        if (success)
        {
            backToMenuButton.gameObject.SetActive(true);
            tryAgainButton.gameObject.SetActive(false);
        }
        else
        {
            backToMenuButton.gameObject.SetActive(false);
            tryAgainButton.gameObject.SetActive(true);
        }

    }

    public void GoToMenu()
    {
        GameManager.gm.LoadScene("Menu");
    }

    public void CloseDialog()
    {
        shadowAnimator.SetTrigger("PanelOff");
        StartCoroutine(Delay(0.4f));

    }

    IEnumerator Delay(float sec)
    {
        yield return new WaitForSeconds(sec);
        shadow.SetActive(false);
        AddListenersToButtons();
    }

}
