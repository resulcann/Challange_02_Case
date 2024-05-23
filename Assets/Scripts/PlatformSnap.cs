using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Utilities;

public class PlatformSnap : LocalSingleton<PlatformSnap>
{
    [SerializeField] private MeshSlicer _meshSlicer;
    [SerializeField] private Transform _targetPlatform;
    [SerializeField] private float _snapTolerance = 0.1f;
    [SerializeField] private Transform _lastSnappedPlatform;
    private int _perfectSnapCount = 0;
    private List<FinishLine> _finishLines = new();

    private void Start()
    {
        _finishLines = FindObjectsOfType<FinishLine>().ToList();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.Instance.IsGamePlayStarted && !GameManager.Instance.IsGamePlayFinished)
        {
            _targetPlatform = PlatformSpawner.Instance.GetLastSpawnedPlatform();
            var distance = Mathf.Abs(_lastSnappedPlatform.position.x - _targetPlatform.position.x);
            
            if (distance <= _snapTolerance)
            {
                Debug.Log("Perfect Snap!");
                PerfectSnap();
            }
            else if(distance <= _lastSnappedPlatform.GetComponent<Collider>().bounds.size.x)
            {
                Debug.Log("Normal Snap!");
                NormalSnap();
                ResetCombo();
            }
            else
            {
                Debug.Log("Bad Snap!");
                BadSnap();
                ResetCombo();
            }
        }
    }

    private void PerfectSnap()
    {
        var pitch = 1.0f + (0.05f * _perfectSnapCount);
        _perfectSnapCount++;
        
        _targetPlatform.DOKill();
        _targetPlatform.DOMoveX(_lastSnappedPlatform.position.x, 5f).SetSpeedBased().SetEase(Ease.Linear);
        SetLastSnappedPlatform(_targetPlatform);
        
        AudioPool.Instance.PlayClipByName("combo", false, 0.2f, pitch);
    }

    private void NormalSnap()
    {
        _targetPlatform.DOKill();
        _targetPlatform.DOMoveX(_targetPlatform.position.x, 5f).SetSpeedBased().SetEase(Ease.Linear);

        var sliceableObject = _targetPlatform.GetComponent<SliceableObject>();
        sliceableObject.Slice(_meshSlicer);
        AudioPool.Instance.PlayClipByName("slice", false, 0.2f, Random.Range(0.9f, 1.1f));
    }

    private void BadSnap()
    {
        _targetPlatform.DOKill();
        _targetPlatform.GetComponent<Rigidbody>().isKinematic = false;
        // GAME LOSE YAPILACAK
    }
    
    public void ResetCombo()
    {
        _perfectSnapCount = 0;
    }
    
    public Transform GetLastSnappedPlatform() => _lastSnappedPlatform;

    public void SetLastSnappedPlatform(Transform lastPlatform)
    {
        _lastSnappedPlatform = lastPlatform;
        // son snaplenen platform finishline'a yakınsa artık daha fazla platform spawnlama.
        if (_finishLines.Any(line => Mathf.Abs(line.transform.position.z - lastPlatform.position.z) < _lastSnappedPlatform.localScale.z)
            || GameManager.Instance.IsGamePlayFinished || !GameManager.Instance.IsGamePlayStarted)
        {
            return;
        }
        PlatformSpawner.Instance.SpawnPlatform();
    }
}
