using UnityEngine;
using System.Collections;

public class Player : Mover {

    bool jump, crouch, run, slide, moveLeft, moveRight = false;


    override public void Start()
    {

    }


    // Update is called once per frame
    override public void Update()
    {
        CheckForInputs();

        if (moveLeft)
        {
            //flip the sprite
            //apply left force
            ApplyForce(new Vector3(-0.15f, 0, 0));
        }

        
    }

    //checks inputs and sets flags accordingly
    void CheckForInputs()
    {
        moveLeft = Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.D);
        run = Input.GetKey(KeyCode.LeftShift);
        jump = Input.GetKey(KeyCode.Space);
        crouch = Input.GetKey(KeyCode.LeftControl);
    }
}
