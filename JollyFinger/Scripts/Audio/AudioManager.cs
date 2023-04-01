using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioClip cleaningClip;

    [SerializeField] private AudioClip levelCompletedClip;

    private AudioSource cleaningAudioSource;

    private AudioSource levelCompletedAudioSource;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsBGMusicPlaying
    {
        get { return cleaningAudioSource.isPlaying; }
    }

    void Start()
    {
        cleaningAudioSource = gameObject.AddComponent<AudioSource>();

        levelCompletedAudioSource = gameObject.AddComponent<AudioSource>();

        cleaningAudioSource.clip = cleaningClip;

        levelCompletedAudioSource.clip = levelCompletedClip;
    }

    public void SetCleaningSound(AudioState.State audioState)
    {
        switch (audioState)
        {
            case AudioState.State.Play:
                cleaningAudioSource.loop = true;
                cleaningAudioSource.pitch = 3;
                cleaningAudioSource.Play();
                break;

            case AudioState.State.Pause:
                cleaningAudioSource.Pause();
                break;

            default:
                cleaningAudioSource.Stop();
                break;
        }
    }

    public void SetLevelCompletedSound(AudioState.State audioState)
    {
        switch (audioState)
        {
            case AudioState.State.Play:
                levelCompletedAudioSource.Play();
                break;

            default:
                levelCompletedAudioSource.Stop();
                break;
        }
    }
}