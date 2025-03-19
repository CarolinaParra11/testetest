using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class V2L11b : MonoBehaviour
{
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public Animator animator;
    public TextMeshProUGUI profession;

    public GameObject player;

    public GameObject incomePanel;
    public TextMeshProUGUI incomeText;

    public Dictionary<string, double> conceitos = new Dictionary<string, double>();

    private void Start() { StartCoroutine(End()); }

    private IEnumerator End()
    {
        AvatarType avatar = PlayerManager.pm.type;
        int id = PlayerManager.pm.professionId;

        if (id == 1)
        {
            if (avatar == AvatarType.Kid01 || avatar == AvatarType.Kid03) profession.text = "Dentista";
            else profession.text = "Dentista";
        }
        else if (id == 2)
        {
            if (avatar == AvatarType.Kid01 || avatar == AvatarType.Kid03) profession.text = "Bombeiro";
            else profession.text = "Bombeira";
        }
        else if (id == 3)
        {
            if (avatar == AvatarType.Kid01 || avatar == AvatarType.Kid03) profession.text = "Médico";
            else profession.text = "Médica";
        }
        else if (id == 4)
        {
            if (avatar == AvatarType.Kid01 || avatar == AvatarType.Kid03) profession.text = "Piloto de avião";
            else profession.text = "Pilota de avião";
        }

        if (!PlayerManager.pm.english) player.SetActive(false);

        yield return new WaitForSeconds(3);

        if (PlayerManager.pm.english)
        {
            conceitos.Add("25", 1);
            conceitos.Add("56", 5);

            animator.SetTrigger("Right");
            resultText.text = "Parabéns! Conseguiu fazer seu papel e falar bem em inglês sobre sua profissão! Ganhou 120 reais!";
            StartCoroutine(FlavorManager.fm.SpawnBucks(5));
            PlayerManager.pm.AddCoins(120);
        }
        else
        {
            conceitos.Add("25", 0);
            conceitos.Add("56", 0);

            player.SetActive(false);
            resultText.text = "Você sabe bastante sobre sua profissão, mas precisava ter feito o curso de inglês para palestrar! Procure ir atrás de conhecimento!";
        }

        yield return new WaitForSeconds(2);

        if (PlayerManager.pm.english)
        {
            FlavorManager.fm.ShowHidePanel(incomePanel, true);
            incomeText.text = "120";
        }
        else
        {
            FlavorManager.fm.ShowHidePanel(incomePanel, true);
            incomeText.text = "0";
        }

        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        APIManager.am.Relatorio(conceitos);

        PlayerManager.pm.AddLevel();
    }
}