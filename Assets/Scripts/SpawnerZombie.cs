using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerZombie : MonoBehaviour
{
    public GameObject enemyPrefab; // Pr�fabriqu� de l'ennemi
    public GameObject bossPrefab; // Pr�fabriqu� du boss
    public BoxCollider spawnArea; // Zone de spawn

    public float spawnRate = 2f; // Fr�quence d'apparition des ennemis
    public float bossSpawnRate = 20f; // Fr�quence d'apparition des boss
    public int maxEnemies = 10; // Nombre max d'ennemis simultan�s

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
            CleanupEnemies(); // V�rifie et supprime les ennemis morts

            if (activeEnemies.Count < maxEnemies) // V�rifie si on peut spawn
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnRate); // Attente avant de v�rifier � nouveau
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
            Debug.Log("Boss instanci� avec " + bossHealth + " PV !");
        }
    }


    void CleanupEnemies()
    {
        activeEnemies.RemoveAll(enemy => enemy == null); // Supprime les r�f�rences aux ennemis d�truits
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
