using UnityEngine;

public class FireballProjectile : BasicProjectile
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<IDamageable>();

        if (enemy != null)
        {
            enemy.TakeDamage(_projectileDamage);
            Destroy(gameObject);
        }
    }
}