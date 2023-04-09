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

        if( time > 0.7f && notPressed && dayNightCycle.goBack == false )
        {
            Debug.Log( "time > 0.5f && notPressed && dayNightCycle.goBack == false" );
            time = 0.8f;
            notPressed = false;
            PlayerPrefs.SetFloat( "timeOfDay", time );
            Debug.Log( PlayerPrefs.GetFloat( "timeOfDay" ) );
            dayNightCycle.breakWait = true;
            dayNightCycle.goBack = true;
        }
        else if( time > 0.2f && notPressed && dayNightCycle.goBack == false )
        {
            Debug.Log( "time > 0.2f && notPressed && dayNightCycle.goBack == false" );
            time = 0.7f;
            notPressed = false;
            PlayerPrefs.SetFloat( "timeOfDay", time );
            Debug.Log( PlayerPrefs.GetFloat( "timeOfDay" ) );
            dayNightCycle.breakWait = true;
            dayNightCycle.goBack = false;
        }
        else if( time > 0.7f && notPressed && dayNightCycle.goBack == true )
        {
            Debug.Log( "time > 0.7f && notPressed && dayNightCycle.goBack == true" );
            time = 0.2f;
            notPressed = false;
            PlayerPrefs.SetFloat( "timeOfDay", time );
            Debug.Log( PlayerPrefs.GetFloat( "timeOfDay" ) );
            dayNightCycle.breakWait = true;
            dayNightCycle.goBack = false;
        }
        else if( time > 0.2f && notPressed && dayNightCycle.goBack == true )
        {
            Debug.Log( "time > 0.2f && notPressed && dayNightCycle.goBack == true" );
            time = 0.4f;
            notPressed = false;
            PlayerPrefs.SetFloat( "timeOfDay", time );
            Debug.Log( PlayerPrefs.GetFloat( "timeOfDay" ) );
            dayNightCycle.breakWait = true;
            dayNightCycle.goBack = false;
        }
    }

    void Update( ) 
    {
        if( PlayerPrefs.GetFloat( "timeOfDay" ) < 0.3f )
        {
            Debug.Log( "timeOfDay < 0.3f");
            float sleepy = PlayerPrefs.GetFloat( "Health" );
            sleepy -= 0.001f * Time.deltaTime;
            PlayerPrefs.SetFloat( "Health", sleepy );
        }
    }
}
