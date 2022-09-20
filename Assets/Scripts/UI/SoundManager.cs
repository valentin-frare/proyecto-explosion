using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private SoundType[] sounds;

    private AudioSource audioSource;

    private void Awake() 
    {
        instance = this;
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
            audioSource.PlayOneShot(clip);
        }
        else 
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void PauseSound()
    {

    }

    public void StopSound()
    {

    }
}
