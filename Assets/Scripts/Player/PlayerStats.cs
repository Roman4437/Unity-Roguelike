using System;
using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maximumHealth;
    [SerializeField] private float _damageMitigation;
    
    private float _currentHealth;
    private float _invulnerabilityTimeOnDamageTaken = 0.5f;
    private bool _isPlayerDead;
    private bool _isPlayerInvulnerable;

    public static event Action OnPlayerDeath;
    public static event Action OnPlayerDamageTaken;

    private void Start()
    {
        _currentHealth = _maximumHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_isPlayerInvulnerable == false)
        {
            _currentHealth -= damage * (1 - _damageMitigation);
            
            if (_currentHealth <= 0 && _isPlayerDead == false)
            {
                _isPlayerDead = true;
                Die();
            }

            if (_currentHealth > 0)
            {
                OnPlayerDamageTaken.Invoke();
            }
            
            StartCoroutine(MakeInvulnerableForSeconds(_invulnerabilityTimeOnDamageTaken));
        }
    }

    public void Die()
    {
        OnPlayerDeath.Invoke();
    }

    public IEnumerator MakeInvulnerableForSeconds(float time)
    {
        _isPlayerInvulnerable = true;
        yield return new WaitForSeconds(time);
        _isPlayerInvulnerable = false;
    }
}
