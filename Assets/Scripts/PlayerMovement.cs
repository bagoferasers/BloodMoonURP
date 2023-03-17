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

    [ Header( "Walk Audio: " ) ]
    public AudioSource walk;

    [ Header( "Run Audio: " ) ]
    public AudioSource run;

    private float originalSpeed, timeBetweenTapsLeft, timeBetweenTapsRight;
    private int buttonPressedLeft, buttonPressedRight;
    private Rigidbody2D rb2d;
    private bool moveLeft, moveRight, goJump, arePaused;
    private Button pauseButton, leftButton, rightButton, upButton, attackButton, oButton;
    private Animator animator;
    CanvasGroup m;
    private bool runBool;
    private bool walkBool;

    [ Header( "Put Music script here:")]
    public Music music;
    
    ///////////////////////handle player position/////////////////////////////
    public static Vector3 position;

    // Start is called before the first frame update
    void Start( )
    {

        runBool = false;
        walkBool = false;
        rb2d = GetComponent<Rigidbody2D>( );
        moveLeft = false;
        moveRight = false;
        goJump = false;
        arePaused = false;
        animator = GetComponent< Animator >( );
        buttonPressedLeft = 0;
        buttonPressedRight = 0;
        timeBetweenTapsLeft = 0f;
        timeBetweenTapsRight = 0f;
        originalSpeed = speed;
        oButton = GameObject.Find( "oButton" ).GetComponent< Button >( );
        pauseButton = GameObject.Find( "PauseBlackButton" ).GetComponent< Button >( );
        leftButton = GameObject.Find( "LeftBlackButton" ).GetComponent< Button >( );
        rightButton = GameObject.Find( "RightBlackButton" ).GetComponent< Button >( );
        upButton = GameObject.Find( "UpBlackButton" ).GetComponent< Button >( );
        attackButton = GameObject.Find( "Attack" ).GetComponent< Button >( );
        m = GameObject.Find( "Template" ).GetComponent< CanvasGroup >( );

    }

    void Update( )
    {
        //////////////////////////////////////////////////////////////////////////////////////////
        // handle double tap movement to start running... need to find a way to integrate this with
        // a new character animation

        if( walkBool && !walk.isPlaying )
        {
            run.Stop( );
            walk.Play( );
        }
        else if( runBool && !run.isPlaying )
        {
            walk.Stop( );
            run.Play( );
        }
        else if( !walkBool && !runBool )
        {
            walk.Stop( );
            run.Stop( );
        }
        // double tap for left
        if( !arePaused && ( moveLeft ) )
        {
            if( buttonPressedLeft == 2 && timeBetweenTapsLeft < 0.4f )
            {
                speed = increasedSpeed;
                animator.SetBool( "run", true );
            }
        }
        else if( !arePaused && ( moveRight ) )
        {
            if( buttonPressedRight == 2 && timeBetweenTapsRight < 0.4f )
            {
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
            speed = originalSpeed;
            buttonPressedLeft = 0;
            timeBetweenTapsLeft = 0f;
            animator.SetBool( "run", false );
        }
        if( timeBetweenTapsRight > 0.4f && moveRight != true  )
        {
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
            if( speed == originalSpeed )
            {
                walkBool = true;
                runBool = false;
            }
            else if( speed == increasedSpeed )
            {
                runBool = true;
                walkBool = false;
            }
            rb2d.velocity = new Vector2( 1 * speed * Time.deltaTime, rb2d.velocity.y);
        }
        else if( !arePaused && moveLeft )
        {
            animator.SetBool( "left", true );
            animator.SetBool( "right", false );
            if( speed == originalSpeed )
            {
                walkBool = true;
                runBool = false;
            }
            else if( speed == increasedSpeed )
            {
                runBool = true;
                walkBool = false;
            }
            rb2d.velocity = new Vector2( -1 * speed * Time.deltaTime, rb2d.velocity.y);
        }
        else
        {
            animator.SetBool( "left", false ); 
            animator.SetBool( "right", false ); 
            walkBool = false;
            runBool = false;
        }

        if( !arePaused && goJump )
        {
            if( rb2d.velocity.y == 0 )
            {
                rb2d.AddForce( Vector3.up * jumpForce * Time.deltaTime, ForceMode2D.Impulse );
                music.isJumping = true;
            }
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
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo( 0 );
        if( state.IsName( "CharacterLeftAttackAnimation" ) || state.IsName( "CharacterRightAttackAnimation" )
            || state.IsName( "CharacterLeftAnimation" ) || state.IsName( "CharacterRightAnimation" )
            || state.IsName( "CharacterIdleLeft" ) || state.IsName( "CharacterIdleRight" ) )
            music.isPunching = true;
    }

    public void noAttack( )
    {
        animator.SetBool( "attack", false );
        music.isPunching = false;
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

        // show menu
        m.alpha = 1;
        m.interactable = true;

        // can't interact with game buttons while paused
        pauseButton.interactable = false;
        leftButton.interactable = false;
        rightButton.interactable = false;
        upButton.interactable = false;
        oButton.interactable = false;
        attackButton.interactable = false;
    }

    public void onExitPause( )
    {
        // hide menu
        FadePauseMenu( );

        // now interact with buttons
        pauseButton.interactable = true;
        leftButton.interactable = true;
        rightButton.interactable = true;
        upButton.interactable = true;
        oButton.interactable = true;
        attackButton.interactable = true;
        arePaused = false;
        m.interactable = false;
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