using System.Collections.Generic;
using UnityEngine;

public class CubeFactory
{
    private readonly CubesConfiguration _cubesConfiguration;

    private readonly Dictionary<string, ObjectPool> _pools;

    public CubeFactory(CubesConfiguration cubesConfiguration)
    {
        _cubesConfiguration = cubesConfiguration;
        CubeMediator[] prefabs = _cubesConfiguration.CubePrefabs;
        _pools = new Dictionary<string, ObjectPool>(prefabs.Length);
        foreach (CubeMediator cubeMediator in prefabs)
        {
            ObjectPool objectPool = new ObjectPool(cubeMediator);
            objectPool.Init(10);
            _pools.Add(cubeMediator.Id, objectPool);
        }
    }

    public CubeBuilder Create(string id)
    {
        CubeMediator prefab = _cubesConfiguration.GetShipById(id);

        return new CubeBuilder()
            .FromObjectPool(_pools[id]);
    }
}
