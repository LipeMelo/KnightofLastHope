using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject deathScreen; // Referência ao Panel da tela de morte

    void Start()
    {
        deathScreen.SetActive(false); // Inicialmente desativa a tela de morte
    }

    public void ShowDeathScreen()
    {
        deathScreen.SetActive(true); // Ativa a tela de morte
        Time.timeScale = 0f; // Pausa o jogo
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f; // Retorna o tempo do jogo ao normal
        SceneManager.LoadScene(0); // Carrega a cena do menu principal
    }

    public void ExitGame()
    {
        Debug.Log("Game is exiting..."); // Log para indicar que o jogo está fechando
        Application.Quit(); // Fecha a aplicação (não funciona no editor da Unity)
    }
}
