using UnityEngine;

public class StopManagerInstaller : Installer
{
    [SerializeField]
    private StopManager _stopManager;
    [SerializeField]
    private JoyButton _joyButton;

    public override void Install(ServiceLocator serviceLocator)
    {
#if UNITY_EDITOR
        _stopManager.Configure(new UnityInputAdapter());
        Destroy(_joyButton);
        ServiceLocator.Instance.RegisterService(_stopManager);
        return;
#else
        _stopManager.Configure(new JoystickIInputAdapter(_joyButton));
        ServiceLocator.Instance.RegisterService(_stopManager);
        return;
#endif
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<StopManager>();
    }
}
