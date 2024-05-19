using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameState : MonoBehaviour
{
    private VisualElement _pauseElement;
    
    private bool _isGamePaused;

    public static event Action OnGameStateChange;

    private void Start()
    {
        _pauseElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>(className: "PauseMenu");
    }

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

        _pauseElement.ToggleInClassList("Active");
        OnGameStateChange.Invoke();
    }
}
