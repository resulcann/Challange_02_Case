using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    private float _speed;

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    void Update()
    {
        transform.position += Vector3.forward * (_speed * Time.deltaTime);
    }
}