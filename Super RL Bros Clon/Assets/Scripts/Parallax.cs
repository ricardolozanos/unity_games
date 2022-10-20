using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public float speed = 0.0f;

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        this.rigidbody.velocity = new Vector2(speed, 0);


        float parentPosition = this.transform.parent.transform.position.x;

        if(this.transform.position.x -parentPosition >= 51.3f)
        {
            this.transform.position = new Vector3(parentPosition -42.7f, this.transform.position.y, this.transform.position.z);
        }
	}
}
