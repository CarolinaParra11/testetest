using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

[Serializable]
public class PlayerData
{
  public string v2;
  public string type;
  public string nome;
  public string coins;
  public string level;
  public string blue;
  public string bonus1;
  public string bonus2;
  public string professionID;
  public string english;
  public string ensurance;
  public string dentist;
  public string vault;
  public string gift1;
  public string gift2;
  public string gift3;
  public string safe1;
  public string safe2;
  public string safe3;
  public string v2l20choice;
  public string v2l26choice;
  public string v1l16choices;
  public string ended;
  public string idJogador;
}

[Serializable]
public class PlayerDataRoot
{
  public PlayerData data;
  public string success;
  public string message;

  private AvatarType avatarType;

  public void AssignVariablesToPlayerManger()
  {
    PlayerManager.pm.v2 = bool.Parse(data.v2);
    PlayerManager.pm.type = GetAvatarFromJSON(data.type);
    PlayerManager.pm.nome = data.nome;
    PlayerManager.pm.coins = int.Parse(data.coins);
    PlayerManager.pm.level = int.Parse(data.level);
    PlayerManager.pm.blue = bool.Parse(data.blue);
    PlayerManager.pm.bonus1 = bool.Parse(data.bonus1);
    PlayerManager.pm.bonus2 = bool.Parse(data.bonus2);
    PlayerManager.pm.professionId = int.Parse(data.professionID);
    PlayerManager.pm.english = bool.Parse(data.english);
    PlayerManager.pm.ensurance = bool.Parse(data.ensurance);
    PlayerManager.pm.dentist = bool.Parse(data.dentist);
    PlayerManager.pm.vault = int.Parse(data.vault);
    PlayerManager.pm.gift1 = int.Parse(data.gift1);
    PlayerManager.pm.gift2 = int.Parse(data.gift2);
    PlayerManager.pm.gift3 = int.Parse(data.gift3);
    PlayerManager.pm.safe1 = int.Parse(data.safe1);
    PlayerManager.pm.safe2 = int.Parse(data.safe2);
    PlayerManager.pm.safe3 = int.Parse(data.safe3);
    PlayerManager.pm.v2l20choice = int.Parse(data.v2l20choice);
    PlayerManager.pm.v2l26choice = int.Parse(data.v2l26choice);
    PlayerManager.pm.v1l16choices = StringToArrayToList(data.v1l16choices);
    PlayerManager.pm.ended = bool.Parse(data.ended);
  }

  private AvatarType GetAvatarFromJSON(string type)
  {
    if(type == "Kid01") avatarType = AvatarType.Kid01;
    if(type == "Kid02") avatarType = AvatarType.Kid02;
    if(type == "Kid03") avatarType = AvatarType.Kid03;
    if(type == "Kid04") avatarType = AvatarType.Kid04;

    return avatarType;
  }

// WORK IN PROGRESS
  private List<int> StringToArrayToList(string arg)
  {
    Debug.Log(data.v1l16choices);
    
    /*
    var input = arg;
    var array = (JArray) JsonConvert.DeserializeObject(input);
//    bool isEqual = array[0].Value<string>() == "0";


    Debug.Log(array);
      */

    List<int> tempList = new List<int>();
    //tempList.AddRange(re);
    return tempList;
  }
 
}


