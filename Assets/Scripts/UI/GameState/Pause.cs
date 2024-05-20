using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
   private VisualElement _root;
   private VisualElement _pauseElement;
   
   private Button _resume;
   private Button _exitGame;
   private Button _mainMenu;

   public static event Action OnResumeButtonClick;

   private void OnEnable()
   {
      _root = GetComponent<UIDocument>().rootVisualElement;
      
      _pauseElement = _root.Q<VisualElement>(className: "pause-menu");
      
      _resume = _root.Q<Button>("Resume");
      _exitGame = _root.Q<Button>("ExitGame");
      _mainMenu = _root.Q<Button>("MainMenu");
      
      _resume.clicked += ResumeGame;
      _exitGame.clicked += Quit;
      _mainMenu.clicked += ExitMainMenu;

      GameState.OnGameStateChange += TogglePauseClass;
   }

   private void OnDisable()
   {
      _resume.clicked -= ResumeGame;
      _exitGame.clicked -= Quit;
      _mainMenu.clicked -= ExitMainMenu;
      
      GameState.OnGameStateChange -= TogglePauseClass;
   }

   private void ResumeGame()
   {
      OnResumeButtonClick.Invoke();
   }

   private void Quit()
   {
      Application.Quit();
   }

   private void ExitMainMenu()
   {
      ResumeGame();
      Scenes.Instance.LoadMenu();
   }

   private void TogglePauseClass()
   {
      _pauseElement.ToggleInClassList("pause-menu-active");
   }
}
