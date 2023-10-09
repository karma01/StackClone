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
        VibratePhone.Vibrate(250);

        highScore = PlayerPrefs.GetInt("Score", currentScore);
        highScoreText.text = "High Score:" + highScore;
    }
    /// <summary>
    /// Spawn the tiles by checking the axis it needs to be spawned
    /// </summary>
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

                if (spawnPosition == SpawnPos.zAxis) { spawnPosition = SpawnPos.xAxis; } else { spawnPosition = SpawnPos.zAxis; }
                UIcamera.transform.position = new Vector3(UIcamera.transform.position.x     //when tiles are spawned then update the camera
                    , UIcamera.transform.position.y + 0.2f
                    , UIcamera.transform.position.z);
            }
            else
            {
                Debug.LogWarning("Called");
                tile.transform.position = transform.position;
            }
            tile.spawnPos = spawnPosition;
            increaseColorGradientR += CoreMechanics.PresentCube.transform.localScale.x / CoreMechanics.LastCube.transform.position.y;       ///increase the gradient of cube
            float value = Mathf.Sin(increaseColorGradientR);

            tile.GetComponent<Renderer>().material.color = new Color(1 - value, 0, 1, 1);
        }
        else
        {
            if (currentScore > highScore)
            {
                PlayerPrefs.SetInt("Score", currentScore);
            }
        }
    }
}