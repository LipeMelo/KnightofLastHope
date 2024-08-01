using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Referência ao objeto de texto do score na UI
    private int score = 0; // Pontuação inicial do jogador

    public void AddScore(int points)
    {
        score += points; // Adiciona os pontos à pontuação atual
        UpdateScoreText(); // Atualiza o texto do score na UI
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Atualiza o texto do score com a nova pontuação
    }
}
