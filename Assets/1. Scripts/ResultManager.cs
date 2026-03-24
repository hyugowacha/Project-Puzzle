using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [Header("¸®ÀýÆ® UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI maxComboText;
    [SerializeField] private TextMeshProUGUI feverText;
    [SerializeField] private TextMeshProUGUI totalGemsText;
    [SerializeField] private TextMeshProUGUI rankText;

    private void Start()
    {
        ResultDisplay();
    }

    private void ResultDisplay()
    {
        int finalScore = DataManager.dataManager.finalScore;
        scoreText.text = $"Score {finalScore.ToString("D7")}";

        int maxCombo = DataManager.dataManager.maxCombo;
        maxComboText.text = $"Max Combo X{maxCombo}";

        int feverCount = DataManager.dataManager.feverCount;
        feverText.text = $"Fever X{feverCount}";

        int totalGems = DataManager.dataManager.totalGems;
        totalGemsText.text = $"Total gems {totalGems.ToString("D4")}";

        string rank = DataManager.dataManager.SetRank(finalScore);
        rankText.text = rank;
    }

    public void OnTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
