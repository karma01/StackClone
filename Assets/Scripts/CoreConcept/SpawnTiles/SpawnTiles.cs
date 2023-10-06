using UnityEngine;

public class SpawnTiles : MonoBehaviour

{
    protected Transform tileTransform;
    [SerializeField] private CoreMechanics baseTile;
    private SpawnPos spawnPosition = SpawnPos.zAxis;

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
            }
            else
            {
                tile.transform.position = transform.position;
            }
            tile.spawnPos = spawnPosition;
        }
    }
}