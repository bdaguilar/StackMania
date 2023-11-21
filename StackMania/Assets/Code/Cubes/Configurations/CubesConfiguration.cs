using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Factory/Cubes/Create Cubes Configuration", fileName = "CubesConfiguration", order = 0)]
public class CubesConfiguration : ScriptableObject
{
    [SerializeField]
    private CubeMediator[] _cubesIdPrefabs;
    private Dictionary<string, CubeMediator> _idCubePrefab;

    public CubeMediator[] CubePrefabs => _cubesIdPrefabs;

    private void Awake()
    {
        _idCubePrefab = new Dictionary<string, CubeMediator>();
        foreach (CubeMediator cube in _cubesIdPrefabs)
        {
            _idCubePrefab.Add(cube.Id, cube);
        }
    }

    public CubeMediator GetShipById(string id)
    {
        if (!_idCubePrefab.TryGetValue(id, out var cube))
        {
            throw new Exception($"cube {id} not found");
        }

        return cube;
    }
}
