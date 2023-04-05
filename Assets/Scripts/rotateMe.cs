using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateMe : MonoBehaviour
{
    public bool l;
    public bool r;
    public float wait;

    void Start()
    {
        StartCoroutine( changeDirection( ) );
    }

    IEnumerator changeDirection( )
    {
        while( true )
        {
            if( l )
            {
                transform.rotation = Quaternion.Euler( new Vector3(0, 0, 0 ) );
                yield return new WaitForSeconds( wait );
                transform.rotation = Quaternion.Euler( new Vector3(0, -180, 0 ) );
                yield return new WaitForSeconds( wait );       
            }
            else if( r )
            {
                transform.rotation = Quaternion.Euler( new Vector3(0, 180, 0 ) );
                yield return new WaitForSeconds( wait );   
                transform.rotation = Quaternion.Euler( new Vector3(0, 0, 0 ) );
                yield return new WaitForSeconds( wait );               
            }
            yield return null;
        }
    }
}
