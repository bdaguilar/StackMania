using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnCubeSpawned = delegate { };

    private CubeSpanwer[] spawners;
    private int spawnerIndex = 0;
    private CubeSpanwer currentSpawner;

    private void Awake()
    {
        spawners = FindObjectsOfType<CubeSpanwer>();
        currentSpawner = spawners[spawnerIndex];
        currentSpawner.SpawnCube();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();

            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];
            currentSpawner.SpawnCube();
            OnCubeSpawned();
        }
    }
}
