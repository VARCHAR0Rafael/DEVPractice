using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Attributes")]
    //Variables
    public float healthPoints = 10f;

    [Header("Movement")]
    public float speed = 5f;
    public float jumpForce = 6f;
    public float groundedLeeway = 0.1f;

    private Rigidbody2D rb2D = null;
    private Animator animator = null;
    private float currentHealth = 5f;

    //Funtion for reference on the other classes using get and set for each object, whre using POO.
    public Rigidbody2D Rb2D
    {
        get { return rb2D; }
        protected set { rb2D = value; }

    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        protected set { currentHealth = value; }

    }

    public Animator Animator
    {
        get { return animator; }
        protected set { animator = value; }
    }



    // Start is called before the first frame update
    void Awake()
    {
        //Component moved from player script for the rigid body
        if (GetComponent<Rigidbody2D>())
        {
            rb2D = GetComponent<Rigidbody2D>();
        }
        //same process buth, this time for tha animator
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }

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
