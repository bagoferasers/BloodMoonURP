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
        PlayerPrefs.Save();
        StartCoroutine( goodbye( ) );
    }
    
    public IEnumerator goodbye( )
    {
        yield return new WaitForSeconds( 2 );
        Application.Quit( );
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            PlayerPrefs.SetString( "SceneStart", SceneManager.GetActiveScene( ).name );
            PlayerPrefs.Save();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString( "SceneStart", SceneManager.GetActiveScene( ).name );
        PlayerPrefs.Save();
    }
}