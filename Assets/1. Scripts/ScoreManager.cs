using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("기본 점수 세팅")]
    [SerializeField] private int baseScore = 100;

    private int currentScore = 0;

    public void AddScore(int gemCount)
    {
        int score = baseScore * gemCount;
        currentScore += score;

        Debug.Log($"점수 추가: {score} 총점: {currentScore}");
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
}
