using UnityEngine;

public class StraightMovement : MonoBehaviour
{
    public Transform targetTransform; // Cible vers laquelle l'objet doit se déplacer
    public float speed = 5f; // Vitesse de déplacement

    void Update()
    {
        if (targetTransform != null)
        {
            // Calculer la direction vers la cible (sans rotation)
            Vector3 direction = (targetTransform.position - transform.position).normalized;

            // Déplacement en ligne droite vers la cible
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
