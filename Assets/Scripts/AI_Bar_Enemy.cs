using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bar_Enemy : MonoBehaviour
{
    public Animator animator;
    public float speed = 2f;
    public float speedMultiplier = 2f;
    public bool platformer = true;
    public Transform player;
    public float attackRange = 2f;
    public float patrolRange = 4f;
    public float idleTime = 2f;
    public float attackWaitTime = 0.5f;

    public Rigidbody2D rb2D;
    //public SpriteRenderer spriteRenderer;

    public enum AIState 
    {
        Idle, Patrol, Chase, Attack
    }

    public AIState currentState;
    private Vector3 initialPos;
    private float dest = 0f;
    private bool facingRight = true;
    private bool hasWaited = false;
    private float distanceBetween;
    private float distanceRemaining;
    private float updatedSpeed;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        updatedSpeed = speed * speedMultiplier;
        ChangeAIState( AIState.Idle );
    }

    void Update( ) 
    {
        distanceBetween = Mathf.Abs( player.position.x - transform.position.x );
        distanceRemaining = Mathf.Abs( dest - transform.position.x ); 
        switch( currentState )
        {
            case AIState.Idle:
                // handle case if player is within range
                CheckChase( distanceBetween );
                waitRandomTime( );
                break;
            case AIState.Patrol:
                // handle case if player is within range
                CheckChase( distanceBetween );
                // handle patroling
                RemainingPatrolDistance( distanceRemaining );  
                break;
            case AIState.Chase:
                // handle case if player is out of range
                if( distanceBetween > attackRange )
                    ChangeAIState( AIState.Idle );
                // handle case if player is within attack range
                else if( distanceBetween < 1f )
                    ChangeAIState( AIState.Attack );
                GoChase( );
                break;
            case AIState.Attack:
                //
                break;
        }
    }

    public void ChangeAIState( AIState state )
    {
        Debug.Log( currentState );
        currentState = state;
        Debug.Log( "Entered CHANGEAISTATE" );

        switch( currentState )
        {
            case AIState.Idle:
                Debug.Log( "Entered CHANGEAISTATE IDLE" );
                rb2D.velocity = Vector2.zero;
                animator.SetInteger( "motionX", 0 );
                Debug.Log( "After animator set int" );
                if( facingRight )
                {
                    animator.SetBool( "right", true );
                    animator.SetBool( "left", false );
                }
                else
                {
                    animator.SetBool( "right", false );
                    animator.SetBool( "left", true );
                }
                Debug.Log( "After everything in changeaistate for idle" );
                break;
            case AIState.Patrol:
                if( facingRight )
                {
                    animator.SetBool( "right", true );
                    animator.SetBool( "left", false );
                }
                else
                {
                    animator.SetBool( "right", false );
                    animator.SetBool( "left", true );
                }
                break;
            case AIState.Chase:
                if( facingRight )
                {
                    animator.SetBool( "right", true );
                    animator.SetBool( "left", false );
                }
                else
                {
                    animator.SetBool( "right", false );
                    animator.SetBool( "left", true );
                }
                break;
            case AIState.Attack:
                    animator.SetBool( "attack", true );
                break;
        }
    }

    IEnumerator waitRandomTime( )
    {
        Debug.Log( "Waitingrandomtime" );
        hasWaited = true;
        float f = Random.Range( 1f, idleTime );
        yield return new WaitForSeconds( f );
        ChangeAIState( AIState.Patrol );
        hasWaited = false;
    }

    IEnumerator waitForAttack( )
    {
        hasWaited = true;
        animator.SetBool( "attack", true );
        float f = Random.Range( 0f, attackWaitTime );
        yield return new WaitForSeconds( f );
        animator.SetBool( "attack", false );
        hasWaited = false;
    }

    void OnCollisionEnter2D( Collision2D collision )
    {
        if( collision.collider.CompareTag( "Wall" ) )
        {
            facingRight = !facingRight;
            dest = facingRight ? initialPos.x + patrolRange : initialPos.x - patrolRange;
            ChangeAIState( AIState.Idle );
        }
    }

    public void CheckChase( float distance )
    {
        Debug.Log( "Checking Chase" );
        if( distanceBetween <= attackRange )
        {
            if( player.position.x < transform.position.x )
                facingRight = false;
            else 
                facingRight = true;
            ChangeAIState( AIState.Chase );
        }
    }

    public void GoChase( )
    {
        if( player.position.x < transform.position.x )// handle case of chasing player right and left
        {
            facingRight = false;
            animator.SetInteger( "motionX", -1 );
            rb2D.velocity = new Vector2( -updatedSpeed, rb2D.velocity.y );
        }
        else if( player.position.x > transform.position.x )
        {
            facingRight = true;
            animator.SetInteger( "motionX", 1 );
            rb2D.velocity = new Vector2( updatedSpeed, rb2D.velocity.y );
        }
    }

    public void RemainingPatrolDistance( float distance )
    {
        Debug.Log( "Entered remaniningPatrolDistance" );
        distance = Mathf.Abs( dest - transform.position.x );
        if( distance > 0.1f )
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
            //facingRight = !facingRight;
            dest = facingRight ? initialPos.x + patrolRange : initialPos.x - patrolRange;
            ChangeAIState( AIState.Idle );
        }
    }
}
