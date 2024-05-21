using Ali.Helper;
using UnityEngine;

public class PlayerController : LocalSingleton<PlayerController>
{
    [SerializeField] private float _speed = 5.0f;
    [Space] [Header("REFS")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _centeringSpeed = 2.0f;


    private void FixedUpdate()
    {
        Vector3 forwardMovement = new Vector3(0, 0, 1) * (_speed * Time.deltaTime);
        _rb.MovePosition(_rb.position + forwardMovement);
        
        if (PlatformSnap.Instance.GetLastSnappedPlatform() != null)
        {
            var targetX = PlatformSnap.Instance.GetLastSnappedPlatform().GetComponent<SliceableObject>().GetCenterXPoint();
            var targetPosition = new Vector3(targetX, _rb.position.y, _rb.position.z);
            var newPosition = Vector3.Lerp(_rb.position, targetPosition, _centeringSpeed * Time.deltaTime);
            _rb.MovePosition(newPosition);
        }
    }
}
