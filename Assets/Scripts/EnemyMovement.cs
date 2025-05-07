using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Vitesse de déplacement
    public int damage = 10; // Dégâts infligés au joueur
    public bool isBoss = false; // Détermine si l'ennemi est un boss

    private Transform targetPlayer; // Joueur ciblé
    private bool isChasing = false; // Indique si l'ennemi poursuit un joueur

    void Update()
    {
        if (isChasing && targetPlayer != null)
        {
            // Se dirige vers le joueur le plus proche
            Vector3 direction = (targetPlayer.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            // Avance en ligne droite
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Detection"))
        {
            FindClosestPlayer(); // Trouve le joueur le plus proche
        }

        if (other.CompareTag("Player"))
        {
            if (isBoss)
            {
                AttackAllPlayers(); // Le boss inflige des dégâts à tous les joueurs
            }
            else
            {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage); // Inflige des dégâts au joueur touché
                }
            }
        }
    }

    void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        Transform closestPlayer = null;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player.transform;
            }
        }

        if (closestPlayer != null)
        {
            targetPlayer = closestPlayer;
            isChasing = true;
        }
    }

    void AttackAllPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Inflige des dégâts à tous les joueurs
            }
        }
    }
}
