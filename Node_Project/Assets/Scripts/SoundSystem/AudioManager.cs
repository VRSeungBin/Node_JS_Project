using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioManager() { }
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource buttonSource;

    public float soundVolume = 1.0f;
    public float bgmVolume = 1.0f;

    public bool fadeInMusicflag = false;           //페이드 인아웃

    private void Awake()                            //싱글톤이기 때문에 처리해주는 것들
    {
        if(Instance != null)
        {
            Destroy(gameObject);                    //같은 객체 제거
            return;
        }
        else
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);          //씬 이동시 파괴 되지 않게 하기 위해서
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (!clip) return;
        musicSource.clip = clip;
        musicSource.volume = bgmVolume;
        musicSource.Play();
    }

    public void PlayOneShot(AudioClip clip)
    {
        if (!clip) return;

        sfxSource.clip = clip;
        sfxSource.volume = soundVolume;
        sfxSource.PlayOneShot(sfxSource.clip);
    }

    public void PlayOneShotButton(AudioClip clip)
    {
        if (!clip) return;

        buttonSource.clip = clip;
        buttonSource.volume = soundVolume;
        buttonSource.PlayOneShot(buttonSource.clip);
    }

    public IEnumerator FadeIn(AudioSource audioSource, float fadeTime)
    {
        float startVolume = 0.0f;
        audioSource.volume = startVolume;
        audioSource.Play();

        while (audioSource.volume < bgmVolume)
        {
            audioSource.volume += bgmVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    public IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;
       
        while (audioSource.volume > 0.0f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
    }

    public void FadeInMuisc(AudioClip newMusic, float fadeTime)
    {
        if (!newMusic) return;
        if (fadeInMusicflag) return;

        StartCoroutine(FadeInMusicroutine(newMusic, fadeTime));

    }

    public IEnumerator FadeInMusicroutine(AudioClip newMusic, float fadeTime)
    {
        fadeInMusicflag = true;
        //이전 음악 페이드 아웃
        yield return StartCoroutine(FadeOut(musicSource, fadeTime));
        //새로운 음악을 페이드인 
        musicSource.clip = newMusic;
        yield return StartCoroutine(FadeIn(musicSource, fadeTime));

        fadeInMusicflag = false;
    }


}
