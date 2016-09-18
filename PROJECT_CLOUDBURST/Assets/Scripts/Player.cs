using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    bool jumping, crouch, moveLeft, moveRight = false;
    bool inAir = true;

    bool jumpButtonPressed = false;

    float moveHoriz, moveVert, jump, slide, run = 0;

    protected Vector3 velocity, desiredVelocity, acceleration;
    protected Animator anim;
    protected Sprite currentSprite;
    protected Rigidbody2D rigidB;

    public float runSpeed = 5;
    public float walkSpeed = 3;
    public float moveForce = 1;
    public float jumpForce = 1;
    public float maxVelocity = 2;
    public float mass = 1;

    public Vector2 Velocity { get { return velocity; } }

    void Start()
    {
        acceleration = Vector3.zero;
        velocity = Vector3.zero;
        currentSprite = this.GetComponent<SpriteRenderer>().sprite;
        rigidB = this.GetComponent<Rigidbody2D>();
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
        //crouch = Input.GetKey(KeyCode.LeftControl);
    }

    void Move()
    {
        //runs if button down
        if(run > 0)
        {
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
            inAir = false;
        }
    }


}
