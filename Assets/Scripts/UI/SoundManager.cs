using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SoundType
{
    public string title;
    public AudioClip sound;
}

public class SoundManager : MonoBehaviour
{
    public SoundType[] sounds;

    public void StartSound()
    {

    }

    public void PauseSound()
    {

    }

    public void StopSound()
    {

    }
}
