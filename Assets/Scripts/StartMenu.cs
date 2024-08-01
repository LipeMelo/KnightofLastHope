using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1); // Carrega a cena do jogo (a cena com índice 1)
    }

    public void OnQuitButton()
    {
        Debug.Log("Game is exiting..."); // Exibe um log informando que o jogo está saindo
        Application.Quit(); // Sai do jogo (funciona apenas em builds standalone)
    }
}
