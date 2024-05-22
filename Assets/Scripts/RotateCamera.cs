using UnityEngine;
using Cinemachine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float maxBias = 360f;

    private CinemachineOrbitalTransposer _orbitalTransposer;

    private void Start()
    {
        _orbitalTransposer = virtualCamera.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    }

    private void Update()
    {
        if (_orbitalTransposer != null)
        {
            _orbitalTransposer.m_XAxis.Value += rotationSpeed * Time.deltaTime;
            
            if (_orbitalTransposer.m_XAxis.Value > maxBias)
            {
                _orbitalTransposer.m_XAxis.Value -= maxBias;
            }
        }
    }
}