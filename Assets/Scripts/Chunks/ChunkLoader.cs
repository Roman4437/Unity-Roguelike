using System.Collections.Generic;
using UnityEngine;

public class ChunkLoader : MonoBehaviour
{
    [SerializeField] private List<GameObject> _chunkPrefabs;

    private Dictionary<Vector2, GameObject> _chunks = new ();
    private Vector2 _lastChunk = Vector2.zero;
    private Vector2 _currentChunk = Vector2.zero;

    private void Start()
    {
        LoadChunks(Vector2.zero);
    }
    
    private void Update()
    {
        CheckCurrentChunk();
        UpdateActiveChunks();
    }

    private void CheckCurrentChunk()
    {
        _currentChunk = ConvertVectorToChunkCoordinate(transform.position.x, transform.position.y);
    }

    private void UpdateActiveChunks()
    {
        if (_currentChunk != _lastChunk)
        {
            _lastChunk = _currentChunk;
            foreach (var chunk in _chunks)
            {
                if(Vector2.Distance(chunk.Key, _currentChunk) > 1.5f)
                {
                    chunk.Value.SetActive(false);
                }
            }

            LoadChunks(_currentChunk);
        }
    }

    private void LoadChunks(Vector2 chunk)
    {
        var offsets = new Vector2[]
        {
            new (0f, 0f),
            new (-20f, 0f),
            new (20f, 0f),
            new (-20f, 20f),
            new (20f, 20f),
            new (0f, 20f),
            new (0f, -20f),
            new (-20f, -20f),
            new (20f, -20f)
        };

        foreach (var offset in offsets)
        {
            _chunks.TryGetValue(chunk + offset / 20f, out var chunkInstance);

            if (chunkInstance != null)
            {
                chunkInstance.SetActive(true);
            }
            else
            {
                var index = Random.Range(0, _chunkPrefabs.Count);
                var newChunkInstance = Instantiate(_chunkPrefabs[index], chunk * 20f + offset, Quaternion.identity);
                _chunks.Add(chunk + offset / 20f, newChunkInstance);
            }
        }
    }

    private Vector2 ConvertVectorToChunkCoordinate(float x, float y)
    {
        var worldX = x * 0.05f;
        var worldY = y * 0.05f;

        var signX = Mathf.Sign(x);
        var signY = Mathf.Sign(y);
        
        var roundedX = Mathf.Round(Mathf.Abs(worldX));
        var roundedY = Mathf.Round(Mathf.Abs(worldY));
        
        var chunkX = roundedX * signX;
        var chunkY = roundedY * signY;
        
        return new Vector2(chunkX, chunkY);
    }
}
