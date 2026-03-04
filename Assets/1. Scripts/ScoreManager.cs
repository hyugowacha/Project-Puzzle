using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("기본 점수 세팅")]
    [SerializeField] private int baseScore = 100; //기본점수
    [SerializeField] private int comboBonus = 50; //콤보 보너스

    [Header("피버 타임 세팅")]
    public int maxFeverGage = 40; //깨야하는 블럭 수
    [SerializeField] private float feverDuraction = 10f; //지속 시간
    [SerializeField] private int feverScoreBonus = 2; //스코어 배수
    [SerializeField] private Animator feverTextAnim;


    private int currentScore = 0;
    [HideInInspector] public int currentCombo = 0;

    [HideInInspector] public float feverGage = 0;
    public bool isFever = false;
    private float feverTimer = 0f;

    private void Update()
    {
        if (isFever)
        {
            feverTimer -= Time.deltaTime;
            feverGage = Mathf.Lerp(0, maxFeverGage, feverTimer / feverDuraction);

            if (feverTimer <= 0)
            {
                EndFever();
            }
        }
    }

    public void AddScore(int gemCount, int combo)
    {
        int score = baseScore * gemCount; //기본 점수

        if (combo > 1)
        {
            score += comboBonus * combo; // 콤보 보너스
        }

        if (isFever)
        {
            score = score * feverScoreBonus; //피버 보너스
        }

        currentScore += score;
    }

    public void AddFeverGage(int gemCount)
    {
        if (isFever) return;

        feverGage += gemCount;

        if(feverGage >= maxFeverGage)
        {
            feverGage = maxFeverGage;
            StartFever();
        }
    }

    public void StartFever()
    {
        isFever = true;
        feverTimer = feverDuraction;
        feverGage = 0;

        feverTextAnim.SetTrigger("StartFever");
        Debug.Log("피버타임 시작");
    }

    private void EndFever()
    {
        isFever = false;
        feverTimer = 0;

        Debug.Log("피버타임 종료");
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void SetCombo(int combo)
    {
        currentCombo = combo;
    }


    //public void ResetScore()
    //{
    //    currentScore = 0;
    //    currentCombo = 0;
    //}
}
