using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bar_Enemy : MonoBehaviour
{
    public Animator animator;
    public string currentState;
    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;
    public float speed = 2f;
    public bool platformer = true;
    public Transform player;
    public float attackRange = 2f;

    // Start is called before the first frame update
    void Start()
    {
        ChangeState( "RowanIdle" );
        rb2D = GetComponent< Rigidbody2D >( );
        spriteRenderer = GetComponent< SpriteRenderer >( );
    }

    public void ChangeState( string state )
    {
        if( currentState == state )
            return;
        currentState = state;
        animator.Play( state );
    }

    void Update( ) 
    {
        if( player != null && Vector2.Distance( transform.position, player.position ) <= attackRange )
        {
            ChangeState( "RowanLeftAttack" );
        }
        else
        {
            
        }
    }
}
