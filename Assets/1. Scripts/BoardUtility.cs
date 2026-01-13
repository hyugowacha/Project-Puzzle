using UnityEngine;

public static class BoardUtility 
{
    public const int Width = 8;
    public const int Height = 8;

    public static bool InBounds(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    public static bool AreNeighbors(Vector2Int a, Vector2Int b)
    {
        int dx = Mathf.Abs(a.x - b.x);
        int dy = Mathf.Abs(a.y - b.y);
        return (dx + dy == 1);
    }

    public static Vector3 GetCellWorldPos(int x, int y)
    {
        return new Vector3(x - 4 + 0.5f, y - 4 + 0.5f, 0f);
    }
}
