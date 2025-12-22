using UnityEngine;
using UnityEngine.InputSystem;

public class Gem : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log($"Clicked: {transform.position.x}, {transform.position.y}");
    }

    private void OnMouseDrag()
    {
        Debug.Log(Mouse.current.position);
    }

    private void OnMouseUp()
    {
        Vector2 screenPos = Mouse.current.position.ReadValue();

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(screenPos.x, screenPos.y, 0f)
        );

        Debug.Log($"Mouse Up World Pos: {worldPos}");
    }
}
