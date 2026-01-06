using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public enum SwapResult
{
    Success, OutOfBounds, NotNeighbor, NoMatch
}

public class BoardManager : MonoBehaviour
{
    public const int Width = 8;
    public const int Height = 8;

    public Gem[,] gemBoard = new Gem[Width, Height];

    public Gem[] gemPrefabs;

    [SerializeField] private GemSpawner spawner;

    private void Start()
    {
        spawner.FillBoard(Width, Height);
    }

    public SwapResult TrySwap(Vector2Int a, Vector2Int b)
    {
        if (!InBounds(a.x, a.y) || !InBounds(b.x, b.y)) 
        {
            return SwapResult.OutOfBounds;
        }

        if(!AreNeighbors(a, b))
        {
            return SwapResult.NotNeighbor;
        }

        SwapCells(a, b);

        bool matched = HasMatchAt(a.x, a.y) || HasMatchAt(b.x, b.y);

        if (!matched)
        {
            SwapCells(a, b);
            return SwapResult.NoMatch;
        }

        return SwapResult.Success;
    }

    public bool InBounds(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    public bool AreNeighbors(Vector2Int a, Vector2Int b)
    {
        int dx = Mathf.Abs(a.x - b.x);
        int dy = Mathf.Abs (a.y - b.y);
        return (dx + dy == 1);
    }

    public void SwapCells(Vector2Int a, Vector2Int b)
    {
        Gem gemA = gemBoard[a.x, a.y];
        Gem gemB = gemBoard[b.x, b.y];

        gemBoard[a.x, a.y] = gemB;
        gemBoard[b.x, b.y] = gemA;

        if(gemA != null)
        {
            gemA.SetCell(b.x, b.y, Cell(b.x, b.y));
        }

        if(gemB != null)
        {
            gemB.SetCell(a.x, a.y, Cell(a.x, a.y));
        }
    }

    public bool HasMatchAt(int x, int y)
    {
        if (!InBounds(x, y)) return false;

        Gem gem = gemBoard[x, y];
        if(gem == null) return false;

        GemType type = gem.type;

        int i = 1;

        i += CountSame(x, y, -1, 0, type);
        i += CountSame(x, y, 1, 0, type);
        if (i >= 3) return true;

        int j = 1;

        j += CountSame(x, y, 0, -1, type);
        j += CountSame(x, y, 0, 1, type);
        return j >= 3;
    }

    public int CountSame(int x, int y, int dx, int dy, GemType type)
    {
        int count = 0;
        int nx = x + dx;
        int ny = y + dy;    

        while(InBounds(nx, ny))
        {
            Gem ng = gemBoard[nx, ny];
            if (ng == null || ng.type != type) break;

            count++;
            nx += dx;
            ny += dy;
        }

        return count;
    }

    public Vector3 Cell(int x, int y)
    {
        return new Vector3
        (
            x - 4 + 0.5f, y - 4 + 0.5f, 0f
        );
    }


}
