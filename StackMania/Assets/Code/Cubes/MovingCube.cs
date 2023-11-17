using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    public static MovingCube CurrentCube { get; private set;}
    public static MovingCube LastCube { get; private set; }
    public MoveDirection MoveDirection { get; set; }

    [SerializeField]
    private float _movementSpeed = 1.0f;


    private void OnEnable()
    {
        if (LastCube == null)
            LastCube = GameObject.Find("StartingCube").GetComponent<MovingCube>();

        CurrentCube = this;
        GetComponent<Renderer>().material.color = GetRandomColor();

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    private void Update()
    {
        if(MoveDirection == MoveDirection.Z)
            transform.position += transform.forward * Time.deltaTime * _movementSpeed;
        else
            transform.position += transform.right * Time.deltaTime * _movementSpeed;
    }

    internal void Stop()
    {
        _movementSpeed = 0;
        float hangover = GetHangover();

        float max = MoveDirection == MoveDirection.Z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;

        if (Mathf.Abs(hangover) >= max)
        {
            CurrentCube = null;
            LastCube = null;
            SceneManager.LoadScene(0);
        }

        float direction = hangover > 0 ? 1f : -1f;

        if (MoveDirection == MoveDirection.Z)
            SplitCubeOnZ(hangover, direction);
        else
            SplitCubeOnX(hangover, direction);


        LastCube = this;
    }

    private float GetHangover()
    {
        if (MoveDirection == MoveDirection.Z)
            return transform.position.z - LastCube.transform.position.z;
        else
            return transform.position.x - LastCube.transform.position.x;
    }

    private void SplitCubeOnX(float hangover, float direction)
    {
        float newXSize = LastCube.transform.localScale.x - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.x - newXSize;

        float newXPosition = LastCube.transform.position.x + (hangover / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + (fallingBlockSize / 2f * direction);

        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
    }

    private void SplitCubeOnZ(float hangover, float direction)
    {
        float newZSize = LastCube.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZPosition = LastCube.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + (fallingBlockSize / 2f * direction);

        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if(MoveDirection == MoveDirection.Z)
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