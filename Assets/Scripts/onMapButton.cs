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
    public Button right, left;
    public EventTrigger upButton;
    public RawImage rightImage;
    public RawImage leftImage;
    private Color c;
    // Start is called before the first frame update
    void Start()
    {
        oButton = GameObject.Find( "oButton" ).GetComponent< Button >( );
        pauseButton = GameObject.Find( "PauseBlackButton" ).GetComponent< Button >( );
        attackButton = GameObject.Find( "Attack" ).GetComponent< Button >( );
        mapCanvas.alpha = 0;
        mapCanvas.interactable = false;
        arePaused = false;
        right.interactable = false;
        left.interactable = false;
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
        leftButton.enabled = false;
        rightButton.enabled = false;
        upButton.enabled = false;
        oButton.interactable = false;
        attackButton.interactable = false;
        right.interactable = true;
        left.interactable = true;
        GameObject.Find( "Quests" ).GetComponent< Canvas >( ).sortingOrder = 1;
        GameObject.Find( "Map" ).GetComponent< Canvas >( ).sortingOrder = 0;
        ColorUtility.TryParseHtmlString("#FFE9BD", out c);
        leftImage.color = c;
        ColorUtility.TryParseHtmlString("#6C614A", out c);
        rightImage.color = c;
    }

    private void closeMap( )
    {
        mapCanvas.alpha = 0;
        pauseButton.interactable = true;
        leftButton.enabled = true;
        rightButton.enabled = true;
        upButton.enabled = true;
        oButton.interactable = true;
        attackButton.interactable = true;
        arePaused = false;
        mapCanvas.interactable = false;
        right.interactable = false;
        left.interactable = false;
    }

    public void viewQuests( )
    {
        Debug.Log( "viewQuests is pressed" );
        GameObject.Find( "Quests" ).GetComponent< Canvas >( ).sortingOrder = 1;
        GameObject.Find( "Map" ).GetComponent< Canvas >( ).sortingOrder = 0;
        ColorUtility.TryParseHtmlString("#FFE9BD", out c);
        leftImage.color = c;
        ColorUtility.TryParseHtmlString("#6C614A", out c);
        rightImage.color = c;
    }

    public void viewMap( )
    {
        Debug.Log( "viewMap is pressed" );
        GameObject.Find( "Quests" ).GetComponent< Canvas >( ).sortingOrder = 0;
        GameObject.Find( "Map" ).GetComponent< Canvas >( ).sortingOrder = 1;
        ColorUtility.TryParseHtmlString("#FFE9BD", out c);
        rightImage.color = c;
        ColorUtility.TryParseHtmlString("#6C614A", out c);
        leftImage.color = c;
    }
}
