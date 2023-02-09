using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class character : MonoBehaviour
{
    [ Header( "Movement" ) ]
    public float speed;

    Rigidbody2D rb2d;
    public float forceMultiplier;

    private bool moveLeft, moveRight;
    private float jumpForce;

    // Start is called before the first frame update
    void Start( )
    {
        rb2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        jumpForce = 1000;
        moveLeft = false;
        moveRight = false;
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
        if( rb2d.velocity.y == 0 )
        {
            rb2d.AddForce( Vector2.up * jumpForce * Time.deltaTime );
        }
    }

    public void stopMoving( )
    {
        moveLeft = false;
        moveRight = false;
        rb2d.velocity = Vector2.zero;
    }
}