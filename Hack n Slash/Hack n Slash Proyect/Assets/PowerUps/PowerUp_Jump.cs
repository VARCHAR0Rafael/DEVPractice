using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Jump : Powerups
{
    [Header("Settings")]

    public float amount = 10f;
    public float duration = 2f;

    public override void Activate()
    {
        Player.jumpForce += amount;
        Debug.Log("Player: " + Player.jumpForce);
        base.Activate();
    }





}