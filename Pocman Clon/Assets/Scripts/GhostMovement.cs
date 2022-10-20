using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour {


    public bool shouldWaitHome = false;

    public Transform[] waypoints;
    int currentWayPoint = 0;

    public float speed = 1f;

    private void Update()
    {
        if (GameManager.sharedInstance.invincibleTime > 5)
        {
            GetComponent<Animator>().SetBool("vuln1", true);
            GetComponent<Animator>().SetFloat("DirX", 0f);
            GetComponent<Animator>().SetFloat("DirY", 0f);



        }
        else if (GameManager.sharedInstance.invincibleTime > 0.5 && GameManager.sharedInstance.invincibleTime < 5)
        {

            GetComponent<Animator>().SetBool("vuln1", false);
            GetComponent<Animator>().SetBool("vuln2", true);


        }
        else if (GameManager.sharedInstance.invincibleTime < 0.5 && GameManager.sharedInstance.invincibleTime > 0.1)
        {
            GetComponent<Animator>().SetFloat("DirY", -0.2f);
            GetComponent<Animator>().SetBool("vuln2", false);
        }
    }

    private void FixedUpdate()
    {

        if (!GameManager.sharedInstance.gamePaused && GameManager.sharedInstance.gameStarted)
        {
            if (!shouldWaitHome)
            {
                float distanceToWaypoint = Vector2.Distance((Vector2)this.transform.position,
                                                         (Vector2)waypoints[currentWayPoint].position);
                if (distanceToWaypoint < 0.1f)
                {
                    currentWayPoint = (currentWayPoint + 1) % waypoints.Length;
                    Vector2 newDirection = waypoints[currentWayPoint].position - this.transform.position;
                    if (GameManager.sharedInstance.invincibleTime <= 0)
                    {


                        GetComponent<Animator>().SetFloat("DirX", newDirection.x);
                        GetComponent<Animator>().SetFloat("DirY", newDirection.y);
                    }

                }
                else
                {
                    Vector2 newPos = Vector2.MoveTowards(this.transform.position,
                                                         waypoints[currentWayPoint].position,
                                                        speed * Time.deltaTime);
                    GetComponent<Rigidbody2D>().MovePosition(newPos);
                }
                GetComponent<AudioSource>().volume = 0.2f;
            }
        }
        else
        {
            GetComponent<AudioSource>().volume = 0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag== "pacman" && GameManager.sharedInstance.invincibleTime <= 0)
        {

            GameManager.sharedInstance.gameStarted = false;

            Destroy(otherCollider.gameObject);


            StartCoroutine("RestartGame");

        }
        else if (otherCollider.tag == "pacman" && GameManager.sharedInstance.invincibleTime > 0)
        {
            UIManager.sharedInstance.ScorePoints(500);
            GameObject home = GameObject.Find("GhostHome");
            this.transform.position = home.transform.position;

            this.currentWayPoint=0;
            this.shouldWaitHome = true;
            StartCoroutine("homeAwake");
        }
    }
    IEnumerator homeAwake()
    {
        yield return new WaitForSeconds(3f);
        this.shouldWaitHome = false;
        speed *= 1.2f;
    }
    IEnumerator RestartGame()
    {

        yield return new WaitForSeconds(3f);
        GameManager.sharedInstance.RestartGame();
    }
}
