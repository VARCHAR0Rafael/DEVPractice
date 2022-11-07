using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Heal : Powerups
{
    [Header("Settings")]

    public float amount = 5f;

    public override void Activate()
    {
        if (Player.currentHealth < Player.healthPoints)
        {
            Player.CurrentHealth += amount;
            if (Player.currentHealth > Player.healthPoints)
            {
                Player.currentHealth = Player.healthPoints;
            }
            Debug.Log("Player: " + Player.CurrentHealth);
            base.Activate();
        }
    }
}
