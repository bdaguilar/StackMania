using UnityEngine;

[CreateAssetMenu(menuName = "Configurations/Spawn/Create CubeToSpawnConfiguration", fileName = "CubeToSpawnConfiguration", order = 0)]
public class CubeToSpawnConfiguration : ScriptableObject
{
    [SerializeField]
    private CubeId _cubeId;
    [SerializeField]
    private float _speed;

    public CubeId CubeId => _cubeId;
    public float Speed => _speed;
}
