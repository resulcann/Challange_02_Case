using Lean.Pool;
using UnityEngine;
using Utilities;

public class PoolManager : LocalSingleton<PoolManager>
{
    [SerializeField] private LeanGameObjectPool _platformPool;
    
    public GameObject SpawnPlatform()
    {
        GameObject result = null;
        _platformPool.TrySpawn(ref result);
        return result;
    }
    public void DespawnPlatform(GameObject platform) { _platformPool.Despawn(platform); }

    public LeanGameObjectPool GetPlatformPool() => _platformPool;
}
