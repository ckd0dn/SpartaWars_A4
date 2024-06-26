using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [Header("BGM")]
    public AudioClip bgmClips;                                      // ����� ����� ����
    public float bgmVolume;                                         // ����� ũ��
    private AudioSource bgmAudioSource;                             // ������� ����� ����� �ҽ�

    [Header("SFX")]
    public AudioClip[] sfxClips;                                    // ȿ���� ����� ���� �迭
    public float sfxVolume;                                         // ȿ���� ũ��
    private AudioSource sfxAudioSource;                             // ȿ������ ����� ����� �ҽ� ( ������ ���� �ٹ������� �Ҹ��� �� �� �����Ƿ� ����� �ҽ� �迭�� ��� )

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
        // ����� �÷��̾� �ʱ� ����
        GameObject bgmPlayer = new GameObject("BGM Player");
        bgmPlayer.transform.SetParent(this.transform);
        bgmAudioSource = bgmPlayer.AddComponent<AudioSource>();
        bgmAudioSource.playOnAwake = true;
        bgmAudioSource.loop = true;
        bgmAudioSource.clip = bgmClips;
        bgmAudioSource.Play();

        // ȿ���� �÷��̾� �ʱ� ����
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
