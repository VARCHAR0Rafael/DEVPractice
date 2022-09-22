//Script for the enemy IA.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : Character, IDamageble
{
    //Funtion for damage done to the enemy.
    public virtual void ApplyDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
}
