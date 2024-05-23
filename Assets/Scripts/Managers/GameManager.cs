using System;
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
    public static event GameManagerEvent OnNewLevel;
    
    public bool IsGamePlayFinished { get; set; } = false;
    public bool IsGamePlayStarted { get; set; } = false;
    private int _levelIndex = 0;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Init();
    }

    public void Init()
    {
        PlayerController.Instance.Init();
        CurrencyManager.Instance.Init();
        UIManager.Instance.Init();
        SetGamePlayCam();
        PlatformSnap.Instance.ResetCombo();
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
        PlatformSpawner.Instance.SpawnPlatform(2.75f,true);
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
            IncreaseLevel();
            SetLevelEndCam();
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

    private void IncreaseLevel()
    {
        _levelIndex++;
    }

    private void SetGamePlayCam()
    {
        var rotatorCam = _levelEndCam.GetComponent<RotateCamera>();
        rotatorCam.Reset();

        _gamePlayCam.Priority = 10;
        _levelEndCam.Priority = 9;

        _gamePlayCam.Follow = PlayerController.Instance.transform;
        _gamePlayCam.LookAt = PlayerController.Instance.transform;
    }

    private void SetLevelEndCam()
    {
        var rotatorCam = _levelEndCam.GetComponent<RotateCamera>();
        rotatorCam.StartRotating();
        
        _gamePlayCam.Priority = 9;
        _levelEndCam.Priority = 10;
    }

    public int GetLevelIndex() => _levelIndex;
}
