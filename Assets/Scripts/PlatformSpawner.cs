using Ali.Helper;
using UnityEngine;

public class PlatformSpawner : LocalSingleton<PlatformSpawner>
{
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private float _platformSpeed = 3.0f;
    [SerializeField] private Transform _initialLastPlatform;

    private Transform _lastSpawnedPlatform;
    private bool _spawnFromLeft = true;

    protected override void Awake()
    {
        base.Awake();
        _lastSpawnedPlatform = _initialLastPlatform;
        SpawnPlatform(5.5f); // Başlangıçta hemen bir platform spawnla
    }

    public void SpawnPlatform(float zOffset = 2.75f)
    {
        Vector3 spawnPosition = _lastSpawnedPlatform.position + new Vector3(0, 0, zOffset);
        float side = _spawnFromLeft ? -10 : 10;
        var newPlatform = PoolManager.Instance.SpawnPlatform();
        if(newPlatform)
        {
            newPlatform.transform.SetParent(PoolManager.Instance.GetPlatformPool().transform);
            newPlatform.transform.position = spawnPosition;
            newPlatform.transform.rotation = Quaternion.identity;
        }
        //GameObject newPlatform = Instantiate(_platformPrefab, spawnPosition, Quaternion.identity);
        newPlatform.GetComponent<PlatformMover>().SetSpeed(_platformSpeed);
        newPlatform.GetComponent<PlatformMover>().MoveFromSide(side);

        _lastSpawnedPlatform = newPlatform.transform;
        _spawnFromLeft = !_spawnFromLeft;
    }

    public Transform GetLastSpawnedPlatform() => _lastSpawnedPlatform;

    public float GetPlatformSpeed() => _platformSpeed;
}