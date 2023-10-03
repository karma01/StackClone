using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using touched = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class CoreMechanics : MonoBehaviour
{
    [SerializeField] private GameObject tileObject;
    [SerializeField] private GameObject lastTileObject;

    private void Update()
    {
        IndentifyTouch();
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    /// <summary>
    /// Identify if the player has touched the screen
    /// </summary>
    private void IndentifyTouch()
    {
        bool isTouched = touched.fingers[0].isActive;
        if (isTouched)
        {
            touched myTouch = touched.activeTouches[0];
            if (myTouch.phase == TouchPhase.Began)
            {
                Debug.Log("IS Touched");
                TilesMechanics();
            }
        }
    }
    /// <summary>
    /// This methods is responsible for handling the mechanics of tiles.
    /// </summary>
    private void TilesMechanics()
    {
        
        tileObject.transform.localScale = lastTileObject.transform.localScale;
        lastTileObject = tileObject;
       
    }
}