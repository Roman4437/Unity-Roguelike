using System;
using System.Collections;
using UnityEngine;

public class BasicProjectileCast : MonoBehaviour
{
    [SerializeField] private BasicProjectile _projectilePrefab;
    [SerializeField] private Transform _castPoint;
    [SerializeField] private int _projectileCount;
    [SerializeField, Min(0.15f)] private float _castSpeed;
    [SerializeField, Min(0.15f)] private float _skillCooldown;

    private bool _isSkillReady = true;
    private bool _isAbleCasting = true;
    private const float _projectileSpreadAngle = 18f;

    public static event Action<float> OnCast;

    private void OnEnable()
    {
        PlayerStats.OnPlayerDeath += DisableCasting;
        GameState.OnGameStateChange += UpdateCasting;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDeath -= DisableCasting;
        GameState.OnGameStateChange -= UpdateCasting;
    }
    
    private void LaunchProjectiles()
    {
        var cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        FlipPlayerOnCastingInOppositeDirection(cursor);
        
        var direction = cursor - _castPoint.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        var halfCount = _projectileCount / 2;
        var extraProjectile = _projectileCount % 2;
        
        for (var i = -halfCount; i < halfCount + extraProjectile; i++)
        {
            var coefficient = (1 - extraProjectile) * _projectileSpreadAngle / 2;
            var rotation = Quaternion.Euler(0f, 0f, angle + coefficient + i * _projectileSpreadAngle);
            
            var projectile = Instantiate(_projectilePrefab, _castPoint.position, rotation);
            projectile.Launch(Quaternion.AngleAxis(coefficient + i * _projectileSpreadAngle, Vector3.forward) * direction);  
        }
    }

    public void ActivateSkill()
    {
        if (_isSkillReady && _isAbleCasting)
        {
            OnCast.Invoke(_castSpeed);
            LaunchProjectiles();
            StartCoroutine(StartSkillCooldown());
        }
    }

    private IEnumerator StartSkillCooldown()
    {
        _isSkillReady = false;
        yield return new WaitForSeconds(_skillCooldown);
        _isSkillReady = true;
    }
    
    private void FlipPlayerOnCastingInOppositeDirection(Vector2 cursor)
    {
        if (cursor.x < transform.position.x)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else 
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }

    private void UpdateCasting()
    {
        _isAbleCasting = !_isAbleCasting;
    }

    private void DisableCasting()
    {
        _isAbleCasting = false;
        GameState.OnGameStateChange -= UpdateCasting;
    }
}