using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class whenEnterShowThis : MonoBehaviour
{
    public string nameOfSceneToChangeTo;
    public GameObject objectToInteractWith;
    private bool p;

    void Start( )
    {
        p = false;
        objectToInteractWith.gameObject.SetActive( false );
    }    

    void Update( )
    {
        if( objectToInteractWith.activeSelf && p )
            goToScene( );
    }    

    public void goToScene( )
    {
        SceneManager.LoadScene( nameOfSceneToChangeTo );
    }

    void OnTriggerEnter( Collider thisCollider )
    {
        Debug.Log( "entered ontrigger" );
        if( thisCollider.gameObject.CompareTag( "Player" ) )
            objectToInteractWith.gameObject.SetActive( true );
    }

    void OnTriggerExit( Collider thisCollider )
    {
        Debug.Log( "exited ontrigger" );
        if( thisCollider.gameObject.CompareTag( "Player" ) )
            objectToInteractWith.gameObject.SetActive( false );
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
}