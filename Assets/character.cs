using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class character : MonoBehaviour
{
    [ Header( "Movement" ) ]
    public float speed;
    public float jumpForce;
    public float forceMultiplier;

    Rigidbody2D rb2d;
    private bool moveLeft, moveRight, goJump;
    
    // Start is called before the first frame update
    void Start( )
    {
        rb2d = GetComponent<Rigidbody2D>();
        moveLeft = false;
        moveRight = false;
        goJump = false;
    }

    // Update is called once per frame
    void Update( )
    {
            //rb2d.MovePosition( transform.position + ( new Vector3( Input.GetAxisRaw( "Horizontal" ),Input.GetAxisRaw( "Vertical" ) ) * Time.deltaTime * speed ) );
            if( moveRight )
            {
                Debug.Log( "Moving right." );
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
            else if( moveLeft )
            {
                Debug.Log( "Moving left." );
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else if( goJump )
            {
                if( rb2d.velocity.y == 0 )
                {
                    rb2d.AddForce( Vector3.up * jumpForce * Time.deltaTime );
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
}