using UnityEngine;
using UnityEngine.UI;

public class PauseView : BaseInGameView
{
    [SerializeField]
    private Button _resumeGameButton;
    [SerializeField]
    private Button _restartGameButton;
    [SerializeField]
    private Button _stopGameButton;


    private void Awake()
    {
        _resumeGameButton.onClick.AddListener(OnResumePressed);
        _restartGameButton.onClick.AddListener(OnRestartPressed);
        _stopGameButton.onClick.AddListener(OnBackToMainMenuPressed);
        gameObject.SetActive(false);
    }

    private void OnResumePressed()
    {
        _mediator.OnResumeGamePressed();
    }

    public override void Show()
    {
        gameObject.SetActive(true);
    }
}