using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void exitGame( )
    {
        Debug.Log( SceneManager.GetActiveScene( ).name );
        PlayerPrefs.SetString( "SceneStart", SceneManager.GetActiveScene( ).name );
        StartCoroutine( goodbye( ) );
    }
    
    public IEnumerator goodbye( )
    {
        yield return new WaitForSeconds( 2 );
        Debug.Log( "exitgame" );
        Application.Quit( );
    }
}