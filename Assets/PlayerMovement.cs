using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [ Header( "Movement" ) ]
    public float speed;
    public float jumpForce;

    Rigidbody2D rb2d;
    private bool moveLeft, moveRight, goJump, arePaused;
    GameObject menu;
    GameObject pauseButton;
    GameObject leftButton;
    GameObject rightButton;
    GameObject upButton;
    GameObject title;
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
        title = GameObject.Find( "Title" );
    }

    void Update( )
    {

    }

    void FixedUpdate( )
    {        
        /* player direction animation */
        float directX = Input.GetAxisRaw( "Horizontal" );
        animator = GetComponent< Animator >( );

        if( !arePaused && moveRight  )
        {
            animator.SetBool( "right", true );
            animator.SetBool( "left", false ); 
            animator.SetBool( "forwardidle", false );
            Debug.Log( "Moving right." );
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if( !arePaused && moveLeft )
        {
            animator.SetBool( "left", true );
            animator.SetBool( "right", false );
            animator.SetBool( "forwardidle", false );
            Debug.Log( "Moving left." );
            transform.position += Vector3.left * speed * Time.deltaTime;
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