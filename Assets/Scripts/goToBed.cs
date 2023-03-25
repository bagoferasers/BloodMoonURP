using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goToBed : MonoBehaviour
{
    public void sleep( )
    {
        PlayerPrefs.SetFloat( "Health", PlayerPrefs.GetFloat( "MaxHealth" ) );
        PlayerPrefs.SetFloat( "Shield", PlayerPrefs.GetFloat( "MaxShield" ) );
    }
}
