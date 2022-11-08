using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp_Speed : Powerups
{
    [Header("Settings")]

    public float amount = 8f;
    public float duration = 10f;
    public GameObject pickUpEffect;

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
        playerstats.speed += amount;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(duration);

        playerstats.speed -= amount;

        Destroy(gameObject);
    }
}
