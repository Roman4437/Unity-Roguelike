using System;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyDefaultStats _enemyDefaultStats;
    [SerializeField] private FloatingDamageText _damagetextPrefab;
    
    protected float _health;
    protected float _damage;
    protected float _movementSpeed;
    protected bool _isAlive = true;

    protected Rigidbody2D _rigidbody;

    private void Start()
    {
        _health = _enemyDefaultStats.health;
        _damage = _enemyDefaultStats.damage;
        _movementSpeed = _enemyDefaultStats.movementSpeed;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
    {
        var finalDamage = damage * (1 - _enemyDefaultStats.damageMitigation);
        _health -= finalDamage;
        
        var text = Instantiate(_damagetextPrefab, transform.position, Quaternion.identity);
        text.SetDamage(finalDamage);

        if (_health <= 0 && _isAlive)
        {
            _rigidbody.velocity = Vector2.zero;
            _isAlive = false;
            GetComponent<Animator>().SetTrigger("Death");
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}