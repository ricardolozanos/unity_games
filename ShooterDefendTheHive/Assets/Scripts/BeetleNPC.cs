using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BeetleNPC : MonoBehaviour {

    Animator m_Animator;
    public GameObject nextCucumberToDestroy;
    public bool hasReachedThePlayer = false;

    //me atacan? me defiendo
    public bool cherryHit = false;
    public float smoothTime = 3.0f;
    public Vector3 smoothVelocity = Vector3.zero;

    public HealthManager healthManager;

    private static BeetleManager beetleManager;


    private void Start()
    {

        m_Animator = GetComponent<Animator>();
        if (beetleManager == null)
        {
            beetleManager = GameObject.Find("BeetlesValue").GetComponent<BeetleManager>();
        }
        healthManager = GameObject.Find("Health Slider").GetComponent<HealthManager>();

        beetleManager.RecalculateBeetles();
    }

    private void Update()
    {
        
        if (cherryHit )
        {

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Transform transPlayer = player.transform;
            this.gameObject.transform.LookAt(transform);
            if (!hasReachedThePlayer)
            {
                m_Animator.Play("Run_Standing");
            }
            transform.position = Vector3.SmoothDamp(transform.position,
                                                        transPlayer.position,
                                                        ref smoothVelocity,
                                                        smoothTime);
            var rotationAngle = Quaternion.LookRotation(transPlayer.position - this.transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation,
                                    rotationAngle,
                                    smoothTime) * Quaternion.Euler(0, -90, 0);


            /*var rotationAngle = Quaternion.LookRotation(transPlayer.position - this.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                rotationAngle,
                                smoothTime)*Quaternion.Euler(0,-90,0);*/

        }
       
    }


    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {

            hasReachedThePlayer = true;

            healthManager.ReduceHealth();


            if (!cherryHit)
            {


                BeetlePatrol.isAttacking = true;
                GameObject thePlayer = collision.gameObject;
                Transform trans = thePlayer.transform;
                this.gameObject.transform.LookAt(trans);

                m_Animator.Play("Attack_OnGround");
                var rotationAngle = Quaternion.LookRotation(trans.position - this.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                    rotationAngle,
                                    smoothTime);
                

                StartCoroutine(DestroyBeetle());
            }
            else if (cherryHit)
            {
                m_Animator.Play("Attack_Standing");
                BeetlePatrol.isAttacking = true;
                GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
                Transform trans = thePlayer.transform;
                this.gameObject.transform.LookAt(trans);
                var rotationAngle = Quaternion.LookRotation(trans.position - this.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                    rotationAngle,
                                    smoothTime);
                
                StartCoroutine(DestroyBeetleStanding());
            }
            
        }

       
    }

    private void OnTriggerEnter(Collider otherCollider)
    {

        if (otherCollider.gameObject.tag == "Cucumber")//food
        {
            nextCucumberToDestroy = otherCollider.gameObject;
            BeetlePatrol.isEating = true;
            m_Animator.Play("Eat_OnGround");
            StartCoroutine(DestroyCucumber());
        }
        if(otherCollider.gameObject.tag == "Cherry")//bullet
        {

            PointsManager.AddPoints(30);

            BeetlePatrol.isAttacking = true;
            cherryHit = true;
            m_Animator.Play("Stand");
            


            /*var rotationAngle = Quaternion.LookRotation(transPlayer.position - this.transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                rotationAngle,
                                smoothTime)*Quaternion.Euler(0,-90,0);*/

        }
    }

   /* IEnumerator Wait(float Sec)
    {
        yield return new WaitForSeconds(Sec);
    }Wait(3.0f);*/

    IEnumerator DestroyCucumber()
    {
        PointsManager.AddPoints(-5);
        yield return new WaitForSeconds(3.0f);
        Destroy(nextCucumberToDestroy.transform.parent.gameObject);
        BeetlePatrol.isEating = false;
    }
    
    IEnumerator DestroyBeetle()
    {
        PointsManager.AddPoints(50);
        yield return new WaitForSecondsRealtime(5.0f);
        m_Animator.Play("Die_OnGround");
        Destroy(this.gameObject, 2.0f);
        hasReachedThePlayer = false;

        beetleManager.RecalculateBeetles();
    }

    IEnumerator DestroyBeetleStanding()
    {
        PointsManager.AddPoints(50);
        yield return new WaitForSeconds(5.0f);
        m_Animator.Play("Die_Standing");
        Destroy(this.gameObject, 2.0f);
        cherryHit = false;
        hasReachedThePlayer = false;

        beetleManager.RecalculateBeetles();
    }
    


}
