using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private float _spawnRate = 2.0f;
    [SerializeField] private float _platformSpeed = 3.0f;
    private GameObject _currentPlatform;

    private void Start()
    {
        InvokeRepeating("SpawnPlatform", 0, _spawnRate);
    }

    private void SpawnPlatform()
    {
        if (_currentPlatform != null) return;
        var platform = Instantiate(_platformPrefab, new Vector3(0, 0, -10), Quaternion.identity);
        platform.GetComponent<PlatformMover>().SetSpeed(_platformSpeed);
        _currentPlatform = platform;
    }
}