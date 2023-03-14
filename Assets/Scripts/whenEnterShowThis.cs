using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class whenEnterShowThis : MonoBehaviour
{
    [ Header( "Scene to change to:" ) ]
    public string scene;

    [ Header( "Circle to fade in and out:" ) ]
    public GameObject circle;

    [ Header( "Connected ScenePortal:" ) ]
    public GameObject sp;

    private CanvasGroup canvasGroup;
    private SpriteRenderer spriteRend;
    private Color color;
    private bool pressed;
    private transportScene transportScene;

    void Start( )
    {
        pressed = false;
        circle.SetActive( false );
        canvasGroup = GetComponent< CanvasGroup >( );
        spriteRend = circle.GetComponent< SpriteRenderer >( );
        color = spriteRend.color;
        transportScene = sp.GetComponent< transportScene >( );
    }    

    void Update( )
    {
        if( circle.activeInHierarchy && pressed && GameObject.Find( "oButton" ).GetComponent< Button >( ).interactable == true )
        {
            PlayerPrefs.SetString( "startPosition", transportScene.idConnected );
            transportScene.ChangeToScene( scene );
        }
    }    

    private void OnTriggerEnter2D( Collider2D thisCollider )
    {
        if( thisCollider.tag == "Player" )
            FadeMeIn( );
    }

    private void OnTriggerExit2D( Collider2D thisCollider )
    {
        if( thisCollider.tag == "Player" )
            FadeMeOut( );
    }

    public void isPressed( )
    {
        pressed = true;
    }

    public void isNotPressed( )
    {
        pressed = false;
    }

    private void FadeMeIn( )
    {
        circle.SetActive( true );
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
            canvasGroup.alpha += Time.deltaTime * 7;
            color.a = canvasGroup.alpha;
            spriteRend.color = color;
            yield return null;
        }         
    }

    IEnumerator fadeOut( )
    {
        while( canvasGroup.alpha > 0 )
        {
            canvasGroup.alpha -= Time.deltaTime * 7;
            color.a = canvasGroup.alpha;
            spriteRend.color = color;
            yield return null;
        }
        circle.SetActive( false );
        yield return null;
    }
}