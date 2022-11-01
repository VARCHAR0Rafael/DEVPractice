using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Jump : MonoBehaviour
{
    [Header("Settings")]

    public float amount = 10f;
    public float duration = 2f;
    public GameObject pickUpEffect;

    /* public override void Activate()
     {
         Player.jumpForce += amount;
         Debug.Log("Player: " + Player.jumpForce);
         base.Activate();
     }*/

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        Instantiate(pickUpEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}