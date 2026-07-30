using System.Collections.Generic;
using UnityEngine;

public enum EBgm
{
    Plain,
    Clear,
    Die,
    Underground
}

public enum ESfx
{
    SFX_Move,
    SFX_Attack,
    SFX_Jump,
    SFX_Hit
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; 

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField] private AudioClip[] sfxClips;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    private Dictionary<EBgm, AudioClip> bgmDict; //BGM 딕셔너리
    private Dictionary<ESfx, AudioClip> sfxDict; //SFX 딕셔너리

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        InitDictionaries();
    }

    private void InitDictionaries()
    {
        bgmDict = new Dictionary<EBgm, AudioClip>();
        for (int i = 0; i < bgmClips.Length; i++) //클립 배열의 길이만큼
        {
            bgmDict[(EBgm)i] = bgmClips[i]; //키와 해시 설정
        }

        sfxDict = new Dictionary<ESfx, AudioClip>();
        for (int i = 0; i < sfxClips.Length; i++)
        {
            sfxDict[(ESfx)i] = sfxClips[i];
        }
    }

    public void PlayBGM(EBgm bgmType)
    {
        if (bgmDict.TryGetValue(bgmType, out var clip))
        {
            bgmSource.clip = clip;
            bgmSource.loop = true; //배경음악은 기본적으로 반복 재생
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning("BGM not found in Dictionary!");
        }
    }

    public void PlaySFX(ESfx sfxType)
    {
        if (sfxDict.TryGetValue(sfxType, out var clip))
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX not found in Dictionary!");
        }
    }
}
