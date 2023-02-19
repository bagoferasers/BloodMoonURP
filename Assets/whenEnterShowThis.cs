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
    private SceneChange sc;
    private GameObject menuCanvas;

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
        if( c.activeSelf && p )
            sc.apartment( );
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
}