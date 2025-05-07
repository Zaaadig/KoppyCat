using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string bulletPrefabPath = "Prefabs/Bullet"; // Chemin relatif dans Resources
    public Transform firePoint;
    public float fireRate = 0.5f; // Cadence de tir (secondes entre chaque tir)
    public float damage = 10f; // Dégâts de base
    private Coroutine shootingCoroutine;

    void Start()
    {
        shootingCoroutine = StartCoroutine(C_Shoot());
    }

    private IEnumerator C_Shoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

    void Shoot()
    {
        GameObject bulletPrefab = Resources.Load<GameObject>(bulletPrefabPath);
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletBehaviour bulletScript = bullet.GetComponent<BulletBehaviour>();
            if (bulletScript != null)
            {
                bulletScript.SetDamage((int)damage); // Appliquer les dégâts actuels
            }
        }
        else
        {
            Debug.LogError("Le prefab Bullet n'a pas été trouvé dans Resources/Prefabs !");
        }
    }

    // **Ajout de la méthode pour augmenter la cadence de tir**
    public void IncreaseFireRate(float multiplier)
    {
        StopCoroutine(shootingCoroutine); // Arrêter la coroutine actuelle
        fireRate /= multiplier; // Augmenter la cadence de tir
        shootingCoroutine = StartCoroutine(C_Shoot()); // Redémarrer la coroutine avec le nouveau fireRate
        Debug.Log(gameObject.name + " a reçu un power-up ! Nouvelle cadence de tir : " + fireRate);
    }

    public void IncreaseDamage(float multiplier)
    {
        damage *= multiplier; // Augmenter les dégâts
        Debug.Log(gameObject.name + " a reçu un power-up ! Nouveaux dégâts : " + damage);
    }
}
