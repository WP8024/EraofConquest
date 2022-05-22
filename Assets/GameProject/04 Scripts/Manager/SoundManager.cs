using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGSound;
    public AudioSource SFXsound;
    public static SoundManager instance;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(instance);
        
    }

    public void SetBGMVolum(float volume)
    {
        BGSound.volume = volume;
    }
    public void SetSFXVolum(float volume)
    {
        SFXsound.volume = volume;
    }
}
