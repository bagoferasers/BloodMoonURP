using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bar_Enemy : MonoBehaviour
{
    public Animator animator;
    public float speed = 2f;
    public bool platformer = true;
    public Transform player;
    public float attackRange = 2f;
    public float patrolRange = 4f;
    public float idleTime = 2f;

    Rigidbody2D rb2D;
    SpriteRenderer spriteRenderer;

    public enum AIState 
    {
        Idle, Patrol, Chase, Attack
    }

    public AIState currentState;
    private Vector3 initialPos;
    private float dest = 0f;
    private bool facingRight = true;
    private bool collided = false;
    private bool hasWaited = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        ChangeAIState( AIState.Idle );
        rb2D = GetComponent< Rigidbody2D >( );
        spriteRenderer = GetComponent< SpriteRenderer >( );
    }

    public void ChangeAIState( AIState state )
    {
        if( currentState == state )
            return;
        currentState = state;
        switch( currentState )
        {
            case AIState.Idle:
                if( facingRight )
                    animator.Play( "RowanIdleRight" );
                else 
                    animator.Play( "RowanIdleLeft" );
                break;
            case AIState.Patrol:
                if( facingRight )
                    animator.Play( "RowanRightRun" );
                else
                    animator.Play( "RowanLeftRun" );
                break;
            case AIState.Chase:
                if( facingRight )
                    animator.Play( "RowanRightRun" );
                else
                    animator.Play( "RowanLeftRun" );
                break;
            case AIState.Attack:
                if( facingRight )
                    animator.Play( "RowanRightAttack" );
                else
                    animator.Play( "RowanLeftAttack" );
                break;
        }
    }

    IEnumerator waitRandomTime( )
    {
        hasWaited = true;
        float f = Random.Range( 1f, idleTime );
        yield return new WaitForSeconds( f );
        ChangeAIState( AIState.Patrol );
        hasWaited = false;
    }

    void OnCollisionEnter2D( Collision2D collision )
    {
        if( collision.collider.CompareTag( "Wall" ) )
        {
            animator.SetInteger( "motionX", 0 );
            collided = true;
            ChangeAIState( AIState.Idle );
            facingRight = !facingRight;
            dest = facingRight ? initialPos.x + patrolRange : initialPos.x - patrolRange;
        }
    }

    void OnCollisionExit2D( Collision2D collision )
    {
        if( collision.collider.CompareTag( "Wall" ) )
            collided = false;
    }

    void Update( ) 
    {
        switch( currentState )
        {
            case AIState.Idle:
                if( !hasWaited )
                    StartCoroutine( waitRandomTime( ) );
                break;
            case AIState.Patrol:
                float distanceRemaining = Mathf.Abs( dest - transform.position.x );
                float distanceBetween = Mathf.Abs( player.position.x - transform.position.x );
                if( distanceBetween <= attackRange )
                {
                    if( player.position.x < transform.position.x )
                        facingRight = false;
                    else 
                        facingRight = true;
                    ChangeAIState( AIState.Chase );
                }
                    
                if( distanceRemaining > 0.1f )
                {
                    if( facingRight )
                    {
                        rb2D.velocity = new Vector2( speed, rb2D.velocity.y );
                        animator.SetInteger( "motionX", 1 );
                    }
                    else
                    {
                        rb2D.velocity = new Vector2( -speed, rb2D.velocity.y );
                        animator.SetInteger( "motionX", -1 );
                    }                        
                }
                else
                {
                    ChangeAIState( AIState.Idle );
                    animator.SetInteger( "motionX", 0 );
                    facingRight = !facingRight;
                    dest = facingRight ? initialPos.x + patrolRange : initialPos.x - patrolRange;
                }
                break;
            case AIState.Chase:
                if( player != null )
                {
                    float updatedSpeed = speed * 2;
                    if( player.position.x < transform.position.x )
                    {
                        facingRight = false;
                        rb2D.velocity = new Vector2( -updatedSpeed, rb2D.velocity.y );
                        animator.SetInteger( "motionX", -1 );
                    }
                    else if( player.position.x > transform.position.x )
                    {
                        facingRight = true;
                        rb2D.velocity = new Vector2( updatedSpeed, rb2D.velocity.y );
                        animator.SetInteger( "motionX", 1 );
                    }
                }
                break;
            case AIState.Attack:
            {
                rb2D.velocity = Vector2.zero;
                if ( player != null && ( player.position.x < transform.position.x ) )
                    ChangeAIState( AIState.Chase );
                else
                {
                    
                }
                break;
            }
        }
    }
}
