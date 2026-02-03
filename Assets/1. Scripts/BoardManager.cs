using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class BoardManager : MonoBehaviour
{
    public Gem[,] gemBoard = new Gem[BoardUtility.Width, BoardUtility.Height];

    [SerializeField] private GemSpawner spawner;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private ParticleSystem popEffect;

    private MatchChecker matchChecker;
    public bool IsLocked { get; private set; }

    private void Awake()
    {
        matchChecker = new MatchChecker(this);
    }

    private void Start()
    {
        spawner.FillBoard();
    }

    public void RequestSwap(Gem gem, Vector2Int dir)
    {
        if (IsLocked) return;

        Vector2Int a = new Vector2Int(gem.x, gem.y);
        Vector2Int b = a + dir;

        SwapResult result = TrySwap(a, b);

        if (result == SwapResult.Success)
        {
            //Debug.Log($"스왑 성공: {a} → {b}");
            StartCoroutine(MatchProcess());
        }
        else
        {
            //Debug.Log($"스왑 실패: {result}");
        }
    }

    public SwapResult TrySwap(Vector2Int a, Vector2Int b)
    {
        if (!BoardUtility.InBounds(a.x, a.y) || !BoardUtility.InBounds(b.x, b.y))
        {
            return SwapResult.OutOfBounds;
        }

        if (!BoardUtility.AreNeighbors(a, b))
        {
            return SwapResult.NotNeighbor;
        }

        SwapCells(a, b);

        bool matched = matchChecker.HasMatchAt(a.x, a.y) || matchChecker.HasMatchAt(b.x, b.y);

        if (!matched)
        {
            StartCoroutine(NotSwapAnimation(a, b));
            return SwapResult.NoMatch;
        }

        return SwapResult.Success;
    }

    private IEnumerator NotSwapAnimation(Vector2Int a, Vector2Int b)
    {
        yield return new WaitForSeconds(0.2f);

        SwapCells(a, b);
    }


    public void SwapCells(Vector2Int a, Vector2Int b)
    {
        Gem gemA = gemBoard[a.x, a.y];
        Gem gemB = gemBoard[b.x, b.y];

        gemBoard[a.x, a.y] = gemB;
        gemBoard[b.x, b.y] = gemA;

        if (gemA != null)
        {
            gemA.SetCell(b.x, b.y, BoardUtility.GetCellWorldPos(b.x, b.y));
        }

        if (gemB != null)
        {
            gemB.SetCell(a.x, a.y, BoardUtility.GetCellWorldPos(a.x, a.y));
        }
    }

    //-------------------------------------------------

    private IEnumerator MatchProcess()
    {
        IsLocked = true;

        bool hasMatch = true;

        while (hasMatch)
        {
            List<Gem> matchGem = FindAllMatch();

            if(matchGem.Count == 0)
            {
                hasMatch = false;
                break;
            }

            Debug.Log($"매치 수: {matchGem.Count}");


            yield return new WaitForSeconds(0.15f);

            scoreManager.AddScore(matchGem.Count);

            yield return StartCoroutine(DestroyGem(matchGem));

            yield return StartCoroutine(DropGems());

            yield return StartCoroutine(FillEmptyCell());

            yield return new WaitForSeconds(0.1f);
        }

        IsLocked = false;
    }

    private List<Gem> FindAllMatch()
    {
        HashSet<Gem> matches = new HashSet<Gem>();

        for (int x = 0; x < BoardUtility.Width; x++)
        {
            for(int y = 0; y < BoardUtility.Height; y++)
            {
                Gem gem = gemBoard[x, y];
                if (gem == null) continue;

                List<Gem> horizontalMatch = GetMatchInDirection(x, y, 1, 0);

                if(horizontalMatch.Count >= 3)
                {
                    matches.UnionWith(horizontalMatch);
                }

                List<Gem> verticalMatch = GetMatchInDirection(x, y, 0, 1);

                if(verticalMatch.Count >= 3)
                {
                    matches.UnionWith(verticalMatch);
                }
            }
        }

        return new List<Gem>(matches);
    }

    private List<Gem> GetMatchInDirection(int startX, int startY, int dx, int dy)
    {
        List<Gem> matches = new List<Gem>();
        Gem startGem = gemBoard[startX, startY];

        if(startGem == null)
        {
            return matches;
        }

        matches.Add(startGem);

        int x = startX + dx;
        int y = startY + dy;

        while(BoardUtility.InBounds(x, y))
        {
            Gem gem = gemBoard[x, y];
            if (gem == null || gem.type != startGem.type) break;

            matches.Add(gem);
            x += dx;
            y += dy;
        }

        return matches; 
    }

    private IEnumerator DestroyGem(List<Gem> gems)
    {
        foreach (Gem gem in gems)
        {
            if (gem != null)
            {
                Vector3 effectPos = gem.transform.position;
                effectPos.z = -3f;

                Instantiate(popEffect, effectPos, Quaternion.identity);
                gemBoard[gem.x, gem.y] = null;
                spawner.DespawnGem(gem);
            }
        }

        yield return new WaitForSeconds(0.2f);
    }

    private IEnumerator DropGems()
    {
        bool gemsDropped = false;

        for(int x = 0; x<BoardUtility.Width; x++)
        {
            int emptyY = 0;

            for(int y = 0; y<BoardUtility.Height; y++)
            {
                if (gemBoard[x,y]  != null)
                {
                    if (y != emptyY)
                    {
                        Gem gem = gemBoard[x, y];
                        gemBoard[x, y] = null;
                        gemBoard[x, emptyY] = gem;

                        gem.SetCell(x,emptyY,BoardUtility.GetCellWorldPos(x,emptyY));
                        gemsDropped = true;
                    }
                    emptyY++;
                }
            }
        }

        if (gemsDropped)
        {
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator FillEmptyCell()
    {
        bool gemsFilled = false;

        for (int x = 0; x < BoardUtility.Width; x++)
        {
            for (int y = 0; y < BoardUtility.Height; y++)
            {
                if (gemBoard[x, y] == null)
                {
                    spawner.SpawnGem(x, y, true); // 위에서 떨어지는 효과
                    gemsFilled = true;
                }
            }
        }

        if (gemsFilled)
        {
            yield return new WaitForSeconds(0.1f); // 생성 애니메이션 대기
        }
    }

    //----------------------------------------------

    public GemType? GetBanTypeHorizontal(int x, int y)
    {
        if (x < 2) return null;

        Gem a = gemBoard[x - 1, y];
        Gem b = gemBoard[x - 2, y];

        if (a == null || b == null) return null;

        return a.type == b.type ? a.type : null;
    }
    public GemType? GetBanTypeVertical(int x, int y)
    {
        if (y < 2) return null;

        Gem a = gemBoard[x, y - 1];
        Gem b = gemBoard[x, y - 2];

        if (a == null || b == null) return null;

        return a.type == b.type ? a.type : null;
    }



}
