using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Bar_Enemy : MonoBehaviour
{
    [ Header( "Player Health and Shields" ) ]
    public Slider PlayerHealthBar;
    public Slider PlayerShieldBar;

    public AnimationClip die;

    public Slider EnemyHealthBar;
    public Slider EnemyShieldBar;

    public GameObject EnemyHealthShield;
    public ParticleSystem PlayerBloodRight;
    public ParticleSystem PlayerBloodLeft;
    public ParticleSystem EnemyBloodRight;
    public ParticleSystem EnemyBloodLeft;
    public Animator animator;
    public Animator playerAnimator;
    public float speed = 2f;
    public float speedMultiplier = 2f;
    public bool platformer = true;
    public Transform player;
    public float attackRange = 2f;
    public float patrolRange = 4f;
    public float idleTime = 0f;
    public float attackWaitTime = 0.2f;
    public Rigidbody2D rb2D;
    public enum AIState 
    {
        Idle, Patrol, Chase, Attack, Dead
    }
    public AIState currentState;
    private Vector3 initialPos;
    private float dest = 0f;
    private bool facingRight = true;
    private bool hasWaited = false;
    private float distanceBetween;
    private float distanceRemaining;
    private float updatedSpeed;

    public float s = 1f;
    public float targetY = -8f;
    public float targetRotationX = -40f;
    public float axeHurt;
    private Quaternion pos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        updatedSpeed = speed * speedMultiplier;
        pos = Quaternion.Euler( targetRotationX, transform.rotation.y, transform.rotation.z );
        ChangeAIState( state: AIState.Idle );
        if( PlayerPrefs.GetFloat( "EnemyShield" ) == 0 && PlayerPrefs.GetFloat( "EnemyHealth" ) == 0 )
        {
            PlayerPrefs.SetFloat( "EnemyShield", EnemyShieldBar.value );  
            PlayerPrefs.SetFloat( "EnemyHealth", EnemyHealthBar.value );
        }
        else
        {
            EnemyShieldBar.value = PlayerPrefs.GetFloat( "EnemyShield" );  
            EnemyHealthBar.value = PlayerPrefs.GetFloat( "EnemyHealth" );            
        }

    }

    void Update( ) 
    {
        distanceBetween = Mathf.Abs( player.position.x - transform.position.x );
        distanceRemaining = Mathf.Abs( dest - transform.position.x ); 

        if( EnemyHealthBar.value == 0f )
            ChangeAIState( state: AIState.Dead );

        if( playerAnimator.GetBool( "attack" ) == true )
        {
            if( EnemyShieldBar.value > 0f )
            {
                EnemyShieldBar.value -= axeHurt;
                PlayerPrefs.SetFloat( "EnemyShield", EnemyShieldBar.value );            
            }
            else if( EnemyHealthBar.value > 0f )
            {
                EnemyHealthBar.value -= axeHurt;
                PlayerPrefs.SetFloat( "EnemyHealth", EnemyHealthBar.value );
            }
            if( facingRight )
                EnemyBloodLeft.Play( );
            else
                EnemyBloodRight.Play( );
        }

        switch( currentState )
        {
            case AIState.Idle:
                EnemyHealthShield.SetActive( false );
                animator.SetBool( "attack", false );
                animator.SetInteger( "motionX", 0 );
                rb2D.velocity = Vector2.zero;
                CheckChase( distance: distanceBetween );
                break;
            case AIState.Patrol:
                EnemyHealthShield.SetActive( false );
                // handle case if player is within range
                CheckChase( distance: distanceBetween );
                // handle patroling
                RemainingPatrolDistance( distance: distanceRemaining );  
                break;
            case AIState.Chase:
                EnemyHealthShield.SetActive( true );
                // handle case if player is out of range
                if( ( !facingRight && distanceBetween > attackRange + 0.5f ) || ( facingRight && distanceBetween > attackRange + 4f ) )
                    ChangeAIState( state: AIState.Idle );
                // handle case if player is within attack range
                else if( ( !facingRight && distanceBetween < 0.5f ) || ( facingRight && distanceBetween < 4f ) )
                    ChangeAIState( state: AIState.Attack );
                GoChase( );
                break;
            case AIState.Attack:
                rb2D.velocity = Vector2.zero;
                if( player.position.x < transform.position.x )
                    facingRight = false;
                else 
                    facingRight = true;
                break;
        }
    }

    public void ChangeAIState( AIState state )
    {
        currentState = state;
        switch( currentState )
        {
            case AIState.Idle:
                rb2D.velocity = Vector2.zero;
                animator.SetBool( "attack", false );
                animator.SetInteger( "motionX", 0 );
                if( !hasWaited )
                    StartCoroutine( waitRandomTime( ) );
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
                if( !hasWaited )
                    StartCoroutine( waitForAttack( ) );
                break;
            case AIState.Dead:
                animator.Play( die.name );
                StartCoroutine( deathMovement( ) );
                EnemyShieldBar.value = 0f;
                EnemyHealthBar.value = 0f;
                rb2D.isKinematic = true;
                EnemyHealthShield.SetActive( false );
                break;            
        }
    }

    IEnumerator waitRandomTime( )
    {
        hasWaited = true;
        float f = Random.Range( 0f, idleTime );
        yield return new WaitForSeconds( f );
        ChangeAIState( state: AIState.Patrol );
        hasWaited = false;
    }

    IEnumerator waitForAttack( )
    {
        hasWaited = true;
        animator.SetBool( "attack", true );
        if( !facingRight )
            PlayerBloodLeft.Play( );
        else
            PlayerBloodRight.Play( );
        
        if( PlayerShieldBar.value > 0f )
        {
            PlayerShieldBar.value -= axeHurt;
            PlayerPrefs.SetFloat( "Shield", PlayerShieldBar.value );            
        }
        else if( PlayerHealthBar.value > 0f )
        {
            PlayerHealthBar.value -= axeHurt;
            PlayerPrefs.SetFloat( "Health", PlayerHealthBar.value );
        }

        yield return new WaitForSeconds( .5f );
        animator.SetBool( "attack", false );
        hasWaited = false;
        ChangeAIState( state: AIState.Chase );
    }

    IEnumerator deathMovement( )
    {
        while( transform.position.y > targetY )
        {
            EnemyBloodLeft.Play( );
            EnemyBloodRight.Play( );
            Vector3 pos = transform.position;
            pos.y -= s * Time.deltaTime;
            transform.position = pos;
            yield return null;
        }
        while( Quaternion.Angle( transform.rotation, pos ) > 0.01f )
        {
            EnemyBloodLeft.Play( );
            EnemyBloodRight.Play( );
            transform.rotation = Quaternion.RotateTowards( transform.rotation, pos, s * Time.deltaTime );
            yield return null;
        }
    }

    void OnCollisionEnter2D( Collision2D collision )
    {
        if( collision.collider.CompareTag( "Wall" ) )
        {
            facingRight = !facingRight;
            dest = facingRight ? initialPos.x + patrolRange : initialPos.x - patrolRange;
            ChangeAIState( state: AIState.Idle );
        }
    }

    public void CheckChase( float distance )
    {
        if( ( !facingRight && distanceBetween < attackRange + 0.5f ) || ( facingRight && distanceBetween < attackRange + 4f ) )
        {
            if( player.position.x < transform.position.x )
                facingRight = false;
            else 
                facingRight = true;
            ChangeAIState( state: AIState.Chase );
        }
    }

    public void GoChase( )
    {
        if( player.position.x < ( transform.position.x + 0.5f ) )// handle case of chasing player right and left
        {
            facingRight = false;
            animator.SetInteger( "motionX", -1 );
            rb2D.velocity = new Vector2( -updatedSpeed, rb2D.velocity.y );
        }
        else if( player.position.x > ( transform.position.x + 4f ) )
        {
            facingRight = true;
            animator.SetInteger( "motionX", 1 );
            rb2D.velocity = new Vector2( updatedSpeed, rb2D.velocity.y );
        }
    }

    public void RemainingPatrolDistance( float distance )
    {
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
            facingRight = !facingRight;
            dest = facingRight ? initialPos.x + patrolRange : initialPos.x - patrolRange;
            ChangeAIState( state: AIState.Idle );
        }
    }
}
