using System;
using UnityEngine;

public class StopManager : MonoBehaviour
{
    [SerializeField]
    private CubeToSpawnConfiguration _cubeToSpawnConfiguration;

    public static event Action OnCubeSpawned = delegate { };

    private CubesSpawner spawner;
    private IInput _input;

    public void Configure(IInput input)
    {
        _input = input;
    }

    private void Awake()
    {
        spawner = FindObjectOfType<CubesSpawner>();
    }

    void Update()
    {
        if (_input.IsStopActionPressed())
        {
            if (CubeMediator.CurrentCube != null)
                CubeMediator.CurrentCube.Stop();

            ScoreSystem scoreSystem = ServiceLocator.Instance.GetService<IScoreSystem>() as ScoreSystem;
            scoreSystem.AddScore(100);

            spawner.SpawnCube(_cubeToSpawnConfiguration);
            OnCubeSpawned();
        }
    }
}
