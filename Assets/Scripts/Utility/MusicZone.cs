using UnityEngine;

public class BGMZone : MonoBehaviour
{
    public AudioSource audioSource;
    public float fadeTime = 1f;
    private float maxVolume = 0.5f;
    private float targetVolume;

    private void Start()
    {
        targetVolume = 0;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = targetVolume;
    }

    private void Update()
    {
        if (!Mathf.Approximately(audioSource.volume, targetVolume))
        {
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, (maxVolume / fadeTime) * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.StopBGM(SoundManager.Instance.MainBGM);
            targetVolume = SoundManager.Instance.bgmSource.volume;
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetVolume = 0f;
            audioSource.Stop();
            SoundManager.Instance.StartBGM(SoundManager.Instance.MainBGM);
        }
    }
}