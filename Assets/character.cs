using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    [ Header( "Movement" ) ]
    public float speed;

    Rigidbody2D rb2d;
    public float forceMultiplier;

    // Start is called before the first frame update
    void Start( )
    {
        rb2d = GetComponent<Rigidbody2D>( );
    }

    // Update is called once per frame
    void Update( )
    {
        //rb2d.MovePosition( transform.position + ( new Vector3( Input.GetAxisRaw( "Horizontal" ),Input.GetAxisRaw( "Vertical" ) ) * Time.deltaTime * speed ) );
    }

    public void goRight( )
    {
        Debug.Log( "Moving right." );
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    public void goLeft( )
    {
        Debug.Log( "Moving left." );
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}