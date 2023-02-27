using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;



public class SceneChange: MonoBehaviour 
{ 
    private static GameObject player;
    private static Vector3 villageSpawnPoint;
    private static Vector3 apartmentDownstairsSpawnPoint;
    private static Vector3 startVillage;
    private static Vector3 startApartment;
    private static Vector3 startUpstairsApartment;
    private static Vector3 startNine;
    private static Vector3 startTavern;
    private PlayerMovement PlayerMovement;

/*
    void Start( )
    {
        player = GameObject.FindWithTag( "Player" );
        if( player )
            PlayerMovement = player.GetComponent< PlayerMovement >( );
    }

    public Vector3 getPosition( )
    {
        player = GameObject.FindWithTag( "Player" );
        if( player )
            PlayerMovement = player.GetComponent< PlayerMovement >( );
        return player.transform.position;
    }
*/

    public void mainMenu( )
    {
        FadeMainOut( );
        StartCoroutine( ChangeToScene( "Main" ) );
    }

    public void village( )
    {
        FadeMainOut( );
        StartCoroutine( ChangeToScene( "Village" ) );
        //PlayerMovement.position = new Vector3( 10f, -5.3f, 0 );
    }

    public void apartment( )
    {
        FadeMainOut( );
        StartCoroutine( ChangeToScene( "ApartmentScene" ) );
        //PlayerMovement.position = new Vector3( 0, -5.3f, 0 );
    }

    public void upstairsApartment( )
    {
        FadeMainOut( );
        StartCoroutine( ChangeToScene( "ApartmentUpstairs" ) );
        //PlayerMovement.position = new Vector3( 7.1f, -1.1f, 0 );
    }

    public void returningDownstairsApartment( )
    {
        FadeMainOut( );
        StartCoroutine( ChangeToScene( "ApartmentScene" ) );
        //PlayerMovement.position = new Vector3( 38f, -5.3f, 0f );
        //GameObject.Find( "Player" ).transform.position = new Vector2( 38f, -5.3f );
    }

    public void returnFromApartment( )
    {
        FadeMainOut( );        
        StartCoroutine( ChangeToScene( "Village" ) );
        //PlayerMovement.position = new Vector3( 10f, -5.3f, 0 );
    }

    public void insideApartment( )
    {
        FadeMainOut( );
        StartCoroutine( ChangeToScene( "InsideApartment" ) );
    }

    public void returnToUpstairs( )
    {
        FadeMainOut( );
        StartCoroutine( ChangeToScene( "ApartmentUpstairs" ) );
    }

    public void nineLives( )
    {
        FadeMainOut( );
        StartCoroutine( ChangeToScene( "NineScene" ) );
    }

    public void spiritTavern( )
    {
        FadeMainOut( );
        StartCoroutine( ChangeToScene( "SpiritTavernScene" ) );
    }

    /* Changes scenes while implementing a fade-out for 3 seconds. */
    public IEnumerator ChangeToScene( string sceneToChangeTo )
    {
        yield return new WaitForSeconds( 2 );
        SceneManager.LoadScene( sceneToChangeTo );
    }

    public void FadeMainOut( )
    {
        StartCoroutine( FadeOutMain( ) );
    }

    public void FadeOut( )
    {
        StartCoroutine( FadeMeOut( ) );
    }

    /* Fades main scene to black by decrementing alpha over time. */
    IEnumerator FadeOutMain( )
    {
        GameObject g = GameObject.Find( "darkyboi" );
        CanvasGroup canvasGroup = g.GetComponentInParent< CanvasGroup >( );
        CanvasGroup canvasGroupMenu = GetComponent< CanvasGroup >( );

        while( canvasGroup.alpha < 1 )
        {
            canvasGroup.alpha += Time.deltaTime / (float)2.09;
            canvasGroupMenu.alpha -= Time.deltaTime / (float)2.09;
            yield return null;
        }
        /* This makes sure buttons aren't interactable while fading out. */
        canvasGroup.interactable = false;
        canvasGroupMenu.interactable = false;
        yield return null;
    }

    /* Fades scene to black by decrementing alpha over time. */
    IEnumerator FadeMeOut( )
    {
        CanvasGroup canvasGroup = GetComponent< CanvasGroup >( );

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