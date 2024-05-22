using UnityEngine;

namespace Utilities
{
    public class Follower : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [Space]
        [SerializeField] private bool _followPosition;
        [SerializeField] private Vector3 _positionOffset;
        [Space]
        [SerializeField] private bool _followRotation;
    
        private void Update()
        {
            if(_target)
            {
                if(_followPosition)
                {
                    transform.position = _target.position + _positionOffset;
                }
                if (_followRotation)
                {
                    transform.rotation = _target.rotation;
                }
            }
        }
    }
}