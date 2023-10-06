using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreMechanics : MonoBehaviour
{
    public static CoreMechanics PresentCube { get; private set; }
    public static CoreMechanics LastCube { get; private set; }
    public SpawnPos spawnPos { get; set; }

    [SerializeField] private float movementSpeed;
    [SerializeField] private Oscillator osc;
    public bool isGameOver = false;
    private bool Gameover = false;

    private Vector3 forardpos;
    private Vector3 leftpos;

    private void OnEnable()
    {
        if (LastCube == null) { LastCube = this; }

        Debug.Log(LastCube.gameObject.name);
        PresentCube = this;

        Debug.Log(PresentCube.gameObject.name);
        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

    private void Start()
    {
        osc = GetComponent<Oscillator>();
        forardpos = transform.forward;
        leftpos = LastCube.transform.right;
    }

    /// <summary>
    /// stop the movement of the cube
    /// </summary>
    ///
    public void StopMovement()
    {
        movementSpeed = 0;
        if (osc != null)
        {
            osc.period = 0;
        }

        float leftoverZValue = GetLeftoverZValue();        //the value to cut
        if (Mathf.Abs(leftoverZValue) >= LastCube.transform.localScale.z)       //if the value exceeeds the edge then..
        {
            isGameOver = true;
            this.gameObject.AddComponent<Rigidbody>();
            Invoke("LoadScene", 2f);

            return;
        }
        float directionToCutEdge = leftoverZValue > 0 ? 1f : -1f;       //determines the sides where the cube needs to be spawned

        ReTransformCube(leftoverZValue, directionToCutEdge);        //Transform the cube scale
    }

    private float GetLeftoverZValue()
    {
        if (spawnPos == SpawnPos.zAxis)
        {
            return transform.position.z - LastCube.transform.position.z;
        }
        else if (spawnPos == SpawnPos.xAxis)
        {
            return transform.position.x - LastCube.transform.position.x;
        }
        return 0f;
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Re-transform the cube to the updated position
    /// </summary>
    /// <param name="ZValue"></param>

    private void ReTransformCube(float ZValue, float direction)
    {
        float lastCubeScale = LastCubeScale();
        float newSize = lastCubeScale - Mathf.Abs(ZValue);         //Calculate the new Size after certain pos is removed i.e ZVa;
        float fallSize = lastCubeScale - newSize;            //.i.e size that got left(fall size)...  gives same value as abs of ZValue.

        float cubeEdge = 0f;
        if (spawnPos == SpawnPos.zAxis)
        {
            cubeEdge = TransformZTile(ZValue, direction, newSize);
        }
        else if (spawnPos == SpawnPos.xAxis)          //for xAxis Movement
        {
            cubeEdge = TransformXTile(ZValue, direction, newSize);
        }

        float fallingBlockPos = cubeEdge + (fallSize / 2f) * direction;

        SpawnFallCube(fallingBlockPos, Mathf.Abs(ZValue));          //past the position and Absolute value of z valuesize
    }

    private float TransformXTile(float ZValue, float direction, float newSize)
    {
        float cubeEdge;
        float newZPos = LastCube.transform.position.x + (ZValue / 2);           //calculate the position value to change when  the size is adjusted

        transform.localScale = new Vector3(newSize, transform.localScale.y, transform.localScale.z);        //set the scale of current cube after removing the fall size value
        transform.position = new Vector3(newZPos, transform.position.y, transform.position.z);
        cubeEdge = transform.position.x + (newSize / 2) * direction;
        return cubeEdge;
    }

    private float TransformZTile(float ZValue, float direction, float newSize)
    {
        float cubeEdge;
        float newZPos = LastCube.transform.position.z + (ZValue / 2);           //calculate the position value to change when  the size is adjusted

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);        //set the scale of current cube after removing the fall size value
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPos);
        cubeEdge = transform.position.z + (newSize / 2) * direction;
        return cubeEdge;
    }

    private float LastCubeScale()
    {
        if (spawnPos == SpawnPos.zAxis)
        {
            return LastCube.transform.localScale.z;
        }
        else if (spawnPos == SpawnPos.xAxis)
        {
            return LastCube.transform.localScale.x;
        }
        return 0;
    }

    /// <summary>
    /// CREATE A CUBE AND SPAWN IT IN THE CUT POSITION
    /// </summary>
    private void SpawnFallCube(float cubeZpos, float fallSize)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.transform.position = new Vector3(transform.position.x, transform.position.y, cubeZpos);
        cube.transform.localScale = new Vector3(transform.lossyScale.x, transform.localScale.y, fallSize);
        cube.AddComponent<BoxCollider>();
        cube.AddComponent<Rigidbody>();
        Destroy(cube.gameObject, 2f);
        LastCube = this;
        leftpos = LastCube.transform.right;
    }

    /// <summary>
    /// procides the movement to the cube
    /// </summary>
    public void ProvideMovement()
    {
        if (spawnPos == SpawnPos.zAxis)
        {
            if (osc != null)
            {
                osc.ProvideMotion(forardpos);
            }
        }
        else if (spawnPos == SpawnPos.xAxis)
        {
            if (osc != null)
                osc.ProvideMotion(leftpos);
        }
    }
}