using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light;
    public float minIntensity;
    public float maxIntensity;
    public float maxDuration;
    private float duration;
    public float speed;
    public float delay;
    //private float randomRange;
    private float originalDuration;

    void Start( )
    {
        originalDuration = duration;
        StartCoroutine( Flicker( ) );
    }

    private IEnumerator Flicker( )
    {
        while( true )
        {
            //randomRange = Random.Range( 0f, 1f );
            duration = Random.Range( 1, maxDuration );
            while( duration > 0 )
            {
                float i = Mathf.Lerp( minIntensity, maxIntensity, Mathf.PingPong( Time.deltaTime * speed, 1f ) );
                light.intensity = i;
                yield return null;
                duration -= Time.deltaTime;
            }
            yield return new WaitForSeconds( delay );
            duration = originalDuration;
        }
    }
}
