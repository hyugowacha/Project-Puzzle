using Unity.Mathematics;
using UnityEngine;

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

    public Vector3 Cell(int x, int y)
    {
        return new Vector3
        (
            x - 4 + 0.5f, y - 4 + 0.5f, 0f
        );
    }


}
