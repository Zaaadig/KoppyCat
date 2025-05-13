using UnityEngine;
using TMPro; // Ajout pour utiliser TextMeshPro

public class BonusSoldats : MonoBehaviour
{
    public float speed = 2f; // Vitesse de d�placement
    public int value = 4; // Nombre initial de soldats
    public int maxValue = 10; // Nombre maximal de soldats
    public GameObject playerPrefab; // Prefab du joueur
    public float initialRadius = 1f; // Rayon initial du cercle
    public float radiusIncrement = 0.5f; // Augmentation du rayon � chaque cercle
    public float angleIncrement = 30f; // Angle entre chaque copie
    public Transform refTransform;

    // Nouveau : R�f�rence au texte du Canvas
    public TMP_Text soldierCountText;

    private void Start()
    {
        UpdateSoldierText(); // Mettre � jour l'affichage au d�marrage
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
            if (value < maxValue)
            {
                value++; // Augmenter le nombre de soldats
                UpdateSoldierText(); // Mettre � jour l'affichage
            }
            Destroy(other.gameObject); // D�truire la balle apr�s impact
        }

        // V�rifier si le joueur touche le bonus
        if (other.CompareTag("Player"))
        {
            SpawnPlayerCopies(other.transform); // Utiliser le transform du joueur
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

        Debug.Log("Bonus activ� ! " + value + " soldats invoqu�s.");
    }

    void UpdateSoldierText()
    {
        if (soldierCountText != null)
        {
            soldierCountText.text = "Soldats � invoquer : " + value;
        }
        else
        {
            Debug.LogWarning("SoldierCountText n'est pas assign� !");
        }
    }
}
