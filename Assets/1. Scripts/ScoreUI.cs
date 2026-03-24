using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [Header("점수 관련")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private ScoreManager scoreManager;

    [Header("콤보 관련")]
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private Animator comboAnimator;

    [Header("피버 관련")]
    [SerializeField] private Slider feverGageSlider;


    private int lastCombo = 1;

    private void Update()
    {
        int score = scoreManager.GetScore();
        scoreText.text = score.ToString("D6");

        UpdateFeverGage();
        UpdateFeverTime();
    }

    public void ScoreAnimation(int currentCombo)
    {

        if (currentCombo > lastCombo)
        {
            comboText.text = $"COMBO X {currentCombo}";
            comboAnimator.SetTrigger("Combo");
        }
    }


    private void UpdateFeverGage()
    {
        feverGageSlider.maxValue = scoreManager.maxFeverGage;
        feverGageSlider.value = scoreManager.feverGage;
    }

    private void UpdateFeverTime()
    {
        bool isFever = scoreManager.isFever;


    }
}
