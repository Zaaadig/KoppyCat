using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton pour accéder facilement au GameManager
    public bool canMoveLeft = true;
    public bool canMoveRight = true;
    public GameObject m_playerPrefab;

    void Awake()
    {
        Instance = this; // Assure qu'il n'y a qu'un seul GameManager
    }

    private void Start()
    {
        m_playerPrefab.SetActive(true);
    }
}
