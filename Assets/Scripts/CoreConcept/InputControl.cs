using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using touched = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class InputControl : MonoBehaviour
{
    [SerializeField] private SpawnTiles spawnTiles;
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        IndentifyTouch();
    }
    /// <summary>
    /// Identify when player touches the screen
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
               
                CoreMechanics.PresentCube.StopMovement();
                spawnTiles.SpawnTilePrefab();           //spawn the next prefab

            }
        }
      
        if (CoreMechanics.PresentCube != null)
            { CoreMechanics.PresentCube.ProvideMovement(); }
    }
}