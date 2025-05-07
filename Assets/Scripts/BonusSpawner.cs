using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BonusSpawner : MonoBehaviour
{
    public List<GameObject> bonusPrefabs; // Liste des prefabs de bonus
    public Transform spawnPoint; // Position o� le bonus appara�t
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
        // V�rifie qu'il y a des bonus disponibles
        if (bonusPrefabs.Count == 0)
        {
            Debug.LogWarning("Aucun bonus disponible dans la liste !");
            return;
        }

        // Choisit un bonus al�atoire
        int randomIndex = Random.Range(0, bonusPrefabs.Count);
        GameObject selectedBonus = bonusPrefabs[randomIndex];

        // Instancie le bonus � la position d�finie
        Instantiate(selectedBonus, spawnPoint.position, Quaternion.Euler(0, 180, 0));
    }
}
