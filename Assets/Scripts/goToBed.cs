using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goToBed : MonoBehaviour
{
    [ Header( "Health and Shields" ) ]
    public Slider healthBar;
    public Slider shieldBar;

    public void sleep( )
    {
        Debug.Log( "Sleeping" );
        //shieldBar.value = PlayerPrefs.GetFloat( "HealthMax" );
        //healthBar.value = PlayerPrefs.GetFloat( "ShieldhMax" );
        PlayerPrefs.SetFloat( "Health", PlayerPrefs.GetFloat( "HealthMax"  ) );
        PlayerPrefs.SetFloat( "Shield", PlayerPrefs.GetFloat( "ShieldhMax" ) );
    }
}
