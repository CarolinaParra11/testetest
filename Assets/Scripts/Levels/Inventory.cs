using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyBox;

public class Inventory : MonoBehaviour
{
    // PlayerData
    private int currentLevel;
    private bool isV2;
    private PlayerManager playerManager;
    public Button backButton;

    #region ACHIEVEMENTS VARIABLES
    [Foldout("Achievements", true)]
    public GameObject scroll;
    public GameObject diploma;
    public GameObject medic;
    public GameObject dentist;
    public GameObject bGift1;
    public GameObject fGift;
    public GameObject blueCoin;
    public GameObject trophy;
    #endregion

    #region GIFTS VARIABLES
    [Foldout("Gifts Row 1", true)]
    public Image piggyA, bankA;
    public GameObject[] toys_A_V1;
    public GameObject[] toys_A_V2;
    public GameObject[] sadGiftA;
    [Separator]
    public GameObject ribbonA;
    public GameObject coinA;
    public GameObject bucksA;
    public TextMeshProUGUI valueA;

    [Foldout("Gifts Row 2", true)]
    public Image piggyB, bankB;
    public GameObject[] toys_B_V1;
    public GameObject[] toys_B_V2;
    public GameObject[] sadGiftB;
    [Separator]
    public GameObject ribbonB;
    public GameObject coinB;
    public GameObject bucksB;
    public TextMeshProUGUI valueB;

    [Foldout("Gifts Row 3", true)]
    public Image piggyC, bankC;
    public GameObject[] toys_C_V1;
    public GameObject[] toys_C_V2;
    public GameObject[] sadGiftC;
    [Separator]
    public GameObject ribbonC;
    public GameObject coinC;
    public GameObject bucksC;
    public TextMeshProUGUI valueC;
    #endregion

    #region VAULT VARIABLES
    [Foldout("Vault", true)]
    public GameObject vaultObject;
    public GameObject vCoin;
    public GameObject vBucks;
    public TextMeshProUGUI valueInVault;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerManager = PlayerManager.pm;
        currentLevel = playerManager.level;
        isV2 = playerManager.v2;

        backButton.onClick.AddListener(delegate { GameManager.gm.LoadScene("Bolodim"); });

        CheckAchievements();
        CheckGifts();
        CheckVault();
    }

    #region Achievement

    void CheckAchievements()
    {

        // CONQUISTAS         
        if(!isV2)
        {
            CheckOnAchievementInt(scroll, 6, playerManager.professionId); // DIPLOMA DE TRABALHO
        }
        if(isV2)
        {
            CheckOnAchievementInt(scroll, 2, playerManager.professionId);
            CheckOnAchievementBoolean(medic, 3, playerManager.ensurance);
            CheckOnAchievementBoolean(dentist, 3, playerManager.dentist);
            CheckOnAchievementBoolean(diploma, 3, playerManager.english);
        }

        
       // ACHIEVEMENTS ESPECIAIS
        if(!isV2) // V1
        {
            CheckOnAchievementBoolean(blueCoin, 9, playerManager.blue);
            CheckOnAchievementBoolean(bGift1, 11, playerManager.bonus1);
            CheckOnAchievementBoolean(fGift, 20, playerManager.bonus2);
            CheckTrophy(trophy);
        }
        else if(isV2) // V2
        {
            CheckOnAchievementBoolean(blueCoin, 5, playerManager.blue);
            CheckOnAchievementBoolean(bGift1, 9, playerManager.bonus1);
            CheckOnAchievementBoolean(fGift, 24, playerManager.bonus2);
            CheckTrophy(trophy);
        }
    }

    void CheckOnAchievementBoolean(GameObject achievIcon, int levelRequired, bool parameter)
    { 
            if (currentLevel > levelRequired)
            {
                achievIcon.SetActive(true);

                if (parameter) achievIcon.transform.GetChild(0).gameObject.SetActive(true);
            }
        
    }

    void CheckOnAchievementInt(GameObject achievIcon, int levelRequired, int parameter)
    {
        if(currentLevel > levelRequired)
        {
            achievIcon.SetActive(true);
            if (parameter != 0) achievIcon.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void CheckTrophy(GameObject trophy)
    {
        if(PlayerManager.pm.ended)
        {
        trophy.SetActive(true);
        }
        else trophy.SetActive(false);
    }

    #endregion

    #region Gifts
    void CheckGifts()
    {
        // Check currency of the version
        if (!isV2)
        {
            piggyA.enabled = true;
            piggyB.enabled = true;
            piggyC.enabled = true;
            bankA.enabled = false;
            bankB.enabled = false;
            bankC.enabled = false;

            coinA.SetActive(true);
            coinB.SetActive(true);
            coinC.SetActive(true);
            TurnOnPresent(toys_A_V1, playerManager.gift1, 10, valueA, playerManager.safe1, 1, ribbonA, sadGiftA);
            TurnOnPresent(toys_B_V1, playerManager.gift2, 13, valueB, playerManager.safe2, 2, ribbonB, sadGiftB);
            TurnOnPresent(toys_C_V1, playerManager.gift3, 27, valueC, playerManager.safe3, 3, ribbonC, sadGiftC);
        }
        else
        {
            piggyA.enabled = false;
            piggyB.enabled = false;
            piggyC.enabled = false;
            bankA.enabled = true;
            bankB.enabled = true;
            bankC.enabled = true;

            bucksA.SetActive(true);
            bucksB.SetActive(true);
            bucksC.SetActive(true);
            TurnOnPresent(toys_A_V2, playerManager.gift1, 10, valueA, playerManager.safe1, 15, ribbonA, sadGiftA);
            TurnOnPresent(toys_B_V2, playerManager.gift2, 13, valueB, playerManager.safe2, 40, ribbonB, sadGiftB);
            TurnOnPresent(toys_C_V2, playerManager.gift3, 27, valueC, playerManager.safe3, 100, ribbonC, sadGiftC);
        }
    }

    void TurnOnPresent(GameObject[] array, int gift, int safeLevel, TextMeshProUGUI deposit, int safeValue, int valueNeeded, GameObject ribbon, GameObject[] sadGifts)
    {
        if (gift != 0)
        {
            if (currentLevel > 7 && currentLevel < safeLevel)
            {
                Tapaganha();
            }
            if (currentLevel >= safeLevel)
            {
                if (safeValue >= valueNeeded) Ganho();
                else Numganho();
            }
            if (currentLevel < 7) Debug.Log("deveria ta vazio!");
        }

        // STATES
        void Tapaganha()
        {
            Debug.Log("ta pa ganha");

            array[gift - 1].SetActive(true);
            Image temp = array[gift - 1].GetComponent<Image>();
            var tempColor = temp.color;
            tempColor.a = 0.5f;
            temp.color = tempColor;

            deposit.text = safeValue.ToString();
        }

        void Ganho()
        {
            Debug.Log("ganhooooooooooooooo");

            array[gift - 1].SetActive(true);
            Image temp1 = array[gift - 1].GetComponent<Image>();
            var tempColor1 = temp1.color;
            tempColor1.a = 1f;
            temp1.color = tempColor1;

            ribbon.SetActive(true);
        }

        void Numganho()
        {
            Debug.Log("tururu");

            if (!isV2)
            {
                sadGifts[0].SetActive(true);
                sadGifts[2].SetActive(true);
                sadGifts[2].gameObject.GetComponent<TextMeshProUGUI>().text = safeValue.ToString();
            }
            else
            {
                sadGifts[1].SetActive(true);
                sadGifts[2].SetActive(true);
                sadGifts[2].gameObject.GetComponent<TextMeshProUGUI>().text = safeValue.ToString();
            }
        }
    }

    #endregion

    #region Vault
    void CheckVault()
    {
        if (!isV2)
        {
            vaultObject.SetActive(false);
        }
        else
        {
            vBucks.SetActive(true);
            valueInVault.text = playerManager.vault.ToString();
        }
    }



    #endregion

}