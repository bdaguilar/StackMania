using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Level/Create Level Configuration", fileName = "LevelConfiguration", order = 0)]
public class LevelConfiguration : ScriptableObject
{
    [SerializeField]
    private SpawnConfiguration[] _spawnConfigruation;

    public SpawnConfiguration[] SpawnConfigurations => _spawnConfigruation;
}
