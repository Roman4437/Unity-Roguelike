using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
   public static Scenes Instance;
   
   private void Awake()
   {
      Instance = this;
   }
   
   public void LoadNewGame()
   {
      SceneManager.LoadScene(Scene.Gameplay.ToString());
   }

   public void LoadMenu()
   {
      SceneManager.LoadScene(Scene.Menu.ToString());
   }
}
