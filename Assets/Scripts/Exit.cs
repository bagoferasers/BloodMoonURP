using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    void Start( )
    {
        canvasGroup = GameObject.Find( "darkyboi" ).GetComponent< CanvasGroup >( );
    }

    public void exitGame( )
    {
        PlayerPrefs.SetString( "SceneStart", SceneManager.GetActiveScene( ).name );
        PlayerPrefs.Save( );
        FadeThisOnePlease( );
        StartCoroutine( goodbye( ) );
    }
    
    public IEnumerator goodbye( )
    {
        yield return new WaitForSeconds( 2 );
        Debug.Log( "Exiting Game" );
        Application.Quit( );
    }

    private void OnApplicationPause(bool pause)
    {
        if ( pause )
        {
            PlayerPrefs.SetString( "SceneStart", SceneManager.GetActiveScene( ).name );
            PlayerPrefs.Save( );
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString( "SceneStart", SceneManager.GetActiveScene( ).name );
        PlayerPrefs.Save( );
    }

    public void FadeThisOnePlease( )
    {
        StartCoroutine( FadeOut( ) );
    }

    IEnumerator FadeOut( )
    {
        while( canvasGroup.alpha < 1 )
        {
            canvasGroup.alpha += Time.deltaTime / 2;
            yield return null;
        }
        canvasGroup.interactable = false;
        yield return null;
    }
}