using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class onMapButton : MonoBehaviour
{
    private bool arePaused;
    public CanvasGroup mapCanvas;
    private Button pauseButton, attackButton, oButton;
    public EventTrigger leftButton;
    public EventTrigger rightButton;
    public EventTrigger upButton;
    // Start is called before the first frame update
    void Start()
    {
        oButton = GameObject.Find( "oButton" ).GetComponent< Button >( );
        pauseButton = GameObject.Find( "PauseBlackButton" ).GetComponent< Button >( );
        // leftButton = GameObject.Find( "LeftBlackButton" ).GetComponent< Button >( );
        // rightButton = GameObject.Find( "RightBlackButton" ).GetComponent< Button >( );
        // upButton = GameObject.Find( "UpBlackButton" ).GetComponent< Button >( );
        attackButton = GameObject.Find( "Attack" ).GetComponent< Button >( );

        arePaused = false;
        mapCanvas.alpha = 0;
        mapCanvas.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPauseForMap( )
    {
        if( arePaused == false )
            openMap( );
        else 
            closeMap( );
    }

    private void openMap( )
    {
        arePaused = true;
        mapCanvas.alpha = 1;
        mapCanvas.interactable = true;
        
        pauseButton.interactable = false;
        // leftButton.interactable = false;
        // rightButton.interactable = false;
        // upButton.interactable = false;
        leftButton.enabled = false;
        rightButton.enabled = false;
        upButton.enabled = false;

        oButton.interactable = false;
        attackButton.interactable = false;
        
        
    }

    private void closeMap( )
    {
        mapCanvas.alpha = 0;
        
        pauseButton.interactable = true;

        // leftButton.interactable = true;
        // rightButton.interactable = true;
        // upButton.interactable = true;
        leftButton.enabled = true;
        rightButton.enabled = true;
        upButton.enabled = true;

        oButton.interactable = true;
        attackButton.interactable = true;

        arePaused = false;
        mapCanvas.interactable = false;
    }
}
