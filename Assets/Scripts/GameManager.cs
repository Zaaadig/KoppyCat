using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton pour acc�der facilement au GameManager
    public bool canMoveLeft = true;
    public bool canMoveRight = true;

    void Awake()
    {
        Instance = this; // Assure qu'il n'y a qu'un seul GameManager
    }
}
