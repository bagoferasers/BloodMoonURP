using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    [ Header( "Put mixer here:" ) ]
    public AudioMixer mixer;

    [ Header( "Put sliders here:" ) ]
    public Slider masterSlider;
    public Slider systemSlider;
    public Slider musicSlider;
    public Slider effectsSlider;

    public AudioSource[ ] jumpPunchList;
    public float howManySilent;
    private AudioSource clip;
    private float random;
    public bool isJumping;

    // start game with player prefs volumes?
    void Start( )
    {
        masterSlider.value = PlayerPrefs.GetFloat( "MasterVolume" );
        systemSlider.value = PlayerPrefs.GetFloat( "SystemVolume" );
        musicSlider.value = PlayerPrefs.GetFloat( "MusicVolume" );
        effectsSlider.value = PlayerPrefs.GetFloat( "EffectsVolume" );
        random = Random.Range( 0f, 1f );
        howManySilent = 0.5f;
        isJumping = false;
        clip = GetComponent< AudioSource >( );
    }

    void Update( )
    {
        random = Random.Range( 0f, 1f );
        if( random < howManySilent )
        {
            int songToPlay = Random.Range( 0, jumpPunchList.Length );
            if( isJumping )
                jumpPunchList[ songToPlay ].Play( );
        }
        isJumping = false;
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