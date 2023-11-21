using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private float _speed;
    private ICube _cube;
    private MoveDirection _moveDirection;

    public void Configure(ICube cubeMediator, float speed, MoveDirection moveDirection)
    {
        _cube = cubeMediator;
        _speed = speed;
        _moveDirection = moveDirection;
    }

    public void Move()
    {
        if (_moveDirection == MoveDirection.Z)
            transform.position += transform.forward * Time.deltaTime * _speed;
        else
            transform.position += transform.right * Time.deltaTime * _speed;
    }

    public void SetDirection(MoveDirection moveDirection)
    {
        _moveDirection = moveDirection;
    }
}
