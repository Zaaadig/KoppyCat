using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Vitesse du joueur
    public Transform leftLimit; // Point limite gauche
    public Transform rightLimit; // Point limite droite

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        // V�rifier si le mouvement est bloqu� globalement via le GameManager
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
        Debug.Log("Collision d�tect�e avec : " + other.gameObject.name); // V�rifier si une collision est d�tect�e

        if (other.CompareTag("LeftBlocker"))
        {
            GameManager.Instance.canMoveLeft = false; // Bloquer le d�placement global vers la gauche
            Debug.Log("Blocage gauche activ� !");
        }
        else if (other.CompareTag("RightBlocker"))
        {
            GameManager.Instance.canMoveRight = false; // Bloquer le d�placement global vers la droite
            Debug.Log("Blocage droite activ� !");
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Sortie de collision avec : " + other.gameObject.name); // V�rifier si la sortie est d�tect�e

        if (other.CompareTag("LeftBlocker"))
        {
            GameManager.Instance.canMoveLeft = true; // R�activer le d�placement global vers la gauche
            Debug.Log("Blocage gauche d�sactiv� !");
        }
        else if (other.CompareTag("RightBlocker"))
        {
            GameManager.Instance.canMoveRight = true; // R�activer le d�placement global vers la droite
            Debug.Log("Blocage droite d�sactiv� !");
        }
    }
}