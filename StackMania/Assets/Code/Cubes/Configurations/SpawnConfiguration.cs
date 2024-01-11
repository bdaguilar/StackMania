using System;
using UnityEngine;

[Serializable]
public class SpawnConfiguration
{
    [SerializeField]
    private CubeToSpawnConfiguration[] _cubeToSpawnConfigurations;

    public CubeToSpawnConfiguration[] CubeToSpawnConfigurations => _cubeToSpawnConfigurations;
}
