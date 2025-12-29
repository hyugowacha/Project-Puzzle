using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private BoardManager board;
    [SerializeField] private Gem[] gemPrefabs;
    private int maxTry = 20;

    public void FillBoard(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                SpawnGem(x, y);
            }
        }
    }

    void SpawnGem(int x, int y)
    {
        GemType? banH = BanHorizon(x, y);
        GemType? banV = BanVertical(x, y);

        int index = PickSafe(banH, banV);

        Gem gem = Instantiate(gemPrefabs[index]);
        gem.transform.position = board.Cell(x, y);
        board.gemBoard[x, y] = gem;
    }

    GemType? BanHorizon(int x, int y)
    {
        if (x < 2)
        {
            return null;
        }

        var a = board.gemBoard[x - 1, y];
        var b = board.gemBoard[x - 2, y];

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

        var a = board.gemBoard[x, y - 1];
        var b = board.gemBoard[x, y - 2];

        if (a == null || b == null)
        {
            return null;
        }

        return a.type == b.type ? a.type : null;
    }

    int PickSafe(GemType? banH, GemType? banV)
    {
        for (int i = 0; i < 20; i++)
        {
            int index = UnityEngine.Random.Range(0, gemPrefabs.Length);
            GemType t = gemPrefabs[index].type;

            if (t == banH) continue;
            if (t == banV) continue;

            return index;
        }

        return UnityEngine.Random.Range(0, gemPrefabs.Length);
    }

}
