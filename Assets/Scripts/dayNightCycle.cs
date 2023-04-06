using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dayNightCycle : MonoBehaviour
{

    void Start()
    {
        //if just starting game, set timeOfDay to morning
        if( PlayerPrefs.GetFloat( "HasStartedGame" ) == 0 )
            PlayerPrefs.SetFloat( "HasStartedGame", 8f );
    }

    // Update is called once per frame
    void Update()
    {
        //update float over time
        float time = PlayerPrefs.GetFloat( "HasStartedGame" );
        time += 0.01f;
        PlayerPrefs.SetFloat( "HasStartedGame",  time );

        //pass float to lights
        setLightsToTime( PlayerPrefs.GetFloat( "HasStartedGame" ) );

        //pass float to moon and stars on/off
        setObjectsToTime( PlayerPrefs.GetFloat( "HasStartedGame" ) );

        //pass float to sounds changing
        setSoundsToTime( PlayerPrefs.GetFloat( "HasStartedGame" ) );
    }

    private void setLightsToTime( float time )
    {

    }

    private void setObjectsToTime( float time )
    {

    }

    private void setSoundsToTime( float time )
    {

    }

}
