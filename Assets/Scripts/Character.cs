using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //**Movement Mechanics**
    [SerializeField]
    private float speed = 300f;
    [SerializeField]
    protected bool sideFacing = true;   //true == right false == left
    protected Vector2 direction;
    protected bool canMove = true;
    protected bool movingLeft;
    protected bool movingRight;
    //**Movement Mechanics**

    private Animator animator;
    private SpriteRenderer spriteRend;

    // ***Attack Mechanics***

    protected bool fastPunch;
    protected bool roundKick;
    protected bool uppercut;

    protected bool playerHit;
    protected bool enemyHit;

    [SerializeField]
    private Transform fastPunchCheck;
    [SerializeField]
    private Transform upperCutSideCheck;
    [SerializeField]
    private Transform upperCutTopCheck;

    private float fastPunchRadius = 0.01f;
    private float upperCutRadius = 0.01f;

    [SerializeField]
    private LayerMask theOpponent;

    // ***Attack Mechanics***

    //**Jumping Mechanics**
    [SerializeField]
    private float jumpForce = 200f;

    protected bool makeJump;
    private Rigidbody2D rigBody;
    [SerializeField]
    private Rigidbody2D oppontRigBody;
    [SerializeField]
    private LayerMask theGround;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private float groundCheckRadius = 0.1f;
    [SerializeField]
    private bool onTheGround = false;
    [SerializeField]
    private bool doubleJumpReady = true;
    private float jumpRecharge = 1.0f;
    //**Jumping Mechanics**

    //**Death Mechanics**
    [SerializeField]
    protected Stat health;
    [SerializeField]
    protected bool isDead;
    [SerializeField]
    private Sprite alivePic;
    [SerializeField]
    private Sprite deathPic;
    private float deathAnimationTime = 0.4f;
    //**Death Mechanics**

    // Use this for initialization
    protected virtual void Start ()
    {
        animator = GetComponent<Animator>();    //creates reference to Animator Component
        rigBody = GetComponent<Rigidbody2D>();
        spriteRend = GetComponent<SpriteRenderer>();
        //Debug.Log(direction.x);
    }
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        animator.SetBool("sideFacing", sideFacing);
        Move();
        CheckForAttack();
        CheckForDeath();
        if (!doubleJumpReady && onTheGround)
        {
            jumpRecharge -= Time.deltaTime;
            if (jumpRecharge <= 0.0f)
            {
                doubleJumpReady = true;
                ResetLayers();
            }
        }
        //Debug.Log(jumpCount);
    }
    void FixedUpdate()
    {
        if ((Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, theGround)) || (Physics2D.OverlapCircle(wallCheck.position, groundCheckRadius, theGround)))
        {
            onTheGround = true;
        }
        else
        {
            onTheGround = false;
        }
        if (Physics2D.OverlapCircle(wallCheck.position, groundCheckRadius, theGround))
        {
            animator.SetBool("onTheWall", true);
        }
        else
        {
            animator.SetBool("onTheWall", false);
        }
    }
    public void Move()
    {
        //rigBody.AddForce(direction * speed * Time.deltaTime);
        this.transform.Translate(direction * speed * Time.deltaTime);
        if (makeJump)
        {
            CreateJump();
        }
        if (movingLeft)
        {
            sideFacing = false;
            rigBody.velocity = new Vector2(-2, rigBody.velocity.y);
            if (onTheGround)
            {
                animator.SetBool("walk", true);
            }
        }
        else if (movingRight)
        {
            sideFacing = true;
            rigBody.velocity = (new Vector2(2, rigBody.velocity.y));
            if (onTheGround)
            {
                animator.SetBool("walk", true);
            }
        }
        else if (!movingRight && !movingLeft)
        {
            animator.SetBool("walk", false);
        }
    }
    public void ResetLayers()
    {
        for (int i = 1; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }
    }
    public void CreateJump()
    {
        if (onTheGround)
        {
            rigBody.AddForce(new Vector2(0, jumpForce));
            makeJump = false;
        }
        else if (!onTheGround && doubleJumpReady)
        {
            rigBody.AddForce(new Vector2(0, jumpForce));
            animator.SetTrigger("doubleJump");
            makeJump = false;
            doubleJumpReady = false;
        }
        
        //GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpForce); //different jump option
    }
    public void CheckForAttack()
    {
        //place all attack animations here
        if (fastPunch)
        {
            animator.SetTrigger("fastPunch");
            //connect hit with enemy and apply damage/knockback here
            //add knockback multiplier depending on opponent current health
            //
            if (Physics2D.OverlapCircle(fastPunchCheck.position, fastPunchRadius, theOpponent))
            {
                if (sideFacing)
                {
                    oppontRigBody.AddForce(new Vector2(100, 50));
                }
                else
                {
                    oppontRigBody.AddForce(new Vector2(-100, 50));
                }
            }
            
            fastPunch = false;
        }
        if (roundKick)
        {
            animator.SetTrigger("roundKick");
            roundKick = false;
        }
        if (uppercut)
        {
            animator.SetTrigger("uppercut");
            if ((Physics2D.OverlapCircle(upperCutSideCheck.position, upperCutRadius, theOpponent) || (Physics2D.OverlapCircle(upperCutTopCheck.position, upperCutRadius, theOpponent))))
            {
                oppontRigBody.AddForce(new Vector2(0, 300));
            }
            uppercut = false;
        }
    }
    public void CheckForDeath()
    {
        if (health.MyCurrentValue == 0)
        {
            animator.SetBool("death", true);
            deathAnimationTime -= Time.deltaTime;
            spriteRend.sprite = deathPic;
            isDead = true;
            canMove = false;

            if (deathAnimationTime <= 0)
            {
                animator.enabled = false;
            }
        }
        else if (isDead && health.MyCurrentValue > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = alivePic;
            deathAnimationTime = 0.4f;
            animator.SetBool("death", false);
            animator.enabled = true;
            isDead = false;
            canMove = true;
        }
    }
}
