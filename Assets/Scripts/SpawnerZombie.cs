using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerZombie : MonoBehaviour
{
    public GameObject enemyPrefab; // Préfabriqué de l'ennemi
    public GameObject bossPrefab; // Préfabriqué du boss
    public BoxCollider spawnArea; // Zone de spawn

    public float spawnRate = 2f; // Fréquence d'apparition des ennemis
    public float bossSpawnRate = 20f; // Fréquence d'apparition des boss
    public int maxEnemies = 10; // Nombre max d'ennemis simultanés

    private List<GameObject> activeEnemies = new List<GameObject>(); // Liste des ennemis actifs
    private static int bossHealth = 1000; // PV du premier boss (static pour conserver la valeur)

    void Start()
    {
        StartCoroutine(SpawnEnemies()); // Lance la boucle de spawn des ennemis
        StartCoroutine(C_SpawnBoss()); // Lance la boucle de spawn des boss
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            CleanupEnemies(); // Vérifie et supprime les ennemis morts

            if (activeEnemies.Count < maxEnemies) // Vérifie si on peut spawn
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnRate); // Attente avant de vérifier à nouveau
        }
    }

    IEnumerator C_SpawnBoss()
    {
        while (true)
        {
            yield return new WaitForSeconds(bossSpawnRate); // Attente avant de spawn un boss

            SpawnBoss();
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomPosition();
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0, 180, 0));
        activeEnemies.Add(newEnemy);
    }

    void SpawnBoss()
    {
        bossHealth = Mathf.RoundToInt(bossHealth * 1.5f);

        Vector3 spawnPosition = GetRandomPosition();
        GameObject newBoss = Instantiate(bossPrefab, spawnPosition, Quaternion.Euler(0, 180, 0));

        BossHealth bossHealthScript = newBoss.GetComponentInChildren<BossHealth>();
        if (bossHealthScript != null)
        {
            bossHealthScript.SetHealth(bossHealth);
            Debug.Log("Boss instancié avec " + bossHealth + " PV !");
        }
    }


    void CleanupEnemies()
    {
        activeEnemies.RemoveAll(enemy => enemy == null); // Supprime les références aux ennemis détruits
    }

    Vector3 GetRandomPosition()
    {
        Vector3 center = spawnArea.bounds.center;
        Vector3 size = spawnArea.bounds.size;

        float x = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float z = Random.Range(center.z - size.z / 2, center.z + size.z / 2);
        float y = center.y;

        return new Vector3(x, y, z);
    }
}
