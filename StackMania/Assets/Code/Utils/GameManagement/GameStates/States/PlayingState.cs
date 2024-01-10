using System;

public class PlayingState : IGameState, IEventObserver
{
    private int _aliveShips;
    private bool _allShipsSpawned = false;
    private Action<GameStates> _onStatedEndedCallback;

    public void Start(Action<GameStates> onStatedEndedCallback)
    {
        _onStatedEndedCallback = onStatedEndedCallback;
        _aliveShips = 0;
        _allShipsSpawned = false;
        ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EventIds.CubeMissed, this);
    }

    public void Stop()
    {
        ServiceLocator.Instance.GetService<IEventQueue>().Unsubscribe(EventIds.CubeMissed, this);
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.CubeMissed)
        {
             _onStatedEndedCallback.Invoke(GameStates.GameOver);
        }
    }
}

