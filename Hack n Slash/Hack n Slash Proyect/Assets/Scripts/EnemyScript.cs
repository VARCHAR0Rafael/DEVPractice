//Script for the enemy IA.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Character, IDamageble
{
    public Animator anim;
    //Funtion for damage done to the enemy.
    public virtual void ApplyDamage(float amount)
    {
        CurrentHealth -= amount;
        anim.SetTrigger("Hurt");
        if (CurrentHealth <= 0)
        {
            anim.SetBool("isDead", true);
            Die();
        }
    }
}
