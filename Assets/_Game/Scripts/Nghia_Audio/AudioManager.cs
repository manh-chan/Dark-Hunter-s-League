using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;


    [Header("Music Sources")]
    public AudioSource menuMusicSource;
    public AudioSource gameMusicSource;

    [Header("Music Clips")]
    public AudioClip menuMusic;
    public List<AudioClip> gameMusics;

    [Header("SFX")]
    public AudioClip buttonClick;
    public AudioClip enemyDie;
    public AudioClip levelUp;

    [Header("SFX Settings")]
    public GameObject sfxAudioSourcePrefab; // Prefab có AudioSource
    public int maxSFXSources = 10;

    private Queue<AudioSource> sfxPool = new Queue<AudioSource>();

    [Header("Volume Settings")]
    [Range(0f, 1f)] private float volumeMaster = 1f;
    [Range(0f, 1f)] private float musicVolume = 1f;
    [Range(0f, 1f)] private float sfxVolume = 1f;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        InitSFXPool();
        ApplyVolumes();
        LoadVolumes();
    }
    public void ApplyVolumes()
    {
        float finalMusicVolume = volumeMaster * musicVolume;
        float finalSFXVolume = volumeMaster * sfxVolume;

        if (menuMusicSource != null) menuMusicSource.volume = finalMusicVolume;
        if (gameMusicSource != null) gameMusicSource.volume = finalMusicVolume;

        foreach (AudioSource sfx in sfxPool)
        {
            if (sfx != null) sfx.volume = finalSFXVolume;
        }
    }
    void InitSFXPool()
    {
        for (int i = 0; i < maxSFXSources; i++)
        {
            GameObject go = Instantiate(sfxAudioSourcePrefab, transform);
            go.SetActive(false);
            sfxPool.Enqueue(go.GetComponent<AudioSource>());
        }
    }

    AudioSource GetSFXSource()
    {
        if (sfxPool.Count > 0)
        {
            var src = sfxPool.Dequeue();
            src.gameObject.SetActive(true);
            return src;
        }
        else
        {
            GameObject go = Instantiate(sfxAudioSourcePrefab, transform);
            return go.GetComponent<AudioSource>();
        }
    }

    void ReturnSFXSource(AudioSource src)
    {
        src.Stop();
        src.gameObject.SetActive(false);
        sfxPool.Enqueue(src);
    }

    // --- MUSIC ---
    public void PlayMenuMusic()
    {
        StopAllMusic(); // Dừng nhạc gameplay nếu đang phát
        menuMusicSource.clip = menuMusic;
        menuMusicSource.loop = true;
        menuMusicSource.Play();
    }

    public void StartGameMusic()
    {
        StopAllMusic(); // Dừng nhạc menu nếu đang phát
        PlayNextGameMusic();
    }

    void PlayNextGameMusic()
    {
        int randIndex = Random.Range(0, gameMusics.Count);
        gameMusicSource.clip = gameMusics[randIndex];
        gameMusicSource.loop = false;
        gameMusicSource.Play();
        Invoke(nameof(PlayNextGameMusic), gameMusicSource.clip.length);
    }

    void StopAllMusic()
    {
        if (menuMusicSource.isPlaying)
            menuMusicSource.Stop();

        if (gameMusicSource.isPlaying)
            gameMusicSource.Stop();

        CancelInvoke(nameof(PlayNextGameMusic)); // Dừng vòng lặp nhạc random game
    }

    // --- SFX ---
    public void PlaySFX(AudioClip clip)
    {
        var src = GetSFXSource();
        src.clip = clip;
        src.volume = volumeMaster * sfxVolume;
        src.Play();
        StartCoroutine(ReturnAfterPlaying(src));
    }

    System.Collections.IEnumerator ReturnAfterPlaying(AudioSource src)
    {
        yield return new WaitForSeconds(src.clip.length);
        ReturnSFXSource(src);
    }

    public void PlayButtonClick() => PlaySFX(buttonClick);
    public void PlayEnemyDie() => PlaySFX(enemyDie);
    public void PlayLevelUp() => PlaySFX(levelUp);
    void LoadVolumes()
    {
        volumeMaster = PlayerPrefs.GetFloat("VolumeMaster", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        ApplyVolumes();
    }
    public void SetMasterVolume(float value)
    {
        volumeMaster = value;
        ApplyVolumes();
        PlayerPrefs.SetFloat("VolumeMaster", volumeMaster);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
        ApplyVolumes();
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = value;
        ApplyVolumes();
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }
}
