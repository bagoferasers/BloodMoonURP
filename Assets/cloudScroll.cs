using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudScroll : MonoBehaviour
{
    [ Header( "buff out that cloudboi" ) ]
    public float buffer;

    [ Header( "min/max Y axis spawn" ) ]
    public float minY;
    public float maxY;

    [ Header( "min/max speed" ) ]
    public int min;
    public int max;

    int speed;
    float origXlocation;
    Vector3 v2;

    void Start( ) 
    {
        /* Get current x position and set speed to random range between the min and max speed. */
        Vector3 v = GetComponent<RectTransform>( ).localPosition;
        origXlocation = v.x;
        speed = Random.Range( min, max );
    }

    void Update( ) 
    {
        /* I don't know what these transform.localScale.x do and I'm too afraid to ask. */
        if( transform.localScale.x > 0 ) 
            v2 = new Vector3( 4, 0, 0 );

        if( transform.localScale.x < 0 ) 
            v2 = new Vector3( -4, 0, 0 );
        
        /* Move clouds to the right. */
        transform.Translate( v2 * Time.deltaTime * speed, Space.World );

        /* If cloud is outside of screen width, reset position and randomize Y axis and speed. */
        if( transform.position.x > ( Screen.width + buffer ) ) 
        {
            transform.position = new Vector3( origXlocation - buffer,  Random.Range( minY, maxY ), 0 );
            speed = Random.Range( min, max );
        }
    }
}