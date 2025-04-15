using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static SoundManager Instance { get; private set; }

    // Bgm과 효과음 AudioSource
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("BGM")]
    public AudioClip MainBGM;
    public AudioClip EndingBGM;

    [Header("SFX")]
    public AudioClip clickEffect;
    public AudioClip successEffect;
    public AudioClip walkEffect;

    [Header("Setting")]
    public Slider bgmSlider;
    public Slider sfxSlider;
    // Scene전환이 되면 Slider UI가 없어져서 값 저장하기위한 변수 선언, Slider를 수정하지 않으면 null일 수 있어 nullable로 선언
    // 다른 스크립트에서 값을 가져가려고 만들었지만 만들고나니 그냥 bgmSource.volume 으로 가져올 수 있었다.
    public float? bgmSliderValue;
    public float? sfxSliderValue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayInitialBGM(MainBGM);
    }
       
    public void PlayInitialBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.loop = true; // BGM 반복 재생 설정

        if (bgmSliderValue == null) // nullable
        {
            bgmSource.Play();
        }
        else
        {
            bgmSource.Play();
            bgmSource.volume = (float)bgmSliderValue; // 명시적변환
        }
    }

    public void StopBGM(AudioClip clip)
    {
        bgmSource.Stop();
    }

    public void StartBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.loop = true;
        if (bgmSliderValue == null)
        {
            bgmSource.Play();
        }
        else
        {
            bgmSource.Play();
            bgmSource.volume = (float)bgmSliderValue;
        }
    }

    // BGM 재생 함수
    public void PlayBGM(AudioClip clip)
    {
        bgmSource.clip = clip;
        bgmSource.loop = true;
        if (sfxSliderValue == null)
        {
            bgmSource.Play();
        }
        else
        {
            bgmSource.Play();
            bgmSource.volume = (float)bgmSliderValue;
        }
    }

    // SFX 재생 함수
    public void PlaySFX(AudioClip clip)
    {
        if (sfxSliderValue == null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            sfxSource.volume = (float)sfxSliderValue;
            sfxSource.PlayOneShot(clip);
        }
    }

    // BGM 소리 조절 함수
    public void ChangeBGM(float value)
    {
        bgmSource.volume = value;
        bgmSliderValue = value;
    }

    // SFX 소리 조절 함수
    public void ChangeSFX(float value)
    {
        sfxSource.volume = value;
        sfxSliderValue = value;
    }

    // 특정 위치에서 SFX 재생 함수 (3D 오디오 설정 포함)
    // 0.0f: 2D 오디오, 화면의 모든 위치에서 동일한 볼륨으로 들립니다.
    // 1.0f: 3D 오디오, 소리가 발생한 위치를 기준으로 볼륨이 달라집니다.
    //       1.0f로 설정하면 완전히 3D 오디오로 작동합니다.
    //public void PlaySFX(AudioClip clip, Vector3 position, float spatialBlend = 1.0f, float maxDistance = 50.0f)
    //{
    //    GameObject tempGO = new GameObject("TempAudio");
    //    AudioSource aSource = tempGO.AddComponent<AudioSource>();
    //    tempGO.transform.position = position;

    //    aSource.clip = clip;
    //    aSource.spatialBlend = spatialBlend;
    //    aSource.maxDistance = maxDistance;
    //    aSource.Play();

    //    Destroy(tempGO, clip.length);
    //}
}