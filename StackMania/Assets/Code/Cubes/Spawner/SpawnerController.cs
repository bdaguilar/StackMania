using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField]
    private MoveDirection _moveDirection;

    public MoveDirection MoveDirection => _moveDirection;
}
