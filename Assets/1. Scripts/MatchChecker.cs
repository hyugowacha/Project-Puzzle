using UnityEngine;

public class MatchChecker
{
    private BoardManager board;

    public MatchChecker(BoardManager boardManager)
    {
        board = boardManager;
    }

    public bool HasMatchAt(int x, int y)
    {
        if (!BoardUtility.InBounds(x, y)) return false;

        Gem gem = board.gemBoard[x, y];
        if (gem == null) return false;

        GemType type = gem.type;

        // 가로 체크
        int horizontalCount = 1;
        horizontalCount += CountSame(x, y, -1, 0, type);
        horizontalCount += CountSame(x, y, 1, 0, type);
        if (horizontalCount >= 3) return true;

        // 세로 체크
        int verticalCount = 1;
        verticalCount += CountSame(x, y, 0, -1, type);
        verticalCount += CountSame(x, y, 0, 1, type);
        return verticalCount >= 3;
    }

    private int CountSame(int x, int y, int dx, int dy, GemType type)
    {
        int count = 0;
        int nx = x + dx;
        int ny = y + dy;

        while (BoardUtility.InBounds(nx, ny))
        {
            Gem ng = board.gemBoard[nx, ny];
            if (ng == null || ng.type != type) break;

            count++;
            nx += dx;
            ny += dy;
        }

        return count;
    }
}
