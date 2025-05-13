using UnityEngine;

public class BonusSoldats : MonoBehaviour
{
    public float speed = 2f; // Vitesse de d�placement
    public int value = 4; // Valeur de base du bonus
    public int maxValue = 10; // Valeur maximale
    public GameObject playerPrefab; // R�f�rence au prefab du joueur
    public float initialRadius = 1f; // Rayon initial du cercle
    public float radiusIncrement = 0.5f; // Augmentation du rayon � chaque copie
    public float angleIncrement = 30f; // Angle entre chaque copie
    public Transform refTransform;

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
            if (value < maxValue)
            {
                value++; // Augmenter la valeur du bonus
            }
            Destroy(other.gameObject); // D�truire la balle apr�s impact
        }

        // V�rifier si le joueur touche le bonus
        if (other.CompareTag("Player"))
        {
            SpawnPlayerCopies(refTransform.transform);
            Destroy(gameObject); // D�truire le bonus apr�s activation
        }
    }

    void SpawnPlayerCopies(Transform playerTransform)
    {
        float currentRadius = initialRadius;
        float currentAngle = 0f;

        for (int i = 0; i < value; i++)
        {
            // Calculer la position en spirale autour du joueur
            float x = playerTransform.position.x + Mathf.Cos(Mathf.Deg2Rad * currentAngle) * currentRadius;
            float z = playerTransform.position.z + Mathf.Sin(Mathf.Deg2Rad * currentAngle) * currentRadius;
            Vector3 spawnPosition = new Vector3(x, playerTransform.position.y, z);

            // Instancier une copie du joueur
            Instantiate(playerPrefab, spawnPosition, playerTransform.rotation);

            // Mettre � jour l'angle et le rayon pour le prochain spawn
            currentAngle += angleIncrement;
            if (currentAngle >= 360f) // Une fois un cercle complet, augmenter le rayon
            {
                currentAngle -= 360f;
                currentRadius += radiusIncrement;
            }
        }
    }
}
