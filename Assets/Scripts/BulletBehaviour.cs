using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 20f;
    public float maxRange = 10f;
    public int damage = 10; // D�g�ts de base
    public float lifespan = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, lifespan); // D�truire automatiquement apr�s 'lifespan' secondes
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Appliquer les d�g�ts aux ennemis
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        // Appliquer les d�g�ts aux boss
        BossHealth boss = other.GetComponent<BossHealth>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    // M�thode pour modifier les d�g�ts des balles
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
