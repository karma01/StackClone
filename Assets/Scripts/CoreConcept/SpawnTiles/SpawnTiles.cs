using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTiles : MonoBehaviour
{
    [SerializeField] private CoreMechanics baseTile;

    public void SpawnTilePrefab()
    {
     

        var tile = Instantiate(baseTile);
       tile.transform.position= new Vector3(transform.position.x
          , CoreMechanics.LastCube.transform.position.y + baseTile.transform.localScale.y
          , transform.position.z);



    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, baseTile.transform.localScale);

    }
}
