using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateControllerInstaller : Installer
{
    [SerializeField]
    private GameStateController _gameStateController;

    public override void Install(ServiceLocator serviceLocator)
    {
        ServiceLocator.Instance.RegisterService(_gameStateController);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<GameStateController>();
    }
}
