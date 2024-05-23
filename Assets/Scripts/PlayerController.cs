using Cinemachine;
using DG.Tweening;
using UnityEngine;
using Utilities;

public class PlayerController : LocalSingleton<PlayerController>
{
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _centeringSpeed = 2.0f;

    [Space] [Header("REFS")] 
    [SerializeField] private Transform _modelTransform;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _collider;
    [SerializeField] private SimpleAnimancer _simpleAnimancer;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private bool _canMove = false;

    public void Init()
    {
        _modelTransform.localPosition = Vector3.zero;
        _modelTransform.localEulerAngles = Vector3.zero;
        _canMove = false;
        _simpleAnimancer.PlayAnimation("Idle");
    }
    private void Start()
    {
        GameManager.OnGameplayStarted += StartMoving;
        GameManager.OnGameplayEnded += StopMoving;
    }

    private void OnDestroy()
    {
        GameManager.OnGameplayStarted -= StartMoving;
        GameManager.OnGameplayEnded -= StopMoving;
    }

    private void FixedUpdate()
    {
        if (!_canMove) return;
        
        Vector3 forwardMovement = new Vector3(0, 0, 1) * (_speed * Time.deltaTime);
        _rb.MovePosition(_rb.position + forwardMovement);
        
        if (PlatformSnap.Instance.GetLastSnappedPlatform() != null)
        {
            var targetX = PlatformSnap.Instance.GetLastSnappedPlatform().GetComponent<SliceableObject>().GetCenterXPoint();
            var targetPosition = new Vector3(targetX, _rb.position.y, _rb.position.z);
            var newPosition = Vector3.Lerp(_rb.position, targetPosition, _centeringSpeed * Time.deltaTime);
            _rb.MovePosition(newPosition);
        }
        
        CheckIsFalling();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collectible>())
        {
            var collectible = other.GetComponent<Collectible>();
            collectible.Collect();
        }
        else if (other.CompareTag("Finish"))
        {
            var finishLine = other.GetComponentInParent<FinishLine>();
            if (!finishLine.GetIsTriggered())
            {
                finishLine.SetIsTriggered(true);
                GameManager.Instance.FinishGamePlay(true);
                transform.DOMoveX(other.transform.position.x, 0.25f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    _simpleAnimancer.PlayAnimation("Dance");
                    finishLine.PlatformRiseUp();
                    PlatformSnap.Instance.SetLastSnappedPlatform(finishLine.GetPlatform());
                });
            }
        }
    }
    public void StartMoving()
    {
        _canMove = true;
        _simpleAnimancer.PlayAnimation("Run");
    }

    private void StopMoving()
    {
        _canMove = false;
    }

    private void CheckIsFalling()
    {
        if (_rb.velocity.y < -0.5f)
        {
            _virtualCamera.Follow = null;
            _virtualCamera.LookAt = null;
            _simpleAnimancer.PlayAnimation("Fall");
            GameManager.Instance.FinishGamePlay(false);
        }
    }
    public SimpleAnimancer GetSimpleAnimancer() => _simpleAnimancer;
}
