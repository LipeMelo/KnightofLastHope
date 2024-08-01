using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f; // Velocidade de movimento do inimigo
    private Transform player; 
    private Animator animator; 
    private bool isChasing = true;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Encontrar o jogador pela tag
        animator = GetComponent<Animator>(); // Obter o componente Animator do inimigo

        // Verifica se as referências são válidas
        if (player == null)
        {
            Debug.LogError("Player não encontrado!");
        }
        if (animator == null)
        {
            Debug.LogError("Animator não encontrado!");
        }
    }

    void Update()
    {
        if (isChasing && player != null)
        {
            // Direção para o jogador
            Vector3 direction = (player.position - transform.position).normalized;

            // Atualizar os parâmetros do Animator
            UpdateAnimatorParameters(direction);

            // Mover o inimigo na direção do jogador
            transform.Translate(direction * speed * Time.deltaTime);

            // Verificar se o inimigo alcançou o jogador
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer < 0.5f)
            {
                isChasing = false; // Parar de perseguir quando alcançar o jogador
            }

            // Inverter a escala do objeto se estiver se movendo para a esquerda
            if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Inverte no eixo X para virar para a esquerda
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Mantém a escala original para a direita
            }
        }
    }

    void UpdateAnimatorParameters(Vector3 direction)
    {
        // Define os parâmetros do Animator com base na direção do movimento
        animator.SetBool("Moving", direction.magnitude > 0);

        if (direction.magnitude > 0)
        {
            // Defina os valores MoveX e MoveY baseados na direção do movimento
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);

            // Defina os parâmetros de movimento direcional específico
            animator.SetBool("MovingRight", direction.x > 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y));
            animator.SetBool("MovingLeft", direction.x < 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y));
            animator.SetBool("MovingUp", direction.y > 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x));
            animator.SetBool("MovingDown", direction.y < 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x));
        }
        else
        {
            // Se não estiver se movendo, desative todos os parâmetros de direção de movimento
            animator.SetBool("MovingRight", false);
            animator.SetBool("MovingLeft", false);
            animator.SetBool("MovingUp", false);
            animator.SetBool("MovingDown", false);
        }
    }
}
