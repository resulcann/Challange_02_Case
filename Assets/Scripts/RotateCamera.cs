using UnityEngine;
using Cinemachine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float maxBias = 360f;

    private CinemachineOrbitalTransposer _orbitalTransposer;
    private bool _canRotate = false;

    private void Awake()
    {
        _orbitalTransposer = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    private void Update()
    {
        if (_orbitalTransposer != null && _canRotate)
        {
            _orbitalTransposer.m_XAxis.Value += rotationSpeed * Time.deltaTime;
            
            if (_orbitalTransposer.m_XAxis.Value > maxBias)
            {
                _orbitalTransposer.m_XAxis.Value -= maxBias;
            }
        }
    }

    public void StartRotating() => _canRotate = true;
    public void Reset()
    {
        _canRotate = false;
        _orbitalTransposer.m_XAxis.Value = 0;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0f, 0f);
    }
}