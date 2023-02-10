using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class character : MonoBehaviour
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
    }

    // Update is called once per frame
    void FixedUpdate( )
    {
        //rb2d.MovePosition( transform.position + ( new Vector3( Input.GetAxisRaw( "Horizontal" ),Input.GetAxisRaw( "Vertical" ) ) * Time.deltaTime * speed ) );
        if( !arePaused && moveRight )
        {
            Debug.Log( "Moving right." );
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if( !arePaused && moveLeft )
        {
            Debug.Log( "Moving left." );
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        
        if( !arePaused && goJump )
        {
            if( rb2d.velocity.y == 0 )
            {
                rb2d.AddForce( Vector3.up * jumpForce * speed * Time.deltaTime );
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
        arePaused = false;
        
        // get canvas group from menu and button objects
        CanvasGroup m = menu.GetComponent< CanvasGroup >( );
        Button p = pauseButton.GetComponent<Button>( );
        Button l = pauseButton.GetComponent<Button>( );
        Button r = pauseButton.GetComponent<Button>( );
        Button u = pauseButton.GetComponent<Button>( );

        // show menu
        m.alpha = 0;

        // can't interact with game buttons while paused
        p.interactable = true;
        l.interactable = true;
        r.interactable = true;
        u.interactable = true;
    }
}