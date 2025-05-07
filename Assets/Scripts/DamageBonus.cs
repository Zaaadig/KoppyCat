using TMPro;
using UnityEngine;

public class DamageBonus : MonoBehaviour
{
    public float speed = 2f; // Vitesse de d�placement
    public int maxHealth = 5; // Points de vie du bonus
    private int currentHeaalth;
    public float damageMultiplier = 2f; // Multiplicateur de d�g�ts
    public GameObject m_self;
    public TMP_Text healthText;

    private void Start()
    {
        currentHeaalth = maxHealth;
        UpdateHealthText(); 

    }

    void Update()
    {
        // D�placement en ligne droite selon l'axe forward
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // V�rifier si le bonus est touch� par une balle
        if (other.CompareTag("Bullet"))
        {
            maxHealth--; // R�duire les PV du bonus
            Destroy(other.gameObject); // D�truire la balle apr�s impact
            UpdateHealthText();

            if (maxHealth <= 0)
            {
                ActivatePowerUp(); // Activer le power-up
                Destroy(m_self); // D�truire le bonus
            }
        }
    }

    void ActivatePowerUp()
    {
        // Trouver toutes les armes dans la sc�ne
        Gun[] guns = FindObjectsByType<Gun>(FindObjectsSortMode.None);

        foreach (Gun gun in guns)
        {
            gun.IncreaseDamage(damageMultiplier);
        }

        Debug.Log("Power-up activ� ! D�g�ts augment�s.");
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "PV : " + currentHeaalth;
        }
        else
        {
            Debug.LogWarning("HealthText n'est pas assign� !");
        }
    }
}
