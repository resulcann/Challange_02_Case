using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Utilities
{
    public class SimpleMoveLoop1D : MonoBehaviour
    {
        [SerializeField] private float _targetYPos;
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _delayDuration = 0f;
        [SerializeField] private bool _isLocalPos;

        private void Start()
        {
            StartCoroutine(LoopProcess());
        }

        private IEnumerator LoopProcess()
        {
            yield return new WaitForSeconds(_delayDuration);
            while(true)
            {
                if(_isLocalPos)
                {
                    yield return transform.DOLocalMoveY(_targetYPos, _moveSpeed).SetSpeedBased().SetRelative().SetLoops(-1, LoopType.Yoyo).WaitForCompletion();
                }
                else
                {
                    yield return transform.DOMoveY(_targetYPos, _moveSpeed).SetSpeedBased().SetRelative().SetLoops(-1, LoopType.Yoyo).WaitForCompletion();
                }
            }
        }
    }
}