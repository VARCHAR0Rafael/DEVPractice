using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //Variables
    public float healthPoints = 10f;
    public float speed = 5f;
    public float jumpForce = 6f;
    public float groundedLeeway = 0.1f;
    private Rigidbody2D rb2d = null;
    private float currentHealth = 5f;

    //Funtion for reference on the other classes.
    public Rigidbody2D Rb2D
    {
        get { return rb2d; }
        protected set { rb2d = value; }

    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        protected set { currentHealth = value; }

    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = healthPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Funtion to Check if the player is grounded
    protected bool Grounded()
    {
        return Physics2D.Raycast(transform.position, -Vector2.up, groundedLeeway);
    }

    //Funtion to set the enemy to died.
    protected virtual void Die()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}
