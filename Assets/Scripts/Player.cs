using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private float startingHealth = 100;

    [SerializeField]
    private float maxHealth = 100;

    //**Swapping Sides**

    private bool sideOn = true;         //true is right, false is left

    //**Swapping Sides**

    private Transform[] childrenTransform;

	// Use this for initialization
	protected override void Start ()
    {
        health.Initialized(startingHealth, maxHealth);   //player starting game with max health
        base.Start();
        childrenTransform = GetComponentsInChildren<Transform>();
    }
	
	// Update is called once per frame
	protected override void Update ()   //overrides Character.cs Update()
    {
        GetInput();
        base.Update();  //runs overridden Character.cs Update() function
        //changing side character is facing for animation use
        if (sideFacing)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (!sideFacing)
        {
            transform.localScale = new Vector3(-1, 1f, 1f);
        }
        SwitchSides();
        //Debug.Log(childrenTransform[1].position.x);
        //Debug.Log(childrenTransform[1].position.y);
    }
    private void SwitchSides()
    {
        //Changing position of the attack and wall checks
        if (sideFacing && !sideOn)
        {
            for (int i = 2; i < 5; i++)
            {
                childrenTransform[i].transform.position = childrenTransform[i].transform.parent.TransformPoint(0.142f, -0.0542f, 0);
                sideOn = true;
            }
            //Changing position of upperCutTopCheck to stay on top of the Player
            childrenTransform[5].transform.position = childrenTransform[5].transform.parent.TransformPoint(-0.05f, 0.248f, 0);

            //Changing position of groundCheck to stay below the Player
            childrenTransform[1].transform.position = childrenTransform[1].transform.parent.TransformPoint(-0.05f, -0.276f, 0);
        }
        else if (!sideFacing && sideOn)
        {
            for (int i = 2; i < 5; i++)
            {
                childrenTransform[i].transform.position = childrenTransform[i].transform.parent.TransformPoint(0.287f, -0.0542f, 0);
                sideOn = false;
            }
            //Changing position of upperCutTopCheck to stay on top of the Player
            childrenTransform[5].transform.position = childrenTransform[5].transform.parent.TransformPoint(0.074f, 0.248f, 0);

            //Changing position of groundCheck to stay below the Player
            childrenTransform[1].transform.position = childrenTransform[1].transform.parent.TransformPoint(0.074f, -0.276f, 0);
        }
    }
    private void GetInput()     //key inputs from player
    {
        if (canMove)
        {
            // Move Left/Right
            if (InputManager.MainHorizontal() < 0)
            {
                Move();
                movingLeft = true;
                this.sideFacing = false;
            }
            else
            {
                movingLeft = false;
            }
            if (InputManager.MainHorizontal() > 0)
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
            if (InputManager.Jump())
            {
                this.makeJump = true;
            }

            // Fast Punch
            if (InputManager.Punch())
            {
                this.fastPunch = true;
            }

            // Round Kick
            if (InputManager.Kick())
            {
                this.roundKick = true;
            }
            // Upercut
            if (InputManager.SuperUppercut())
            {
                uppercut = true;
            }
        }
        

        //FOR DEBUGGING ONLY
        //
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
        }
        if (Input.GetKeyDown(KeyCode.P) && isDead == true)
        {
            health.MyCurrentValue += 100;
            isDead = false;
            ResetLayers();
        }
        //
        //
    }
}
