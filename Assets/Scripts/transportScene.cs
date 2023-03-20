using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class transportScene : MonoBehaviour
{
    public string sceneToChangeTo;
    public string id;
    public string idConnected;
    public GameObject player;
    private CanvasGroup canvasGroup;

    void Start( )
    {
        canvasGroup = GameObject.Find( "darkyboi" ).GetComponent< CanvasGroup >( );
        GameObject[ ] gameObjects = GameObject.FindGameObjectsWithTag( "ScenePortal" );
        
        foreach( GameObject g in gameObjects )
        {
            transportScene ts = g.GetComponent< transportScene >( );
            if( ts.id == PlayerPrefs.GetString( "startPosition" ) )
                player.transform.position = ts.transform.position;
        }
    }

    public void ChangeToScene( string sceneToChangeTo )
    {
        FadeThisOnePlease( );
        if( sceneToChangeTo != null )
            StartCoroutine( ChangeScene( sceneToChangeTo ) );
        else    
            StartCoroutine( ChangeScene( "Village" ) );
    }

    public void ChangeSceneFromMain( )
    {
        FadeThisOnePlease( );
        if( PlayerPrefs.GetString( "SceneStart" ) == "Main" )
            PlayerPrefs.SetString( "SceneStart", "Village" );
        StartCoroutine( ChangeScene( PlayerPrefs.GetString( "SceneStart" ) ) );
    }

    public void resetPrefs( )
    {
        // save volume mixers
        float masterVol = PlayerPrefs.GetFloat( "MasterVolume" );
        float musicVol = PlayerPrefs.GetFloat( "MusicVolume" );
        float sysVol = PlayerPrefs.GetFloat( "SystemVolume" );
        float effVol = PlayerPrefs.GetFloat( "EffectsVolume" );

        // reset all prefs 
        PlayerPrefs.DeleteAll( );

        // return values of volume mixers
        PlayerPrefs.SetFloat( "MasterVolume", masterVol );
        PlayerPrefs.SetFloat( "MusicVolume", musicVol );
        PlayerPrefs.SetFloat( "SystemVolume", sysVol );
        PlayerPrefs.SetFloat( "EffectsVolume", effVol );
        PlayerPrefs.SetString( "SceneStart", "Village" );

        // cry
    }

    public IEnumerator ChangeScene( string sceneToChangeTo )
    {
        yield return new WaitForSeconds( 2 );
        SceneManager.LoadScene( sceneToChangeTo );
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