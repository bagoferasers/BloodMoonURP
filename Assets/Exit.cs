using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public void exitGame( )
    {
        StartCoroutine( goodbye( ) );
    }
    
    public IEnumerator goodbye( )
    {
        yield return new WaitForSeconds( 3 );
        Debug.Log( "exitgame" );
        Application.Quit( );
    }
}