using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stayPut : MonoBehaviour
{
    private GameObject g;
    private Vector3 point;
    
    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        point = new Vector3( g.transform.position.x, transform.position.y, g.transform.position.z );
        transform.position = Vector3.MoveTowards( transform.position, point, 0 );
    }
}
