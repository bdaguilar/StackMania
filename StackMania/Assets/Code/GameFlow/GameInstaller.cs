using UnityEngine;

public class GameInstaller : GeneralInstaller
{
    [SerializeField]
    private PauseView _pauseMenu;
    [SerializeField]
    private GameOverView _gameOverView;
    [SerializeField]
    private CubesConfiguration _cubesConfiguration;

    protected override void DoInstallDependencies()
    {
        InstallCubeFactory();
    }
    private void InstallCubeFactory()
    {
        CubeFactory cubeFactory = new CubeFactory(Instantiate(_cubesConfiguration));
        ServiceLocator.Instance.RegisterService(cubeFactory);
    }

    protected override void DoOnEnable()
    {
    }

    protected override void DoStart()
    {
        ServiceLocator.Instance.RegisterService(_pauseMenu);
        ServiceLocator.Instance.RegisterService(_gameOverView);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<PauseView>();
        ServiceLocator.Instance.UnregisterService<GameOverView>();
        ServiceLocator.Instance.UnregisterService<CubeFactory>();
    }

    public override void DoInstallDependenciesOnCommand()
    {
    }
}