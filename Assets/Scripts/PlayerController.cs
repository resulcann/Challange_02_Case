using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    [Space] [Header("REFS")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Collider _collider;
    
    [SerializeField] private Transform _targetPlatform;
    [SerializeField] private float _centeringSpeed = 2.0f;

    private void Update()
    {
        Vector3 forwardMovement = new Vector3(0, 0, 1) * (_speed * Time.deltaTime);
        _rb.MovePosition(_rb.position + forwardMovement);
        
        if (_targetPlatform != null)
        {
            float targetX = _targetPlatform.position.x;
            Vector3 targetPosition = new Vector3(targetX, _rb.position.y, _rb.position.z);
            Vector3 newPosition = Vector3.Lerp(_rb.position, targetPosition, _centeringSpeed * Time.deltaTime);
            _rb.MovePosition(newPosition);
        }
    }

    public void Fall()
    {
        _rb.useGravity = true;
        _collider.enabled = false;
    }
}