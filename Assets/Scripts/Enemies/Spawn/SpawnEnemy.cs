using System;
using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private BasicEnemy _monsterPrefab;
    
    private float _spawnInterval = 10f;
    private bool _isAbleSpawning = true;

    private void OnEnable()
    {
        PlayerStats.OnPlayerDeath += DisableSpawn;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDeath -= DisableSpawn;
    }

    private void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        while (_isAbleSpawning)
        {
            Instantiate(_monsterPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void DisableSpawn()
    {
        _isAbleSpawning = false;
    }
}