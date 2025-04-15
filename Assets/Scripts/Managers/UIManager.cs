using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using static UnityEngine.Rendering.BoolParameter;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public TextMeshProUGUI[] text;
    public GameObject clearText; // Clear 텍스트 오브젝트를 드래그 앤 드롭으로 할당합니다.
    public float displayTime = 2.0f; // 텍스트가 표시될 시간(초)


    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        if (clearText != null)
        {
            clearText.gameObject.SetActive(false); // 시작할 때 텍스트 비활성화
        }
    }

    public void TurnOnText()
    {
        StartCoroutine(ShowClearText());
    }

    private IEnumerator ShowClearText()
    {
        if (clearText != null)
        {
            clearText.gameObject.SetActive(true); // 텍스트 활성화
            yield return new WaitForSeconds(displayTime); // displayTime 동안 대기
            clearText.gameObject.SetActive(false); // 텍스트 비활성화
        }
    }

    public void TurnOnText(TextMeshProUGUI puzzleText)
    {
        puzzleText.alpha = 255.0f;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.clickEffect); // 클릭 효과음 재생
    }
}