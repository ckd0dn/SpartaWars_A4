using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [Header("BGM")]
    public AudioClip bgmClips;                                      // 배경음 오디오 파일
    public float bgmVolume;                                         // 배경음 크기
    private AudioSource bgmAudioSource;                             // 배경음을 재생할 오디오 소스

    [Header("SFX")]
    public AudioClip[] sfxClips;                                    // 효과음 오디오 파일 배열
    public float sfxVolume;                                         // 효과음 크기
    private AudioSource sfxAudioSource;                             // 효과음을 재생할 오디오 소스 ( 유닛이 동시 다발적으로 소리를 낼 수 있으므로 오디오 소스 배열을 사용 )

    [Header("Volume")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    public GameObject SoundPanel;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    private void Start()
    {
        LoadVolume();
    }

    void Init()
    {
        // 배경음 플레이어 초기 셋팅
        GameObject bgmPlayer = new GameObject("BGM Player");
        bgmPlayer.transform.SetParent(this.transform);
        bgmAudioSource = bgmPlayer.AddComponent<AudioSource>();
        bgmAudioSource.playOnAwake = true;
        bgmAudioSource.loop = true;
        bgmAudioSource.clip = bgmClips;
        bgmAudioSource.Play();

        // 효과음 플레이어 초기 셋팅
        GameObject sfxPlayer = new GameObject("SFX Player");
        sfxPlayer.transform.SetParent(this.transform);
        sfxAudioSource = sfxPlayer.AddComponent<AudioSource>();
        sfxAudioSource.playOnAwake = false;
        sfxAudioSource.loop = false;        
    }

    public void PlaySFX(SFXSound sfx)
    {       
        sfxAudioSource.PlayOneShot(sfxClips[(int)sfx]);
    }

    public void SaveBGMVolume()
    {   
        bgmVolume = bgmSlider.value;

        PlayerPrefs.SetFloat("BGM Volume", bgmVolume);

        bgmAudioSource.volume = bgmVolume / 100;
    }

    public void SaveSFXVolume()
    {
        sfxVolume = sfxSlider.value;

        PlayerPrefs.SetFloat("SFX Volume", sfxVolume);

        sfxAudioSource.volume = sfxVolume / 100;
    }

    public void LoadVolume()
    {
        bgmVolume = PlayerPrefs.GetFloat("BGM Volume", 50);
        sfxVolume = PlayerPrefs.GetFloat("SFX Volume", 50);

        bgmAudioSource.volume = bgmVolume / 100;
        sfxAudioSource.volume = sfxVolume / 100;

        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;
    }

    public void InitData()
    {
        PlayerPrefs.SetFloat("SFX Volume", 50);
        PlayerPrefs.SetFloat("BGM Volume", 50);

        bgmVolume = 50;
        sfxVolume = 50;

        bgmAudioSource.volume = bgmVolume / 100;
        sfxAudioSource.volume = sfxVolume / 100;

        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;
    }

}
