using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;

public class SceneChange: MonoBehaviour 
{ 
    public void mainMenu( )
    {
        StartCoroutine( ChangeToScene( "MainMenu" ) );
    }

    public void village( ) 
    {  
        StartCoroutine( ChangeToScene( "Village" ) );
    }

    /* Changes scenes while implementing a fade-out for 3 seconds. */
    public IEnumerator ChangeToScene( string sceneToChangeTo )
    {
        FadeMeOut( );
        yield return new WaitForSeconds( 3 );
        SceneManager.LoadScene( sceneToChangeTo );
    }

    public void FadeMeOut( )
    {
        StartCoroutine ( FadeOut( ) );
    }

    /* Fades scene to black by decrementing alpha over time. */
    IEnumerator FadeOut( )
    {
        CanvasGroup canvasGroup = GetComponent< CanvasGroup >( );

        while( canvasGroup.alpha > 0 )
        {
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }

        /* This makes sure buttons aren't interactable while fading out. */
        canvasGroup.interactable = false;
        yield return null;
    }
} 