using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class CubeMediator : RecyclableObject, IEventObserver
{
    [SerializeField]
    private MovementController _movementController;

    private IInput _inputController;

    public Action<CubeMediator> OnRecycle;

    internal override void Init()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EventIds.GameOver, this);
        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EventIds.Victory, this);
        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EventIds.RestartGame, this);
    }

    internal override void Release()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().Unsubscribe(EventIds.GameOver, this);
        ServiceLocator.Instance.GetService<IEventQueue>().Unsubscribe(EventIds.Victory, this);
        ServiceLocator.Instance.GetService<IEventQueue>().Unsubscribe(EventIds.RestartGame, this);
    }

    public void Process(EventData eventData)
    {
        throw new NotImplementedException();
    }
}
