using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BonusSpawner : MonoBehaviour
{
    public GameObject soloBonus; // Bonus qui appara�t une fois sur deux
    public List<GameObject> groupBonus; // Liste des deux autres bonus
    public Transform spawnPoint; // Position o� le bonus appara�t
    public float spawnInterval = 5f; // Temps entre chaque spawn

    private bool spawnSoloNext = true; // Alterne entre soloBonus et groupBonus

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
        GameObject selectedBonus;

        if (spawnSoloNext)
        {
            selectedBonus = soloBonus; // Spawn du bonus unique
        }
        else
        {
            // Alterne entre les deux bonus du groupe
            int randomIndex = Random.Range(0, groupBonus.Count);
            selectedBonus = groupBonus[randomIndex];
        }

        // Instancie le bonus � la position d�finie
        Instantiate(selectedBonus, spawnPoint.position, Quaternion.Euler(0, 180, 0));

        // Alterne pour le prochain spawn
        spawnSoloNext = !spawnSoloNext;
    }
}
