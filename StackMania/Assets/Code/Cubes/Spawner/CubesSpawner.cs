using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubesSpawner : MonoBehaviour
{
    [SerializeField]
    private CubeToSpawnConfiguration _cubeToSpawnConfiguration;
    [SerializeField]
    private SpawnerController[] _spawnPositions;

    private int spawnerIndex = 0;
    private CubeFactory _cubeFactory;

    private List<CubeMediator> _spawnedCubes;
    private SpawnerController currentSpawner;

    private void Awake()
    {
        //_cubeFactory = ServiceLocator.Instance.GetService<CubeFactory>();
        _spawnedCubes = new List<CubeMediator>();
        currentSpawner = _spawnPositions[spawnerIndex];
    }

    private async void Start()
    {
        _cubeFactory = ServiceLocator.Instance.GetService<CubeFactory>();
        await Task.Delay(500);
    }

    public void Init()
    {
        SpawnCube(_cubeToSpawnConfiguration);
    }

    public void StopAndReset()
    {
        
    }

    /*private void Update()
    {
        if (!_canSpawn)
        {
            return;
        }

        SpawnCube(_cubeToSpawnConfiguration);
    }*/

    private void SpawnCube(CubeToSpawnConfiguration spawnConfiguration)
    {
        spawnerIndex = spawnerIndex == 0 ? 1 : 0;
        currentSpawner = _spawnPositions[spawnerIndex];
        CubeBuilder cubeBuilder = _cubeFactory.Create(_cubeToSpawnConfiguration.CubeId.Value);
        CubeMediator cube = cubeBuilder.WithPosition(currentSpawner.gameObject.transform.position)
            .WithRotation(currentSpawner.gameObject.transform.rotation)
            .WithConfiguration(_cubeToSpawnConfiguration)
            .WithMoveDirection(currentSpawner.MoveDirection)
            .Build();
        cube.OnRecycle += OnDestroyCube;
        _spawnedCubes.Add(cube);

        if (CubeMediator.LastCube != null && CubeMediator.LastCube.gameObject != GameObject.Find("StartingCube"))
        {
            float x = currentSpawner.MoveDirection == MoveDirection.X ? currentSpawner.gameObject.transform.position.x : CubeMediator.LastCube.transform.position.x;
            float z = currentSpawner.MoveDirection == MoveDirection.Z ? currentSpawner.gameObject.transform.position.z : CubeMediator.LastCube.transform.position.z;

            cube.transform.position = new Vector3(x,
            CubeMediator.LastCube.transform.position.y + cube.transform.localScale.y,
            z);
        }
        else
        {
            cube.transform.position = currentSpawner.gameObject.transform.position;
        }
    }

    private void OnDestroyCube(CubeMediator cube)
    {
        _spawnedCubes.Remove(cube);
        cube.OnRecycle -= OnDestroyCube;
    }

    public void Restart()
    {
        foreach (CubeMediator cube in _spawnedCubes)
        {
            cube.Recycle();
        }

        _spawnedCubes.Clear();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}
