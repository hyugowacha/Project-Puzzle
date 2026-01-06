using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] SwapController swapController;

    Gem selectedGem;
    Vector3 startPos;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit2D hit = Physics2D.Raycast(WorldPos(), Vector2.zero);

            if (hit.collider != null)
            {
                selectedGem = hit.collider.GetComponent<Gem>();
                startPos = WorldPos();
            }
        }

        if (Mouse.current.leftButton.isPressed && selectedGem != null)
        {
            Vector3 delta = WorldPos() - startPos;

            if(delta.magnitude > 0.3f)
            {
                Vector2Int dir = GetDir(delta);
                swapController.RequestSwap(selectedGem, dir);
                selectedGem = null;
            }
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            selectedGem = null;
        }
    }

    Vector3 WorldPos()
    {
        Vector2 pos = Mouse.current.position.ReadValue();
        return cam.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 0));
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
