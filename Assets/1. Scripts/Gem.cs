using UnityEngine;
using UnityEngine.InputSystem;

public enum GemType
{
    Orange, Pink, Red, White, Yellow
}

public class Gem : MonoBehaviour
{
    public GemType type;
    public int x;
    public int y;

    private Vector3 targetPos;
    private bool isMoving;

    [SerializeField] private float moveSpeed = 10f;

    private void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos,
                Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                transform.position = targetPos;
                isMoving = false;
            }
        }
    }

    public void SetCell(int nx, int ny, Vector3 worldPos)
    {
        x = nx;
        y = ny;
        targetPos = worldPos;
        isMoving = true;
    }
}
