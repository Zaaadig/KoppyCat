using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BonusSpawner : MonoBehaviour
{
    public List<GameObject> bonusPrefabs; // Liste des prefabs de bonus
    public Transform spawnPoint; // Position où le bonus apparaît
    public float spawnInterval = 5f; // Temps entre chaque spawn

    void Start()
    {
        StartCoroutine(SpawnBonusPeriodically());
    }

    IEnumerator SpawnBonusPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval); // Attente avant le prochain spawn
            SpawnBonus();
        }
    }

    void SpawnBonus()
    {
        // Vérifie qu'il y a des bonus disponibles
        if (bonusPrefabs.Count == 0)
        {
            Debug.LogWarning("Aucun bonus disponible dans la liste !");
            return;
        }

        // Choisit un bonus aléatoire
        int randomIndex = Random.Range(0, bonusPrefabs.Count);
        GameObject selectedBonus = bonusPrefabs[randomIndex];

        // Instancie le bonus à la position définie
        Instantiate(selectedBonus, spawnPoint.position, Quaternion.Euler(0, 180, 0));
    }
}
