using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class catPets : MonoBehaviour
{
    [ Header( "Circle to fade in and out:" ) ]
    public GameObject circle;
    public bool pressed;
    private CanvasGroup canvasGroup;
    private SpriteRenderer spriteRend;
    private Color color;
    [ Header( "Put catSounds script here:")]
    public catSounds catSounds;
    // Start is called before the first frame update
    void Start()
    {
        pressed = false;
        circle.SetActive( false );
        canvasGroup = GetComponent< CanvasGroup >( );
        spriteRend = circle.GetComponent< SpriteRenderer >( );
        color = spriteRend.color;
    }

    // Update is called once per frame
    void Update()
    {
        if( circle.activeInHierarchy && pressed && GameObject.Find( "oButton" ).GetComponent< Button >( ).interactable == true )
        {
            if( circle.tag == "cat" )
                catSounds.isPetting = true;
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
