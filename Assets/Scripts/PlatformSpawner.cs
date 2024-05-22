using UnityEngine;
using Utilities;

public class PlatformSpawner : LocalSingleton<PlatformSpawner>
{
    [SerializeField] private float _platformSpeed = 3.0f;
    [SerializeField] private Transform _initialLastPlatform;
    [SerializeField] private Material[] _platformMaterials;

    private Transform _lastSpawnedPlatform;
    private bool _spawnFromLeft = true;
    private int _materialIndex = 0;

    protected override void Awake()
    {
        base.Awake();
        _lastSpawnedPlatform = _initialLastPlatform;
    }
    
    public void SpawnPlatform(float zOffset = 2.75f)
    {
        Vector3 spawnPosition = _lastSpawnedPlatform.position + new Vector3(0, 0, zOffset);
        float side = _spawnFromLeft ? -10 : 10;
        var newPlatform = Instantiate(PlatformSnap.Instance.GetLastSnappedPlatform().gameObject, spawnPosition,
            Quaternion.identity, PoolManager.Instance.GetPlatformPool().transform);
        var platformRenderer = newPlatform.GetComponent<Renderer>();
        
        if (newPlatform.GetComponent<PlatformMover>() is null) newPlatform.AddComponent<PlatformMover>();
            newPlatform.GetComponent<PlatformMover>().SetSpeed(_platformSpeed);
        newPlatform.GetComponent<PlatformMover>().MoveFromSide(side);
        platformRenderer.sharedMaterial = _platformMaterials[_materialIndex];
        _materialIndex++;
        if (_materialIndex >= _platformMaterials.Length) _materialIndex = 0;

        _lastSpawnedPlatform = newPlatform.transform;
        _spawnFromLeft = !_spawnFromLeft;
    }

    public Transform GetLastSpawnedPlatform() => _lastSpawnedPlatform;

    public float GetPlatformSpeed() => _platformSpeed;
}