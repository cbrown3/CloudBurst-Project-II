using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    //attribute for target
    public Transform target;
    public float distance;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(target.position.x, target.position.y, distance);
	}
}
