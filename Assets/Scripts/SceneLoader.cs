using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Fonction pour recharger la scène actuelle
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Fonction pour quitter le jeu
    public void QuitGame()
    {
        Debug.Log("Quitter le jeu !");
        Application.Quit();
    }
}