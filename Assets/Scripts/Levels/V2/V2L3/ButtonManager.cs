using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public List<Button> buttons = new List<Button>(); // Lista de botões
    public int maxActiveButtons = 1; // Máximo de botões ativados ao mesmo tempo

    private List<Button> activeButtons = new List<Button>(); // Lista de botões ativados

    private void Start()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => ToggleButton(button));
        }
    }

    private void ToggleButton(Button clickedButton)
    {
        if (activeButtons.Contains(clickedButton))
        {
            // Se já está ativado, desativa e remove da lista
            activeButtons.Remove(clickedButton);
            SetButtonState(clickedButton, false);
        }
        else
        {
            // Adiciona o botão à lista de ativos
            activeButtons.Add(clickedButton);
            SetButtonState(clickedButton, true);

            // Se atingiu o limite, remove a seleção do primeiro botão ativado antes
            if (activeButtons.Count > maxActiveButtons)
            {
                Button firstActive = activeButtons[0];
                activeButtons.RemoveAt(0);
                SetButtonState(firstActive, false);
            }
        }
    }

    private void SetButtonState(Button button, bool isActive)
    {
        ColorBlock colors = button.colors;
        colors.normalColor = isActive ? Color.green : Color.white; // Altere as cores conforme necessário
        button.colors = colors;
    }
}
