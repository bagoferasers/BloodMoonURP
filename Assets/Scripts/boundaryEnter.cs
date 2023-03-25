using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundaryEnter : MonoBehaviour
{
    public string scene;

    [ Header( "Connected ScenePortal:" ) ]
    public GameObject sp;

    private transportScene ts;

    // Start is called before the first frame update
    void Start()
    {
        ts = sp.GetComponent< transportScene >( );
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D( Collider2D thisCollider )
    {
        if( thisCollider.tag == "Player" )
        {
            PlayerPrefs.SetString( "startPosition", ts.idConnected );
            PlayerPrefs.Save( );
            ts.ChangeToScene( scene );
        }
    }
}
