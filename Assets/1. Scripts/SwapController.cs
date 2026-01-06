using UnityEngine;

public class SwapController : MonoBehaviour
{
    [SerializeField] BoardManager board;
    public bool locked;

    public void RequestSwap(Gem gem, Vector2Int dir)
    {
        if (locked) return;

        Vector2Int a = new(gem.x, gem.y);
        Vector2Int b = a + dir;

        SwapResult result = board.TrySwap(a, b);

        if(result == SwapResult.Success)
        {
            Debug.Log("스왑 성공");
        }

        else
        {
            Debug.Log("스왑 실패: " + result);
        }
        
    }

    void LogSwapResult(SwapResult result, Vector2Int a,  Vector2Int b)
    {
        switch (result)
        {
            case SwapResult.Success:
                Debug.Log($"스왑 성공 {a}-{b}");
                break;
            case SwapResult.OutOfBounds:
                Debug.Log($"스왑 실패 가장자리 넘어감 {a}-{b}");
                break;
            case SwapResult.NotNeighbor:
                Debug.Log($"스왑 실패 인접하지 않음 {a}-{b}");
                break;
            case SwapResult.NoMatch:
                Debug.Log($"스왑 실패 매치 안생김 {a}-{b}");
                break;
        }
    }
}
