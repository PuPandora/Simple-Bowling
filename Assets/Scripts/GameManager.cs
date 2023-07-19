using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    // Close Fade
    private float fadeOutTimer;
    private float fadeOutDurationRestart = 2f;
    private float fadeOutDurationQuit = 2f;
    private bool isFadeOutEnd = false;
    // Open Fade
    [SerializeField]
    public float fadeInTimer;
    public float fadeInDuration = 1.5f;

    [SerializeField]
    private float gameSpeed;

    public CanvasGroup FadeScreenCanvasGroup;
    public bool isRestart;
    private bool isQuit;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        StartCoroutine(FadeIn());

        if (Input.GetKeyDown(KeyCode.R))
        {
            isRestart = true;
        }

        // 재시작
        if (isRestart)
            Restart();

        if (isQuit)
        {
            StartCoroutine(Quit());
        }

        // 게임 속도 조절 코드
        Time.timeScale = gameSpeed;

        // 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isQuit = true;
        }
    }

    IEnumerator FadeIn()
    {
        yield return null;

        // 페이드 인 코드
        if (fadeInTimer <= fadeInDuration)
        {
            fadeInTimer += Time.deltaTime;
            FadeScreenCanvasGroup.alpha = fadeInDuration - fadeInTimer;
        }

        // 페이드 인 완료시 종료
        yield break;
    }

    void Restart()
    {
        Debug.Log("Restart 함수 동작");
        FadeOut(fadeOutDurationRestart, 0f);

        if (isFadeOutEnd)
        {
            Debug.Log("씬 초기화!");
            SceneManager.LoadScene(0);
        }
    }

    void FadeOut(float fadeOutDuration, float blackScreenDuration)
    {
        Debug.Log("FadeOut 함수 동작");

        fadeOutTimer += Time.deltaTime;
        FadeScreenCanvasGroup.alpha = fadeOutTimer / fadeOutDuration;

        if (fadeOutTimer >= fadeOutDuration + blackScreenDuration)
            isFadeOutEnd = true;
    }

    public IEnumerator CollidePin()
    {
        Debug.Log("핀 충돌!");
        yield return new WaitForSeconds(4f);

        isRestart = true;
        yield break;
    }

    IEnumerator Quit()
    {
        Debug.Log("Quit 코루틴 작동");
        yield return null;
        FadeOut(fadeOutDurationQuit, 1f);

        if (isFadeOutEnd)
        {
            Debug.Log("게임/에디터 종료");
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }

    }
}
