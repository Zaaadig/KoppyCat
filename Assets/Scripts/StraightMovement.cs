using UnityEngine;

public class StraightMovement : MonoBehaviour
{
    public Transform targetTransform; // Cible vers laquelle l'objet doit se d�placer
    public float speed = 5f; // Vitesse de d�placement

    void Update()
    {
        if (targetTransform != null)
        {
            // Calculer la direction vers la cible (sans rotation)
            Vector3 direction = (targetTransform.position - transform.position).normalized;

            // D�placement en ligne droite vers la cible
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
