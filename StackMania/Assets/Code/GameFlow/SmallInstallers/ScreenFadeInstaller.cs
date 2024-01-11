using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFadeInstaller : Installer
{
    [SerializeField]
    private ScreenFade _screenFade;

    public override void Install(ServiceLocator serviceLocator)
    {
        ServiceLocator.Instance.RegisterService(_screenFade);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<ScreenFade>();
    }
}
