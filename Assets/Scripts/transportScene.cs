using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.ScenePortal;
/*
public class ScenePortal
{
    public string id;
    public string idConnected;
    public Transform playerTransform;
}
*/
public class transportScene : MonoBehaviour
{
    public string sceneToChangeTo;
    public string id;
    public string idConnected;
    public GameObject player;
    //private ScenePortal scenePortals;
    //public List< ScenePortal > scenePortalList;
/*
    // Start is called before the first frame update
    void Start( )
    {
        string spString = PlayerPrefs.GetString( "startPos" );
        foreach( ScenePortal sp in scenePortalList )
        {
            if( sp.id == startPosition )
            {
                player.transform.position = sp.transform.position;
            }
        }
    }
*/

    public void ChangeToScene( string sceneToChangeTo )
    {
        if( sceneToChangeTo == "Main" )
            FadeMainOut( );
        else    
            FadePortalOut( );
        StartCoroutine( ChangeScene( sceneToChangeTo ) );
    }

    public IEnumerator ChangeScene( string sceneToChangeTo )
    {
        //PlayerPrefs.SetString( "startPosition", id );
        yield return new WaitForSeconds( 1 );
        SceneManager.LoadScene( sceneToChangeTo );
    }

    public void FadeMainOut( )
    {
        StartCoroutine( FadeOutMain( ) );
    }

    IEnumerator FadeOutMain( )
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

    public void FadePortalOut( )
    {
        StartCoroutine( FadeOutPortal( ) );
    }

    IEnumerator FadeOutPortal( )
    {
        CanvasGroup canvasGroup = GameObject.Find( "darkyboi" ).GetComponent< CanvasGroup >( );
        while( canvasGroup.alpha < 1 )
        {
            canvasGroup.alpha += Time.fixedDeltaTime / 16;
            yield return null;
        }
        canvasGroup.interactable = false;
        yield return null;
    }
}