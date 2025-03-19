using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public Button loginButton;
    public Button logoutButton;
    public Button resetButton;
    public Button playBtn;
    public Button bolodixBtn;
    public Button exitBtn;
    public Button dialogBtn;
    public Button yesBtn;
    public Button noBtn;
    public Button yesResetBtn;
    public Button noResetBtn;


     public Image avatar1;
      public Image avatar2;
       public Image avatar3;
     public Image avatar4;


    public TextMeshProUGUI nameStudent;
    public TextMeshProUGUI classNumber;
    public TextMeshProUGUI idadeNumber;

    public TextMeshProUGUI fase;
    public TextMeshProUGUI dinheiro;

    private AvatarType avatarType;

    public GameObject dialog;
    public GameObject dialog2;
    public GameObject dialogReset;

    private void Start()
    {

        TurnOffGameObjects(dialog);
        TurnOffGameObjects(loginButton.gameObject);
        TurnOffGameObjects(logoutButton.gameObject);
           TurnOffGameObjects(avatar1.gameObject);
           TurnOffGameObjects(avatar2.gameObject);
         TurnOffGameObjects(avatar3.gameObject);
         TurnOffGameObjects(avatar4.gameObject);

        AddListenersToButtons();

        StudentDataToText();
        GetPlayerAvatarChoice();

        CheckCredentialsButton();

        PlayerManager.pm.bloqueado = true;

        if (!APIManager.am.userLogged || APIManager.UserIsB2b)
            resetButton.gameObject.SetActive(false);

        if (PlayerManager.pm.bloqueado)
            playBtn.gameObject.SetActive(false);

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
        if (!playBtn.IsActive())
        {
            Debug.Log("Calling Load API...");
            APIManager.am.LoadPlayerData();
            Debug.Log("Player blocked: " + PlayerManager.pm.bloqueado);
            if (!PlayerManager.pm.bloqueado) playBtn.gameObject.SetActive(true);
            if (APIManager.am.userLogged && !APIManager.UserIsB2b) resetButton.gameObject.SetActive(true);
        }
    }

    void AddListenersToButtons()
    {
        playBtn.onClick.AddListener(delegate { Play();   AudioManager.am.PlaySFX(AudioManager.am.button); playBtn.onClick.RemoveAllListeners(); });
        bolodixBtn.onClick.AddListener(delegate { GoToBolodix();   AudioManager.am.PlaySFX(AudioManager.am.button); bolodixBtn.onClick.RemoveAllListeners(); });
        exitBtn.onClick.AddListener(delegate { ExitGame();   AudioManager.am.PlaySFX(AudioManager.am.button); exitBtn.onClick.RemoveAllListeners(); });
        loginButton.onClick.AddListener(delegate { Login();   AudioManager.am.PlaySFX(AudioManager.am.button); loginButton.onClick.RemoveAllListeners(); });
        logoutButton.onClick.AddListener(delegate { LogOut();    AudioManager.am.PlaySFX(AudioManager.am.button);logoutButton.onClick.RemoveAllListeners(); });

        dialogBtn.onClick.AddListener(delegate { CloseDialog(dialog);   AudioManager.am.PlaySFX(AudioManager.am.button); dialogBtn.onClick.RemoveAllListeners(); });
        yesBtn.onClick.AddListener(delegate { LogOutConfirmed();   AudioManager.am.PlaySFX(AudioManager.am.button); yesBtn.onClick.RemoveAllListeners();  });
        noBtn.onClick.AddListener(delegate{ CloseDialog(dialog2);   AudioManager.am.PlaySFX(AudioManager.am.button); noBtn.onClick.RemoveAllListeners();  });

        resetButton.onClick.AddListener(delegate{ ResetPlayer(); AudioManager.am.PlaySFX(AudioManager.am.button); resetButton.onClick.RemoveAllListeners();  });
        yesResetBtn.onClick.AddListener(delegate { ConfirmResetPlayer();   AudioManager.am.PlaySFX(AudioManager.am.button); yesResetBtn.onClick.RemoveAllListeners();  });
        noResetBtn.onClick.AddListener(delegate{ CloseDialog(dialogReset);   AudioManager.am.PlaySFX(AudioManager.am.button); noResetBtn.onClick.RemoveAllListeners();  });
    }

    void CheckCredentialsButton()
    {
        if (!APIManager.am.userLogged)
        {

            loginButton.gameObject.SetActive(true);
            logoutButton.gameObject.SetActive(false);

        }
        else if (APIManager.am.userLogged)
        {
            loginButton.gameObject.SetActive(false);
            logoutButton.gameObject.SetActive(true);
        }
    }

    void GetPlayerAvatarChoice()
    {
        avatarType = PlayerManager.pm.type;

            if (!APIManager.am.userLogged)
            {
             return;   
            }
            else
            {
            if(avatarType == AvatarType.Kid01) avatar1.gameObject.SetActive(true);
            else if (avatarType == AvatarType.Kid02) avatar2.gameObject.SetActive(true);
            else if (avatarType == AvatarType.Kid03) avatar3.gameObject.SetActive(true);
            else if (avatarType == AvatarType.Kid04) avatar4.gameObject.SetActive(true);
            else if (avatarType == AvatarType.lucas) { avatar1.gameObject.SetActive(false); avatar2.gameObject.SetActive(false); avatar3.gameObject.SetActive(false); avatar4.gameObject.SetActive(false); }
            }

    }

    void StudentDataToText()
    {
        nameStudent.text = "Nome: " + (APIManager.am.userLogged ? PlayerManager.pm.nome.ToString() : "");
        idadeNumber.text = APIManager.am.user_idade.ToString();

        fase.text = (APIManager.am.userLogged ? PlayerManager.pm.level.ToString() : "");
        dinheiro.text = (APIManager.am.userLogged ? PlayerManager.pm.coins.ToString() : "");

    
    }

    private void TurnOffGameObjects(GameObject buttonObject)
    {
        if (buttonObject.activeInHierarchy)
            buttonObject.SetActive(false);
    }

    public void ResetPlayer()
    {
        OpenDialog(dialogReset, "Você tem certeza que quer redefinir seu progresso e começar do início?");
    }

    public void ConfirmResetPlayer()
    {
        APIManager.am.ResetPlayer();
        StartCoroutine(DelayOpenDialogReset());
        CloseDialog(dialogReset);
    }

    public void Play()
    {
        if (!APIManager.am.userLogged)
        {
            OpenDialog(dialog, "Você precisa entrar em sua conta para acessar esta opção!");
        }
        else if (APIManager.am.userLogged)
        {
            if (PlayerManager.pm.v2 || (!PlayerManager.pm.v2 && !PlayerManager.pm.vA1 && !PlayerManager.pm.vA2))
            {
                if (PlayerManager.pm.level > 0 && PlayerManager.pm.level < 28)
                {
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    if (!PlayerManager.pm.v2) GameManager.gm.LoadScene("V1L" + PlayerManager.pm.GetLevel());
                    else GameManager.gm.LoadScene("V2L" + PlayerManager.pm.GetLevel());
                }
                else if (!PlayerManager.pm.ended && PlayerManager.pm.level > 27)
                {
                    PlayerManager.pm.ended = true;
                    if (!PlayerManager.pm.v2) AudioManager.am.PlayVoice(AudioManager.am.ending[4]);
                }
                else if (PlayerManager.pm.level == 0)
                {
                    if(!PlayerManager.pm.v2) GameManager.gm.LoadScene("AvatarSelection");
                    else GameManager.gm.LoadScene("AvatarSelection");
                }
            }
            else
            {
                if (PlayerManager.pm.level > 0 && PlayerManager.pm.level < 10)
                {
                    AudioManager.am.PlaySFX(AudioManager.am.button);
                    if (PlayerManager.pm.vA2 && !PlayerManager.pm.vA1) GameManager.gm.LoadScene("A2L" + PlayerManager.pm.GetLevel());
                    else GameManager.gm.LoadScene("A1L" + PlayerManager.pm.GetLevel());
                }
                else if (!PlayerManager.pm.ended && PlayerManager.pm.level > 9)
                {
                    PlayerManager.pm.ended = true;
                    if (!PlayerManager.pm.v2) AudioManager.am.PlayVoice(AudioManager.am.ending[4]);
                }
                else if (PlayerManager.pm.level == 0)
                {
                    if (!PlayerManager.pm.v2) GameManager.gm.LoadScene("AvatarSelection");
                    else GameManager.gm.LoadScene("AvatarSelection");
                }
            }
        }
    }

    public void GoToBolodix()
    {
        if (!APIManager.am.userLogged)
        {
            OpenDialog(dialog, "Você precisa entrar em sua conta para acessar esta opção!");
        }
        else if (APIManager.am.userLogged)
        {

            AudioManager.am.PlaySFX(AudioManager.am.button);
            GameManager.gm.LoadScene("Bolodim");
        }
    }

    public void ExitGame()
    {

        AudioManager.am.PlaySFX(AudioManager.am.button);

        // Save stuff here

        Application.Quit();
    }

    public void Login()
    {

        AudioManager.am.PlaySFX(AudioManager.am.button);
        GameManager.gm.LoadScene("Login");
    }

    public void LogOut()
    {
        if(!APIManager.am.userLogged)
        {
            OpenDialog(dialog, "Você precisa entrar em sua conta para acessar esta opção!");
        }
        else{
        OpenDialog(dialog2, "Você tem certeza que deseja sair de sua conta?");

        }
    }

    public void LogOutConfirmed()
    {
        CloseDialog(dialog2);
        dialogBtn.onClick.RemoveAllListeners();

        StartCoroutine(DelayOpenDialog());

    }

    IEnumerator DelayOpenDialog()
    {
        yield return new WaitForSeconds(0.5f);
        APIManager.am.Logout();
        
        OpenDialog(dialog, "Você saiu de sua conta com sucesso!");
        dialogBtn.onClick.AddListener(delegate {CloseDialog(dialog); StartCoroutine(ReloadScene());  AudioManager.am.PlaySFX(AudioManager.am.button); dialogBtn.onClick.RemoveAllListeners();});
    }

    IEnumerator DelayOpenDialogReset()
    {
        yield return new WaitForSeconds(0.5f);
        //APIManager.am.Logout();

        OpenDialog(dialog, "Você redefiniu seu progresso!");
        dialogBtn.onClick.AddListener(delegate {  StartCoroutine(ReloadScene()); AudioManager.am.PlaySFX(AudioManager.am.button); dialogBtn.onClick.RemoveAllListeners(); });
    }

    public void OpenDialog(GameObject dialog, string message)
    {

        dialog.SetActive(true);
        dialog.GetComponent<Animator>().SetTrigger("openPanel");
        
        TextMeshProUGUI dialogMessage = GameObject.FindGameObjectWithTag("message").GetComponent<TextMeshProUGUI>();
        dialogMessage.text = message;
    }

    public void CloseDialog(GameObject dialog)
    {
        dialog.GetComponent<Animator>().SetTrigger("PanelOff");
        StartCoroutine(Delay(dialog, 0.4f));
    }

    IEnumerator Delay(GameObject dialog, float sec)
    {
        yield return new WaitForSeconds(sec);
        dialog.SetActive(false);
        AddListenersToButtons();
    }

    IEnumerator ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
     
        yield return new WaitForSeconds(0.3f);
        GameManager.gm.LoadScene(scene.name);
    }


}
