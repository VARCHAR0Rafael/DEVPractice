//Used for player inputs, movement, attack, etc.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IDamageble
{
    //Variables for Inputs for movement, attacking, etc.
    public KeyCode meleeAttackmove = KeyCode.Mouse0;
    public KeyCode rangeAttackmove = KeyCode.Mouse1;
    public KeyCode jumping = KeyCode.Space;
    public string xmovementaxis = "Horizontal";

    //Variables for combat
    public Transform meleeAttackOrigin = null;
    public Transform rangeAttackOrigin = null;
    public GameObject projectile = null;//reference for the projectile.
    public float meleeAttackRadius = 0.6f;//For the radius of the attack.
    public float meleeDamage = 2f;//For the damage done.
    public float meleeAttackDelay = 1.1f;//For the cool down.
    public float rangeAttackDelay = 0.5f;//For the cool down.
    public LayerMask enemyLayer = 8;

    //Variables for Physics
    private Rigidbody2D rb2d = null;
    private float moveIntentionX = 0;
    private bool attemptJump = false;
    private bool attempMeleeAttack = false;
    private bool attempRangeAttack = false;
    private float timeUntilMeleeAttackReady = 0;
    private float timeUntilRangeAttackReady = 0;


    // Start is called before the first frame update
    void Start()
    {
        //Making sure our rigid body exist to allow our character to move.
        if (GetComponent<Rigidbody2D>())
        {
            rb2d = GetComponent<Rigidbody2D>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //Getting inputs from the function
        GetInput();
        HandleJump();
        HandleMeleeAttack();
        HandleRangeAttacks();
        
    }

    private void FixedUpdate()
    {
        //Making the player to move
        HandleRun();
    }

    private void OnDrawGizmosSelected()
    {
        if (meleeAttackOrigin != null)
        {
            Gizmos.DrawWireSphere(meleeAttackOrigin.position, meleeAttackRadius);
        }
    }
    //Funtion for getting inputs.
    private void GetInput()
    {
        moveIntentionX = Input.GetAxis(xmovementaxis);//Getting movement horizantal inputs.
        attempMeleeAttack = Input.GetKeyDown(meleeAttackmove);//Getting attack inputs.
        attempRangeAttack = Input.GetKeyDown(rangeAttackmove);//Getting range attack inputs.
        attemptJump = Input.GetKeyDown(jumping);//Getting jump inputs.
    }

    private void HandleJump()
    {
        //Making jump
        if (attemptJump && Grounded())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
    }

    //Funtion for making melee attacks.
    private void HandleMeleeAttack()
    {
        //Making melee attack, so first we ask if we can attack by the cool down.
        if (attempMeleeAttack && timeUntilMeleeAttackReady <= 0)
        {
            Debug.Log("Attack");
            //Making logic for attacking
            Collider2D[] overLappedColliders = Physics2D.OverlapCircleAll(meleeAttackOrigin.position, meleeAttackRadius, enemyLayer);
            for (int i = 0; i < overLappedColliders.Length; i++)
            {
                IDamageble enemyAtributes = overLappedColliders[i].GetComponent<IDamageble>();
                if (enemyAtributes != null)
                {
                    enemyAtributes.ApplyDamage(meleeDamage);
                }
            }
            timeUntilMeleeAttackReady = meleeAttackDelay;

        }else
        {
            timeUntilMeleeAttackReady -= Time.deltaTime;
        }

    }

    //Function for making range attacks
    private void HandleRangeAttacks()
    {
        //Making range attack, so first we ask if we can attack by the cool down.
        if (attempRangeAttack && timeUntilRangeAttackReady <= 0)
        {
            Debug.Log("Range Attack");
            Instantiate(projectile, rangeAttackOrigin.position, rangeAttackOrigin.rotation);
            
            timeUntilRangeAttackReady = rangeAttackDelay;
        }
        else
        {
            timeUntilRangeAttackReady -= Time.deltaTime;
        }

    }

    private void HandleRun()
    {
        //Rotate the character when it moves.
        if (moveIntentionX > 0 && transform.rotation.y == 0)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else if (moveIntentionX < 0 && transform.rotation.y != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //Calculate velocity of rigid body.
        rb2d.velocity = new Vector2(moveIntentionX * speed, rb2d.velocity.y);

    }
     //Funtion for die,
    public virtual void ApplyDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

}
