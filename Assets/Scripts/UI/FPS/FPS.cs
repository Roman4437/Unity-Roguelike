using UnityEngine;
using UnityEngine.UIElements;

public class FPS : MonoBehaviour
{
    private Label _fpsText; 
    private float deltaTime;

    private void Start()
    {
        _fpsText = GetComponent<UIDocument>().rootVisualElement.Q<Label>("FPS");
        InvokeRepeating(nameof(UpdateFPS), 1f, 1f);
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        }
    }

    private void UpdateFPS()
    {
        var fps = 1.0f / deltaTime;
        _fpsText.text = string.Format("{0:0.} FPS", fps);
    }
}
