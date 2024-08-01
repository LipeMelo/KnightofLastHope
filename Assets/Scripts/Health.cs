using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health; // Quantidade atual de saúde do jogador
    public int numOfHearts; // Número máximo de corações
    public Image[] hearts; // Array de imagens que representam os corações na UI
    public Sprite fullHeart; // Sprite do coração cheio
    public Sprite emptyHeart; // Sprite do coração vazio
    public DeathScreen deathScreen; // Referência à tela de morte

    void Update()
    {
        // Garante que a saúde não exceda o número máximo de corações
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        // Atualiza a UI dos corações
        UpdateHeartsUI();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se colidiu com um inimigo
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(); // Aplica dano ao jogador
            Destroy(collision.gameObject); // Destroi o inimigo
        }
    }

    void TakeDamage()
    {
        health--; // Reduz a saúde do jogador em 1
        UpdateHeartsUI(); // Atualiza a UI dos corações

        // Verifica se a saúde do jogador chegou a zero
        if (health <= 0)
        {
            deathScreen.ShowDeathScreen(); // Mostra a tela de morte
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            // Define o sprite do coração como cheio ou vazio, dependendo da saúde atual
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            // Ativa ou desativa a imagem do coração, dependendo do número máximo de corações
            hearts[i].enabled = (i < numOfHearts);
        }
    }
}
