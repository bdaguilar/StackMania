using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{
    private ICube _cube;
    private Renderer _renderer;

    public void Configure(ICube cubeMediator)
    {
        _cube = cubeMediator;
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }
}
