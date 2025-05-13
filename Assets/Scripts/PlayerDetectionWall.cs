using UnityEngine;

public class PlayerDetectionWall : MonoBehaviour
{
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
