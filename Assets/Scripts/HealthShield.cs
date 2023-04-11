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

        if( PlayerPrefs.GetInt( "HasStartedGame" ) == 1 )
        {
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
        else
        {       
            PlayerPrefs.SetInt( "HaveSlept", 0 );
            float maxH = 20f;
            float maxS = 20f;
            pm.healthBar.maxValue = 20f;
            pm.shieldBar.maxValue = 20f;
            healthMaxText.text = maxH.ToString( );
            shieldMaxText.text = maxS.ToString( );

            float h = 15f;
            float s = 15f;
            pm.healthBar.value = 15f;
            pm.shieldBar.value = 15f;
            healthText.text = h.ToString( );
            shieldText.text = s.ToString( );
        }
    }

    // Update is called once per frame
    void Update()
    {
        shieldText.text = pm.shieldBar.value.ToString( );
        healthText.text = pm.healthBar.value.ToString( );
        shieldMaxText.text = pm.healthBar.maxValue.ToString( );
        healthMaxText.text = pm.shieldBar.maxValue.ToString( );
        PlayerPrefs.SetFloat( "MaxHealth", pm.healthBar.maxValue );
        PlayerPrefs.SetFloat( "MaxShield", pm.shieldBar.maxValue );
        PlayerPrefs.SetFloat( "Health", pm.healthBar.value );
        PlayerPrefs.SetFloat( "Shield", pm.shieldBar.value );
    }

    public void setHealth( float value )
    {
        PlayerPrefs.SetFloat( "Health", value );
    }

    public void setMaxHealth( float value )
    {
        PlayerPrefs.SetFloat( "MaxHealth", value );
    }
}
