using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    [ Header( "Movement" ) ]
    public float speed = 3f;

    Rigidbody2D rb2d;
    public float forceMultiplier = 10;

    //[Header("Temp Objects")]
    //public MyBox myBox;

    // Start is called before the first frame update
    void Start( )
    {
        //GetComponent<SpriteRenderer>().color = Color.red;
        rb2d = GetComponent<Rigidbody2D>( );
        //rb2d = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //myBox = GameObject.FindGameObjectWithTag("Box").GetComponent<MyBox>();
    }

    // Update is called once per frame
    void Update( )
    {
        //transform.position += new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0) * speed * Time.deltaTime;
        rb2d.MovePosition( transform.position + ( new Vector3( Input.GetAxisRaw( "Horizontal" ),Input.GetAxisRaw( "Vertical" ) ) * Time.deltaTime * speed ) );

        //rb2d.position += new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0) * speed * Time.deltaTime;
        //amount of time since last frame
        //Time.deltaTime;
        ///if( Input.GetKeyDown( KeyCode.K ) )
        ///{
            //GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
            //sr.color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
            //myBox.ChangeColor(Color.black);
            ///GameObject newBox = Instantiate( myBoxGamePrefab, transform.position, Quaternion.identity );
        //}

        ///if(Input.GetKeyDown(KeyCode.E))
        ///{
            ///if(myBox != null)
            ///{
                ///myBox.ChangeColor(Color.black);
            ///}
        ///}
    }


    void FixedUpdate( )
    {
        //rb2d.MovePosition( transform.position + ( new Vector3( Input.GetAxisRaw( "Horizontal" ),Input.GetAxisRaw( "Vertical" ) ) * Time.deltaTime * speed ) );
        //rb2d.AddForce( new Vector2( Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")) * forceMultiplier * Time.deltaTime );
        //rb2d.velocity = ( new Vector3( Input.GetAxisRaw( "Horizontal" ),Input.GetAxisRaw( "Vertical" ) ) * speed );

    }     
    /*   
    private void OnTriggerEnter2D( Collider2D other )
    {
        Debug.Log( "We collided with a box!" );
        other.GetComponent<AudioSource>().Play();
        //other.GetComponent< SpriteRenderer >( );
        //Destroy(other.GameObject);
        if(other.GetComponent<MyBox>() == null)
        {
            myBox = other.GetComponent<MyBox>();
        }
        if(other.tag == "Food!")
        {
            Destroy(other.gameObject);
            transform.localScale *= 1.1f;
        }
        
    }

    private void OnTriggerExit2D( Collider2D other )
    {
        Debug.Log( "We exited a box!" );
        //other.GetComponent< SpriteRenderer >( );
        //Destroy(other.GameObject);
        if(other.GetComponent<MyBox>() != null)
        {
            myBox = null;
        }
    }
    */
}