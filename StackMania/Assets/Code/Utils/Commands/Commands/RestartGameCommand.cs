using System;
using System.Threading.Tasks;
using UnityEngine;

public class RestartGameCommand : ICommand
{
    public async Task Execute()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().EnqueueEvent(new EventData(EventIds.RestartGame));
        await new ResumeGameCommand().Execute();
        await new StopGameCommand().Execute();
        await new StartBattleFromMenuCommand().Execute();
        CubeMediator.LastCube = GameObject.Find("StartingCube").GetComponent<CubeMediator>();
    }
}

