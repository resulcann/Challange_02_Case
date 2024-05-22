using System.Collections;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using Utilities;

public class GameManager : LocalSingleton<GameManager>
{
    [SerializeField] private CinemachineVirtualCamera _gamePlayCam;
    [SerializeField] private CinemachineVirtualCamera _levelEndCam;
    
    public delegate void GameManagerEvent();
    public static event GameManagerEvent OnGameplayStarted;
    public static event GameManagerEvent OnGameplayEnded;
    
    public bool IsGamePlayFinished { get; set; } = false;
    public bool IsGamePlayStarted { get; set; } = false;

    private void Start()
    {
        Application.targetFrameRate = 60;
        CurrencyManager.Instance.Init();
        UIManager.Instance.Init();
    }
    private void OnDestroy()
    {
        DOTween.KillAll();
    }
    
    private void OnApplicationQuit()
    {
        DOTween.KillAll();
    }
    public void StartGameplay() // GAMEPLAY START
    {
        IsGamePlayStarted = true;
        IsGamePlayFinished = false;
        PlatformSpawner.Instance.SpawnPlatform(5.5f);
        OnGameplayStarted?.Invoke();
    }

    public void FinishGamePlay(bool success)
    {
        if(IsGamePlayFinished)
        {
            return;
        }
        IsGamePlayFinished = true;

        if (success) // WIN
        {
            StartCoroutine(WinProcess());
            _levelEndCam.gameObject.SetActive(true);
            _gamePlayCam.gameObject.SetActive(false);
        }
        else // LOSE
        {
            UIManager.Instance.ShowLosePanel(true);
        }
        
        OnGameplayEnded?.Invoke();
    }

    private IEnumerator WinProcess()
    {
        PreWinPanel.Instance.Show(2f);
        yield return new WaitForSeconds(2f);
        UIManager.Instance.ShowWinPanel(true);
    }
}
