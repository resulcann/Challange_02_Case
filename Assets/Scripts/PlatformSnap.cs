using Ali.Helper;
using UnityEngine;
using DG.Tweening;

public class PlatformSnap : LocalSingleton<PlatformSnap>
{
    [SerializeField] private Transform _targetPlatform;
    [SerializeField] private float _snapTolerance = 0.1f;
    [SerializeField] private Transform _lastSnappedPlatform;
    private bool _shouldSpawnPlatform = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _targetPlatform = PlatformSpawner.Instance.GetLastSpawnedPlatform();
            float distance = Mathf.Abs(transform.position.x - _targetPlatform.position.x);
            if (distance <= _snapTolerance)
            {
                PerfectSnap();
                Debug.Log("Perfect Snap!");
            }
            else if(distance < 2.75f)
            {
                NormalSnap();
                Debug.Log("Normal Snap!");
            }
            else
            {
                BadSnap();
                Debug.Log("Bad Snap!");
            }
        }
    }

    private void PerfectSnap()
    {
        _targetPlatform.DOKill();
        _targetPlatform.DOMoveX(_lastSnappedPlatform.position.x, 1f).SetSpeedBased().SetEase(Ease.OutBack);
        SetLastSnappedPlatform(_targetPlatform);
    }

    private void NormalSnap()
    {
        _targetPlatform.DOKill();
        _targetPlatform.DOMoveX(_targetPlatform.position.x, 1f).SetSpeedBased().SetEase(Ease.OutBack);
        SetLastSnappedPlatform(_targetPlatform);
    }
    private void BadSnap()
    {
        _targetPlatform.DOKill();
        _targetPlatform.GetComponent<Rigidbody>().isKinematic = false;

        // GAME LOSE YAPILACAK
    }
    public Transform GetLastSnappedPlatform() => _lastSnappedPlatform;
    public void SetLastSnappedPlatform(Transform lastPlatform)
    {
        _lastSnappedPlatform = lastPlatform;
        AllowNextPlatformSpawn();
        if (_shouldSpawnPlatform)
        {
            PlatformSpawner.Instance.SpawnPlatform(); // Snapleme işlemi tamamlandıktan sonra yeni platform spawn et
            _shouldSpawnPlatform = false;  // Bir sonraki snap'e kadar spawn engelle
        }
    }
     public void AllowNextPlatformSpawn()
    {
        _shouldSpawnPlatform = true;  // Snapleme işlemi tamamlandıktan sonra yeni spawn izni
    }
}
