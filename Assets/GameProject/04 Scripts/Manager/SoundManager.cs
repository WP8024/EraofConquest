using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    
    public AudioSource BGMSound;

    public AudioClip[] bgmlist;
    public static SoundManager instance;

    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    { //씬이름과 클립이름 같은걸 재생
        for(int i =0;i < bgmlist.Length; i++)
        {
            if (arg0.name == bgmlist[i].name)
            {
                BGMSoundPlay(bgmlist[i]);
            }
        }
    }


    public void SFXPlay(string sfxName,AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("GameSE")[0];
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go, clip.length);
    }
    public void BGMSoundPlay(AudioClip clip)
    {
        BGMSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        BGMSound.clip = clip;
        BGMSound.loop = true;
        BGMSound.volume = 0.1f;
        BGMSound.Play();
    }
    public void SetBGMVolum(float volume)
    {
        mixer.SetFloat("BGMVolume", Mathf.Log10(volume) * 20);

    }
    public void SetSFXVolum(float volume)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
}
