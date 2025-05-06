using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Vitesse du joueur
    public Transform leftLimit; // Point limite gauche
    public Transform rightLimit; // Point limite droite

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        // Vérifier si le mouvement est bloqué globalement via le GameManager
        if ((moveInput < 0 && GameManager.Instance.canMoveLeft) || (moveInput > 0 && GameManager.Instance.canMoveRight))
        {
            Vector3 newPosition = transform.position + Vector3.right * moveInput * speed * Time.deltaTime;

            // Limiter le mouvement avec les points de limite
            newPosition.x = Mathf.Clamp(newPosition.x, leftLimit.position.x, rightLimit.position.x);

            transform.position = newPosition;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision détectée avec : " + other.gameObject.name); // Vérifier si une collision est détectée

        if (other.CompareTag("LeftBlocker"))
        {
            GameManager.Instance.canMoveLeft = false; // Bloquer le déplacement global vers la gauche
            Debug.Log("Blocage gauche activé !");
        }
        else if (other.CompareTag("RightBlocker"))
        {
            GameManager.Instance.canMoveRight = false; // Bloquer le déplacement global vers la droite
            Debug.Log("Blocage droite activé !");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Sortie de collision avec : " + other.gameObject.name); // Vérifier si la sortie est détectée

        if (other.CompareTag("LeftBlocker"))
        {
            GameManager.Instance.canMoveLeft = true; // Réactiver le déplacement global vers la gauche
            Debug.Log("Blocage gauche désactivé !");
        }
        else if (other.CompareTag("RightBlocker"))
        {
            GameManager.Instance.canMoveRight = true; // Réactiver le déplacement global vers la droite
            Debug.Log("Blocage droite désactivé !");
        }
    }
}