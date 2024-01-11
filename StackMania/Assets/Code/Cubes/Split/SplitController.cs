using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SplitController : MonoBehaviour
{
    private ICube _cube;
    private MoveDirection _moveDirection;

    public void Configure(ICube cubeMediator, MoveDirection moveDirection)
    {
        _cube = cubeMediator;
        _moveDirection = moveDirection;
    }

    public void Stop()
    {
        float hangover = GetHangover();

        float max = _moveDirection == MoveDirection.Z ? CubeMediator.LastCube.transform.localScale.z : CubeMediator.LastCube.transform.localScale.x;

        if (Mathf.Abs(hangover) >= max)
        {
            CubeMediator.CurrentCube = null;
            CubeMediator.LastCube = null;
            ServiceLocator.Instance.GetService<IEventQueue>().EnqueueEvent(new EventData(EventIds.CubeMissed));
            return;
        }

        float direction = hangover > 0 ? 1f : -1f;

        if (_moveDirection == MoveDirection.Z)
            SplitCubeOnZ(hangover, direction);
        else
            SplitCubeOnX(hangover, direction);
    }

    private float GetHangover()
    {
        if (_moveDirection == MoveDirection.Z)
            return transform.position.z - CubeMediator.LastCube.transform.position.z;
        else
            return transform.position.x - CubeMediator.LastCube.transform.position.x;
    }

    private void SplitCubeOnX(float hangover, float direction)
    {
        float newXSize = CubeMediator.LastCube.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = CubeMediator.LastCube.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + (fallingBlockSize / 2f * direction);

        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
    }

    private void SplitCubeOnZ(float hangover, float direction)
    {
        float newZSize = CubeMediator.LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZPosition = CubeMediator.LastCube.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + (fallingBlockSize / 2f * direction);

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (_moveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockZPosition, transform.position.y, transform.position.z);
        }


        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

        Destroy(cube.gameObject, 1f);
    }
}
