﻿using System;

public class ScoreSystem : IEventObserver, IScoreSystem
{
    private readonly IDataStore _dataStore;
    private const string _userData = "UserData";
    private int _currentScore;

    public int CurrentScore => _currentScore;

    public ScoreSystem(IDataStore dataStore)
    {
        _dataStore = dataStore;
        IEventQueue eventQueue = ServiceLocator.Instance.GetService<IEventQueue>();
        eventQueue.Subscribe(EventIds.GameOver, this);
        //ServiceLocator.Instance.GetService<IEventQueue>().Subscribe(EventIds.ShipDestroyed, this);
    }

    public void Reset()
    {
        _currentScore = 0;
    }

    public void Process(EventData eventData)
    {
        if (eventData.EventId == EventIds.GameOver)
        {
            UpdateBestScores(_currentScore);
            return;
        }
    }

    private void SaveBestScores(int[] bestScores)
    {
        UserData userData = new UserData();
        userData.BestScores = bestScores;
        _dataStore.SetData(userData, _userData);
    }

    public int[] GetBestScores()
    {
        UserData userData = _dataStore.GetData<UserData>(_userData) ?? new UserData();
        return userData.BestScores;
    }

    private void UpdateBestScores(int newScore)
    {
        int[] bestScores = GetBestScores();
        int scoreIndex = 0;
        for (; scoreIndex < bestScores.Length; ++scoreIndex)
        {
            if (bestScores[scoreIndex] < newScore)
            {
                break;
            }
        }

        if (!(scoreIndex < bestScores.Length))
        {
            return;
        }

        int oldScore = bestScores[scoreIndex];
        bestScores[scoreIndex] = newScore;
        scoreIndex += 1;
        for (; scoreIndex < bestScores.Length; ++scoreIndex)
        {
            newScore = bestScores[scoreIndex];
            bestScores[scoreIndex] = oldScore;
            oldScore = newScore;
        }
            SaveBestScores(bestScores);
    }

    public void AddScore(int scoreToAdd)
    {
        _currentScore += scoreToAdd;
        ServiceLocator.Instance.GetService<ScoreView>().UpdateScore(_currentScore);
    }
}
