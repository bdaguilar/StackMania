using System;
using UnityEngine;
using UnityEngine.Assertions;

public class CubeBuilder
{
    public enum InputMode
    {
        Unity,
        Joystick
    }

    private ObjectPool _objectPool;

    private Vector3 _position = Vector3.zero;
    private Quaternion _rotation = Quaternion.identity;
    private JoyButton _joyButton;
    private CubeToSpawnConfiguration _cubeConfiguration;
    private MoveDirection _moveDirection;

    public CubeBuilder FromObjectPool(ObjectPool objectPool)
    {
        _objectPool = objectPool;
        return this;
    }

    public CubeBuilder WithPosition(Vector3 position)
    {
        _position = position;
        return this;
    }

    public CubeBuilder WithRotation(Quaternion rotation)
    {
        _rotation = rotation;
        return this;
    }

    public CubeBuilder WithMoveDirection(MoveDirection moveDirection)
    {
        _moveDirection = moveDirection;
        return this;
    }

    public CubeBuilder WithConfiguration(CubeToSpawnConfiguration shipConfiguration)
    {
        _cubeConfiguration = shipConfiguration;
        return this;
    }

    public CubeMediator Build()
    {
        CubeMediator cube = _objectPool.Spawn<CubeMediator>(_position, _rotation);
        CubeConfiguration cubeConfiguration = new CubeConfiguration(_cubeConfiguration.Speed,
                                                                    _moveDirection);
        cube.Configure(cubeConfiguration);
        return cube;
    }
}
