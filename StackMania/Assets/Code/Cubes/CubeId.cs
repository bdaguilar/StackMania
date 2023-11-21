using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Factory/Cube/Create Cube", fileName = "CubeId", order = 0)]
public class CubeId : ScriptableObject
{
    [SerializeField]
    private string _value;

    public string Value => _value;
}
