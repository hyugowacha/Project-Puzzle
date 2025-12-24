using Unity.Mathematics;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public const int Width = 8;
    public const int Height = 8;

    public Gem[,] board = new Gem[Width, Height];

    public Gem[] gemPrefabs;

    private void Start()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                SpawnGem(x, y);           
            }
        }
    }

    public Vector3 Cell(int x, int y)
    {
        return new Vector3
        (
            x - 4 + 0.5f, y - 4 + 0.5f, 0f
        );
    }

    void SpawnGem(int x, int y)
    {
        GemType? banH = BanHorizon(x, y); 
        GemType? banV = BanVertical(x, y); 

        int index = PickSafe(banH,banV);

        Gem gem = Instantiate(gemPrefabs[index]);
        gem.transform.position = Cell(x, y);
        board[x, y] = gem;
    }

    GemType? BanHorizon(int x, int y)
    {
        if (x < 2)
        {
            return null;
        }

        var a = board[x - 1, y];
        var b = board[x - 2, y];

        if (a == null || b == null)
        {
            return null;
        }

        return a.type == b.type ? a.type : null;
    }

    GemType? BanVertical(int x, int y)
    {
        if (y < 2)
        {
            return null;
        }

        var a = board[x, y - 1];
        var b = board[x, y - 2];

        if (a == null || b == null)
        {
            return null;
        }

        return a.type == b.type ? a.type : null;
    }

    int PickSafe(GemType? banH, GemType? banV)
    {
        for (int i = 0; i < 50; i++)
        {
            int index = UnityEngine.Random.Range(0,gemPrefabs.Length);
            GemType t = gemPrefabs[index].type;

            if (t == banH) continue;
            if(t==banV ) continue;

            return index;
        }

        return UnityEngine.Random.Range(0, gemPrefabs.Length);
    }

}
