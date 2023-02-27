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
            if( string.Equals( nameOfSceneToChangeTo, "ApartmentScene" ) )
                sc.apartment( );
            else if( string.Equals( nameOfSceneToChangeTo, "NineScene" ) )
                sc.nineLives( );
            else if( string.Equals( nameOfSceneToChangeTo, "SpiritTavernScene" ) )
                sc.spiritTavern( );
            else if( string.Equals( nameOfSceneToChangeTo, "ApartmentUpstairs" ) )
                sc.upstairsApartment( );
            else if( string.Equals( nameOfSceneToChangeTo, "ApartmentSceneReturning" ) )
                sc.returningDownstairsApartment( );
            else if( string.Equals( nameOfSceneToChangeTo, "VillageReturnFromApartment" ) )
                sc.returnFromApartment( );
            else if( string.Equals( nameOfSceneToChangeTo, "InsideApartment" ) )
                sc.insideApartment( );
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