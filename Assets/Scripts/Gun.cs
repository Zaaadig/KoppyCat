using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string bulletPrefabPath = "Prefabs/Bullet"; // Chemin relatif dans Resources
    public Transform firePoint;
    public float fireRate = 0.5f; // Cadence de tir (secondes entre chaque tir)
    public float damage = 10f; // D�g�ts de base
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
                bulletScript.SetDamage((int)damage); // Appliquer les d�g�ts actuels
            }
        }
        else
        {
            Debug.LogError("Le prefab Bullet n'a pas �t� trouv� dans Resources/Prefabs !");
        }
    }

    // **Ajout de la m�thode pour augmenter la cadence de tir**
    public void IncreaseFireRate(float multiplier)
    {
        StopCoroutine(shootingCoroutine); // Arr�ter la coroutine actuelle
        fireRate /= multiplier; // Augmenter la cadence de tir
        shootingCoroutine = StartCoroutine(C_Shoot()); // Red�marrer la coroutine avec le nouveau fireRate
        Debug.Log(gameObject.name + " a re�u un power-up ! Nouvelle cadence de tir : " + fireRate);
    }

    public void IncreaseDamage(float multiplier)
    {
        damage *= multiplier; // Augmenter les d�g�ts
        Debug.Log(gameObject.name + " a re�u un power-up ! Nouveaux d�g�ts : " + damage);
    }
}
