//Used for player inputs, movement, attack, etc.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IDamageble
{
    //Variables for Inputs for movement, attacking, etc.
    //public bool isAlive = true;
    public KeyCode meleeAttackmove = KeyCode.Mouse0;
    public KeyCode rangeAttackmove = KeyCode.Mouse1;
    public KeyCode jumping = KeyCode.Space;
    public string xmovementaxis = "Horizontal";

    //Variables for combat.
    public Transform meleeAttackOrigin = null;
    public Transform rangeAttackOrigin = null;
    public GameObject projectile = null;//reference for the projectile.
    public float meleeAttackRadius = 0.6f;//For the radius of the attack.
    public float meleeDamage = 2f;//For the damage done.
    public float meleeAttackDelay = 1.0f;//For the cool down.
    public float rangeAttackDelay = 0.5f;//For the cool down.
    public LayerMask enemyLayer = 8;

    //Variables for Physics.
    private float moveIntentionX = 0;
    private bool attemptJump = false;
    private bool attempMeleeAttack = false;
    private bool attempRangeAttack = false;
    private float timeUntilMeleeAttackReady = 0;
    private float timeUntilRangeAttackReady = 0;
    private bool isMeleeAttacking = false;



    // Update is called once per frame
    void Update()
    {
        //Calling the function
        GetInput();
        HandleJump();
        HandleMeleeAttack();
        HandleRangeAttacks();
        HandleAnimations();


    }

    private void FixedUpdate()
    {
        //Making the player to move
        if (isAlive == true)
        {
            HandleRun();
        }

        if (isAlive && Rb2D.position.y < -6f)
        {
            FindObjectOfType<GameManger>().EndGame();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (meleeAttackOrigin != null && isAlive == true)
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
        if (attemptJump && Grounded() && isAlive == true)
        {
            Rb2D.velocity = new Vector2(Rb2D.velocity.x, jumpForce);
        }
    }

    //Funtion for making melee attacks.
    private void HandleMeleeAttack()
    {
        //Making melee attack, so first we ask if we can attack by the cool down.
        if (attempMeleeAttack && timeUntilMeleeAttackReady <= 0 && isAlive == true)
        {
            Debug.Log("Attack");
            //Making logic for attacking
            Collider2D[] overLappedColliders = Physics2D.OverlapCircleAll(meleeAttackOrigin.position, meleeAttackRadius, enemyLayer);
            for (int i = 0; i < overLappedColliders.Length; i++)
            {
                IDamageble enemyAtributes = overLappedColliders[i].GetComponent<IDamageble>();
                if (enemyAtributes != null && isAlive == true)
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
        if (attempRangeAttack && timeUntilRangeAttackReady <= 0 && isAlive == true)
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
        if (moveIntentionX > 0 && transform.rotation.y == 0 && !isMeleeAttacking && isAlive == true)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else if (moveIntentionX < 0 && transform.rotation.y != 0 && !isMeleeAttacking && isAlive == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        //Calculate velocity of rigid body.
        Rb2D.velocity = new Vector2(moveIntentionX * speed, Rb2D.velocity.y);

    }

    //Making the animations.
    private void HandleAnimations()
    {
        Animator.SetBool("Grounded", Grounded());

        //Animation for melee attack
        if (attempMeleeAttack && isAlive == true)
        {
            if (!isMeleeAttacking && isAlive == true)
            {
                StartCoroutine(MeleeAttackAnimDelay());
                if (Grounded() == false && isAlive == true)
                {
                    Animator.SetTrigger("Jump");
                }

            }

        }

        if (!attemptJump && isAlive == true)
        {
            Animator.SetBool("Jump", false);
        }
        //Animation for jumping.
        if (attemptJump && Grounded() || Rb2D.velocity.y > 1f && isAlive == true)
        {
            if (!isMeleeAttacking && isAlive == true)
            {
                Animator.SetTrigger("Jump");
            }
        }

        if (Mathf.Abs(moveIntentionX) > 0.1f && Grounded() && isAlive == true)
        {
            //Animator.SetInteger("AnimState", 2);
            Animator.SetTrigger("Run");
        }
        else
        {
            //Animator.SetInteger("AnimState", 0);
            Animator.SetBool("Run", false);
        }

    }
     //Funtion for die.
    public virtual void ApplyDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            isAlive = false;
            FindObjectOfType<GameManger>().EndGame();
            Die();
        }
    }

    //Funtion for not attack spam.
    private IEnumerator MeleeAttackAnimDelay()
    {
        Animator.SetTrigger("Attack");
        isMeleeAttacking = true;
        yield return new WaitForSeconds(meleeAttackDelay);
        isMeleeAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            currentHealth -= 5;
            Animator.SetTrigger("hurt");
            if (CurrentHealth <= 0)
            {
                isAlive = false;
                FindObjectOfType<GameManger>().EndGame();
                Die();
            }
        }
    }

}
