using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager pm;

    public bool vA1;
    public bool vA2;
    public bool v2;
    public AvatarType type;
    private AvatarType avatarType;

    public string nome;
    public int startCoins;
    public int coins;
    public int level;

    public bool blue;
    public bool bonus1;
    public bool bonus2;

    public int professionId;
    public bool english;
    public bool ensurance;
    public bool dentist;
    public int vault;

    public int gift1, gift2, gift3;
    public int safe1, safe2, safe3;

    public int v2l20choice;
    public int v2l26choice;
    public List<int> v1l16choices;

    [Header("A2 settings - START")]
    public bool basketGame = false;
    public bool tenisGame = false;
    public bool gym = false;

    public bool koreanCourse = false;
    public bool spanishCourse = false;
    public bool englishCourse = false;

    public bool charger = false;


    [Header("A2 settings - END")]



    public bool ended;
    public bool bloqueado;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestroy");
        if (objs.Length > 5) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        pm = this;
    }

    private void Start()
    {
        if (level == -1) GameManager.gm.LoadScene("V1L-1");
        else if (level == 0) GameManager.gm.LoadScene("MainMenu");
        else if (vA2 && !v2 ) GameManager.gm.LoadScene("A2L" + level);
        else if (vA1 && !v2 ) GameManager.gm.LoadScene("A1L" + level);
        else if (!v2) GameManager.gm.LoadScene("V1L" + level);
        else GameManager.gm.LoadScene("V2L" + level);
    }

    public string GetProfessionName()
    {
        if (type == AvatarType.Kid01 || type == AvatarType.Kid03)
        {
            if (professionId == 1) return "Dentista";
            else if (professionId == 2) return "Bombeiro";
            else if (professionId == 3) return "Médico";
            else if (professionId == 4) return "Piloto de avião";
            else return "Profissional";
        }
        else
        {
            if (professionId == 1) return "Dentista";
            else if (professionId == 2) return "Bombeira";
            else if (professionId == 3) return "Médica";
            else if (professionId == 4) return "Pilota de avião";
            else return "Profissional";
        }
    }

    public int GetCoins()
    {
        return coins;
    }

    public int GetStartingCoins()
    {
        return startCoins;
    }

    public void AddCoins(int n)
    {
        coins += n;
    }

    public void RemoveCoins(int n)
    {
        coins -= n;
    }

    public int GetLevel()
    {
        return level;
    }

    public void AddLevel()
    {
        level++;
        APIManager.am.SavePlayer();
    }

    public void AddChoice(int i)
    {
        v1l16choices.Add(i);
    }

    public void RemoveChoice(int i)
    {
        v1l16choices.RemoveAt(v1l16choices.IndexOf(i));
    }

    #region API ASSIST METHODS

  public AvatarType GetAvatarFromJSON(int type)
  {
      if(type == 0) avatarType = AvatarType.lucas;
    if(type == 1) avatarType = AvatarType.Kid01;
    if(type == 2) avatarType = AvatarType.Kid02;
    if(type == 3) avatarType = AvatarType.Kid03;
    if(type == 4) avatarType = AvatarType.Kid04;

    return avatarType;
  }

  public int GetIntFromAvatarType(AvatarType type)
  {
      int i = 0;
      if(type == AvatarType.lucas) i = 0;
      if(type == AvatarType.Kid01) i = 1;
      if(type == AvatarType.Kid02) i = 2;
      if(type == AvatarType.Kid03) i = 3;
      if(type == AvatarType.Kid04) i = 4;

      return i;
  }
    #endregion

}