using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    //Variables.
    public Rigidbody2D rb2d = null;
    public float projectileSpeed = 15f;
    public float projectileDamage = 1f;
    public float delaySeconds = 3f;
    private WaitForSeconds cullDelay = null;
    // Start is called before the first frame update
    void Start()
    {
        //Initialized the culldelay
        cullDelay = new WaitForSeconds(delaySeconds);
        StartCoroutine(DelaydeCull());

        //Making the animation and logic for the projectile to move.
        rb2d.velocity = transform.right * projectileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Checking if projectile hits the enemy and if it does then make damage and destroy projectile.
        if (collider.gameObject.layer == 8)//equal to 8 becuase thats the enemy layer.
        {
            IDamageble enemyAtributes = collider.GetComponent<IDamageble>();
            if (enemyAtributes != null)
            {
                enemyAtributes.ApplyDamage(projectileDamage);
            }

            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    //Preventing projectiles that missed to fly around for etrnity.
    private IEnumerator DelaydeCull()
    {
        yield return cullDelay;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
