using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 20; // Points de vie totaux (2 coups de 10 d�g�ts)
    public GameObject m_self;
    private int currentHealth;
    

    void Start()
    {
        currentHealth = maxHealth; // Initialisation des PV
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Ennemi touch� ! PV restants : " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Ennemi �limin� !");

        Destroy(m_self); // Supprime l'ennemi
    }
}