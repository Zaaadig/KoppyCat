using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FireRateBonus : MonoBehaviour
{
    public float speed = 2f; // Vitesse de déplacement
    public int maxHealth = 5; // Points de vie du bonus
    private int currentHealth;
    public float fireRateMultiplier = 4f; // Multiplicateur de cadence de tir
    public GameObject m_self;
    public TMP_Text healthText;

    private void Start()
    {
        currentHealth = maxHealth;
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
            currentHealth--; // Réduire les PV du bonus
            Destroy(other.gameObject); // Détruire la balle après impact
            UpdateHealthText();

            if (currentHealth <= 0)
            {
                ActivatePowerUp(); // Activer le power-up
                Destroy(m_self); // Détruire le bonus
            }
        }
    }

    void ActivatePowerUp()
    {
        // Trouver tous les joueurs dans la scène
        Gun[] guns = FindObjectsByType<Gun>(FindObjectsSortMode.None);

        foreach (Gun gun in guns)
        {
            gun.IncreaseFireRate(fireRateMultiplier);
        }

        Debug.Log("Power-up activé ! Cadence de tir augmentée.");
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "PV : " + currentHealth;
        }
    }
}
