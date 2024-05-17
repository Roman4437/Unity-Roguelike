using System;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private GameObject _ui;
    
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
        Time.timeScale = Convert.ToInt32(!_isGamePaused);
        OnGameStateChange.Invoke();
        _ui.SetActive(!_ui.activeSelf);
    }
}
