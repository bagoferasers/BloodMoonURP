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

    void Start( )
    {
        GameObject[ ] gameObjects = GameObject.FindGameObjectsWithTag( "ScenePortal" );
        foreach( GameObject g in gameObjects )
        {
            transportScene ts = g.GetComponent< transportScene >( );
            Debug.Log( PlayerPrefs.GetString( "startPosition" ) );
            if( ts.id == PlayerPrefs.GetString( "startPosition" ) )
                player.transform.position = ts.transform.position;
        }
    }

    public void ChangeToScene( string sceneToChangeTo )
    {
        FadeThisOnePlease( );
        StartCoroutine( ChangeScene( sceneToChangeTo ) );
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
        CanvasGroup canvasGroup = GameObject.Find( "darkyboi" ).GetComponent< CanvasGroup >( );
        while( canvasGroup.alpha < 1 )
        {
            canvasGroup.alpha += Time.fixedDeltaTime / 2;
            yield return null;
        }
        canvasGroup.interactable = false;
        yield return null;
    }
}