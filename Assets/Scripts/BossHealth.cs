using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public GameObject m_self; // Référence au boss
    public Image healthBarFill; // Référence à l'image de la barre de vie
    private int currentHealth;
    private int maxHealth;

    public void SetHealth(int health)
    {
        maxHealth = health;
        currentHealth = health;
        UpdateHealthBar();
    }

    void Start()
    {
        Debug.Log("Boss initialisé avec " + currentHealth + " PV !");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Boss touché ! PV restants : " + currentHealth);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Boss éliminé !");
        Destroy(m_self); // Supprime le boss
    }
}
