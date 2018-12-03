using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    #region movement_variables
    public float movespeed;
    #endregion

    #region targeting_variables
    public Transform player;
    #endregion

    #region attack_variables
    public float explosionDamage;
    public float explosionRadius;
    public GameObject explosionObj;
    #endregion

    #region health_variables
    public float maxHealth;
    float currHealth;
    #endregion

    #region physics_components
    Rigidbody2D enemyRB;
    #endregion

    #region Unity_functions
    //Runs once when object is created
    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        currHealth = maxHealth;
    }

    //Runs every frame
    private void Update()
    {
        //If we don't know where the player is, don't do anything
        if (player == null)
        {
            return;
        }

        Move();
    }
    #endregion

    #region movement_functions
    //Moves toward the player
    void Move()
    {
        //Figure out correct movement vector. Player_Position - Current_Position = Direction of player relative to self
        Vector2 direction = player.position - transform.position;

        enemyRB.velocity = direction.normalized * movespeed;
    }
    #endregion

    #region explosion_functions
    //Explosion deals damage in a radius of length 'explosionRadius' and affects all players
    void Explode()
    {
        //Call AudioManager to play explosion sound
        FindObjectOfType<AudioManager>().Play("Explosion");

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                hit.transform.GetComponent<PlayerController>().TakeDamage(explosionDamage);

                Debug.Log("Hit Player with explosion");
                Instantiate(explosionObj,transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }

    }


    //If Enemy Collides with Player, Explode
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.CompareTag("Player"))
        {
            Explode();
        }
    }
    #endregion

    #region health_functions

    //Enemy Takes Damage based on 'value' which is passed by caller
    public void TakeDamage(float value)
    {
        FindObjectOfType<AudioManager>().Play("BatHurt");
        //Decrement health
        currHealth -= value;
        Debug.Log("Enemy Health is now" + currHealth.ToString());

        //Change UI

        //Trigger other Sounds/animations etc.

        //Check for death
        if (currHealth <= 0)
        {
            Die();
        }
    }

    //Destroys the enemy object
    void Die()
    {
        //Trigger anything we need


        //Destroy Gameobject
        Destroy(this.gameObject);
    }

    #endregion



}
