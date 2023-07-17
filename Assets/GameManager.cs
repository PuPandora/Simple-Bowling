using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isRestart = false;
    [SerializeField]
    // Close Fade
    private float fadeTimer;
    private float fadeDuration = 2f;
    // Open Fade
    [SerializeField]
    private bool isGameStart = true;
    public float openFadeTimer;
    public float openFadeDuration = 1.5f;

    [SerializeField]
    private float gameSpeed;

    public CanvasGroup FadeScreenCanvasGroup;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        Debug.Log(fadeTimer);
        // R키를 누르거나 Restart가 감지되었을 때
        if (Input.GetKeyDown(KeyCode.R) || isRestart)
        {
            Restart();
        }

        // 게임 속도 조절 코드
        Time.timeScale = gameSpeed;

        if (isGameStart)
        {
            OpenFade();
        }

        // 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void OpenFade()
    {
        Debug.Log(openFadeTimer <= openFadeDuration);

        if (openFadeTimer <= openFadeDuration)
        {
            // 페이드 열기
            openFadeTimer += Time.deltaTime;
            // 점점 감소해야함
            FadeScreenCanvasGroup.alpha = openFadeDuration - openFadeTimer;
        }
        else if (openFadeTimer >= openFadeDuration)
        {
            // 호출 끝내기
            isGameStart = false;
        }
    }

    void Restart()
    {
        isRestart = true;

        Debug.Log("Restart 함수 동작");
        fadeTimer += Time.deltaTime;
        FadeScreenCanvasGroup.alpha = fadeTimer / fadeDuration;

        if (fadeTimer >= fadeDuration)
        {
            Debug.Log("씬 초기화!");
            SceneManager.LoadScene(0);
        }
        
    }

    public void CollidePin()
    {
        Debug.Log("핀 충돌!");
        Invoke("Restart", 4);
    }
}
