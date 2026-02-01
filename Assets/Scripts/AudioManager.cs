using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource backgroundMusicSource;
    public AudioSource keySource;
    public AudioSource maskSource;
    public AudioSource moneySource;
    public AudioSource doorSource;
    public AudioSource playerBulletSource;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayBackgroundMusic(AudioClip clip, float volume = 1f)
    {
        if (clip == null || backgroundMusicSource == null) return;

        backgroundMusicSource.clip = clip;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = volume;
        backgroundMusicSource.Play();
    }

    public void StopBackgroundMusic()
    {
        if (backgroundMusicSource != null)
            backgroundMusicSource.Stop();
    }

    public void PlayKey(AudioClip clip, float volume = 1f) => PlayOneShot(keySource, clip, volume);
    public void PlayMask(AudioClip clip, float volume = 1f) => PlayOneShot(maskSource, clip, volume);
    public void PlayMoney(AudioClip clip, float volume = 1f) => PlayOneShot(moneySource, clip, volume);
    public void PlayDoor(AudioClip clip, float volume = 1f) => PlayOneShot(doorSource, clip, volume);
    public void PlayPlayerBullet(AudioClip clip, float volume = 1f) => PlayOneShot(playerBulletSource, clip, volume);

    void PlayOneShot(AudioSource source, AudioClip clip, float volume)
    {
        if (source != null && clip != null)
            source.PlayOneShot(clip, volume);
    }
}