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
        // RŰ�� �����ų� Restart�� �����Ǿ��� ��
        if (Input.GetKeyDown(KeyCode.R) || isRestart)
        {
            Restart();
        }

        // ���� �ӵ� ���� �ڵ�
        Time.timeScale = gameSpeed;

        if (isGameStart)
        {
            OpenFade();
        }

        // ���� ����
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
            // ���̵� ����
            openFadeTimer += Time.deltaTime;
            // ���� �����ؾ���
            FadeScreenCanvasGroup.alpha = openFadeDuration - openFadeTimer;
        }
        else if (openFadeTimer >= openFadeDuration)
        {
            // ȣ�� ������
            isGameStart = false;
        }
    }

    void Restart()
    {
        isRestart = true;

        Debug.Log("Restart �Լ� ����");
        fadeTimer += Time.deltaTime;
        FadeScreenCanvasGroup.alpha = fadeTimer / fadeDuration;

        if (fadeTimer >= fadeDuration)
        {
            Debug.Log("�� �ʱ�ȭ!");
            SceneManager.LoadScene(0);
        }
        
    }

    public void CollidePin()
    {
        Debug.Log("�� �浹!");
        Invoke("Restart", 4);
    }
}
