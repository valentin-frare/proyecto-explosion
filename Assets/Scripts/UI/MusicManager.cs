using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using System;

[Serializable]
public class MusicState
{
    public Level level;
    public GameState state;
    public AudioClip music;
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] private List<MusicState> musics;
    [SerializeField] private AudioMixerGroup mixer;

    private AudioSource audioSource;

    private void Awake() 
    {
        instance = this;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer;
    }

    private void Start() 
    {
        GameManager.instance.OnGameStateChanged += OnGameStateChangedHandler;
    }

    private void OnGameStateChangedHandler(GameState state)
    {
        AudioClip clip = musics.Find(music => music.level == GameManager.instance.level && music.state == state)?.music;

        clip ??= musics.Find(music => music.state == state)?.music;

        if (clip == null) return;

        if (clip == audioSource.clip) return;

        StartCoroutine(FadeToOtherClip(clip));
    }

    public IEnumerator FadeToOtherClip(AudioClip clip)
    {
        while (audioSource.volume > 0)
        {
            audioSource.volume -= .03f;
            yield return null;
        }
        audioSource.clip = clip;
        audioSource.Play();
        while (audioSource.volume < 1)
        {
            audioSource.volume += .03f;
            yield return null;
        }
        yield break;
    }

    public void MuteMixer(bool active)
    {
        if (active)
        {
            mixer.audioMixer.SetFloat("Music", -80f);
        }
        else
        {
            mixer.audioMixer.SetFloat("Music", 0f);
        }
    }
}
