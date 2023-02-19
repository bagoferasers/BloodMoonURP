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
            goToScene( );
    }    

    public void goToScene( )
    {
        SceneManager.LoadScene( nameOfSceneToChangeTo );
    }

    private void OnTriggerEnter2D( Collider2D thisCollider )
    {
        Debug.Log( "entered ontrigger" );
        if( thisCollider.tag == "showMe" || thisCollider.tag == "Player" )
        {
            c.SetActive( true );
            FadeMeIn( );
        }
    }

    private void OnTriggerExit2D( Collider2D thisCollider )
    {
        Debug.Log( "exited ontrigger" );
        if( thisCollider.tag == "showMe" || thisCollider.tag == "Player" )
        {
            c.SetActive( false );
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
        while( canvasGroup.alpha < 0 )
        {
            canvasGroup.alpha += Time.deltaTime / 2;
            color.a += Time.deltaTime / 2;
            spriteRend.color = color;
            yield return null;
        }
    }

    IEnumerator fadeOut( )
    {
        while( canvasGroup.alpha < 0 )
        {
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
    }
}