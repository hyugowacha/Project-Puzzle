using System.Xml.Schema;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager dataManager;

    [Header("게임 결과")]
    public int finalScore = 0;
    public int maxCombo = 0;
    public int feverCount = 0;
    public int totalGems = 0;

    private void Awake()
    {
        dataManager = this;
        transform.SetParent(null); 
        DontDestroyOnLoad(gameObject); 
    }

    public void SaveResult(int score, int combo, int fever, int gems)
    {
        finalScore = score;
        maxCombo = combo;
        feverCount = fever;
        totalGems = gems;
    }

    public string SetRank(int score)
    {
        if (score >= 100000) return "S+";
        if (score >= 50000) return "S";
        if (score >= 40000) return "A";
        if (score >= 30000) return "B";
        if (score >= 10000) return "C";
        return "D";
    }
}
