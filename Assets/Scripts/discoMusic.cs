using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class discoMusic : MonoBehaviour
{
    public AudioSource[ ] discoList;
    
    // Start is called before the first frame update
    void Start()
    {
        playDisco( );
    }

    public void playDisco( )
    {
        int songToPlay = Random.Range( 0, discoList.Length );
        discoList[ songToPlay ].Play( );
    }
}
