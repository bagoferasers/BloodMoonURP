using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catSounds : MonoBehaviour
{
    public AudioSource[ ] mewList;
    private float random;
    public bool isPetting;


    // Start is called before the first frame update
    void Start()
    {
        isPetting = false;
    }

    // Update is called once per frame
    void Update()
    {
        int songToPlay = Random.Range( 0, mewList.Length );
        if( isPetting )
            mewList[ songToPlay ].Play( ); 
        isPetting = false;
    }
}
