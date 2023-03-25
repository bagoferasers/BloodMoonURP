using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthShield : MonoBehaviour
{
    public float maxHealth;
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        if( PlayerPrefs.GetFloat( "MaxHealth" ) != 0 )
            maxHealth = PlayerPrefs.GetFloat( "MaxHealth" );
        if( PlayerPrefs.GetFloat( "Health" ) != 0 )
            health = PlayerPrefs.GetFloat( "Health" );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHealth( float value )
    {
        health = value;
        PlayerPrefs.SetFloat( "Health", value );
    }

    public void setMaxHealth( float value )
    {
        maxHealth = value;
        PlayerPrefs.SetFloat( "MaxHealth", value );
    }
}
