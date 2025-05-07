using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public GameObject m_self; // R�f�rence au boss
    public Image healthBarFill; // R�f�rence � l'image de la barre de vie
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
        Debug.Log("Boss initialis� avec " + currentHealth + " PV !");
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Boss touch� ! PV restants : " + currentHealth);

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
        Debug.Log("Boss �limin� !");
        Destroy(m_self); // Supprime le boss
    }
}
