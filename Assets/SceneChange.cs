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
        FadeMainOut( );
        StartCoroutine( ChangeToScene( "MainMenu" ) );
    }

    public void village( )
    {
        FadeMainOut( );
        StartCoroutine( ChangeToScene( "Village" ) );
    }

    /* Changes scenes while implementing a fade-out for 3 seconds. */
    public IEnumerator ChangeToScene( string sceneToChangeTo )
    {
        yield return new WaitForSeconds( 3 );
        SceneManager.LoadScene( sceneToChangeTo );
    }

    public void FadeMainOut( )
    {
        StartCoroutine( FadeOutMain( ) );
    }

    public void FadeOut( )
    {
        StartCoroutine( FadeMeOut( ) );
    }

    /* Fades main scene to black by decrementing alpha over time. */
    IEnumerator FadeOutMain( )
    {
        GameObject g = GameObject.Find( "darkyboi" );
        CanvasGroup canvasGroup = g.GetComponentInParent< CanvasGroup >( );
        CanvasGroup canvasGroupMenu = GetComponent< CanvasGroup >( );

        while( canvasGroup.alpha < 1 )
        {
            canvasGroup.alpha += Time.deltaTime * (float)0.3;
            //canvasGroupMenu.alpha -= Time.deltaTime / 2;
            canvasGroupMenu.alpha -= Time.deltaTime * (float)0.3;
            yield return null;
        }

        /* This makes sure buttons aren't interactable while fading out. */
        canvasGroup.interactable = false;
        canvasGroupMenu.interactable = false;
        yield return null;
    }

    /* Fades scene to black by decrementing alpha over time. */
    IEnumerator FadeMeOut( )
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