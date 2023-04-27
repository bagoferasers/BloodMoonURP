using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onMapButton : MonoBehaviour
{
    private bool arePaused;
    public CanvasGroup mapCanvas;
    private Button pauseButton, leftButton, rightButton, upButton, attackButton, oButton;

    // Start is called before the first frame update
    void Start()
    {
        arePaused = false;
        mapCanvas.alpha = 0;
        mapCanvas.interactable = false;
        oButton = GameObject.Find( "oButton" ).GetComponent< Button >( );
        pauseButton = GameObject.Find( "PauseBlackButton" ).GetComponent< Button >( );
        leftButton = GameObject.Find( "LeftBlackButton" ).GetComponent< Button >( );
        rightButton = GameObject.Find( "RightBlackButton" ).GetComponent< Button >( );
        upButton = GameObject.Find( "UpBlackButton" ).GetComponent< Button >( );
        attackButton = GameObject.Find( "Attack" ).GetComponent< Button >( );
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
        mapCanvas.alpha = 1;
        arePaused = true;
        mapCanvas.interactable = true;
        pauseButton.interactable = false;
        leftButton.interactable = false;
        rightButton.interactable = false;
        upButton.interactable = false;
        oButton.interactable = false;
        attackButton.interactable = false;
    }

    private void closeMap( )
    {
        mapCanvas.alpha = 0;
        mapCanvas.interactable = false;
        pauseButton.interactable = true;
        leftButton.interactable = true;
        rightButton.interactable = true;
        upButton.interactable = true;
        oButton.interactable = true;
        attackButton.interactable = true;
        arePaused = false;
    }
}
