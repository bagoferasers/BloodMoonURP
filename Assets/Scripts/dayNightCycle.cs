using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dayNightCycle : MonoBehaviour
{
    public static bool goBack = true;
    private float time;
    public static bool breakWait = false;
    public float timeToWait;
    public float valueToIncrementAndDecrement;

    void Start()
    {
        DontDestroyOnLoad( this.gameObject );
    }

    // Update is called once per frame
    void Update()
    {            
        if( PlayerPrefs.GetFloat( "timeOfDay" ) < 0.3f && PlayerPrefs.GetInt( "HaveSlept" ) == 0 && GameObject.Find( "HealthBar" ) != null )
            StartCoroutine( sleepOrDie( ) );

        //pass float to lights
        setLightsToTime( PlayerPrefs.GetFloat( "timeOfDay" ) );

        //pass float to moon and stars on/off
        setObjectsToTime( PlayerPrefs.GetFloat( "timeOfDay" ) );

        //pass float to sounds changing
        setSoundsToTime( PlayerPrefs.GetFloat( "timeOfDay" ) );

        checkForObjectsForNight( );
        
        checkForWaiting( );
    }

    IEnumerator sleepOrDie( )
    {
        GameObject.Find( "HealthBar" ).GetComponent< Slider >( ).value -= 0.0001f;
        yield return null;
    }

    private void setLightsToTime( float time )
    {
        GameObject[] globalLights = GameObject.FindGameObjectsWithTag( "GlobalLights" );
                

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
            PlayerPrefs.SetInt( "HaveSlept", 0 );
        }    
        else if( PlayerPrefs.GetFloat( "timeOfDay" ) > 1f )
        {
            StartCoroutine( waitForDayNight( timeToWait, true ) );
            PlayerPrefs.SetFloat( "timeOfDay", 1f ); 
        }
    }

    private void checkForObjectsForNight( )
    {
        GameObject[] objectsForNight = GameObject.FindGameObjectsWithTag( "ObjectsForNight" );
        if( PlayerPrefs.GetFloat( "timeOfDay") > 0.6f )
        {
            foreach( GameObject g in objectsForNight )
            {
                g.GetComponent< UnityEngine.Rendering.Universal.Light2D >( ).intensity = 0;
            }       
        }
        else
        {
            foreach( GameObject g in objectsForNight )
            {
                g.GetComponent< UnityEngine.Rendering.Universal.Light2D >( ).intensity = 1;
            }   
        }
        
    }

    IEnumerator waitForDayNight( float timeToWait, bool gb )
    {
        float time = 0f;
        while ( time < timeToWait )
        {
            time += valueToIncrementAndDecrement * Time.fixedDeltaTime;
            if( breakWait )
            {
                Debug.Log( "breakWait is true" );
                breakWait = false;
                goBack = gb;
                setLightsToTime( PlayerPrefs.GetFloat( "timeOfDay" ) );
                setObjectsToTime( PlayerPrefs.GetFloat( "timeOfDay" ) );
                setSoundsToTime( PlayerPrefs.GetFloat( "timeOfDay" ) );
                yield break;
            }

            yield return null;
        }
        goBack = gb;
    }
}
