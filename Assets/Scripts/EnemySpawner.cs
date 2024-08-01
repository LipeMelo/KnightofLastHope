using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab do inimigo
    public Transform[] spawnPoints; // Pontos de spawn no mapa

    public float initialSpawnDelay = 2f; // Delay inicial entre os spawns
    public float minSpawnDelay = 0.5f; // Delay mínimo entre os spawns
    public float spawnDelayDecreaseRate = 0.05f; // Taxa de redução do delay por segundo

    private float currentSpawnDelay; // Delay atual entre os spawns

    void Start()
    {
        currentSpawnDelay = initialSpawnDelay;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Escolhe um ponto de spawn aleatório
            Transform randomSpawnPoint = GetRandomSpawnPoint();

            // Instancia o inimigo no ponto de spawn escolhido
            Instantiate(enemyPrefab, randomSpawnPoint.position, Quaternion.identity);

            // Ajusta o delay de spawn para o próximo inimigo
            yield return new WaitForSeconds(currentSpawnDelay);

            // Reduz o delay de spawn gradualmente
            currentSpawnDelay -= spawnDelayDecreaseRate * Time.deltaTime;

            // Limita o delay mínimo
            if (currentSpawnDelay < minSpawnDelay)
            {
                currentSpawnDelay = minSpawnDelay;
            }
        }
    }

    Transform GetRandomSpawnPoint()
    {
        // Escolhe um ponto de spawn aleatório dentre os disponíveis
        int randomIndex = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomIndex];
    }
}
