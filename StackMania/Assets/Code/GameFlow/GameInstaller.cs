using UnityEngine;

public class GameInstaller : GeneralInstaller
{
    [SerializeField]
    private GameStateController _gameStateController;
    [SerializeField]
    private ScreenFade _screenFade;
    [SerializeField]
    private Menu _menu;
    [SerializeField]
    private PauseView _pauseMenu;
    [SerializeField]
    private GameOverView _gameOverView;
    [SerializeField]
    private CubesSpawner _cubeSpawner;
    [SerializeField]
    private CubesConfiguration _cubesConfiguration;


    protected override void DoInstalDependencies()
    {
        InstallCubeFactory();
    }
    private void InstallCubeFactory()
    {
        CubeFactory cubeFactory = new CubeFactory(Instantiate(_cubesConfiguration));
        ServiceLocator.Instance.RegisterService(cubeFactory);
    }

    protected override void DoStart()
    {
        ServiceLocator.Instance.RegisterService(_gameStateController);
        ServiceLocator.Instance.RegisterService(_screenFade);
        ServiceLocator.Instance.RegisterService(_menu);
        ServiceLocator.Instance.RegisterService(_pauseMenu);
        ServiceLocator.Instance.RegisterService(_gameOverView);
        ServiceLocator.Instance.RegisterService(_cubeSpawner);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<GameStateController>();
        ServiceLocator.Instance.UnregisterService<ScreenFade>();
        ServiceLocator.Instance.UnregisterService<Menu>();
        ServiceLocator.Instance.UnregisterService<PauseView>();
        ServiceLocator.Instance.UnregisterService<GameOverView>();
        ServiceLocator.Instance.UnregisterService<CubesSpawner>();
    }
}