using UnityEngine;
using DG.Tweening;

public class PlatformMover : MonoBehaviour
{
    private float _speed;

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }

    public void MoveFromSide(float startPositionX)
    {
        transform.position = new Vector3(startPositionX, transform.position.y, transform.position.z);
        float endPositionX = transform.position.x > 0 ? -20 : 20;
        transform.DOMoveX(endPositionX, _speed).SetRelative().SetSpeedBased().SetEase(Ease.Linear);
    }
}
