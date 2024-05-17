using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed;
    [SerializeField] protected float _projectileDamage;
    
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction)
    {
        _rigidbody.velocity = direction.normalized * _projectileSpeed;
    }
}