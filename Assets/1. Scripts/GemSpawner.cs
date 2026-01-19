using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private BoardManager board;
    [SerializeField] private Gem[] gemPrefabs;
    [SerializeField] private GemPool pool;


    public void FillBoard()
    {
        for (int x = 0; x < BoardUtility.Width; x++)
        {
            for (int y = 0; y < BoardUtility.Height; y++)
            {
                SpawnGem(x, y, false);
            }
        }
    }

    public void SpawnGem(int x, int y, bool dropFromTop = false)
    {
        GemType? banH = board.GetBanTypeHorizontal(x, y);
        GemType? banV = board.GetBanTypeVertical(x, y);

        GemType type = SelectRandomType(banH, banV);
        Gem gem = CreateGem(type, x, y, dropFromTop);

        board.gemBoard[x, y] = gem;
    }
    private Gem CreateGem(GemType type, int x, int y, bool dropFromTop)
    {
        Gem gem = pool.Get(type);
        Vector3 pos = BoardUtility.GetCellWorldPos(x, y);

        if(dropFromTop == true)
        {
            Vector3 spawnPos = BoardUtility.GetCellWorldPos(x, y + 1);
            gem.transform.position = spawnPos;
            gem.SetCell(x, y, pos);
        }

        else
        {
            gem.transform.position = pos;
            gem.SetCell(x, y, pos);
        }

        return gem;
    }

    private GemType SelectRandomType(GemType? banH, GemType? banV)
    {
        int index = PickSafe(banH, banV);
        return gemPrefabs[index].type;
    }

    private int PickSafe(GemType? banH, GemType? banV)
    {
        for (int i = 0; i < 20; i++)
        {
            int index = Random.Range(0, gemPrefabs.Length);
            GemType t = gemPrefabs[index].type;

            if (t == banH) continue;
            if (t == banV) continue;

            return index;
        }

        return Random.Range(0, gemPrefabs.Length);
    }

    public void DespawnGem(Gem gem)
    {
        pool.Release(gem);
    }
}
