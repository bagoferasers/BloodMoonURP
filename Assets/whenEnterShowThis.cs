using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class whenEnterShowThis : MonoBehaviour
{
    public string nameOfSceneToChangeTo;
    private bool p;
    public GameObject c;
    private CanvasGroup canvasGroup;
    private SpriteRenderer spriteRend;
    private Color color;

    void Start( )
    {
        p = false;
        c.SetActive( false );
        canvasGroup = GetComponent< CanvasGroup >( );
        spriteRend = c.GetComponent< SpriteRenderer >( );
        color = spriteRend.color;
    }    

    void Update( )
    {
        if( c.activeSelf && p )
        {
            goToScene( );
        }
    }    

    public void goToScene( )
    {
        FadeOutSceneThisWay( );
        SceneManager.LoadScene( nameOfSceneToChangeTo );
    }

    private void OnTriggerEnter2D( Collider2D thisCollider )
    {
        Debug.Log( "entered ontrigger" );
        if( thisCollider.tag == "showMe" || thisCollider.tag == "Player" )
        {
            FadeMeIn( );
        }
    }

    private void OnTriggerExit2D( Collider2D thisCollider )
    {
        Debug.Log( "exited ontrigger" );
        if( thisCollider.tag == "showMe" || thisCollider.tag == "Player" )
        {
            FadeMeOut( );
        }
    }

    public void isPressed( )
    {
        Debug.Log( "p is pressed" );
        p = true;
    }

    public void isNotPressed( )
    {
        Debug.Log( "p is not pressed" );
        p = false;
    }

    private void FadeMeIn( )
    {
        StartCoroutine( fadeIn( ) );
    }

    private void FadeMeOut( )
    {
        StartCoroutine( fadeOut( ) );
    }

    IEnumerator fadeIn( )
    {
        c.SetActive( true );
        while( canvasGroup.alpha < 1 )
        {
            color.a += Time.deltaTime;
            canvasGroup.alpha += Time.deltaTime;
            spriteRend.color = color;
            yield return null;
        }        
    }

    IEnumerator fadeOut( )
    {
        while( canvasGroup.alpha > 0 )
        {
            canvasGroup.alpha -= Time.deltaTime;
            color.a = canvasGroup.alpha;
            spriteRend.color = color;
            yield return null;
        }
        c.SetActive( false );
        yield return null;
    }

    public void FadeOutScene( )
    {
        StartCoroutine ( FadeOutSceneThisWay( ) );
    }

    /* Fades scene to black by decrementing alpha over time. */
    IEnumerator FadeOutSceneThisWay( )
    {
        CanvasGroup canvasGroup = GameObject.Find( "darkyboi" ).GetComponent< CanvasGroup >( );

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