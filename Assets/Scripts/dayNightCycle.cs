using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightCycle : MonoBehaviour
{
    private bool goBack = false;
    private float time;

    public float timeToWait;
    public float valueToIncrementAndDecrement;

    void Start()
    {
        //DontDestroyOnLoad( this.gameObject );
        Debug.Log( PlayerPrefs.GetFloat( "timeOfDay" ) );
        //if just starting game, set timeOfDay to morning
        if( PlayerPrefs.GetFloat( "timeOfDay" ) == 0f )
            PlayerPrefs.SetFloat( "timeOfDay", 0.4f );
    }

    // Update is called once per frame
    void Update()
    {            
        checkForWaiting( );

        //pass float to lights
        setLightsToTime( PlayerPrefs.GetFloat( "timeOfDay" ) );

        //pass float to moon and stars on/off
        setObjectsToTime( PlayerPrefs.GetFloat( "timeOfDay" ) );

        //pass float to sounds changing
        setSoundsToTime( PlayerPrefs.GetFloat( "timeOfDay" ) );
    }

    private void setLightsToTime( float time )
    {
        GameObject[] globalLights = GameObject.FindGameObjectsWithTag( "GlobalLights" );
        GameObject[] objectsForNight = GameObject.FindGameObjectsWithTag( "ObjectsForNight" );        
        if( goBack == true )
        {
            time = PlayerPrefs.GetFloat( "timeOfDay" );
            time -= valueToIncrementAndDecrement * Time.deltaTime;
            PlayerPrefs.SetFloat( "timeOfDay", time );
            foreach( GameObject g in globalLights )
            {
                g.GetComponent< UnityEngine.Rendering.Universal.Light2D >( ).intensity = time;
            }            
        }
        else if( goBack == false )
        {
            time = PlayerPrefs.GetFloat( "timeOfDay" );
            time += valueToIncrementAndDecrement * Time.deltaTime;
            PlayerPrefs.SetFloat( "timeOfDay", time );
            foreach( GameObject g in globalLights )
            {
                g.GetComponent< UnityEngine.Rendering.Universal.Light2D >( ).intensity = time;
            }        
        }
    }

    private void setObjectsToTime( float time )
    {

    }

    private void setSoundsToTime( float time )
    {

    }

    private void checkForWaiting( )
    { 
        if( PlayerPrefs.GetFloat( "timeOfDay" ) < 0.2f )
        {
            StartCoroutine( waitForDayNight( timeToWait, false ) );
            PlayerPrefs.SetFloat( "timeOfDay", 0.2f ); 
        }    
        else if( PlayerPrefs.GetFloat( "timeOfDay" ) > 1f )
        {
            StartCoroutine( waitForDayNight( timeToWait, true ) );
            PlayerPrefs.SetFloat( "timeOfDay", 1f ); 
        }
    }

    IEnumerator waitForDayNight( float timeToWait, bool gb )
    {
        yield return new WaitForSeconds( timeToWait ); 
        goBack = gb;
    }
}
