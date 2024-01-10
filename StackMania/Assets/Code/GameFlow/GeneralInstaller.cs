using System;
using UnityEngine;

public abstract class GeneralInstaller : MonoBehaviour
{
    [SerializeField]
    private Installer[] _installers;

    private void Awake()
    {
        InstallDependencies();
    }

    private void Start()
    {
        DoStart();
    }

    private void OnEnable()
    {
        DoOnEnable();
    }

    abstract protected void DoStart();

    abstract protected void DoOnEnable();

    abstract protected void DoInstallDependencies();

    abstract public void DoInstallDependenciesOnCommand();

    private void InstallDependencies()
    {
        foreach (Installer installer in _installers)
        {
            installer.Install(ServiceLocator.Instance);
        }
        DoInstallDependencies();
    }
}

