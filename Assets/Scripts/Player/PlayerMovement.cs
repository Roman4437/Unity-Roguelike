using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    [SerializeField] private float _speed;
    
    private bool _isAbleMoving = true;
    
    private Rigidbody2D _rigidbody;

    private void OnEnable()
    {
        PlayerStats.OnPlayerDeath += DisableMovement;
        BasicProjectileCast.OnCast += Cast;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDeath -= DisableMovement;
        BasicProjectileCast.OnCast -= Cast;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_isAbleMoving)
        {
            UpdateDirection();
            Move();
        }
    }

    private void Move()
    {
        var inputX = Input.GetAxisRaw(Horizontal);
        var inputY = Input.GetAxisRaw(Vertical);

        var direction = new Vector2(inputX, inputY);

        _rigidbody.velocity = direction.normalized * _speed;
    }

    public IEnumerator LockForSeconds(float seconds)
    {
        _rigidbody.velocity = Vector2.zero;
        _isAbleMoving = false;
        yield return new WaitForSeconds(seconds);
        _isAbleMoving = true;
    }
    
    private void UpdateDirection()
    {
        var inputX = Input.GetAxisRaw(Horizontal);
        
        if (inputX < 0)
        {
            transform.localScale = new Vector2(-1f, 1);
        }
        else if (inputX > 0)
        {
            transform.localScale = new Vector2(1f, 1);
        }
    }
    
    private void DisableMovement()
    {
        StopAllCoroutines();
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody.velocity = Vector2.zero;
        _isAbleMoving = false;
    }

    private void Cast(float seconds)
    {
        StartCoroutine(LockForSeconds(seconds));
    }
}