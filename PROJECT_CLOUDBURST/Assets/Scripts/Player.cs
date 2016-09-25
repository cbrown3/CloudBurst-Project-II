using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    bool jumpButtonPressed, slideCrouchButtonPressed = false;
    bool inAir = true;
    bool running, walking, crouching, sliding = false;

    float moveHoriz, moveVert, jump, run, slideCrouch = 0;

    Vector2 initialColliderSize;

    protected Vector3 velocity, desiredVelocity, acceleration;
    protected Animator anim;
    protected Sprite currentSprite;
    protected Rigidbody2D rigidB;

    public float wallFriction = 2;
    public float runSpeed = 5;
    public float walkSpeed = 3;
    public float moveForce = 1;
    public float jumpForce = 1;
    public float maxVelocity = 2;
    public float mass = 1;
    public BoxCollider2D slidingCol, notSlidingCol;

    public Vector2 Velocity { get { return velocity; } }

    void Start()
    {
        acceleration = Vector3.zero;
        velocity = Vector3.zero;
        currentSprite = this.GetComponent<SpriteRenderer>().sprite;
        rigidB = this.GetComponent<Rigidbody2D>();
        initialColliderSize = this.GetComponent<BoxCollider2D>().size;
        anim = this.GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        CheckForInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    //checks inputs and sets flags accordingly
    void CheckForInputs()
    {
        moveHoriz = Input.GetAxis("Horizontal");

        jump = Input.GetAxisRaw("Jump");

        run = Input.GetAxis("Run");

        slideCrouch = Input.GetAxisRaw("SlideCrouch");

    }

    void Move()
    {

        //runs if button down
        if(run > 0)
        {
            //insert running animation code here

            if(Mathf.Abs(rigidB.velocity.x) > runSpeed)
            {
                rigidB.AddForce(new Vector2(rigidB.velocity.x * moveForce * -0.6f, 0));
            }
        }
        else
        {
            if (Mathf.Abs(rigidB.velocity.x) > walkSpeed)
            {
                rigidB.AddForce(new Vector2(rigidB.velocity.x * moveForce * -0.6f, 0));
            }
        }

        //moves the player left and right
        if (Mathf.Abs(moveHoriz) > 0) rigidB.AddForce(new Vector3(moveHoriz * moveForce, 0, 0));


        //jump
        Jump();

        //accounts for sliding and crouching
        SlideOrCrouch();

        //flips orientation of sprite based on movement direction
        if (rigidB.velocity.x < 0) this.GetComponent<SpriteRenderer>().flipX = true;
        if (rigidB.velocity.x > 0) this.GetComponent<SpriteRenderer>().flipX = false;

        //changes animations based on state
        ChangeAnimation();

    }

    //changes the animation based on the player's current state
    void ChangeAnimation()
    {
        if(Mathf.Abs(rigidB.velocity.x) > 0)
        {
            anim.SetBool("walking", true);
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }


    //slides if moving, or crouches if not
    void SlideOrCrouch()
    {
        if (slideCrouch == 1 && !slideCrouchButtonPressed)
        {
            slideCrouchButtonPressed = true;

            //for collider
            //Vector2 currentSize = this.GetComponent<BoxCollider2D>().size;

            if (rigidB.velocity.x != 0)
            {
                //change the sprite/play animation (rotating now for placeholder)
                anim.SetBool("sliding", true);
                SetSlidingCollider(true);
                //get current offset of collider and size and "flip" them
                //this.GetComponent<BoxCollider2D>().size = new Vector2(currentSize.y, currentSize.x);
            }
            else
            {
                //change the sprite/play animation (doing nothing for now)

                //change the collider to a square (we don't want more width, but still decreased height
                //this.GetComponent<BoxCollider2D>().size = new Vector2(currentSize.x, currentSize.x);

            }
        }

        if(slideCrouch == 0)
        {
            if (slideCrouchButtonPressed)
            {
                //reset player to standing animation
                //this.GetComponent<BoxCollider2D>().size = new Vector2(initialColliderSize.x, initialColliderSize.y);

                slideCrouchButtonPressed = false;
            }

            anim.SetBool("sliding", false);
            SetSlidingCollider(false);
        }
    }

    void SetSlidingCollider(bool shouldISlide)
    {
        if(shouldISlide)
        {
            slidingCol.enabled = true;
            notSlidingCol.enabled = false;
        }
        else
        {
            slidingCol.enabled = false;
            notSlidingCol.enabled = true;
        }
    }

    //handles jumping
    void Jump()
    {
        //makes the player jump if the jump button is pressed
        //does not allow for the jump button to be held
        if (jump == 1)
        {
            //jumps if not already in air
            if (!inAir & !jumpButtonPressed)
            {
                rigidB.velocity = Vector2.zero;
                jumpButtonPressed = true;
                rigidB.AddForce(new Vector3(0, jumpForce, 0));
                inAir = true;
            }
        }
        //resets jump button flag
        if (jump == 0)
        {
            jumpButtonPressed = false;
        }
    }

    //collision detection
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall")
        {
            inAir = false;
        }
        if(col.gameObject.tag == "Terrain")
        {
            //change to regular standing animation here

            inAir = false;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall" || col.gameObject.tag == "Terrain")
        {
            //change the sprite/animation to falling(in air) here if the velocity Y is less than 0
            //else, change to the jumping animation

            inAir = true;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if(col.gameObject.tag == "Wall" && rigidB.velocity.y <= 0)
        {
            //change the sprite/animation to wall clinging here

            //add friction
            rigidB.velocity.Set(0, 0);
            rigidB.AddForce(new Vector2(0, wallFriction));
        }
    }


}
