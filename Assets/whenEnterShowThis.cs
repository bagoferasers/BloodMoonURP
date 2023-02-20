using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class whenEnterShowThis : MonoBehaviour
{
    public string nameOfSceneToChangeTo;
    public GameObject c;

    private CanvasGroup canvasGroup;
    private SpriteRenderer spriteRend;
    private Color color;
    private SceneChange sc;
    private GameObject menuCanvas;
    private bool p;

    void Start( )
    {
        p = false;
        c.SetActive( false );
        canvasGroup = GetComponent< CanvasGroup >( );
        spriteRend = c.GetComponent< SpriteRenderer >( );
        color = spriteRend.color;
        menuCanvas = GameObject.Find( "MenuCanvas" );
        sc = menuCanvas.GetComponent< SceneChange >( );
    }    

    void Update( )
    {
        if( c.activeInHierarchy && p )
        {
            Debug.Log( nameOfSceneToChangeTo );
            Debug.Log( "c is active and p is pressed" );
            if( string.Equals( nameOfSceneToChangeTo, "ApartmentScene" ) )
            {
                Debug.Log( "passed appartment scene" );
                sc.apartment( );
            } 
            else if( string.Equals( nameOfSceneToChangeTo, "NineScene" ) )
            {
                Debug.Log( "passed 9 lives scene" );
                sc.nineLives( );
            }
        }
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
        c.SetActive( true );
        StartCoroutine( fadeIn( ) );
    }

    private void FadeMeOut( )
    {
        StartCoroutine( fadeOut( ) );
    }

    IEnumerator fadeIn( )
    {
        Debug.Log( "Setting active." );
        c.SetActive( true );
        while( canvasGroup.alpha < 1 )
        {
            color.a += Time.deltaTime;
            canvasGroup.alpha += Time.deltaTime * 2;
            spriteRend.color = color;
            yield return null;
        }        
    }

    IEnumerator fadeOut( )
    {
        while( canvasGroup.alpha > 0 )
        {
            canvasGroup.alpha -= Time.deltaTime * 2;
            color.a = canvasGroup.alpha;
            spriteRend.color = color;
            yield return null;
        }
        c.SetActive( false );
        yield return null;
    }
}