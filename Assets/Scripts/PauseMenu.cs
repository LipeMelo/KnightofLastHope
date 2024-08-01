using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // Variável estática para verificar se o jogo está pausado
    public GameObject pauseMenuUI; // Referência ao painel da UI do menu de pausa

    void Update()
    {
        // Verifica se a tecla Escape foi pressionada
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume(); // Se o jogo está pausado, retoma o jogo
            }
            else
            {
                Pause(); // Se o jogo não está pausado, pausa o jogo
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Desativa a UI do menu de pausa
        Time.timeScale = 1f; // Retoma o tempo do jogo
        GameIsPaused = false; // Define que o jogo não está mais pausado
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true); // Ativa a UI do menu de pausa
        Time.timeScale = 0f; // Pausa o tempo do jogo
        GameIsPaused = true; // Define que o jogo está pausado
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; // Retoma o tempo do jogo antes de carregar o menu principal
        SceneManager.LoadScene(0); // Carrega a cena do menu principal
    }

    public void QuitGame()
    {
        Debug.Log("Game is exiting..."); // Log para indicar que o jogo está fechando
        Application.Quit(); // Fecha a aplicação (não funciona no editor da Unity)
    }
}
