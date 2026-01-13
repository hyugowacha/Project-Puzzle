using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class BoardManager : MonoBehaviour
{
    public Gem[,] gemBoard = new Gem[BoardUtility.Width, BoardUtility.Height];

    [SerializeField] private GemSpawner spawner;

    private MatchChecker matchChecker;
    public bool IsLocked { get; private set; }

    private void Awake()
    {
        matchChecker = new MatchChecker(this);
    }

    private void Start()
    {
        spawner.FillBoard();
    }

    public void RequestSwap(Gem gem, Vector2Int dir)
    {
        if (IsLocked) return;

        Vector2Int a = new Vector2Int(gem.x, gem.y);
        Vector2Int b = a + dir;

        SwapResult result = TrySwap(a, b);

        if (result == SwapResult.Success)
        {
            Debug.Log($"스왑 성공: {a} → {b}");
        }
        else
        {
            Debug.Log($"스왑 실패: {result}");
        }
    }

    public SwapResult TrySwap(Vector2Int a, Vector2Int b)
    {
        if (!BoardUtility.InBounds(a.x, a.y) || !BoardUtility.InBounds(b.x, b.y))
        {
            return SwapResult.OutOfBounds;
        }

        if (!BoardUtility.AreNeighbors(a, b))
        {
            return SwapResult.NotNeighbor;
        }

        SwapCells(a, b);

        bool matched = matchChecker.HasMatchAt(a.x, a.y) || matchChecker.HasMatchAt(b.x, b.y);

        if (!matched)
        {
            SwapCells(a, b); // 롤백
            return SwapResult.NoMatch;
        }

        return SwapResult.Success;
    }


    public void SwapCells(Vector2Int a, Vector2Int b)
    {
        Gem gemA = gemBoard[a.x, a.y];
        Gem gemB = gemBoard[b.x, b.y];

        gemBoard[a.x, a.y] = gemB;
        gemBoard[b.x, b.y] = gemA;

        if (gemA != null)
        {
            gemA.SetCell(b.x, b.y, BoardUtility.GetCellWorldPos(b.x, b.y));
        }

        if (gemB != null)
        {
            gemB.SetCell(a.x, a.y, BoardUtility.GetCellWorldPos(a.x, a.y));
        }
    }

    public GemType? GetBanTypeHorizontal(int x, int y)
    {
        if (x < 2) return null;

        Gem a = gemBoard[x - 1, y];
        Gem b = gemBoard[x - 2, y];

        if (a == null || b == null) return null;

        return a.type == b.type ? a.type : null;
    }
    public GemType? GetBanTypeVertical(int x, int y)
    {
        if (y < 2) return null;

        Gem a = gemBoard[x, y - 1];
        Gem b = gemBoard[x, y - 2];

        if (a == null || b == null) return null;

        return a.type == b.type ? a.type : null;
    }


}
