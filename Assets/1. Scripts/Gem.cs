using UnityEngine;
using UnityEngine.InputSystem;

public enum GemType
{
    Blue, Green, Pink, SkyBlue, Yellow
}

public class Gem : MonoBehaviour
{
    public GemType type;
    public int x;
    public int y;

    public void SetCell(int nx, int ny, Vector3 worldPos)
    {
        x = nx;
        y = ny;
        transform.position = worldPos;
    }
}
