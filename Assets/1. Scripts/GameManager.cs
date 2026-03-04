using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    [Header("게임 세팅")]
    public float gameTime = 60;

    [Header("플레이 스타트 관련 UI")]
    [SerializeField] private GameObject readyText;
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject gameoverText;

    [Header("게임 상황")]
    public float remainingTime;
    public bool isGamePlaying = false;
    public bool isGameOver = false;

    [HideInInspector] public UnityEvent OnGameStart;
    [HideInInspector] public UnityEvent OnGameOver;

    private void Awake()
    {
        if(gameManager == null)
        {
            gameManager = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        remainingTime = gameTime;
        isGamePlaying = false;
        isGameOver = false;

        gameoverText.SetActive(false);

        StartCoroutine(GameStartSequence());
    }


    void Update()
    {
        if(isGamePlaying && !isGameOver)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0)
            {
                remainingTime = 0;
                GameOver();
            }
        }
    }

    private IEnumerator GameStartSequence()
    {
        yield return new WaitForSeconds(1.5f);

        readyText.SetActive(true);
        startText.SetActive(false);

        yield return new WaitForSeconds(1.5f);

        readyText.SetActive(false);
        startText.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        if(startText != null)
        {
            startText.SetActive(false);
        }

        StartGame();
    }

    private void StartGame()
    {
        isGamePlaying = true;
        Debug.Log("게임 시작");
    }

    private void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        isGamePlaying = false;

        gameoverText.SetActive(true);

        Debug.Log("타임 오버");
    }
}
