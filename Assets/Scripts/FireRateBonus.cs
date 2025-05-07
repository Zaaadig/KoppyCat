using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FireRateBonus : MonoBehaviour
{
    public float speed = 2f; // Vitesse de d�placement
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
        // D�placement en ligne droite selon l'axe forward
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // V�rifier si le bonus est touch� par une balle
        if (other.CompareTag("Bullet"))
        {
            currentHealth--; // R�duire les PV du bonus
            Destroy(other.gameObject); // D�truire la balle apr�s impact
            UpdateHealthText();

            if (currentHealth <= 0)
            {
                ActivatePowerUp(); // Activer le power-up
                Destroy(m_self); // D�truire le bonus
            }
        }
    }

    void ActivatePowerUp()
    {
        // Trouver tous les joueurs dans la sc�ne
        Gun[] guns = FindObjectsByType<Gun>(FindObjectsSortMode.None);

        foreach (Gun gun in guns)
        {
            gun.IncreaseFireRate(fireRateMultiplier);
        }

        Debug.Log("Power-up activ� ! Cadence de tir augment�e.");
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "PV : " + currentHealth;
        }
    }
}
