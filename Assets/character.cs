using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class character : MonoBehaviour
{
    [ Header( "Movement" ) ]
    public float speedOriginal;
    public float speedTablet;
    public float jumpForceOriginal;
    public float jumpForceTablet;

    Rigidbody2D rb2d;
    private bool moveLeft, moveRight, goJump, arePaused;
    GameObject menu;
    GameObject pauseButton;
    GameObject leftButton;
    GameObject rightButton;
    GameObject upButton;
    GameObject title;
    
    // Start is called before the first frame update
    void Start( )
    {
        rb2d = GetComponent<Rigidbody2D>( );
        moveLeft = false;
        moveRight = false;
        goJump = false;
        arePaused = false;
        menu = GameObject.Find( "Template" );
        pauseButton = GameObject.Find( "PauseBlackButton" );
        leftButton = GameObject.Find( "LeftBlackButton" );
        rightButton = GameObject.Find( "RightBlackButton" );
        upButton = GameObject.Find( "UpBlackButton" );
        title = GameObject.Find( "Title" );
    }

    void FixedUpdate( )
    {
        if( Screen.height <= 1080 )
        {
            if( !arePaused && moveRight  )
            {
                Debug.Log( "Moving right." );
                transform.position += Vector3.right * speedOriginal * Time.deltaTime;
            }
            else if( !arePaused && moveLeft )
            {
                Debug.Log( "Moving left." );
                transform.position += Vector3.left * speedOriginal * Time.deltaTime;
            }
            
            if( !arePaused && goJump )
            {
                if( rb2d.velocity.y == 0 )
                {
                    Debug.Log("Time.deltaTime = " + Time.deltaTime + "\n" );
                    rb2d.AddForce( Vector3.up * jumpForceOriginal * Time.deltaTime, ForceMode2D.Impulse );
                }
                goJump = false;
            }            
        }
        else
        {
            if( !arePaused && moveRight  )
            {
                Debug.Log( "Moving right." );
                transform.position += new Vector3(1,0,0) * speedTablet * Time.deltaTime;
            }
            else if( !arePaused && moveLeft )
            {
                Debug.Log( "Moving left." );
                transform.position += new Vector3(-1,0,0) * speedTablet * Time.deltaTime;
            }
            
            if( !arePaused && goJump )
            {
                if( rb2d.velocity.y == 0 )
                {
                    Debug.Log("Time.deltaTime = " + Time.deltaTime + "\n" );
                    rb2d.AddForce( new Vector3(0,1,0) * jumpForceTablet * Time.deltaTime, ForceMode2D.Impulse );
                }
                goJump = false;
            }       
        }
    }

    public void goRight( )
    {
        moveRight = true;
    }

    public void goLeft( )
    {
        moveLeft = true;
    }

    public void jump( )
    {
        if( !arePaused )
            goJump = true;
    }

    public void stopMoving( )
    {
        moveLeft = false;
        moveRight = false;
        goJump = false;
        rb2d.velocity = Vector2.zero;
    }

    public void onPause( )
    {
        arePaused = true;
        
        // get canvas group from menu and button objects
        CanvasGroup m = menu.GetComponent< CanvasGroup >( );
        Button p = pauseButton.GetComponent<Button>( );
        Button l = pauseButton.GetComponent<Button>( );
        Button r = pauseButton.GetComponent<Button>( );
        Button u = pauseButton.GetComponent<Button>( );

        // show menu
        m.alpha = 1;

        // can't interact with game buttons while paused
        p.interactable = false;
        l.interactable = false;
        r.interactable = false;
        u.interactable = false;
    }

    public void onExitPause( )
    {
        // get canvas group from menu and button objects
        CanvasGroup m = menu.GetComponent< CanvasGroup >( );
        Button p = pauseButton.GetComponent<Button>( );
        Button l = pauseButton.GetComponent<Button>( );
        Button r = pauseButton.GetComponent<Button>( );
        Button u = pauseButton.GetComponent<Button>( );

        // hide menu
        FadePauseMenu( );

        // now interact with buttons
        p.interactable = true;
        l.interactable = true;
        r.interactable = true;
        u.interactable = true;
        m.interactable = true;
        arePaused = false;
    }

    public void FadePauseMenu( )
    {
        StartCoroutine( FadeMeOut( ) );
    }

    /* Fades scene to black by decrementing alpha over time. */
    IEnumerator FadeMeOut( )
    {
        GameObject template = GameObject.Find( "Template" );
        CanvasGroup canvasGroup = template.GetComponent< CanvasGroup >( );

        while( canvasGroup.alpha > 0 )
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}