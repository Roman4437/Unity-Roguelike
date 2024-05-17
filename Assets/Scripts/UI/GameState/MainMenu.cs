using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private Button _newGame;
    private Button _exitGame;

    private void OnEnable()
    {
        _newGame = GetComponent<UIDocument>().rootVisualElement.Q<Button>("NewGame");
        _exitGame = GetComponent<UIDocument>().rootVisualElement.Q<Button>("ExitGame");

        _newGame.clicked += NewGame;
        _exitGame.clicked += ExitGame;
    }

    private void OnDisable()
    {
        _newGame.clicked -= NewGame;
        _exitGame.clicked -= ExitGame;
    }

    private void NewGame()
    {
        Scenes.Instance.LoadNewGame();
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
