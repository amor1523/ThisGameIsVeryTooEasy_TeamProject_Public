using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public bool _dataLoad;

    public bool dataLoad
    {
        get { return _dataLoad; }
    }

    public static StartButton instance;
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

    public void NewGameStart()
    {
        _dataLoad = false;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.successEffect);
        SceneManager.LoadScene(1); // 1: MainScene
    }

    public void LoadGameStart()
    {
        // TODO: 저장된 게임 불러오기
        _dataLoad = true;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.successEffect);
        SceneManager.LoadScene(1); // 1: MainScene
    }
}