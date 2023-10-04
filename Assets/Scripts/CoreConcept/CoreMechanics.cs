using UnityEngine;

public class CoreMechanics : MonoBehaviour
{
    public static CoreMechanics PresentCube { get; private set; }
    public static CoreMechanics LastCube { get; private set; }
    [SerializeField] private float movementSpeed;

    private void Awake()
    {
        PresentCube = this;
        Debug.Log(PresentCube.gameObject.name);
    }

    private void Start()
    {
        LastCube = GameObject.Find("Start").GetComponent<CoreMechanics>();
        Debug.Log(LastCube.gameObject.name);
    }

    /// <summary>
    /// procides the movement to the cube
    /// </summary>
    public void ProvideMovement()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;           //provides movement
        Debug.Log("called");
    }

    /// <summary>
    /// stop the movement of the cube
    /// </summary>
    ///
    public void StopMovement()
    {
        movementSpeed = 0;

        if (Physics.BoxCast(transform.position, transform.lossyScale / 2, Vector3.down, out RaycastHit hit))            //check if the box hits any collider
        {
            float leftoverZValue = transform.position.z - LastCube.transform.position.z;
            float directionToCutEdge;       //determines the sides where the cube needs to be spawned
            if (leftoverZValue > 0) { directionToCutEdge = 1; }
            else directionToCutEdge = -1;
            ReTransformCube(leftoverZValue, directionToCutEdge);
        }
    }

    /// <summary>
    /// Re-transform the cube to the updated position
    /// </summary>
    /// <param name="ZValue"></param>

    private void ReTransformCube(float ZValue, float direction)
    {
        float newSize = LastCube.transform.localScale.z - Mathf.Abs(ZValue);         //Calculate the new Size after certain pos is removed i.e ZVa;
        float fallSize = LastCube.transform.localScale.z - newSize;            //.i.e size that got left(fall size)...  gives same value as abs of ZValue.

        float newZPos = LastCube.transform.position.z + (ZValue / 2);           //calculate the position value to change when  the size is adjusted

        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newSize);        //set the scale of current cube after removing the fall size value
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPos);
        float cubeEdge = transform.position.z + (newSize / 2) * direction;
        float fallingBlockPos = cubeEdge + (fallSize / 2f) * direction;

        SpawnFallCube(fallingBlockPos, Mathf.Abs(ZValue));          //past the position and Absolute value of z valuesize
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
    }

    private void OnDrawGizmos()
    {
        bool cast = Physics.BoxCast(transform.position, transform.lossyScale / 2, Vector3.down, out RaycastHit hit);
        if (cast)
        {
            Gizmos.DrawCube(transform.position, transform.lossyScale);
            Debug.Log("Hit");
        }
    }
}