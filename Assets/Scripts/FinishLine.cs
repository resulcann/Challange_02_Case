using DG.Tweening;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private Transform _platform;
    private bool _isTriggered;


    public void PlatformRiseUp()
    {
        if (GameManager.Instance.GetLevelIndex() > 2) return;
        _platform.gameObject.SetActive(true);
        _platform.DOLocalMoveY(-0.5f, 1f).SetEase(Ease.OutBack);
    }    
    
    public bool GetIsTriggered() => _isTriggered;
    public void SetIsTriggered(bool value) => _isTriggered = value;
    public Transform GetPlatform() => _platform;
}
