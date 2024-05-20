using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameState : MonoBehaviour
{
    private bool _isGamePaused;

    public static event Action OnGameStateChange;
    
    private void OnEnable()
    {
        Pause.OnResumeButtonClick += UpdateState;
    }
    
    private void OnDisable()
    {
        Pause.OnResumeButtonClick -= UpdateState;
    }

    private  void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateState();
        }
    }

    private void UpdateState()
    {
        _isGamePaused = !_isGamePaused;

        if (_isGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        
        OnGameStateChange.Invoke();
    }
}
