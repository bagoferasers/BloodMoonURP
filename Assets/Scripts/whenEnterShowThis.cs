using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class whenEnterShowThis : MonoBehaviour
{
    public string nameOfSceneToChangeTo;
    public GameObject circle;

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
        transportScene = GameObject.Find( "ScenePortal" ).GetComponent< transportScene >( );
    }    

    void Update( )
    {
        if( circle.activeInHierarchy && pressed )
        {
            Debug.Log( nameOfSceneToChangeTo );
            transportScene.ChangeToScene( nameOfSceneToChangeTo );
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
        circle.SetActive( false );
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
        yield return null;
    }
}