using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class UIManager : LocalSingleton<UIManager>
{
    [Space][Header("Finish UI")]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private Transform _tapToStartButton;
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    
    [Space]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Transform _cashIconTransform;

    private Tweener _punchImageTweener;
    private Tweener _smoothTween;
    private Tweener _rotationTweener;

    public void Init()
    {
        _currentLevelText.text = $"Level {GameManager.Instance.GetLevelIndex() + 1}";
        _tapToStartButton.gameObject.SetActive(true);
        ShowLosePanel(false);
        ShowWinPanel(false);
    }
    
    public void ShowWinPanel(bool show)
    {
        _winPanel.SetActive(show);
    }

    public void ShowLosePanel(bool show)
    {
        _losePanel.SetActive(show);
    }
    public void OnNextButtonClick()
    {
        if (GameManager.Instance.GetLevelIndex() > 2)
        {
            SceneManager.LoadScene(0);
            return;
        }
        GameManager.Instance.Init();
    }

    public void OnTryAgainButtonClick()
    {
        SceneManager.LoadScene(0);
    }

    public void OnTapToPlayClick()
    {
        _tapToStartButton.gameObject.SetActive(false);
        GameManager.Instance.StartGameplay();
    }
    public void PunchImage(Transform image ,float punchScale, float duration)
    {
        _punchImageTweener?.Complete();
    
        _punchImageTweener = image.DOPunchScale(image.localScale * punchScale, duration);
    }
    public Canvas GetCanvas() => _canvas;
}
