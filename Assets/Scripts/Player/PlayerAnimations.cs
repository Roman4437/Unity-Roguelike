using System;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);

    private bool _isMoving;

    private Animator _animator;

    private void OnEnable()
    {
        PlayerStats.OnPlayerDeath += TriggerDeath;
        PlayerStats.OnPlayerDamageTaken += TriggerHurt;
        BasicProjectileCast.OnCast += TriggerCast;
        GameState.OnGameStateChange += UpdateAnimatorState;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDeath -= TriggerDeath;
        PlayerStats.OnPlayerDamageTaken -= TriggerHurt;
        BasicProjectileCast.OnCast -= TriggerCast;
        GameState.OnGameStateChange -= UpdateAnimatorState;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimateMovement();
    }

    private void UpdateMovementState()
    {
        var axisX = Input.GetAxisRaw(Horizontal);
        var axisY = Input.GetAxisRaw(Vertical);

        if (axisX != 0 || axisY != 0)
        {
            _isMoving = true;
        }
        else
        { 
            _isMoving = false;
        }
    }

    private void AnimateMovement()
    {
        UpdateMovementState();
        _animator.SetBool("Walk", _isMoving);
    }

    private void TriggerCast(float castSpeed)
    {
        var animationSpeed = 0.15f / castSpeed;
        
        _animator.SetFloat("CastSpeed", animationSpeed);
        _animator.SetTrigger("Cast");
    }

    private void TriggerDeath()
    {
        _animator.SetTrigger("Death");
    }

    private void TriggerHurt()
    {
        _animator.SetTrigger("Hurt");
    }

    private void UpdateAnimatorState()
    {
        _animator.enabled = !_animator.enabled;
    }
}