using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cadastrar : MonoBehaviour
{
    public Button cadastro;
    public TMP_InputField i_nomeAluno;
    public TMP_InputField i_numeroMatricula;

    private string nomeAluno;
    private string numeroAluno;

    private void Start()
    {
        cadastro.onClick.AddListener(delegate { SaveInfo(); cadastro.onClick.RemoveAllListeners(); });
    }

    void SaveInfo()
    {
        nomeAluno = i_nomeAluno.text;
        numeroAluno = i_numeroMatricula.text;

    // Enviar pro database aqui


    //  placeholder 
        PlayerManager.pm.nome = nomeAluno;
        GameManager.gm.LoadScene("AvatarSelection");
    }


}
