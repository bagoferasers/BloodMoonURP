using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudScroll : MonoBehaviour
{
    [ Header( "(float) buff out that cloudboi" ) ]
    public float buffer;

    [ Header( "(float) min/max Y axis spawn" ) ]
    public float minY;
    public float maxY;

    [ Header( "(float) min/max speed" ) ]
    public float min;
    public float max;

    float origZlocation;
    float speed;
    float origXlocation;
    Vector3 v;
    

    void Start( ) 
    {
        /* Get current x position and set speed to random range between the min and max speed. */
        v = transform.position;
        origXlocation = v.x;
        speed = Random.Range( min, max );
    }

    void FixedUpdate( ) 
    {
        StartCoroutine( moveClouds( ) );
    }

    private IEnumerator moveClouds( )
    {
        transform.position += new Vector3( Time.fixedDeltaTime * speed, 0, 0 );

        /* If cloud is outside of screen width, reset position and randomize Y axis and speed. */
        if( transform.position.x > ( buffer ) ) 
        {
            v.x = origXlocation - buffer;
            v.y = Random.Range( minY, maxY );
            transform.position = v;
            speed = Random.Range( min, max );
        }
        yield return null;
    }
}