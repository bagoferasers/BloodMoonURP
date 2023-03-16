using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    [ Header( "Put mixer here:" ) ]
    public AudioMixer mixer;

    // start game with player prefs volumes?
    void Start( )
    {
        DontDestroyOnLoad( gameObject );
        mixer.SetFloat( "MusicVolume", PlayerPrefs.GetFloat( "MusicVolume" ) );
        mixer.SetFloat( "SystemVolume", PlayerPrefs.GetFloat( "SystemVolume" ) );
        mixer.SetFloat( "MasterVolume", PlayerPrefs.GetFloat( "MasterVolume" ) );
        mixer.SetFloat( "EffectsVolume", PlayerPrefs.GetFloat( "EffectsVolume" ) );
    }

    public void setMusicVolume( float musicVolume )
    {
        mixer.SetFloat( "MusicVolume", musicVolume );
        PlayerPrefs.SetFloat( "MusicVolume", musicVolume );
    }
    
    public void setSystemVolume( float sysVolume )
    {
        mixer.SetFloat( "SystemVolume", sysVolume );
        PlayerPrefs.SetFloat( "SystemVolume", sysVolume );
    }

    public void setMasterVolume( float masterVolume )
    {
        mixer.SetFloat( "MasterVolume", masterVolume );
        PlayerPrefs.SetFloat( "MasterVolume", masterVolume );
    }

    public void setEffectsVolume( float effectsVolume )
    {
        mixer.SetFloat( "EffectsVolume", effectsVolume );
        PlayerPrefs.SetFloat( "EffectsVolume", effectsVolume );
    }
}