using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1f; // velocidade do Jogador
    [SerializeField] private GameObject bulletPrefab; // Prefab da Adaga
    [SerializeField] private Vector2 bulletOffset = new Vector2(0.5f, 0.5f); // Ajuste do offset da adaga
    [SerializeField] private float shootCooldown = 0.5f; // Cooldown entre disparos
    private float lastShootTime;
    private Animator animator;
    private int direction = 0; // 0 - idle down, 1 - idle up, 2 - idle right, 3 - idle left, 4 - move up, 5 - move right, 6 - move left, 7 - move down

    void Awake()
    {
        animator = GetComponent<Animator>();
        lastShootTime = -shootCooldown; // Inicializa para permitir disparar imediatamente
    }

    void Update()
    {
        HandleMovement();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }
    }

    void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Normaliza a entrada para evitar movimentos diagonais mais rápidos
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        if (movement.magnitude > 0)
        {
            if (movement.x > 0)
                direction = 5; // Mover para direita
            else if (movement.x < 0)
                direction = 6; // Mover para esquerda
            else if (movement.y > 0)
                direction = 4; // Mover para cima
            else if (movement.y < 0)
                direction = 7; // Mover para baixo
        }
        else
        {
            // Mantém a direção de idle baseada na última direção de movimento
            switch (direction)
            {
                case 5:
                    direction = 2; // Idle para direita
                    break;
                case 6:
                    direction = 3; // Idle para esquerda
                    break;
                case 4:
                    direction = 1; // Idle para cima
                    break;
                case 7:
                    direction = 0; // Idle para baixo
                    break;
            }
        }

        animator.SetInteger("Direction", direction);

        // Inverte a escala do objeto conforme a direção (apenas necessário para movimentar para direita)
        if (direction == 5 || direction == 2) // Movimento ou idle para direita
            transform.localScale = new Vector3(-1, 1, 1); // Invertido
        else if (direction == 6 || direction == 3) // Movimento ou idle para esquerda
            transform.localScale = new Vector3(1, 1, 1); // Normal

        // Move o jogador
        if (movement.magnitude > 0)
        {
            transform.Translate(movement * speed * Time.deltaTime);
        }
    }

    void Shoot()
    {
        if (Time.time - lastShootTime < shootCooldown) // Verifica se o tempo desde o último disparo é menor que o cooldown
            return;

        lastShootTime = Time.time; // Atualiza o tempo do último disparo

        Vector2 shootDirection = Vector2.zero;
        float rotationZ = 0f;
        Vector3 bulletScale = new Vector3(1, 1, 1);
        Vector2 bulletSpawnPosition = transform.position; // Posição inicial da bala

        switch (direction)
        {
            case 0: // Idle para baixo
                shootDirection = Vector2.down;
                rotationZ = 270f; // Rotação para baixo
                bulletSpawnPosition += new Vector2(0, -bulletOffset.y); // Ajuste de offset se necessário
                break;
            case 1: // Idle para cima
                shootDirection = Vector2.up;
                rotationZ = 90f; // Rotação para cima
                bulletSpawnPosition += new Vector2(0, bulletOffset.y); // Ajuste de offset se necessário
                break;
            case 2: // Idle para direita
                shootDirection = Vector2.right;
                rotationZ = 0f; // Rotação para direita
                bulletSpawnPosition += new Vector2(bulletOffset.x, 0); // Ajuste de offset se necessário
                break;
            case 3: // Idle para esquerda
                shootDirection = Vector2.left;
                rotationZ = 0f; // Rotação para esquerda
                bulletScale = new Vector3(-1, 1, 1); // Inverte no eixo X
                bulletSpawnPosition += new Vector2(-bulletOffset.x, 0); // Ajuste de offset se necessário
                break;
            case 4: // Movendo para cima
                shootDirection = Vector2.up;
                rotationZ = 90f; // Rotação para cima
                bulletSpawnPosition += new Vector2(0, bulletOffset.y); // Ajuste de offset se necessário
                break;
            case 5: // Movendo para direita
                shootDirection = Vector2.right;
                rotationZ = 0f; // Rotação para direita
                bulletSpawnPosition += new Vector2(bulletOffset.x, 0); // Ajuste de offset se necessário
                break;
            case 6: // Movendo para esquerda
                shootDirection = Vector2.left;
                rotationZ = 0f; // Rotação para esquerda
                bulletScale = new Vector3(-1, 1, 1); // Inverte no eixo X
                bulletSpawnPosition += new Vector2(-bulletOffset.x, 0); // Ajuste de offset se necessário
                break;
            case 7: // Movendo para baixo
                shootDirection = Vector2.down;
                rotationZ = 270f; // Rotação para baixo
                bulletSpawnPosition += new Vector2(0, -bulletOffset.y); // Ajuste de offset se necessário
                break;
        }

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPosition, Quaternion.Euler(0, 0, rotationZ));
        bullet.transform.localScale = bulletScale;
        bullet.GetComponent<Bullet>().Initialize(shootDirection);
    }
}
