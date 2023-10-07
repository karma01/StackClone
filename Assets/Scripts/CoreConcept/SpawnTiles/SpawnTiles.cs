using TMPro;
using UnityEngine;

public class SpawnTiles : MonoBehaviour

{
    protected Transform tileTransform;
    [SerializeField] private CoreMechanics baseTile;
    private SpawnPos spawnPosition = SpawnPos.zAxis;
    [SerializeField] private GameObject UIcamera;
    [SerializeField] private float increaseColorGradientR;

    private int currentScore = 0;
    private int highScore;
    [SerializeField] public TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    private void Start()
    {
        highScore = PlayerPrefs.GetInt("Score", currentScore);
        highScoreText.text = "High Score:" + highScore;
    }
    public void SpawnTilePrefab()
    {
        if (CoreMechanics.PresentCube.isGameOver == false)
        {
            currentScore++;
            currentScoreText.text = "Current Score:" + currentScore.ToString();
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
                    , UIcamera.transform.position.y + 0.1f
                    , UIcamera.transform.position.z);
            }
            else
            {
                tile.transform.position = transform.position;
            }
            tile.spawnPos = spawnPosition;
            increaseColorGradientR += CoreMechanics.PresentCube.transform.localScale.x / CoreMechanics.LastCube.transform.position.y;
            float value = Mathf.Sin(increaseColorGradientR);

            tile.GetComponent<Renderer>().material.color = new Color(1 - value, 0, 1, 1);
        }
       else
        {
            if(currentScore>highScore)
            {
                PlayerPrefs.SetInt("Score", currentScore);
            }
        }
    }
}