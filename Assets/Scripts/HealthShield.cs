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
        pm.healthBar.maxValue = PlayerPrefs.GetFloat( "MaxHealth" );
        healthMaxText.text = PlayerPrefs.GetFloat( "MaxHealth" ).ToString( );
        pm.healthBar.value = PlayerPrefs.GetFloat( "Health" );
        healthText.text = PlayerPrefs.GetFloat( "Health" ).ToString( );

        // set shield upon load
        pm.shieldBar.maxValue = PlayerPrefs.GetFloat( "MaxShield" );
        shieldMaxText.text = PlayerPrefs.GetFloat( "MaxShield" ).ToString( );
        pm.shieldBar.value = PlayerPrefs.GetFloat( "Shield" );
        shieldText.text = PlayerPrefs.GetFloat( "Shield" ).ToString( );
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat( "Health", pm.healthBar.value );
        PlayerPrefs.SetFloat( "Shield", pm.shieldBar.value );
        shieldText.text = PlayerPrefs.GetFloat( "Shield" ).ToString( );
        healthText.text = PlayerPrefs.GetFloat( "Health" ).ToString( );
        shieldMaxText.text = PlayerPrefs.GetFloat( "MaxShield" ).ToString( );
        healthMaxText.text = PlayerPrefs.GetFloat( "MaxHealth" ).ToString( );
    }

    public void setHealth( float value )
    {
        PlayerPrefs.SetFloat( "Health", value );
    }

    public void setMaxHealth( float value )
    {
        //maxHealth = value;
        PlayerPrefs.SetFloat( "MaxHealth", value );
    }
}
