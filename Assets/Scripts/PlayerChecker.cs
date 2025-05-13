using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    public float checkInterval = 1f; // Temps entre chaque v�rification (en secondes)

    private void Start()
    {
        // Lancer la v�rification en boucle
        InvokeRepeating(nameof(CheckPlayers), checkInterval, checkInterval);
    }

    void CheckPlayers()
    {
        // Trouver tous les objets avec le tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // V�rifier si le nombre de joueurs est 0
        if (players.Length == 1)
        {
            OnNoPlayersLeft(); // D�clencher l'�v�nement
        }
    }

    void OnNoPlayersLeft()
    {
        Debug.Log("Tous les joueurs ont disparu ! D�clenchement de l'�v�nement.");
        // Ici, tu peux ajouter ton �v�nement, par exemple :
        // - Afficher un �cran de Game Over
        // - Red�marrer la sc�ne
        // - Activer un effet sp�cial
    }
}
