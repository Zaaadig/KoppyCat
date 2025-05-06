using UnityEngine;

public class FireRateBonus : MonoBehaviour
{
    public float speed = 2f; // Vitesse de déplacement
    public int health = 5; // Points de vie du bonus
    public float fireRateMultiplier = 4f; // Multiplicateur de cadence de tir

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
            health--; // Réduire les PV du bonus
            Destroy(other.gameObject); // Détruire la balle après impact

            if (health <= 0)
            {
                ActivatePowerUp(); // Activer le power-up
                Destroy(gameObject); // Détruire le bonus
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
}
