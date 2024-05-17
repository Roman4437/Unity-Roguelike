using UnityEngine;

public class MeleeEnemy : BasicEnemy
{
    private Transform _player;
    
    private bool _hasReachedPlayer;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
    }
    
    private void FixedUpdate()
    {
        if (_hasReachedPlayer == false && _isAlive)
        {
            FlipEnemyTowardsPlayer();
            MoveTowardsPlayer();
        }
    }
    
    private void MoveTowardsPlayer()
    {
        var direction = _player.position - transform.position;
        _rigidbody.velocity = direction.normalized * _movementSpeed;
    }

    private void FlipEnemyTowardsPlayer()
    {
        if (_player.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            _hasReachedPlayer = true;
        }
        
        var player = collider.gameObject.GetComponent<IDamageable>();

        if (player != null && _isAlive)
        {
            player.TakeDamage(_damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            _hasReachedPlayer = false;
        }
    }
}
