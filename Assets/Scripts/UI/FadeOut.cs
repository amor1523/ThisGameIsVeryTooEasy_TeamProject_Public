using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    public Image image; // Image 컴포넌트를 연결합니다
    public float fadeDuration = 2.0f; // 페이드 인 지속 시간

    void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }

        StartCoroutine(FadeInRoutine());
    }

    IEnumerator FadeInRoutine()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.successEffect); // Success 효과음 재생
        Color color = image.color;
        color.a = 0f; // 알파 값을 0으로 설정하여 완전히 투명하게 만듭니다
        image.color = color;

        float rate = 1.0f / fadeDuration;
        float progress = 0.0f;

        while (progress < 1.0f)
        {
            color.a = Mathf.Lerp(0, 1, progress);
            image.color = color;
            progress += rate * Time.deltaTime;

            yield return null;
        }

        color.a = 1.0f; // 알파 값을 1로 설정하여 완전히 불투명하게 만듭니다
        image.color = color;
        SceneManager.LoadScene(0); //StartScene

        // StartScene 로드 후 초기화
        InitializeStartScene();
    }

    private void InitializeStartScene()
    {
        // 마우스 커서 보이게 설정
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // StartScene의 배경음악 재생
        SoundManager.Instance.PlayBGM(SoundManager.Instance.MainBGM);
    }
}