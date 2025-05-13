using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    public float checkInterval = 1f; // Temps entre chaque vérification (en secondes)

    private void Start()
    {
        // Lancer la vérification en boucle
        InvokeRepeating(nameof(CheckPlayers), checkInterval, checkInterval);
    }

    void CheckPlayers()
    {
        // Trouver tous les objets avec le tag "Player"
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        // Vérifier si le nombre de joueurs est 0
        if (players.Length == 1)
        {
            OnNoPlayersLeft(); // Déclencher l'événement
        }
    }

    void OnNoPlayersLeft()
    {
        Debug.Log("Tous les joueurs ont disparu ! Déclenchement de l'événement.");
        // Ici, tu peux ajouter ton événement, par exemple :
        // - Afficher un écran de Game Over
        // - Redémarrer la scène
        // - Activer un effet spécial
    }
}
