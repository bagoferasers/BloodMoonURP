using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catSounds : MonoBehaviour
{
    public AudioSource[ ] mewList;

    // Update is called once per frame
    public void playMew( )
    {
        int songToPlay = Random.Range( 0, mewList.Length );
        for( int i = 0; i < mewList.Length; i++ )
        {
            if( i == songToPlay )
                mewList[ i ].Play( );
            else
                mewList[ i ].Stop( );
        }
    }
}
