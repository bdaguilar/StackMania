using System;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class CubeMediator : RecyclableObject, IEventObserver, ICube
{
    public static CubeMediator CurrentCube { get; private set; }
    public static CubeMediator LastCube { get; private set; }

    [SerializeField]
    private CubeId _cubeId;
    [SerializeField]
    private MovementController _movementController;

    public Action<CubeMediator> OnRecycle;

    public string Id => _cubeId.Value;

    internal override void Init()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EventIds.GameOver, this);
        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EventIds.RestartGame, this);
    }

    internal override void Release()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().Unsubscribe(EventIds.GameOver, this);
        ServiceLocator.Instance.GetService<IEventQueue>().Unsubscribe(EventIds.RestartGame, this);
    }

    public void Configure(CubeConfiguration shipConfiguration)
    {
        _movementController.Configure(this, shipConfiguration.Speed, shipConfiguration.Direction);
    }

    private void Update()
    {
        _movementController.Move();
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId != EventIds.GameOver &&
            eventData.EventId != EventIds.RestartGame)
        {
            return;
        }

        RecycleShip();
    }

    private void RecycleShip()
    {
        OnRecycle?.Invoke(this);
        Recycle();
    }
}
