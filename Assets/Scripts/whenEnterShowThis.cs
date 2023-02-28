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
    private transportScene t;

    void Start( )
    {
        p = false;
        c.SetActive( false );
        canvasGroup = GetComponent< CanvasGroup >( );
        spriteRend = c.GetComponent< SpriteRenderer >( );
        color = spriteRend.color;
        menuCanvas = GameObject.Find( "MenuCanvas" );
        sc = menuCanvas.GetComponent< SceneChange >( );
        t = GameObject.Find( "ScenePortal" ).GetComponent< transportScene >( );
    }    

    void Update( )
    {
        if( c.activeInHierarchy && p )
        {
            Debug.Log( nameOfSceneToChangeTo );
            t.ChangeToScene( nameOfSceneToChangeTo );
        }
    }    

    private void OnTriggerEnter2D( Collider2D thisCollider )
    {
        if( thisCollider.tag == "showMe" || thisCollider.tag == "Player" )
            FadeMeIn( );
    }

    private void OnTriggerExit2D( Collider2D thisCollider )
    {
        if( thisCollider.tag == "showMe" || thisCollider.tag == "Player" )
            FadeMeOut( );
    }

    public void isPressed( )
    {
        p = true;
    }

    public void isNotPressed( )
    {
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
        while( canvasGroup.alpha < 1 )
        {
            canvasGroup.alpha += Time.deltaTime * 2;
            color.a = canvasGroup.alpha;
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