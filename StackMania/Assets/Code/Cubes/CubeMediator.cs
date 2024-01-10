using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MovementController))]
[RequireComponent (typeof(SplitController))]
[RequireComponent (typeof (ColorController))]
public class CubeMediator : RecyclableObject, IEventObserver, ICube
{
    public static CubeMediator CurrentCube { get; set; }
    public static CubeMediator LastCube { get; set; }

    [SerializeField]
    private CubeId _cubeId;
    [SerializeField]
    private MovementController _movementController;
    [SerializeField]
    private SplitController _splitController;
    [SerializeField]
    private ColorController _colorController;

    public Action<CubeMediator> OnRecycle;

    private MoveDirection _direction;

    public string Id => _cubeId.Value;

    private void OnEnable()
    {
        if (LastCube == null)
            LastCube = GameObject.Find("StartingCube").GetComponent<CubeMediator>();

        CurrentCube = this;
        //GetComponent<Renderer>().material.color = GetRandomColor();

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

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
        _direction = shipConfiguration.Direction;
        _movementController.Configure(this, shipConfiguration.Speed, _direction);
        _splitController.Configure(this, _direction);
        _colorController.Configure(this);
    }

    public void Stop()
    {
        _movementController.Stop();
        _splitController.Stop();
        LastCube = this;
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
