﻿using UnityEngine;

public class GlobalInstaller : GeneralInstaller
{
    protected override void DoStart()
    {
        ServiceLocator.Instance.GetService<CommandQueue>().AddCommand(new LoadSceneCommand("MenuScene"));
        ISerializer serializer = new JsonUtilityAdapter();
        PlayerPrefsDataStoreAdapter playerPrefsDataStoreAdapter = new PlayerPrefsDataStoreAdapter(serializer);
        ScoreSystem scoreSystem = new ScoreSystem(playerPrefsDataStoreAdapter);
        ServiceLocator.Instance.RegisterService<IScoreSystem>(scoreSystem);
    }

    protected override void DoInstallDependencies()
    {
        ServiceLocator.Instance.RegisterService<CommandQueue>(CommandQueue.Instance);
    }

    protected override void DoOnEnable()
    {
       
    }

    public override void DoInstallDependenciesOnCommand()
    {
    }
}
