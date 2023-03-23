using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class moveShelf : MonoBehaviour
{
    public float speed;
    public float distance;

    [ Header( "Circle to fade in and out:" ) ]
    public GameObject circle;

    [ Header( "Audio: " ) ]
    public AudioSource audio;

    public GameObject doorToShow;
    public GameObject moveDoor;

    private CanvasGroup canvasGroup;
    private SpriteRenderer spriteRend;
    private Color color;
    private bool pressed;
    private transportScene transportScene;
    //Vector3 dest;
    public Rigidbody2D rb2d;

    void Start( )
    {
        //dest = transform.position + new Vector3( distance, 0f, 0f );
        rb2d = GetComponent<Rigidbody2D>( );
        pressed = false;
        circle.SetActive( false );
        canvasGroup = GetComponent< CanvasGroup >( );
        spriteRend = circle.GetComponent< SpriteRenderer >( );
        color = spriteRend.color;
    }  

    void Update( )
    {
        if( circle.activeInHierarchy && pressed && GameObject.Find( "oButton" ).GetComponent< Button >( ).interactable == true )
        {
            showDoorBehind( );
            moveThis( );
        }
    }    

    public void showDoorBehind( )
    {
        doorToShow.SetActive( true );
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
        if( circle.activeInHierarchy )
            audio.Play( );    
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

    void moveThis( )
    {
        StartCoroutine( moveMe( ) );
    }
    
    private IEnumerator moveMe( )
    {
        float time = 1f;
        float track = 0f;

        while( track < time )
        {
            track += Time.deltaTime;
            // start ->> end
            transform.position = Vector3.Lerp( Vector3.zero, new Vector3(2.5f, 0f, 0f ), track / time );
            yield return null;
        }
        yield return null;
    }
}