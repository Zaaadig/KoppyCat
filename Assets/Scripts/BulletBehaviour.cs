using System;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 20f;
    public float maxRange = 10f;
    public int damage = 10;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BonusSoldats"))
        {

        }
    }
}
