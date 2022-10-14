using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private SoundType[] sounds;
    [SerializeField] private AudioMixerGroup mixer;

    private AudioSource audioSource;

    private void Awake() 
    {
        instance = this;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer;
    }

    public void StartSound(string soundKey, bool oneShot = true)
    {   
        AudioClip clip = null;

        try
        {
            // Utilizo LINQ para obtener el primer resultado de la lista de sonidos que cumpla con...
            clip = sounds.FirstOrDefault( 
                sound => sound.title == soundKey // Esta condición.
            ).sound; // Y tomo el parameto sound porque me devuelve un SoundType y solo necesito el sound.
            // LINQ es como SQL pero en codigo.
        }
        catch (NullReferenceException)
        {
            Debug.LogError("Se intento usar un audio que no se encuentra registrado.");
        }

        if (clip == null) return;

        if (oneShot)
        {
            if (audioSource.isPlaying)
            {
                var _tempAs = gameObject.AddComponent<AudioSource>();
                _tempAs.outputAudioMixerGroup = mixer;
                StartCoroutine(DestroyComponentAfter(_tempAs, clip.length));
                _tempAs.PlayOneShot(clip);
            }
            else
            {
                audioSource.PlayOneShot(clip);
            }
        }
        else 
        {
            if (audioSource.isPlaying)
            {
                var _tempAs = gameObject.AddComponent<AudioSource>();
                _tempAs.outputAudioMixerGroup = mixer;
                StartCoroutine(DestroyComponentAfter(_tempAs, clip.length));
                _tempAs.PlayOneShot(clip);
            }
            else
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }

    public IEnumerator DestroyComponentAfter(AudioSource audioSource, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(audioSource);
    }
}
