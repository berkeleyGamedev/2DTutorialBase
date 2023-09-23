using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisible : Enemy
{
    private Color col;
    private SpriteRenderer character;
    private float maxHP;
    public Enemy invEnemy;
    private float currHP;

    // Start is called before the first frame update
    private void Start()
    {
        character = GetComponent<SpriteRenderer>();
        
        
        col = character.color;
        maxHP = invEnemy.maxHealth;
        currHP = maxHP;
     
    }

    private void Update()
    {
        if (hitten())
        {

            col = new Color(1f, 1f, 1f, 0.5f);

        }
    }

    private bool hitten()
    {
        if (currHP != maxHP)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
