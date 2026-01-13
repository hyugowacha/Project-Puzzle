using UnityEngine;

public class GemSelector : MonoBehaviour
{
    [SerializeField] private BoardManager board;

    private Gem selectedGem;

    public bool HasSelection => selectedGem != null;

    public void TrySelectGem(Vector3 worldPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        selectedGem = hit.collider != null ? hit.collider.GetComponent<Gem>() : null;
    }

    public void RequestSwap(SwipeDirection direction)
    {
        if (selectedGem == null) return;

        Vector2Int dir = direction switch
        {
            SwipeDirection.Up => Vector2Int.up,
            SwipeDirection.Down => Vector2Int.down,
            SwipeDirection.Left => Vector2Int.left,
            SwipeDirection.Right => Vector2Int.right,
            _ => Vector2Int.zero
        };

        board.RequestSwap(selectedGem, dir);
        ClearSelection();
    }

    public void ClearSelection()
    {
        selectedGem = null;
    }
}
