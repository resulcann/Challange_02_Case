using UnityEngine;
using Utilities;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int _amount;
    [Space] 
    [SerializeField] private ParticleSystem _collectParticle;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private SimpleMoveLoop1D _simpleLoop;
    [SerializeField] private AudioClip _collectClip;
    
    private bool _isCollected = false;
    
    public void Collect()
    {
        if (_isCollected) return;
        
        _isCollected = true;
        _renderer.enabled = false;
        _rotator.enabled = false;
        _simpleLoop.enabled = false;
        _collectParticle?.Play(true);
        AudioPool.Instance.PlayClip(_collectClip, false, 0.1f);
        CurrencyManager.Instance.DealCurrency(_amount);
    }
}
