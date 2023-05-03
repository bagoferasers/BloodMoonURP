using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI_Bar_Enemy : MonoBehaviour
{
    [ Header( "Player Health and Shields" ) ]
    public Slider PlayerHealthBar;
    public Slider PlayerShieldBar;
    public Text healthText;
    public Text healthMaxText;
    public Text shieldText;
    public Text shieldMaxText;
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
        EnemyShieldBar.value = EnemyShieldBar.maxValue;
        EnemyHealthBar.value = EnemyHealthBar.maxValue;
        rb2D.isKinematic = false;
        initialPos = transform.position;
        updatedSpeed = speed * speedMultiplier;
        pos = Quaternion.Euler( targetRotationX, transform.rotation.y, transform.rotation.z );

//////////////////am I the problem???????????????/////////////////////////////////////////////////
        if( PlayerPrefs.GetString( "AISTATE" ) == "Dead" )
        {
            transform.position = new Vector2( PlayerPrefs.GetFloat( "EnemyXPos" ), PlayerPrefs.GetFloat( "EnemyYPos" ) );
            Debug.Log( "AI is DEAD!!!!!!!!" );
            EnemyBloodLeft.Stop( );
            EnemyBloodRight.Stop( );  
            EnemyShieldBar.value = 0f;
            EnemyHealthBar.value = 0f;
        }
        else
        {
            Debug.Log( "AI is NOT DEAD!!!!!!!!" );
            ChangeAIState( state: AIState.Idle );
            if( PlayerPrefs.GetFloat( "EnemyHealth" ) == 0 )
            {
                Debug.Log( "AI health is 0!!!!!!!!" );
                EnemyHealthBar.value = EnemyHealthBar.maxValue;
            }
            else
            {
                Debug.Log( "AI health is NOT 0!!!!!!!!" );
                EnemyHealthBar.value = PlayerPrefs.GetFloat( "EnemyHealth" );
            }

            if( PlayerPrefs.GetFloat( "EnemyShield" ) == 0 )
            {
                Debug.Log( "AI shield is 0!!!!!!!!" );
                EnemyShieldBar.value = EnemyShieldBar.maxValue;
            }
            else
            {
                Debug.Log( "AI shield is NOT0!!!!!!!!" );
                EnemyShieldBar.value = PlayerPrefs.GetFloat( "EnemyShield" );
            }
        }
    }

    void Update( ) 
    {
        if( currentState == AIState.Dead )
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionY;
        else
            rb2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

        healthText.text = EnemyHealthBar.value.ToString( );
        healthMaxText.text = EnemyHealthBar.maxValue.ToString( );
        shieldText.text = EnemyShieldBar.value.ToString( );
        shieldMaxText.text = EnemyShieldBar.maxValue.ToString( );

        distanceBetween = Mathf.Abs( player.position.x - transform.position.x );
        distanceRemaining = Mathf.Abs( dest - transform.position.x ); 

        if( EnemyHealthBar.value == 0f && currentState != AIState.Dead )
        {
            Debug.Log( "Changing to Dead" );
            ChangeAIState( state: AIState.Dead );
        }

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
                {
                    shieldText.text = EnemyShieldBar.maxValue.ToString( );
                    EnemyShieldBar.value = EnemyShieldBar.maxValue;
                    ChangeAIState( state: AIState.Idle );
                }
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
                PlayerPrefs.SetString( "AISTATE", "Idle" );
                rb2D.velocity = Vector2.zero;
                animator.SetBool( "attack", false );
                animator.SetInteger( "motionX", 0 );
                if( !hasWaited )
                    StartCoroutine( waitRandomTime( ) );
                break;
            case AIState.Patrol:
                PlayerPrefs.SetString( "AISTATE", "Patrol" );
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
                PlayerPrefs.SetString( "AISTATE", "Chase" );
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
                PlayerPrefs.SetString( "AISTATE", "Attack" );
                if( !hasWaited )
                    StartCoroutine( waitForAttack( ) );
                break;
            case AIState.Dead:
                Debug.Log( "Changing to Dead!" );
                PlayerPrefs.SetString( "AISTATE", "Dead" );
                animator.Play( die.name );
                StartCoroutine( deathMovement( ) );
                EnemyShieldBar.value = 0f;
                EnemyHealthBar.value = 0f;
                rb2D.isKinematic = true;
                EnemyHealthShield.SetActive( false );
                PlayerPrefs.SetFloat( "EnemyXPos", transform.position.x );
                PlayerPrefs.SetFloat( "EnemyYPos", transform.position.y );
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
        Debug.Log( "Moving for death! " );
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
        EnemyBloodLeft.Stop( );
        EnemyBloodRight.Stop( );
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