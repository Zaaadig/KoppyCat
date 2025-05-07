using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 20f;
    public float maxRange = 10f;
    public int damage = 10; // Dégâts de base
    public float lifespan = 2f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, lifespan); // Détruire automatiquement après 'lifespan' secondes
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // Appliquer les dégâts aux ennemis
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        // Appliquer les dégâts aux boss
        BossHealth boss = other.GetComponent<BossHealth>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    // Méthode pour modifier les dégâts des balles
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
