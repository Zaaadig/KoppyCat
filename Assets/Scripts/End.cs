using UnityEngine;
using UnityEngine.UI;

public class End : MonoBehaviour
{
    public GameObject m_reload;
    public GameObject m_quit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ennemi"))
        {
            m_reload.SetActive(true);
            m_quit.SetActive(true);
            print("REUSSI");
        }

    }
}
