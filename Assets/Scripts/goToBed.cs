using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goToBed : MonoBehaviour
{
    [ Header( "Health and Shields" ) ]
    public Slider healthBar;
    public Slider shieldBar;
    private bool notPressed;

    void Start( )
    {
        notPressed = true;
    }

    public void sleep( )
    {
        shieldBar.value = PlayerPrefs.GetFloat( "MaxHealth" );
        healthBar.value = PlayerPrefs.GetFloat( "MaxShield" );
        float time = PlayerPrefs.GetFloat( "timeOfDay" );

        if( time < 0.7f && notPressed )
        {
            Debug.Log( "Time < 0.7f" );
            time += 0.3f;
            notPressed = false;
            PlayerPrefs.SetFloat( "timeOfDay", time );
            Debug.Log( PlayerPrefs.GetFloat( "timeOfDay" ) );
            dayNightCycle.breakWait = true;
        }
            
        else if( time > 0.7f && notPressed ) // not working
        {
            Debug.Log( "Time > 0.7f" );
            float difference = 0;
            float waitTime = 0.3f;
            float remaining = 1f - time;
            remaining -= waitTime;
            time = 0.2f + remaining;
            notPressed = false;
            PlayerPrefs.SetFloat( "timeOfDay", time );
            Debug.Log( PlayerPrefs.GetFloat( "timeOfDay" ) );
            dayNightCycle.breakWait = true;
        }

    }
}
