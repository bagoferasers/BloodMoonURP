using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class whenEnterShowThis : MonoBehaviour
{
    public string nameOfSceneToChangeTo;
    public Button aButton;
    public GameObject objectToInteractWith;
    private bool p;

    void Start( )
    {
        p = false;
    }    

    void Update( )
    {
        if( objectToInteractWith != null && p )
            goToScene( );
    }    

    public void goToScene( )
    {
        SceneManager.LoadScene( nameOfSceneToChangeTo );
    }

    void OnTriggerEnter( Collider thisCollider )
    {
        if( thisCollider.gameObject.CompareTag( "showMe" ) )
            objectToInteractWith.gameObject.SetActive( true );
    }

    void OnTriggerExit( Collider thisCollider )
    {
        if( thisCollider.gameObject.CompareTag( "showMe" ) )
            objectToInteractWith.gameObject.SetActive( false );
    }

    void isPressed( )
    {
        p = true;
    }

    void isNotPressed( )
    {
        p = false;
    }
}