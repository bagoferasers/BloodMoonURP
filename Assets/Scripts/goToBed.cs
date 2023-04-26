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

        if( time > 0.6f && notPressed && dayNightCycle.goBack == false )
        {
            time = 0.9f;
            notPressed = false;
            PlayerPrefs.SetFloat( "timeOfDay", time );
            dayNightCycle.breakWait = true;
            dayNightCycle.goBack = true;
            PlayerPrefs.SetInt( "HaveSlept", 1 );
            PlayerPrefs.SetFloat( "DayCanvas", time );
        }
        else if( time > 0.6f && notPressed && dayNightCycle.goBack == true )
        {
            time = 0.3f;
            notPressed = false;
            PlayerPrefs.SetFloat( "timeOfDay", time );
            dayNightCycle.breakWait = true;
            dayNightCycle.goBack = false;
            PlayerPrefs.SetInt( "HaveSlept", 1 );
            PlayerPrefs.SetFloat( "DayCanvas", time );
        }
        else if( time > 0.1f && notPressed && dayNightCycle.goBack == false )
        {
            time = 0.6f;
            notPressed = false;
            PlayerPrefs.SetFloat( "timeOfDay", time );
            dayNightCycle.breakWait = true;
            PlayerPrefs.SetInt( "HaveSlept", 1 );
            PlayerPrefs.SetFloat( "DayCanvas", time );
        }
        else if( time > 0.1f && notPressed && dayNightCycle.goBack == true )
        {
            time = 0.6f;
            notPressed = false;
            PlayerPrefs.SetFloat( "timeOfDay", time );
            dayNightCycle.breakWait = true;
            dayNightCycle.goBack = false;
            PlayerPrefs.SetInt( "HaveSlept", 1 );
            PlayerPrefs.SetFloat( "DayCanvas", time );
        }
    }
}