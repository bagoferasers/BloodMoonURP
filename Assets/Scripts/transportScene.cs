using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class transportScene : MonoBehaviour
{
    public string sceneToChangeTo;
    public string id;
    public string idConnected;
    public GameObject player;
    private CanvasGroup canvasGroup;

    [ Header( "Put mixer here:" ) ]
    public AudioMixer mixer;

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
        StartCoroutine( playMusic( ) );
    }

    IEnumerator playMusic( )
    {
        float originalVolume = PlayerPrefs.GetFloat( "MusicVolume" );
        float currentVolume = -26f;
        mixer.SetFloat( "MusicVolume", currentVolume );
        while( currentVolume < originalVolume )
        {
            currentVolume += 0.2f;
            mixer.SetFloat( "MusicVolume", currentVolume );
            yield return null;
        }
        yield return null;
    }

    IEnumerator fadeMusic( )
    {
        float originalVolume = PlayerPrefs.GetFloat( "MusicVolume" );
        float currentVolume = PlayerPrefs.GetFloat( "MusicVolume" );
        float targetVolume = -26f;
        mixer.SetFloat( "MusicVolume", currentVolume );
        while( currentVolume > targetVolume )
        {
            currentVolume -= 0.3f;
            mixer.SetFloat( "MusicVolume", currentVolume );
            yield return null;
        }
        yield return null;
    }

    public void ChangeToScene( string sceneToChangeTo )
    {
        PlayerPrefs.SetInt( "HasStartedGame", 1 );
        FadeThisOnePlease( );
        if( sceneToChangeTo != null )
            StartCoroutine( ChangeScene( sceneToChangeTo ) );
        else    
            StartCoroutine( ChangeScene( "Village" ) );
        StartCoroutine( fadeMusic( ) );
    }

    public void ChangeSceneFromMain( )
    {
        FadeThisOnePlease( );
        PlayerPrefs.SetFloat( "timeOfDay", 0.4f );
        if( PlayerPrefs.GetString( "SceneStart" ) == "Main" )
            PlayerPrefs.SetString( "SceneStart", "Village" );
        StartCoroutine( ChangeScene( PlayerPrefs.GetString( "SceneStart" ) ) );
        StartCoroutine( fadeMusic( ) );
        PlayerPrefs.SetInt( "HasStartedGame", 1 );
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
        PlayerPrefs.SetInt( "HasStartedGame", 0 );

        // return values of volume mixers
        PlayerPrefs.SetFloat( "MasterVolume", masterVol );
        PlayerPrefs.SetFloat( "MusicVolume", musicVol );
        PlayerPrefs.SetFloat( "SystemVolume", sysVol );
        PlayerPrefs.SetFloat( "EffectsVolume", effVol );
        PlayerPrefs.SetString( "SceneStart", "Village" );
        PlayerPrefs.SetFloat( "MaxHealth", 20f );
        PlayerPrefs.SetFloat( "MaxShield", 20f );
        PlayerPrefs.SetFloat( "Health", 15f );
        PlayerPrefs.SetFloat( "Shield", 15f );

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