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
    public float maxSpeedIncreaseDuration;

    private float originalSpeed;
    private float timeBetweenTapsLeft;
    private float timeBetweenTapsRight;
    private int buttonPressedLeft;
    private int buttonPressedRight;
    private Rigidbody2D rb2d;
    private bool moveLeft, moveRight, goJump, arePaused;
    private GameObject menu;
    private GameObject pauseButton;
    private GameObject leftButton;
    private GameObject rightButton;
    private GameObject upButton;
    private GameObject attackButton;
    private Animator animator;

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
        attackButton = GameObject.Find( "Attack" );
        animator = GetComponent< Animator >( );
        buttonPressedLeft = 0;
        buttonPressedRight = 0;
        timeBetweenTapsLeft = 0f;
        timeBetweenTapsRight = 0f;
        originalSpeed = speed;
    }

    void Update( )
    {
        //////////////////////////////////////////////////////////////////////////////////////////
        // handle double tap movement to start running... need to find a way to integrate this with
        // a new character animation

        // double tap for left
        if( !arePaused && ( moveLeft ) )
        {
            if( buttonPressedLeft == 2 && timeBetweenTapsLeft < 0.4f )
            {
                Debug.Log( "increasing speed" );
                speed = increasedSpeed;
                animator.SetBool( "run", true );
            }
        }
        else if( !arePaused && ( moveRight ) )
        {
            if( buttonPressedRight == 2 && timeBetweenTapsRight < 0.4f )
            {
                Debug.Log( "increasing speed" );
                speed = increasedSpeed;
                animator.SetBool( "run", true );
            }
        }

        // if button down
        if( buttonPressedLeft != 0 )
            timeBetweenTapsLeft += Time.deltaTime;

        if( buttonPressedRight != 0 )
            timeBetweenTapsRight += Time.deltaTime;

        if( timeBetweenTapsLeft > 0.4f && moveLeft != true  )
        {
            Debug.Log( " timebetweentaps > 0.4f for left");
            speed = originalSpeed;
            buttonPressedLeft = 0;
            timeBetweenTapsLeft = 0f;
            animator.SetBool( "run", false );
        }
        if( timeBetweenTapsRight > 0.4f && moveRight != true  )
        {
            Debug.Log( " timebetweentaps > 0.4f for right");
            speed = originalSpeed;
            buttonPressedRight = 0;
            timeBetweenTapsRight = 0f;
            animator.SetBool( "run", false );
        }
        ///////////////////////////////////////////////////////////////////////////////////////////
    }

    void FixedUpdate( )
    {        
        animator.SetBool( "attack", false );
        /* player direction animation */

        if( !arePaused && moveRight  )
        {
            animator.SetBool( "right", true );
            animator.SetBool( "left", false ); 
            rb2d.velocity = new Vector2( 1 * speed * Time.deltaTime, rb2d.velocity.y);
        }
        else if( !arePaused && moveLeft )
        {
            animator.SetBool( "left", true );
            animator.SetBool( "right", false );
            rb2d.velocity = new Vector2( -1 * speed * Time.deltaTime, rb2d.velocity.y);
        }
        else
        {
            animator.SetBool( "left", false ); 
            animator.SetBool( "right", false ); 
        }

        if( !arePaused && goJump )
        {
            if( rb2d.velocity.y == 0 )
                rb2d.AddForce( Vector3.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse );
            goJump = false;
        }                
    }

    public void goRight( )
    {
        moveRight = true;
        animator.SetInteger( "motionX", 1 );
    }

    public void goLeft( )
    {
        moveLeft = true;
        animator.SetInteger( "motionX", -1 );
    }

    public void buttonClickLeft( )
    {
        buttonPressedLeft += 1;
    }

    public void buttonClickRight( )
    {
        buttonPressedRight += 1;
    }

    public void attack( )
    {
        animator.SetBool( "attack", true );
    }

    public void noAttack( )
    {
        animator.SetBool( "attack", false );
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
        animator.SetInteger( "motionX", 0 );
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