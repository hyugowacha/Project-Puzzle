using UnityEngine;

public class GameStatistics : MonoBehaviour
{
    public static GameStatistics gameStatistics;

    [Header("게임 통계")]
    public int totalGems = 0;
    public int maxCombo = 0;
    public int feverCount = 0;

    private void Awake()
    {
        if(gameStatistics == null)
        {
            gameStatistics = this;
            Debug.Log("GameStatistics 생성");
            ResetStats();
        }
        else
        {
            Debug.Log("GameStatistics 중복");
            Destroy(gameObject);
        }
    }


    public void AddGems(int count)
    {
        totalGems += count;
    }

    public void UpdateMaxCombo(int combo)
    {
        if(combo > maxCombo)
        {
            maxCombo = combo;
        }
    }

    public void AddFeverCount()
    {
        feverCount++;
    }

    public void ResetStats()
    {
        totalGems = 0;
        maxCombo = 0;
        feverCount = 0;
        Debug.Log("통계 초기화");
    }
}
