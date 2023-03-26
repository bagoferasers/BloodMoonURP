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
        shieldBar.value = PlayerPrefs.GetFloat( "MaxHealth" );
        healthBar.value = PlayerPrefs.GetFloat( "MaxShield" );
    }
}
