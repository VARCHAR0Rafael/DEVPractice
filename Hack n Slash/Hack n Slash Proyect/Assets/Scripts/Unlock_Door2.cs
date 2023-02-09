using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock_Door2 : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }
}
