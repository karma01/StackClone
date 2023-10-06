using Cinemachine.Utility;
using UnityEngine;

public class SpawnTiles : MonoBehaviour

{
    protected Transform tileTransform;
    [SerializeField] private CoreMechanics baseTile;
    private SpawnPos spawnPosition = SpawnPos.zAxis;
    [SerializeField] private GameObject UIcamera;
    [SerializeField] private float increaseColorGradientR;

    public void SpawnTilePrefab()
    {
        if (CoreMechanics.PresentCube.isGameOver == false)
        {
            var tile = Instantiate(baseTile);

            tileTransform = tile.transform;
            if (CoreMechanics.LastCube != null && CoreMechanics.LastCube.gameObject != GameObject.Find("Start"))
            {
                tile.transform.position = new Vector3(CoreMechanics.LastCube.transform.position.x
              , CoreMechanics.LastCube.transform.position.y + baseTile.transform.localScale.y
              , CoreMechanics.LastCube.transform.position.z);

                Debug.LogWarning(CoreMechanics.LastCube.gameObject.name);
                if (spawnPosition == SpawnPos.zAxis) { spawnPosition = SpawnPos.xAxis; } else { spawnPosition = SpawnPos.zAxis; }
                UIcamera.transform.position = new Vector3(UIcamera.transform.position.x     //when tiles are spawned then update the camera
                    , UIcamera.transform.position.y + 0.4f
                    , UIcamera.transform.position.z);
            }
            else
            {
                tile.transform.position = transform.position;
            }
            tile.spawnPos = spawnPosition;
            increaseColorGradientR = Mathf.Sin(CoreMechanics.PresentCube.transform.localPosition.y);

            Mathf.Clamp01(increaseColorGradientR);
          
           


            tile.GetComponent<Renderer>().material.color = new Color(increaseColorGradientR,0,1,1);
        }
    }
}