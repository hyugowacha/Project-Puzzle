using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GemSelector gemSelector;

    private PlayerContoller inputActions;
    private Vector2 startScreenPos;
    private bool isDragging;

    private const float DRAG_THRESHOLD_PIXELS = 30f;

    private void Awake()
    {
        inputActions = new PlayerContoller();
    }

    private void Start()
    {
        inputActions.Player.Enable();

        inputActions.Player.TouchPress.started += OnTouchStart;
        inputActions.Player.TouchPress.canceled += OnTouchEnd;
    }

    private void Update()
    {
        if (isDragging && gemSelector.HasSelection)
        {
            Vector2 currentScreenPos = inputActions.Player.TouchPosition.ReadValue<Vector2>();
            Vector2 delta = currentScreenPos - startScreenPos;

            if (delta.magnitude > DRAG_THRESHOLD_PIXELS)
            {
                SwipeDirection direction = GetSwipeDir(delta);
                gemSelector.RequestSwap(direction);
                isDragging = false;
            }
        }
    }

    private void OnTouchStart(InputAction.CallbackContext ctx)
    {
        startScreenPos = inputActions.Player.TouchPosition.ReadValue<Vector2>();
        Vector3 worldPos = GetWorldPos(startScreenPos);
        gemSelector.TrySelectGem(worldPos);
        isDragging = true;
    }

    private void OnTouchEnd(InputAction.CallbackContext ctx)
    {
        isDragging = false;
        gemSelector.ClearSelection();
    }

    private Vector3 GetWorldPos(Vector2 screenPos)
    {
        return cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
    }


    private SwipeDirection GetSwipeDir(Vector2 delta)
    {
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
        {
            return delta.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
        }
        else
        {
            return delta.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
        }
    }
}
