using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthShield : MonoBehaviour
{
    private PlayerMovement pm;
    public Text healthText;
    public Text healthMaxText;
    public Text shieldText;
    public Text shieldMaxText;

    // Start is called before the first frame update
    void Start()
    {
        pm = gameObject.GetComponent< PlayerMovement >( );

        // set health upon load
        if( PlayerPrefs.GetFloat( "MaxHealth" ) != 0 )
        {
            pm.healthBar.maxValue = PlayerPrefs.GetFloat( "MaxHealth" );
            healthMaxText.text = PlayerPrefs.GetFloat( "MaxHealth" ).ToString( );
        }
        else
        {
            pm.healthBar.maxValue = 20f;
            PlayerPrefs.SetFloat( "MaxHealth", 20f );
            healthMaxText.text = PlayerPrefs.GetFloat( "MaxHealth" ).ToString( );
        }

        if( PlayerPrefs.GetFloat( "Health" ) != 0 )
        {
            pm.healthBar.maxValue = PlayerPrefs.GetFloat( "Health" );
            healthText.text = PlayerPrefs.GetFloat( "Health" ).ToString( );
        }
        else
        {
            pm.healthBar.maxValue = 15f;
            PlayerPrefs.SetFloat( "Health", 15f );
            healthText.text = PlayerPrefs.GetFloat( "Health" ).ToString( );
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat( "Health", pm.healthBar.value );
        PlayerPrefs.SetFloat( "Shield", pm.shieldBar.value );
        healthText.text = PlayerPrefs.GetFloat( "MaxHealth" ).ToString( );
        healthMaxText.text = PlayerPrefs.GetFloat( "MaxHealth" ).ToString( );
    }

    public void setHealth( float value )
    {
        //health = value;
        PlayerPrefs.SetFloat( "Health", value );
    }

    public void setMaxHealth( float value )
    {
        //maxHealth = value;
        PlayerPrefs.SetFloat( "MaxHealth", value );
    }
}
