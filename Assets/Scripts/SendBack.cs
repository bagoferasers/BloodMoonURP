using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendBack : MonoBehaviour
{
    [ Header( "Movement" ) ]
    public float force;
    public Rigidbody2D playerRB2D;
    private bool isTouching;
    // Start is called before the first frame update
    void Start()
    {
        isTouching = false;
        //rb2d = GetComponent< Rigidbody2D >( );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if( isTouching && playerRB2D.velocity.y == 0 )
            playerRB2D.AddForce( Vector3.right * force * Time.deltaTime, ForceMode2D.Impulse );
    }
    
    private void OnCollisionEnter2D( Collision2D other ) 
    {
        if( other.gameObject.CompareTag( "Player" ) )
            isTouching = true;
    }

    private void OnCollisionExit2D( Collision2D other ) 
    {
        if( other.gameObject.CompareTag( "Player" ) )
            isTouching = false;
    }
}