//Script for the boss enemy IA.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Enemy : Character, IDamageble
{
    //Variables for combat.
    public Transform meleeAttackOriginEnemy = null;
    public float meleeAttackRadius = 0.6f;//For the radius of the attack.
    public float meleeDamage = 20f;//For the damage done.
    public float meleeRageDamage = 40f;
    public LayerMask playerLayer = 9;
    public Health_barScript health_BarScript;

    //Variables for cooldowns.
    //private float timeUntilMeleeAttackReady = 0;
    public float meleeAttackDelay = 1.0f;//For the cool down.

    //example
    public int attackDamage = 20;
    public int enragedAttackDamage = 40;

    public Vector3 attackOffset;
    public float attackRange = 4f;
    public LayerMask attackMask;
    public bool isInvulnerable = false;

    void Update()
    {
        //Calling the function
        //HandleMeleeAttack();
        health_BarScript.SetHealth(currentHealth);
    }
    public Animator anim;

    /*private void OnDrawGizmosSelected()
    {
        if (meleeAttackOriginEnemy != null)
        {
            Gizmos.DrawWireSphere(meleeAttackOriginEnemy.position, meleeAttackRadius);
        }
    }

   // Vector2.Distance(player.position, rb.position) <= attackRange
    private void HandleMeleeAttack()
    {
        //Making melee attack, so first we ask if we can attack by the cool down.
        if (timeUntilMeleeAttackReady <= 0)
        {
            //Making logic for attacking
            Collider2D[] overLappedColliders = Physics2D.OverlapCircleAll(meleeAttackOriginEnemy.position, meleeAttackRadius, playerLayer);
            for (int i = 0; i < overLappedColliders.Length; i++)
            {
                IDamageble playerAtributes = overLappedColliders[i].GetComponent<IDamageble>();
                if (playerAtributes != null)
                {
                    playerAtributes.ApplyDamage(meleeDamage);
                }
            }
            timeUntilMeleeAttackReady = meleeAttackDelay;

        }
        else
        {
            timeUntilMeleeAttackReady -= Time.deltaTime;
        }
    }
    //Funtion for damage done to the enemy.
    public virtual void ApplyDamage(float amount)
    {
        CurrentHealth -= amount;
        anim.SetTrigger("Hurt");
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }*/

    public virtual void ApplyDamage(float amount)
    {
        if (isInvulnerable)
        {
            return;
        }

        CurrentHealth -= amount;
        anim.SetTrigger("Hurt");
        if (currentHealth < 70)
        {
            GetComponent<Animator>().SetBool("isInRaged", true); 
        }
        if (CurrentHealth <= 0)
        {
            currentHealth = 0;
            health_BarScript.slider.value = 0;
            Die();
        }
    }


    public void Attack()
    {
        //Vector3 pos = transform.position;
        //pos += transform.right * attackOffset.x;
        //pos += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(meleeAttackOriginEnemy.position, attackRange, playerLayer);
        if (colInfo != null)
        {
            colInfo.GetComponent<Player>().ApplyDamage(attackDamage);
        }
    }

    public void EnragedAttack()
    {
        //Vector3 pos = transform.position;
        //pos += transform.right * attackOffset.x;
        //pos += transform.up * attackOffset.y;
        Collider2D colInfo = Physics2D.OverlapCircle(meleeAttackOriginEnemy.position, attackRange, playerLayer);
        if (colInfo != null)
        {
            colInfo.GetComponent<Player>().ApplyDamage(enragedAttackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        //Gizmos.DrawWireSphere(pos, attackRange);
        Gizmos.DrawWireSphere(meleeAttackOriginEnemy.position, attackRange);
    }

 

}
