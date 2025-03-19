using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Toggle v2;
    public Toggle ingles;
    public Toggle seguro;
    public Toggle azul;
    public Toggle presente1;
    public Toggle presente2;

    public TMP_InputField nome;
    public TMP_InputField dinheiro;
    public TMP_InputField fase;

    public TMP_InputField seguranca;

    public TMP_InputField cofre1;
    public TMP_InputField cofre2;
    public TMP_InputField cofre3;

    public TMP_InputField sonho1;
    public TMP_InputField sonho2;
    public TMP_InputField sonho3;

    public Button playButton;

    private void Start()
    {
        playButton.onClick.AddListener(delegate
        {
            Play(v2.isOn,
                nome.text, 
                int.Parse(dinheiro.text),
                int.Parse(fase.text),
                azul.isOn,
                presente1.isOn,
                presente2.isOn,
                ingles.isOn,
                seguro.isOn,
                int.Parse(seguranca.text),
                int.Parse(sonho1.text),
                int.Parse(sonho2.text),
                int.Parse(sonho3.text),
                int.Parse(cofre1.text),
                int.Parse(cofre2.text),
                int.Parse(cofre3.text));
        });
    }

    private void Play(bool v2,
        string nome,
        int coins,
        int level,
        bool blue,
        bool bonus1,
        bool bonus2,
        bool english,
        bool ensurance,
        int vault,
        int gift1,
        int gift2,
        int gift3,
        int safe1,
        int safe2,
        int safe3)
    {
        PlayerManager pm = PlayerManager.pm;

        pm.v2 = v2;
        pm.nome = nome;
        pm.coins = coins;
        pm.level = level;
        pm.blue = blue;
        pm.bonus1 = bonus1;
        pm.bonus2 = bonus2;
        pm.english = english;
        pm.ensurance = ensurance;
        pm.vault = vault;
        pm.gift1 = gift1;
        pm.gift2 = gift2;
        pm.gift3 = gift3;
        pm.safe1 = safe1;
        pm.safe2 = safe2;
        pm.safe3 = safe3;

        if (!pm.v2) GameManager.gm.LoadScene("V1L" + pm.level);
        else GameManager.gm.LoadScene("V2L" + pm.level);
    }
}