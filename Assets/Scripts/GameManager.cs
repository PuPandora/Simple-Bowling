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

        // �����
        if (isRestart)
            Restart();

        if (isQuit)
        {
            StartCoroutine(Quit());
        }

        // ���� �ӵ� ���� �ڵ�
        Time.timeScale = gameSpeed;

        // ���� ����
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isQuit = true;
        }
    }

    IEnumerator FadeIn()
    {
        yield return null;

        // ���̵� �� �ڵ�
        if (fadeInTimer <= fadeInDuration)
        {
            fadeInTimer += Time.deltaTime;
            FadeScreenCanvasGroup.alpha = fadeInDuration - fadeInTimer;
        }

        // ���̵� �� �Ϸ�� ����
        yield break;
    }

    void Restart()
    {
        Debug.Log("Restart �Լ� ����");
        FadeOut(fadeOutDurationRestart, 0f);

        if (isFadeOutEnd)
        {
            Debug.Log("�� �ʱ�ȭ!");
            SceneManager.LoadScene(0);
        }
    }

    void FadeOut(float fadeOutDuration, float blackScreenDuration)
    {
        Debug.Log("FadeOut �Լ� ����");

        fadeOutTimer += Time.deltaTime;
        FadeScreenCanvasGroup.alpha = fadeOutTimer / fadeOutDuration;

        if (fadeOutTimer >= fadeOutDuration + blackScreenDuration)
            isFadeOutEnd = true;
    }

    public IEnumerator CollidePin()
    {
        Debug.Log("�� �浹!");
        yield return new WaitForSeconds(4f);

        isRestart = true;
        yield break;
    }

    IEnumerator Quit()
    {
        Debug.Log("Quit �ڷ�ƾ �۵�");
        yield return null;
        FadeOut(fadeOutDurationQuit, 1f);

        if (isFadeOutEnd)
        {
            Debug.Log("����/������ ����");
            Application.Quit();
            UnityEditor.EditorApplication.isPlaying = false;
        }

    }
}
