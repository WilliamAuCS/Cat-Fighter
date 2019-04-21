using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {
    [SerializeField]
    private float startingHealth = 100;

    [SerializeField]
    private float maxHealth = 100;

    // Use this for initialization
    protected override void Start()
    {
        health.Initialized(startingHealth, maxHealth);   //player starting game with max health
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()   //overrides Character.cs Update()
    {
        GetInput();
        base.Update();  //runs overridden Character.cs Update() function
        if (sideFacing)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (!sideFacing)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    public void ifHit()
    {
        playerHit = true;
    }

    private void GetInput()     //key inputs from player
    {
        if (canMove)
        {
            direction = Vector2.zero;               //zeroing so movement does not get larger exponentionally

            // Move Left/Right
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Move();
                movingLeft = true;
                this.sideFacing = false;
            }
            else
            {
                movingLeft = false;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Move();
                movingRight = true;
                this.sideFacing = true;
            }
            else
            {
                movingRight = false;
            }

            // Jump
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                this.makeJump = true;
            }

            // Fast Punch
            if (Input.GetKeyDown(KeyCode.Comma))
            {
                this.fastPunch = true;
            }

            // Round Kick
            if (Input.GetKeyDown(KeyCode.Period))
            {
                this.roundKick = true;
            }

        }

        //FOR DEBUGGING ONLY
        //
        /*if (Input.GetKeyDown(KeyCode.I)) {
            health.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            health.MyCurrentValue += 10;
        }
        if (Input.GetKeyDown(KeyCode.P) && isDead == true) {
            health.MyCurrentValue += 100;
            isDead = false;
            ResetLayers();
        }*/
        //
        //
    }
}
