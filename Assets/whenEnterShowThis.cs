using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class whenEnterShowThis : MonoBehaviour
{
    //public TextMeshProUGUI showText;
    //public string textToShow;
    public string nameOfSceneToChangeTo;
    public Button aButton;
    public GameObject objectToInteractWith;

    void Start( )
    {
        aButton.onClick.AddListener( Interact );
    }    

    void Update( )
    {
        if( objectToInteractWith != null && Input.GetKeyDown( 0 ) )
            Interact( );
    }    

    public void goToScene( )
    {
        SceneManager.LoadScene( nameOfSceneToChangeTo );
    }

    void OnTriggerEnter( Collider thisCollider )
    {
        if( thisCollider.gameObject.CompareTag( "showMe" ) )
            aButton.gameObject.SetActive( true );
    }

    void OnTriggerExit( Collider thisCollider )
    {
        if( thisCollider.gameObject.CompareTag( "showMe" ) )
            objectToInteractWith.gameObject.SetActive( false );
    }

    void Interact( )
    {
        Debug.Log( "Changing scene" );
        goToScene( );
    }
}