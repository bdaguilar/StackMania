using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set;}

    [SerializeField]
    private float movementSpeed = 1.0f;

    private void OnEnable()
    {
        CurrentCube = this;
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * movementSpeed;
    }

    public void Stop()
    {
        movementSpeed = 0;
    }
}
