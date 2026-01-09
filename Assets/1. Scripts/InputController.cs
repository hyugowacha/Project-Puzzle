using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private SwapController swapController;

    private PlayerContoller inputActions;
    private Gem selectedGem;
    private Vector3 startPos;
    private bool isPressed;

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
        if(isPressed && selectedGem != null)
        {
            Vector3 currentPos = GetWorldPos();
            Vector3 nowPos = currentPos - startPos;

            if(nowPos.magnitude > 0.3f)
            {
                Vector2Int dir = GetDir(nowPos);
                swapController.RequestSwap(selectedGem, dir);
                selectedGem = null;
                isPressed = false;  
            }
        }
    }

    private void OnTouchStart(InputAction.CallbackContext ctx)
    {
        isPressed = true;
        Vector3 worldPos = GetWorldPos();
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if(hit.collider != null)
        {
            selectedGem = hit.collider.GetComponent<Gem>();
            startPos = worldPos;
        }
    }

    private void OnTouchEnd(InputAction.CallbackContext ctx)
    {
        isPressed = false;
        selectedGem = null;
    }

    private Vector3 GetWorldPos()
    {
        Vector2 screenPos = inputActions.Player.TouchPosition.ReadValue<Vector2>();
        return cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0));
    }


    Vector2Int GetDir(Vector3 dir)
    {
        if(Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            return dir.x > 0? Vector2Int.right : Vector2Int.left;
        }

        else
        {
            return dir.y > 0 ? Vector2Int.up : Vector2Int.down;
        }
    }
}
