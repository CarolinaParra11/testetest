using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;
using TMPro;
using SimpleHTTP;
using SimpleJSON;

public class APIManager : MonoBehaviour
{
    public static APIManager am;

    public string url = "https://b25-game.azurewebsites.net/";
    public string userToken;

    #region Login
    private PlayerCredentials credentials;
    private Root<JogadorData> root = new Root<JogadorData>();
    private string playerDataInJSON;

    private AulaInfo aulaInfo;
    private Root<AulaData> aulaRoot = new Root<AulaData>();
    private string urlGo;
    bool canGo;

    public bool userLogged;

    private string user_login;
    public string user_idade;
    private bool user_b2b = false;

    public static bool UserIsB2b {
        get {
            if (APIManager.am == null) return false;
            return APIManager.am.user_b2b;
        }
    }

    public string urlAula;
    #endregion

    #region debug
    public TextMeshProUGUI tokenv;
    public TextMeshProUGUI loggedv;
    public TextMeshProUGUI namev;
    public TextMeshProUGUI idadev;
    public TextMeshProUGUI userv;
    public TextMeshProUGUI senhav;
    public TextMeshProUGUI log;
    #endregion

    private void Start()
    {
        // Clear token when app is opened
        userToken = null;
        am = this;
    }

    #region MAIN METHODS // Used at game objects and etc
    public void SavePlayer()
    {
        StartCoroutine(Save());
    }

    public void ResetPlayer()
    {
        StartCoroutine(ResetData(LoadPlayerData));
    }

    public void LoadPlayerData()
    {
        StartCoroutine(LoadContentFromAPI());
    }

    public void Relatorio(Dictionary<string, double> categorias)
    {
        StartCoroutine(PostRelatorio(categorias));
    }

    public void Logout()
    {
        userLogged = false;
        root = new Root<JogadorData>();

        StartCoroutine(Save());

        ResetPlayerData();
    }

    private void AssigningUserInfo(string login, string token, string idade)
    {
        userToken = token;
        user_login = login;
        user_idade = idade;
    }

    private void ResetPlayerData()
    {
        APIManager.am.user_idade = "";
        APIManager.am.user_b2b = false;

        PlayerManager.pm.type = AvatarType.lucas;
        PlayerManager.pm.nome = "";
        PlayerManager.pm.startCoins = 0;
        PlayerManager.pm.coins = 0;
        PlayerManager.pm.level = 0;
        PlayerManager.pm.blue = false;
        PlayerManager.pm.bonus1 = false;
        PlayerManager.pm.bonus2 = false;
        PlayerManager.pm.professionId = 0;
        PlayerManager.pm.english = false;
        PlayerManager.pm.ensurance = false;
        PlayerManager.pm.dentist = false;
        PlayerManager.pm.vault = 0;
        PlayerManager.pm.gift1 = PlayerManager.pm.gift2 = PlayerManager.pm.gift3 = 0;
        PlayerManager.pm.safe1 = PlayerManager.pm.safe2 = PlayerManager.pm.safe3 = 0;
        PlayerManager.pm.v2l20choice = PlayerManager.pm.v2l26choice = 0;
        PlayerManager.pm.v1l16choices = new List<int>();
        PlayerManager.pm.ended = false;
    }

    #endregion

    #region UNITY API WEB REQUEST METHODS
    public IEnumerator PostCredentials(string i_login, string i_password, string i_deviceID, string version, System.Action doLastFalse, System.Action doLastTrue)
    {
        string serverResponse;

        credentials = new PlayerCredentials(i_login, i_password, i_deviceID, version);
        string packageJson = JsonUtility.ToJson(credentials);

        UnityWebRequest postRequest = UnityWebRequest.Post(url + "api/Jogador/get-token", packageJson);
        postRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(packageJson));
        postRequest.SetRequestHeader("Content-Type", "application/json");
        yield return postRequest.SendWebRequest();

        if (postRequest.isNetworkError || postRequest.isHttpError)
        {
            yield return null;
            doLastFalse();
        }
        else
        {
            serverResponse = postRequest.downloadHandler.text;
            JsonUtility.FromJsonOverwrite(serverResponse, root);

            if (root.data.token == null)
            {
                APIManager.am.user_b2b = false;
                userLogged = false;
                doLastFalse();
            }
            else if (root.data.token != null)
            {
                bool versionCheck = System.Convert.ToBoolean(root.data.jogador.v2);
                APIManager.am.user_b2b = root.data.jogador.b2b;

                if ((versionCheck == PlayerManager.pm.v2) || (PlayerManager.pm.vA1 || PlayerManager.pm.vA2))
                {
                    AssigningUserInfo(root.data.jogador.nome, root.data.token, root.data.jogador.idade);
                    userLogged = true;
                    doLastTrue();
                }
                else
                {
                    userLogged = false;
                    doLastFalse();
                }
            }

            // Debug text
            tokenv.text = root.data.token;
            userv.text = i_login;
            senhav.text = i_password;
            loggedv.text = userLogged.ToString();
            namev.text = root.data.jogador.nome;
            idadev.text = root.data.jogador.idade;
        }
    }

    public IEnumerator AuthOpenAula(bool versao, int level, bool intro, System.Action<string> callbackOnFinish)
    {
        string serverResponse;

        aulaInfo = new AulaInfo(versao, level, intro);
        string packageJson = JsonUtility.ToJson(aulaInfo);

        UnityWebRequest request = UnityWebRequest.Post(url + "api/Aula", packageJson);
        request.SetRequestHeader("Authorization", "Bearer " + userToken);
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(packageJson));

        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            yield break;
        }
        else
        {
            serverResponse = request.downloadHandler.text;
            JsonUtility.FromJsonOverwrite(serverResponse, aulaRoot);
            callbackOnFinish(aulaRoot.data.url);
        }
    }

    IEnumerator PostRelatorio(Dictionary<string, double> categorias)
    {
        var cat = JsonConvert.SerializeObject(categorias);

        UnityWebRequest request = UnityWebRequest.Post(url + "Relatorio", cat);
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(cat));
        request.SetRequestHeader("Authorization", "Bearer " + userToken);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Erro relatorio");
            yield break;
        }
        else
        {
            Debug.Log("Relatorio sent successfully");
        }
    }

    IEnumerator Save()
    {
        SaveLoadRequest savePack;
        string urlPost = url;
        if (PlayerManager.pm.vA1 || PlayerManager.pm.vA2)
        {
            savePack = new SaveLoadRequestVA
            {
                v2 = PlayerManager.pm.v2,
                type = PlayerManager.pm.GetIntFromAvatarType(PlayerManager.pm.type),
                nome = PlayerManager.pm.nome,
                coins = PlayerManager.pm.coins,
                level = PlayerManager.pm.level,
                blue = PlayerManager.pm.blue,
                bonus1 = PlayerManager.pm.bonus1,
                bonus2 = PlayerManager.pm.bonus2,
                professionID = PlayerManager.pm.professionId,
                english = PlayerManager.pm.english,
                ensurance = PlayerManager.pm.ensurance,
                dentist = PlayerManager.pm.dentist,
                vault = PlayerManager.pm.vault,
                gift1 = PlayerManager.pm.gift1,
                gift2 = PlayerManager.pm.gift2,
                gift3 = PlayerManager.pm.gift3,
                safe1 = PlayerManager.pm.safe1,
                safe2 = PlayerManager.pm.safe2,
                safe3 = PlayerManager.pm.safe3,
                v2l20choice = PlayerManager.pm.v2l20choice,
                v2l26choice = PlayerManager.pm.v2l26choice,
                v1l16choices = PlayerManager.pm.v1l16choices,
                ended = PlayerManager.pm.ended,
                vA1 = PlayerManager.pm.vA1,
                vA2 = PlayerManager.pm.vA2,
                basketGame = PlayerManager.pm.basketGame,
                tenisGame = PlayerManager.pm.tenisGame,
                gym = PlayerManager.pm.gym,
                koreanCourse = PlayerManager.pm.koreanCourse,
                spanishCourse = PlayerManager.pm.spanishCourse,
                englishCourse = PlayerManager.pm.englishCourse,
                charger = PlayerManager.pm.charger
            };

            urlPost += "SaveLoad/v2";
        }
        else
        {
            savePack = new SaveLoadRequest
            {
                v2 = PlayerManager.pm.v2,
                type = PlayerManager.pm.GetIntFromAvatarType(PlayerManager.pm.type),
                nome = PlayerManager.pm.nome,
                coins = PlayerManager.pm.coins,
                level = PlayerManager.pm.level,
                blue = PlayerManager.pm.blue,
                bonus1 = PlayerManager.pm.bonus1,
                bonus2 = PlayerManager.pm.bonus2,
                professionID = PlayerManager.pm.professionId,
                english = PlayerManager.pm.english,
                ensurance = PlayerManager.pm.ensurance,
                dentist = PlayerManager.pm.dentist,
                vault = PlayerManager.pm.vault,
                gift1 = PlayerManager.pm.gift1,
                gift2 = PlayerManager.pm.gift2,
                gift3 = PlayerManager.pm.gift3,
                safe1 = PlayerManager.pm.safe1,
                safe2 = PlayerManager.pm.safe2,
                safe3 = PlayerManager.pm.safe3,
                v2l20choice = PlayerManager.pm.v2l20choice,
                v2l26choice = PlayerManager.pm.v2l26choice,
                v1l16choices = PlayerManager.pm.v1l16choices,
                ended = PlayerManager.pm.ended
            };

            urlPost += "SaveLoad";
        }

        var saveData = JsonConvert.SerializeObject(savePack);
        string packageJson = saveData;
        
        UnityWebRequest request = UnityWebRequest.Post(urlPost, packageJson);
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(packageJson));
        request.SetRequestHeader("Authorization", "Bearer " + userToken);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Erro ao salvar");
            yield break;
        }
        else
        {
            Debug.Log("Saved data successfully");
        }
    }

    IEnumerator LoadContentFromAPI()
    {
        string urlGet = (PlayerManager.pm.vA1 || PlayerManager.pm.vA2) ? url + "SaveLoad/v2" : url + "SaveLoad";
        UnityWebRequest request = UnityWebRequest.Get(urlGet);
        request.SetRequestHeader("Authorization", "Bearer " + userToken);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log("Erro ao carregar dados");
            yield break;
        }
        else
        {
            Debug.Log("Data received");
            SaveLoadRequest result = (PlayerManager.pm.vA1 || PlayerManager.pm.vA2) ?
                JsonConvert.DeserializeObject<Root<SaveLoadRequestVA>>(request.downloadHandler.text).data :
                JsonConvert.DeserializeObject<Root<SaveLoadRequest>>(request.downloadHandler.text).data;

            SetValues(result);

            Debug.Log("Data loaded successfully");
        }
    }

    private void SetValues(SaveLoadRequest result)
    {
        PlayerManager.pm.v2 = result.v2;
        PlayerManager.pm.type = PlayerManager.pm.GetAvatarFromJSON(result.type);
        PlayerManager.pm.nome = result.nome;
        PlayerManager.pm.coins = result.coins;
        PlayerManager.pm.level = result.level;
        PlayerManager.pm.blue = result.blue;
        PlayerManager.pm.bonus1 = result.bonus1;
        PlayerManager.pm.bonus2 = result.bonus2;
        PlayerManager.pm.professionId = result.professionID;
        PlayerManager.pm.english = result.english;
        PlayerManager.pm.ensurance = result.ensurance;
        PlayerManager.pm.dentist = result.dentist;
        PlayerManager.pm.vault = result.vault;
        PlayerManager.pm.gift1 = result.gift1;
        PlayerManager.pm.gift2 = result.gift2;
        PlayerManager.pm.gift3 = result.gift3;
        PlayerManager.pm.safe1 = result.safe1;
        PlayerManager.pm.safe2 = result.safe2;
        PlayerManager.pm.safe3 = result.safe3;
        PlayerManager.pm.v2l20choice = result.v2l20choice;
        PlayerManager.pm.v2l26choice = result.v2l26choice;
        PlayerManager.pm.v1l16choices = result.v1l16choices;
        PlayerManager.pm.ended = result.ended;
        PlayerManager.pm.bloqueado = result.bloqueado;
        Debug.Log("Data loaded successfully");

        if (PlayerManager.pm.vA1 || PlayerManager.pm.vA2)
        {
            var resultVA = (SaveLoadRequestVA)result;
            PlayerManager.pm.vA1 = resultVA.vA1;
            PlayerManager.pm.vA2 = resultVA.vA2;
            PlayerManager.pm.basketGame = resultVA.basketGame;
            PlayerManager.pm.tenisGame = resultVA.tenisGame;
            PlayerManager.pm.gym = resultVA.gym;
            PlayerManager.pm.koreanCourse = resultVA.koreanCourse;
            PlayerManager.pm.spanishCourse = resultVA.spanishCourse;
            PlayerManager.pm.englishCourse = resultVA.englishCourse;
            PlayerManager.pm.charger = resultVA.charger;
        }
    }

    IEnumerator ResetData(System.Action doSomething)
    {
        string urlPost = (PlayerManager.pm.vA1 || PlayerManager.pm.vA2) ? url + "api/Jogador/v2/reset" : url + "api/Jogador/reset";
        UnityWebRequest request = UnityWebRequest.Post(urlPost, "");
        request.SetRequestHeader("Authorization", "Bearer " + userToken);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            yield break;
        }
        else
        {
            Debug.Log("Data erased successfully");
            doSomething();
        }
    }

    #endregion

}
