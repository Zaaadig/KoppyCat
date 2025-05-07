using TMPro;
using UnityEngine;

public class DamageBonus : MonoBehaviour
{
    public float speed = 2f; // Vitesse de déplacement
    public int maxHealth = 5; // Points de vie du bonus
    private int currentHeaalth;
    public float damageMultiplier = 2f; // Multiplicateur de dégâts
    public GameObject m_self;
    public TMP_Text healthText;

    private void Start()
    {
        currentHeaalth = maxHealth;
        UpdateHealthText(); 

    }

    void Update()
    {
        // Déplacement en ligne droite selon l'axe forward
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifier si le bonus est touché par une balle
        if (other.CompareTag("Bullet"))
        {
            maxHealth--; // Réduire les PV du bonus
            Destroy(other.gameObject); // Détruire la balle après impact
            UpdateHealthText();

            if (maxHealth <= 0)
            {
                ActivatePowerUp(); // Activer le power-up
                Destroy(m_self); // Détruire le bonus
            }
        }
    }

    void ActivatePowerUp()
    {
        // Trouver toutes les armes dans la scène
        Gun[] guns = FindObjectsByType<Gun>(FindObjectsSortMode.None);

        foreach (Gun gun in guns)
        {
            gun.IncreaseDamage(damageMultiplier);
        }

        Debug.Log("Power-up activé ! Dégâts augmentés.");
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "PV : " + currentHeaalth;
        }
        else
        {
            Debug.LogWarning("HealthText n'est pas assigné !");
        }
    }
}
