using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameEventManager : MonoBehaviour
{
    public BonusSpawner m_bonusSpawner;
    public SpawnerZombie m_spawnerZombie;
    public List<GameObject> m_players = new List<GameObject>();
    public TMP_Text m_spawner;
    public TMP_Text m_bossBuff;
    public TMP_Text m_aS;
    public TMP_Text m_damage;
    public TMP_Text m_soldats;

    private bool eventInProgress = false; // Empêche plusieurs événements simultanés

    private void Start()
    {
        StartCoroutine(C_StartRandom());
    }

    private IEnumerator C_StartRandom()
    {
        while (true) // Boucle infinie pour gérer les événements
        {
            if (!eventInProgress) // Vérifie qu'aucun événement n'est en cours
            {
                eventInProgress = true; // Marque qu'un événement est en cours
                yield return new WaitForSeconds(10f);

                Debug.Log("Un event est en cours");

                int randomEvent = Random.Range(0, 5);

                if (randomEvent == 0)
                {
                    yield return StartCoroutine(C_BonusSpawner());
                }
                else if (randomEvent == 1)
                {
                    yield return StartCoroutine(C_ZombieSpawner());
                }
                else if (randomEvent == 2)
                {
                    yield return StartCoroutine(C_PlayerAS());
                }
                else if (randomEvent == 3)
                {
                    yield return StartCoroutine(C_PlayerDamage());
                }
                else if (randomEvent == 4)
                {
                    yield return StartCoroutine(C_PlayerDelete());
                }

                eventInProgress = false; // Libère l'événement pour en lancer un autre
            }

            yield return new WaitForSeconds(5f); // Attente avant le prochain événement
        }
    }

    private IEnumerator C_BonusSpawner()
    {
        Debug.Log("Événement BonusSpawner déclenché !");
        m_spawner.enabled = true;
        yield return new WaitForSeconds(10f);
        m_spawner.enabled = false;
        m_bonusSpawner.spawnInterval = 6f;
        yield return new WaitForSeconds(20f);
        m_bonusSpawner.spawnInterval = 3f;
    }

    private IEnumerator C_ZombieSpawner()
    {
        Debug.Log("Événement ZombieSpawner déclenché !");
        m_bossBuff.enabled = true;
        yield return new WaitForSeconds(10f);
        m_bossBuff.enabled = false;
        m_spawnerZombie.bossSpawnRate = 5f;
        yield return new WaitForSeconds(20f);
        m_spawnerZombie.bossSpawnRate = 10f;
    }

    private IEnumerator C_PlayerAS()
    {
        Debug.Log("Événement PlayerAS déclenché !");
        m_aS.enabled = true;
        yield return new WaitForSeconds(10f);
        m_aS.enabled = false;

        GameObject[] playersArray = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in playersArray)
        {
            Gun gun = player.GetComponentInChildren<Gun>();
            if (gun != null)
            {
                gun.fireRate *= 2;
            }
        }

        yield return new WaitForSeconds(20f);

        foreach (GameObject player in playersArray)
        {
            Gun gun = player.GetComponentInChildren<Gun>();
            if (gun != null)
            {
                gun.fireRate /= 2;
            }
        }
    }

    private IEnumerator C_PlayerDamage()
    {
        Debug.Log("Événement PlayerDamage déclenché !");
        m_damage.enabled = true;
        yield return new WaitForSeconds(10f);
        m_damage.enabled = false;

        m_players.Clear();
        m_players.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        foreach (GameObject player in m_players)
        {
            if (player == null) continue; // Vérification avant d'accéder à l'objet

            Gun gun = player.GetComponentInChildren<Gun>();
            if (gun != null)
            {
                gun.damage /= 2;
            }
        }

        yield return new WaitForSeconds(20f);

        foreach (GameObject player in m_players)
        {
            if (player == null) continue; // Vérification avant d'accéder à l'objet

            Gun gun = player.GetComponentInChildren<Gun>();
            if (gun != null)
            {
                gun.damage *= 2;
            }
        }

        // Nettoyer la liste des joueurs supprimés
        m_players.RemoveAll(p => p == null);
    }


    private IEnumerator C_PlayerDelete()
    {
        Debug.Log("Événement PlayerDelete déclenché !");
        m_soldats.enabled = true;
        yield return new WaitForSeconds(10f);
        m_soldats.enabled = false;

        m_players.Clear();
        m_players.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        int countToRemove = m_players.Count / 2;

        for (int i = 0; i < countToRemove; i++)
        {
            int lastIndex = m_players.Count - 1;
            if (lastIndex >= 0)
            {
                GameObject playerToRemove = m_players[lastIndex];
                m_players.RemoveAt(lastIndex);
                Destroy(playerToRemove);
            }
        }
    }
}
