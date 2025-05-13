using UnityEngine;

public class PlayerDetectionWall : MonoBehaviour
{
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
