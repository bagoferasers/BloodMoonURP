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

    public AudioSource[ ] jumpList;
    public AudioSource[ ] punchList;
    public float howManySilent;
    private float random;
    public bool isPunching;
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
        isPunching = false;
    }

    void Update( )
    {
        random = Random.Range( 0f, 1f );            
        
        int songToPlay = Random.Range( 0, jumpList.Length );
        int punchToPlay = Random.Range( 0, punchList.Length );

        if( random < howManySilent && isJumping )
            jumpList[ songToPlay ].Play( ); 

        if( isPunching )
            punchList[ punchToPlay ].Play( );

        isJumping = false;
        isPunching = false;
    }

    public void setMusicVolume( float musicVolume )
    {
        if( musicSlider.value < -25f )
        {
            mixer.SetFloat( "MusicVolume", -80f );
            PlayerPrefs.SetFloat( "MusicVolume", -80f );
        }
        else
        {
            mixer.SetFloat( "MusicVolume", musicVolume );
            PlayerPrefs.SetFloat( "MusicVolume", musicVolume );            
        }
    }
    
    public void setSystemVolume( float sysVolume )
    {
        if( systemSlider.value < -25f )
        {
            mixer.SetFloat( "SystemVolume", -80f );
            PlayerPrefs.SetFloat( "SystemVolume", -80f );
        }
        else
        {
            mixer.SetFloat( "SystemVolume", sysVolume );
            PlayerPrefs.SetFloat( "SystemVolume", sysVolume );            
        }
    }

    public void setMasterVolume( float masterVolume )
    {
        if( masterSlider.value < -25f )
        {
            mixer.SetFloat( "MasterVolume", -80f );
            PlayerPrefs.SetFloat( "MasterVolume", -80f );
        }
        else
        {
            mixer.SetFloat( "MasterVolume", masterVolume );
            PlayerPrefs.SetFloat( "MasterVolume", masterVolume );            
        }
    }

    public void setEffectsVolume( float effectsVolume )
    {
        if( effectsSlider.value < -25f )
        {
            mixer.SetFloat( "EffectsVolume", -80f );
            PlayerPrefs.SetFloat( "EffectsVolume", -80f );
        }
        else
        {
            mixer.SetFloat( "EffectsVolume", effectsVolume );
            PlayerPrefs.SetFloat( "EffectsVolume", effectsVolume );            
        }
    }
}