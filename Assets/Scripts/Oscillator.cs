using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    Vector3 forwardPos;
    Vector3 rightPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField][Range(0, 1)] float  movementFactor;
    [SerializeField] public float period = 2f;
    public SpawnPos spawnPos= SpawnPos.xAxis;
    [SerializeField] private float controlMoveOffset;
    private void Start()
    {
     startingPosition = transform.position;
        forwardPos = transform.forward;
        rightPos = transform.right;

    }
    /// <summary>
    /// provide the motion by takin the direction of oscillation
    /// </summary>
    /// <param name="direction"></param>
   
    public void ProvideMotion(Vector3 direction)
    {
        if (period <= Mathf.Epsilon) { return; }//Mathf.Epsilon is the smallest posssible value.. this process is done to prevent nan error
        float cycles = Time.time / period; // to calculate the radian 
        const float tau = Mathf.PI * 2; // VALUE of tou ie 2 of pi
        float rawSineWave = Mathf.Sin(cycles * tau); /// to calculate value between[-1,1]
        movementFactor = (rawSineWave + 1F / 2F); //TO PRODUCE VALUE BETWEEN 0 AND 1

        Vector3 offset = controlMoveOffset * direction * movementFactor;
        transform.position = startingPosition + offset;

    }

}
