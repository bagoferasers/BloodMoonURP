using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    [ Header( "Put mixer here:" ) ]
    public AudioMixer mixer;

    [ Header( "Put music slider here:") ]
    public Slider musicSlider;

    //[ Header( "Put system slider here:") ]
    //public Slider systemSlider;

    //[ Header( "Put master slider here:") ]
    //public Slider masterSlider;

    // start game with player prefs volumes?

    public void setMusicVolume( float musicVolume )
    {
        Debug.Log( "Setting Volume Music" );
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
}