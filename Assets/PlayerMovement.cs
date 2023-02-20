using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [ Header( "Movement" ) ]
    public float jumpForce;
    
    public float speed;
    public float increasedSpeed;
    private float speedIncreaseDurationLeft;
    public float maxSpeedIncreaseDuration;
    private float originalSpeed;
    private float timeBetweenTaps;
    private int buttonPressed;

    private Rigidbody2D rb2d;
    private bool moveLeft, moveRight, goJump, arePaused;
    private GameObject menu;
    private GameObject pauseButton;
    private GameObject leftButton;
    private GameObject rightButton;
    private GameObject upButton;
    private GameObject title;
    private Animator animator;

    //private float directX;
    

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

        buttonPressed = 0;
        timeBetweenTaps = 0f;
        originalSpeed = speed;
        speedIncreaseDurationLeft = 0f;
    }

    void FixedUpdate( )
    {        
        /* player direction animation */
        animator = GetComponent< Animator >( );

        //////////////////////////////////////////////////////////////////////////////////////////
        // handle double tap movement to start running... need to find a way to integrate this with
        // a new character animation
        if( !arePaused && ( moveRight || moveLeft ) )
        {
            Debug.Log( "entered if statement for moving and double tapping" );
            if( buttonPressed == 1 && timeBetweenTaps < 0.4f )
            {
                Debug.Log( "increasing speed" );
                speed = increasedSpeed;
            }
        }

        timeBetweenTaps += Time.deltaTime;

        if( timeBetweenTaps > 0.3f && buttonPressed != 1  )
        {
            Debug.Log( " timebetweentaps > 0.4f");
            speed = originalSpeed;
            buttonPressed = 0;
            timeBetweenTaps = 0f;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////

        if( !arePaused && moveRight  )
        {
            animator.SetBool( "right", true );
            animator.SetBool( "left", false ); 
            animator.SetBool( "forwardidle", false );
            Debug.Log( "Moving right." );
            rb2d.velocity = new Vector2( 1 * speed * Time.deltaTime, rb2d.velocity.y);
            rb2d.velocity.Normalize( );

        }
        else if( !arePaused && moveLeft )
        {
            animator.SetBool( "left", true );
            animator.SetBool( "right", false );
            animator.SetBool( "forwardidle", false );
            Debug.Log( "Moving left." );
            rb2d.velocity = new Vector2( -1 * speed * Time.deltaTime, rb2d.velocity.y);
            rb2d.velocity.Normalize( );
        }
        else
        {
            animator.SetBool( "forwardidle", true );
            animator.SetBool( "left", false ); 
            animator.SetBool( "right", false ); 
        }

        if( !arePaused && goJump )
        {
            if( rb2d.velocity.y == 0 )
            {
                Debug.Log("Time.deltaTime = " + Time.deltaTime + "\n" );
                rb2d.AddForce( Vector3.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse );
                rb2d.velocity.Normalize( );
            }
            goJump = false;
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

    public void buttonClick( )
    {
        buttonPressed += 1;
        Debug.Log( "button pressed = " + buttonPressed );
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
        speed = originalSpeed;
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