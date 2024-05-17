using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
   private Button _resume;
   private Button _exitGame;
   private Button _mainMenu;

   public static event Action OnResumeButtonClick;

   private void Start()
   {
      gameObject.SetActive(false);
   }

   private void OnEnable()
   {
      _resume = GetComponent<UIDocument>().rootVisualElement.Q<Button>("Resume");
      _exitGame = GetComponent<UIDocument>().rootVisualElement.Q<Button>("ExitGame");
      _mainMenu = GetComponent<UIDocument>().rootVisualElement.Q<Button>("MainMenu");
      
      _resume.clicked += ResumeGame;
      _exitGame.clicked += Quit;
      _mainMenu.clicked += ExitMainMenu;
   }

   private void OnDisable()
   {
      _resume.clicked -= ResumeGame;
      _exitGame.clicked -= Quit;
      _mainMenu.clicked -= ExitMainMenu;
   }

   private void ResumeGame()
   {
      OnResumeButtonClick.Invoke();
      gameObject.SetActive(false);
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
}
