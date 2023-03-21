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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if( isTouching && playerRB2D.velocity.y == 0 )
            playerRB2D.AddForce( Vector3.right * force * Time.deltaTime, ForceMode2D.Impulse );
            //make player go owe and die slowly with painful water because ew I'm wet
            //and obviously water makes people die sometimes so I should find a way
            //to make that happen yes that sounds about right why am I typing so much
            //oh yeah trauma thats fun maybe I should make this game about personal trauma that
            //people should never have to endure like oh I don't know
            //reading multiple single line comments in a row not designated by /* */ ahahhahahahhahhahahahhahahhahahahhahaha hhhhhaaaaa ha
            //
            //
            //
            // ...
            // 
            //
            // ha
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