using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightCycle : MonoBehaviour
{
    private bool goBack = false;
    private float time;
    public float timeToWait;

    void Start()
    {
        //DontDestroyOnLoad( this.gameObject );

        //if just starting game, set timeOfDay to morning
        if( PlayerPrefs.GetFloat( "HasStartedGame" ) == 0f )
            PlayerPrefs.SetFloat( "timeOfDay", 0.4f );
    }

    // Update is called once per frame
    void Update()
    {
        //update float over time

        if( PlayerPrefs.GetFloat( "timeOfDay" ) > 1f )
        {
            PlayerPrefs.SetFloat( "timeOfDay", 1f );
            StartCoroutine( waitForDayNight( timeToWait ) );
            goBack = true;                  
        }
        else if( PlayerPrefs.GetFloat( "timeOfDay" ) < 0.2f )
        {
            PlayerPrefs.SetFloat( "timeOfDay", 0.2f );
            StartCoroutine( waitForDayNight( timeToWait ) );
            goBack = false;                  
        }
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
            Debug.Log( "goBack is true" );      
            time = PlayerPrefs.GetFloat( "timeOfDay" );
            time -= 0.0001f;
            PlayerPrefs.SetFloat( "timeOfDay", time );
            foreach( GameObject g in globalLights )
            {
                g.GetComponent< UnityEngine.Rendering.Universal.Light2D >( ).intensity = time;
            }            
        }
        else if( goBack == false )
        {
            Debug.Log( "goBack is false" );                        
            time = PlayerPrefs.GetFloat( "timeOfDay" );
            time += 0.0001f;
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

    IEnumerator waitForDayNight( float timeToWait )
    {
        yield return new WaitForSeconds( timeToWait );
    }
}
