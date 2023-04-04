using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainbowColor : MonoBehaviour
{
    private Color c;
    private float x;
    private float y;
    private float z;
    private UnityEngine.Rendering.Universal.Light2D l;
    public float speed;
    private bool fwdX, fwdY, fwdZ;

    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent< UnityEngine.Rendering.Universal.Light2D >( );
        c = l.color;
        x = 1f;
        y = 0.5f;
        z = 0f;
        fwdX = true;
        fwdY = true;
        fwdZ = true;
    }

    // Update is called once per frame
    void Update()
    {
        if( fwdX == true )
            x += speed * Time.deltaTime;
        else if( fwdX == false )
            x -= speed * Time.deltaTime;
        
        if( fwdY == true )
            y += speed * Time.deltaTime;
        else if( fwdY == false )
            y -= speed * Time.deltaTime;
        
        if( fwdZ == true )
            z += speed * Time.deltaTime;
        else if( fwdZ == false )
            z -= speed * Time.deltaTime;

        if( x > 1f )
            fwdX = false;
        else if( x < 0f )
            fwdX = true;

        if( y > 1f )
            fwdY = false;
        else if( y < 0f )
            fwdY = true;

        if( z > 1f )
            fwdZ = false;
        else if( z < 0.1f )
            fwdZ = true;
        
        l.color = new Color( x, y, z );
    }
}
