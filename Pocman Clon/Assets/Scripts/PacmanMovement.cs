using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour {

    public float speed = 0.4f;

    Vector2 destination = Vector2.zero;




	// Use this for initialization
	void Start () {
        destination = this.transform.position;

	}
	
	// Update is called once per frame
	void FixedUpdate () {


        if (!GameManager.sharedInstance.gamePaused && GameManager.sharedInstance.gameStarted)
        {
            this.GetComponent<Rigidbody2D>().isKinematic = false;

            Vector2 newPos = Vector2.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);

            GetComponent<Rigidbody2D>().MovePosition(newPos);


            float distanceToDestination = Vector2.Distance((Vector2)this.transform.position, destination);

            if (distanceToDestination < 2.0f)
            {
                if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && CanMoveTo(Vector2.up))
                {
                    destination = (Vector2)this.transform.position + Vector2.up;
                }
                if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && CanMoveTo(Vector2.right))
                {
                    destination = (Vector2)this.transform.position + Vector2.right;
                }
                if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && CanMoveTo(Vector2.down))
                {
                    destination = (Vector2)this.transform.position + Vector2.down;
                }
                if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && CanMoveTo(Vector2.left))
                {
                    destination = (Vector2)this.transform.position + Vector2.left;
                }
            }
            Vector2 dir = destination - (Vector2)this.transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            GetComponent<Animator>().SetFloat("DirY", dir.y);
            GetComponent<AudioSource>().volume = 0.2f;
        }
        else
        {
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<AudioSource>().volume = 0.0f;
        }
    }
    //posible direccion, devuevle true si podemos ir en dicha direccion o false si nos impide avanzar
    bool CanMoveTo(Vector2 dir)
    {

        Vector2 pacmanPos = this.transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pacmanPos+dir, pacmanPos);


        Debug.DrawLine(pacmanPos, pacmanPos+dir);

        Collider2D pacmanCollider = GetComponent<Collider2D>();
        Collider2D hitCollider = hit.collider;


        return hit.collider == pacmanCollider;

    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag == "fruit")
        {
            GameManager.sharedInstance.MakeInvincibleFor(15.0f);
            UIManager.sharedInstance.ScorePoints(100);
            Destroy(otherCollider.gameObject);
        }
    }
}
