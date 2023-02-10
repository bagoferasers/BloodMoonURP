using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stayPut : MonoBehaviour
{
    public GameObject g;
    private Vector3 point;
    
    // Start is called before the first frame update
    void Start()
    {
        point = new Vector3( g.transform.position.x, transform.position.y, g.transform.position.z );
        transform.position = Vector3.MoveTowards( transform.position, point, 0 );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        point = new Vector3( g.transform.position.x, transform.position.y, g.transform.position.z );
        transform.position = Vector3.MoveTowards( transform.position, point, 0 );
    }
}
