using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ScoreManager scoreManager;

    private void Update()
    {
        if (scoreManager != null && scoreText != null)
        {
            int score = scoreManager.GetScore();
            scoreText.text = score.ToString("D8");
        }
    }
}
