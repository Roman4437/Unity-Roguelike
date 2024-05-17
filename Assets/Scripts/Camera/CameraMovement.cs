using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private void LateUpdate()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y, -10f);
    }
}