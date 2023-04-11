using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    private Color color;
    private bool fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(  PlayerPrefs.GetInt( "HaveSlept" ) == 0 )
        {
            Debug.Log( "PlayerPrefs have slept is zero " );
            gameObject.SetActive( true );
            if( fade == false )
                StartCoroutine( fadeIn( ) );
            else
                StartCoroutine( fadeOut( ) );               
        }
        else
        {
            Debug.Log( "PlayerPrefs have slept is one " );
            gameObject.SetActive( false );
        }
    }

    IEnumerator fadeIn( )
    {
        while( gameObject.GetComponent< CanvasGroup >( ).alpha < 1 && gameObject.GetComponent< CanvasGroup >( ).alpha >= 0 )
        {
            if( PlayerPrefs.GetInt( "HaveSlept" ) == 1 )
                yield break;
            gameObject.GetComponent< CanvasGroup >( ).alpha += Time.deltaTime * 0.01f;
            color = gameObject.GetComponent< SpriteRenderer >( ).color;
            color.a = gameObject.GetComponent< CanvasGroup >( ).alpha;
            gameObject.GetComponent< SpriteRenderer >( ).color = color;
            yield return null;
        }         
        fade = true;
        yield return null;
    }

    IEnumerator fadeOut( )
    {
        while( gameObject.GetComponent< CanvasGroup >( ).alpha > 0 && gameObject.GetComponent< CanvasGroup >( ).alpha <= 1 )
        {
            if( PlayerPrefs.GetInt( "HaveSlept" ) == 1 )
                yield break;
            gameObject.GetComponent< CanvasGroup >( ).alpha -= Time.deltaTime * 0.01f;
            color = gameObject.GetComponent< SpriteRenderer >( ).color;
            color.a = gameObject.GetComponent< CanvasGroup >( ).alpha;
            gameObject.GetComponent< SpriteRenderer >( ).color = color;
            yield return null;
        }
        fade = false;
        yield return null;
    }
}
