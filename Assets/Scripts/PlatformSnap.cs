using Ali.Helper;
using UnityEngine;
using DG.Tweening;

public class PlatformSnap : LocalSingleton<PlatformSnap>
{
    [SerializeField] private MeshSlicer _meshSlicer;
    [SerializeField] private Transform _targetPlatform;
    [SerializeField] private float _snapTolerance = 0.1f;
    [SerializeField] private Transform _lastSnappedPlatform;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _targetPlatform = PlatformSpawner.Instance.GetLastSpawnedPlatform();
            var distance = Mathf.Abs(_lastSnappedPlatform.position.x - _targetPlatform.position.x);
            
            if (distance <= _snapTolerance)
            {
                PerfectSnap();
                Debug.Log("Perfect Snap!");
            }
            else if(distance <= _lastSnappedPlatform.GetComponent<Collider>().bounds.size.x)
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
        _targetPlatform.DOMoveX(_lastSnappedPlatform.position.x, 1f).SetSpeedBased().SetEase(Ease.Linear);
        SetLastSnappedPlatform(_targetPlatform);
    }

    private void NormalSnap()
    {
        _targetPlatform.DOKill();
        _targetPlatform.DOMoveX(_targetPlatform.position.x, 1f).SetSpeedBased().SetEase(Ease.Linear);

        var sliceableObject = _targetPlatform.GetComponent<SliceableObject>();
        sliceableObject.Slice(_meshSlicer);
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
        PlatformSpawner.Instance.SpawnPlatform();
    }
}
