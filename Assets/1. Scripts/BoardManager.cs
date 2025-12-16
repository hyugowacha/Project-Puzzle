using Unity.Mathematics;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public const int Width = 8;
    public const int Height = 8;

    public Gem[,] board = new Gem[Width, Height];

    public Gem gemPrefeb;

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
        Gem gem = Instantiate(gemPrefeb);
        gem.transform.position = Cell(x, y);
        board[x, y] = gem;
    }

}
