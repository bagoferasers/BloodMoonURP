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

    float speed;
    float origXlocation;
    float origZlocation;
    Vector3 v2;

    void Start( ) 
    {
        /* Get current x position and set speed to random range between the min and max speed. */
        //Vector3 v = GetComponent<RectTransform>( ).localPosition;
        origXlocation = transform.position.x;
        origZlocation = transform.position.z;
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
        if( transform.position.x > ( buffer ) ) 
        {
            transform.position = new Vector3(  -buffer,  Random.Range( minY, maxY ), origZlocation );
            speed = Random.Range( min, max );
        }
    }
}