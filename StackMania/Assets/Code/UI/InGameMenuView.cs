using System;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenuView : MonoBehaviour, IEventObserver, IInGameMenuMediator
{
    [SerializeField]
    private Button _pauseButton;
    [SerializeField]
    private PauseView _pauseView;
    [SerializeField]
    private GameOverView _gameOverView;
    [SerializeField]
    private Button _initButton;

    private CommandQueue _commandQueue;

    private void Awake()
    {
        _pauseButton.onClick.AddListener(OnPauseGamePressed);
        _pauseButton.enabled = false;
        _pauseView.Configure(this);
        _gameOverView.Configure(this);
        _initButton.onClick.AddListener(OnInitGamePressed);
    }

    private void Start()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EventIds.GameOver, this);
        _commandQueue = ServiceLocator.Instance.GetService<CommandQueue>();
    }

    private void HideAllViews()
    {
        _pauseView.Hide();
        _gameOverView.Hide();
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().Unsubscribe(EventIds.GameOver, this);
    }

    public void OnBackToMenuPressed()
    {
        _commandQueue.AddCommand(new LoadSceneCommand("MenuScene"));
        _commandQueue.AddCommand(new ResumeGameCommand());
        HideAllViews();
    }

    public void OnPauseGamePressed()
    {
        _commandQueue.AddCommand(new PauseGameCommand());
        _pauseView.Show();
    }

    public void OnRestartGamePressed()
    {
        HideAllViews();
        _commandQueue.AddCommand(new RestartGameCommand());
        _initButton.gameObject.SetActive(true);
        _initButton.enabled = true;
        _pauseButton.enabled = false;
    }

    public void OnResumeGamePressed()
    {
        _commandQueue.AddCommand(new ResumeGameCommand());
        _pauseView.Hide();
    }

    public void OnInitGamePressed()
    {
        _initButton.gameObject.SetActive(false);
        _initButton.enabled = false;
        _pauseButton.enabled = true;
        ServiceLocator.Instance.GetService<CubesSpawner>().Init();
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.GameOver)
        {
            _gameOverView.Show();
        }
    }
}

