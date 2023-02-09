using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorUnlock_Script : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            FindObjectOfType<AudioManager>().Stop("DOOM");
            FindObjectOfType<AudioManager>().Play("DMC5");
            Destroy(gameObject);
        }
    }
}
