using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string bulletPrefabPath = "Prefabs/Bullet"; // Chemin relatif dans Resources
    public Transform firePoint;
    public float fireRate = 0.5f;
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
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.LogError("Le prefab Bullet n'a pas été trouvé dans Resources/Prefabs !");
        }
    }

    public void IncreaseFireRate(float multiplier)
    {
        StopCoroutine(shootingCoroutine); // Arrêter la coroutine actuelle
        fireRate /= multiplier; // Augmenter la cadence de tir
        shootingCoroutine = StartCoroutine(C_Shoot()); // Redémarrer la coroutine avec le nouveau fireRate
        Debug.Log(gameObject.name + " a reçu un power-up ! Nouvelle cadence de tir : " + fireRate);
    }
}