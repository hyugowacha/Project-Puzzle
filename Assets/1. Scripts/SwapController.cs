using UnityEngine;

public class SwapController : MonoBehaviour
{
    [SerializeField] BoardManager board;

    public void RequestSwap(Gem gem, Vector2Int dir)
    {
        Vector2Int a = new(gem.x, gem.y);

    }
}
