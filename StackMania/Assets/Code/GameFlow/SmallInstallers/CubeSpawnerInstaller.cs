using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawnerInstaller : Installer
{
    [SerializeField]
    private CubesSpawner _cubeSpawner;

    public override void Install(ServiceLocator serviceLocator)
    {
        ServiceLocator.Instance.RegisterService(_cubeSpawner);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<CubesSpawner>();
    }
}
