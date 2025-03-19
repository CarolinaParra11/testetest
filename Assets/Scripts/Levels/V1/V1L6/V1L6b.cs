using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class V1L6b : MonoBehaviour
{
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;

    public GameObject diplomaPanel;
    public Button diplomaButton;
    public TextMeshProUGUI professionText;
    public TextMeshProUGUI nome;

    public GameObject animation1, animation2, animation3, animation4;
    public float waitTime = 5;

    void Start()
    {
        if (PlayerManager.pm.professionId == 1) animation1.SetActive(true);
        else if (PlayerManager.pm.professionId == 2) animation2.SetActive(true);
        else if (PlayerManager.pm.professionId == 3) animation3.SetActive(true);
        else animation4.SetActive(true);

        StartCoroutine(Diploma());
    }

    private IEnumerator Diploma()
    {
        yield return new WaitForSeconds(waitTime);

        professionText.text = PlayerManager.pm.GetProfessionName();
        nome.text = PlayerManager.pm.nome;

        FlavorManager.fm.ShowHidePanel(diplomaPanel, true);

        diplomaButton.onClick.AddListener(delegate { End(); diplomaButton.onClick.RemoveAllListeners(); });
    }

    private void End()
    {
        FlavorManager.fm.ShowHidePanel(diplomaPanel, false);
        FlavorManager.fm.ShowHidePanel(resultPanel, true);
        AudioManager.am.PlayVoice(AudioManager.am.v1l6[4]);
        resultText.text = "Veja como ficou seu Bolodix!";
    }
}