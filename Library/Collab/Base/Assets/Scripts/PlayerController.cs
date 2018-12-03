using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    #region movement_variables
    public float movespeed;
    float x_input;
    float y_input;
    #endregion

    #region attack_variables
    public float damage;
    public float attackspeed;
    float attackTimer;
    public float hitboxTiming;
    bool isAttacking;
    Vector2 currDirection;
    #endregion

    #region health_variables
    public float maxHealth;
    float currHealth;
    public Slider hpSlider;
    #endregion

    #region physics_components
    Rigidbody2D playerRB;
    #endregion

    #region Unity_functions
    //Called once on creation
    private void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();

        attackTimer = 0;

        currHealth = maxHealth;

        hpSlider.value = currHealth / maxHealth;
    }

    //Called every frame
    private void Update()
    {
        //Essentially freezes all controls for duration of attack
        if (isAttacking)
        {
            return;
        }

        //Get input from user WASD keys
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");

        Move();

        //Check for attack input
        if (Input.GetKeyDown(KeyCode.J) && attackTimer <= 0)
        {
            Attack();
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Interact();
        }
    }
    #endregion

    #region movement_functions
    //Moves the player based on WASD inputs and 'movespeed'
    private void Move()
    {
        //if Player is pressing 'D'
        if (x_input > 0)
        {
            playerRB.velocity = Vector2.right * movespeed;
            currDirection = Vector2.right;
        }
        //if Player is pressing 'A'
        else if (x_input < 0)
        {
            playerRB.velocity = Vector2.left * movespeed;
            currDirection = Vector2.left;
        }
        //if Player is pressing 'W'
        else if (y_input > 0)
        {
            playerRB.velocity = Vector2.up * movespeed;
            currDirection = Vector2.up;
        }
        //if Player is pressing 'S'
        else if (y_input < 0)
        {
            playerRB.velocity = Vector2.down * movespeed;
            currDirection = Vector2.down;
        }
        else
        {
            playerRB.velocity = new Vector2(0, 0);
        }
    }
    #endregion

    #region attack_functions

    //Attacks in the direction that the player is facing
    private void Attack()
    {

        //Check to make sure attack is getting called when pressing 'J'
        Debug.Log("Attacking now");

        //Do attack animation and calculate hitboxes
        StartCoroutine(AttackRoutine());

        //Set timer until next attack
        attackTimer = attackspeed;


    }

    //Handle animations and hitboxes for attack mechanism
    IEnumerator AttackRoutine()
    {

        //Pause movement and freeze player duration of attack
        isAttacking = true;
        playerRB.velocity = Vector2.zero;

        //TODO: Start Animation

        //Create hitbox when animation is at certain stage
        yield return new WaitForSeconds(hitboxTiming);

        Debug.Log("Cast hitbox now");

        RaycastHit2D[] hits = Physics2D.BoxCastAll(playerRB.position + currDirection, new Vector2(1, 1), 0f, Vector2.zero, 0);
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Enemy"))
            {
                hit.transform.GetComponent<Enemy>().TakeDamage(damage);
                Debug.Log(transform.name + " dealt " + damage + " damage ");
            }
        }
        isAttacking = false;
    }
    #endregion

    #region health_functions

    //Takes damage based on 'value' parameter, which is passed by caller
    public void TakeDamage(float value)
    {
        //Decrement health
        currHealth -= value;
        Debug.Log("Health is now" + currHealth.ToString());

        //Change UI
        hpSlider.value = currHealth / maxHealth;

        //Trigger other Sounds/animations etc.

        //Check for death
        if (currHealth <= 0)
        {
            Die();
        }
    }

    //Heals player health based on 'value' parameter, passed by caller
    public void Heal(float value)
    {
        //Increment Health
        currHealth += value;
        Debug.Log("Health is now" + currHealth.ToString());

        //Change UI
        hpSlider.value = currHealth / maxHealth;

        //Trigger other Sounds/animations etc.
    }

    //Destroys Player Object and triggers end scene stuff
    void Die()
    {
        //Trigger anything we need to end the game


        //Destroy Gameobject
        Destroy(this.gameObject);
    }

    #endregion

    #region interact_functions

    //Raycast in front of player to open chests or interact with other objects
    void Interact() 
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(playerRB.position + currDirection, new Vector2(.5f, .5f), 0f, Vector2.zero, 0);
        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.CompareTag("Chest"))
            {
                hit.transform.GetComponent<Chest>().Interact();
                Debug.Log(transform.name + " interacted with Chest");
            }
        }
    }  
    #endregion
}
