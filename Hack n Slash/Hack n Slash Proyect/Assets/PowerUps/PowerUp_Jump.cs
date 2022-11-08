using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Jump : Powerups
{
    [Header("Settings")]

    public float amount = 10f;
    public float duration = 10f;
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
            StartCoroutine(PickUp(collision));
        }
    }

    IEnumerator PickUp(Collider2D player)
    {

        //Instantiate(pickUpEffect, transform.position, transform.rotation);
        Player playerstats = player.GetComponent<Player>();
        playerstats.jumpForce += amount;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(duration);

        playerstats.jumpForce -= amount;

        Destroy(gameObject);
    }

}