using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f; //velocidade da adaga
    private Rigidbody2D bulletRigidbody;
    [SerializeField] private float delayToDestroy = 2f; // Tempo para destruir a adaga
    private ScoreManager scoreManager; // Referência ao ScoreManager

    void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>(); // Encontra o ScoreManager na cena
    }

    public void Initialize(Vector2 direction)
    {
        bulletRigidbody.velocity = direction * speed;
    }

    void Start()
    {
        Destroy(gameObject, delayToDestroy);
    }

    // Detecta colisões com outros colliders 2D (inimigo e parede)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se colidiu com o inimigo ou com a parede
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            Destroy(gameObject); // Destroi a adaga

            // Se colidiu com o inimigo, destrua o inimigo também
            if (other.CompareTag("Enemy"))
            {
                Destroy(other.gameObject); // Destroi o inimigo
                scoreManager.AddScore(100); // Adiciona 100 pontos ao score
            }
        }
    }
}
