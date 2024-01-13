using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _asBGM;
    [SerializeField] private AudioSource _asSE;

    [SerializeField] private List<AudioClip> _SEList;
    [SerializeField] private List<AudioClip> _BGMList;

    public static SoundManager Instance { get; private set; }

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

    public enum BGM
    {
        TITLE = 0,
        GAME,
        RESULT
    }

    public enum SE
    {
        NONE = 0,
        SELECT,
        ENTER,
    }

    public void PlayBGM(BGM bgmId)
    {
        _asBGM.clip = _BGMList[(int)bgmId];
        _asBGM.Play();
    }

    public void PlaySE(SE seId)
    {
        _asSE.clip = _SEList[(int)seId];
        _asSE.Play();
    }
}
