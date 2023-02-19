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

    void Start( )
    {
        p = false;
        c.SetActive( false );
    }    

    void Update( )
    {
        if( c.activeSelf && p )
            goToScene( );
    }    

    public void goToScene( )
    {
        SceneManager.LoadScene( nameOfSceneToChangeTo );
    }

    private void OnTriggerEnter2D( Collider2D thisCollider )
    {
        Debug.Log( "entered ontrigger" );
        if( thisCollider.tag == "showMe" || thisCollider.tag == "Player" )
            c.SetActive( true );
    }

    private void OnTriggerExit2D( Collider2D thisCollider )
    {
        Debug.Log( "exited ontrigger" );
        if( thisCollider.tag == "showMe" || thisCollider.tag == "Player" )
            c.SetActive( false );
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