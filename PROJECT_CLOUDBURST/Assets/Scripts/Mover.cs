using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    protected Vector3 velocity, desiredVelocity, acceleration;
    protected Animator anim;
    protected Sprite currentSprite;

    public float maxVelocity = 2;
    public float mass = 1;

    public Vector2 Velocity { get { return velocity; } }
    
    // Use this for initialization
	virtual public void Start () {
        acceleration = Vector3.zero;
        velocity = Vector3.zero;
        currentSprite = this.GetComponent<SpriteRenderer>().sprite;
	}
	
	// Update is called once per frame
	virtual public void Update () {
        //change sprites here?  In player?
        Move();
	}

    //stock move method for all movers
    void Move()
    {
        velocity += acceleration * Time.deltaTime;
        Vector3.ClampMagnitude(velocity, maxVelocity);
        this.transform.forward = velocity.normalized;
        this.transform.position += velocity * Time.deltaTime;
    }

    //applies forces to the mover
    protected void ApplyForce(Vector3 forceToApply)
    {
        acceleration += (forceToApply / mass);
    }


}
