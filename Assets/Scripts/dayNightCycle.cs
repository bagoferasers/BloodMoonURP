using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightCycle : MonoBehaviour
{
    private bool goBack = true;
    private float time;

    public float timeToWait;
    public float valueToIncrementAndDecrement;

    void Start()
    {
        DontDestroyOnLoad( this.gameObject );
    }

    // Update is called once per frame
    void Update()
    {            
        checkForWaiting( );

        checkForObjectsForNight( );

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
        yield return new WaitForSeconds( timeToWait ); 
        goBack = gb;
    }
}
