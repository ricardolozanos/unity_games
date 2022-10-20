using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public string runState, walkState, idleState, jumpState, throwState, dieState;

    bool  isWalking, isRunning, isJumping, isIdle,  isDead, forward, backward, left, right;
    Animator m_Animator;

    public AudioClip jumpClip, throwClip;

    // Use this for initialization
    void Start () {
        m_Animator = GetComponent<Animator>();
        isIdle = true;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!isRunning)
            {
                forward = true;
                Walk();
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!isRunning)
            {
                left = true;
                Walk();
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!isRunning)
            {
                backward = true;
                Walk();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!isRunning)
            {
                right = true;
                Walk();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isWalking)
            {
                isRunning = true;
                isWalking = false;
                forward = true;
                m_Animator.SetBool(runState, true);
                m_Animator.SetBool(idleState, false);
                m_Animator.SetBool(walkState, false);
            }
        }

        //Acciones

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            Throw();
        }

        //actionUp
        if (Input.GetKeyUp(KeyCode.W))
        {
           
            forward = false;
            if (!left && !right && !backward)
            {
                isWalking = false;
                isIdle = true;
                isRunning = false;
                m_Animator.SetBool(idleState, true);
                m_Animator.SetBool(walkState, false);
                m_Animator.SetBool(runState, false);
            }
        }
       

        if (Input.GetKeyUp(KeyCode.A))
        {
            left = false;
            if (!forward && !right && !backward)
            {
                StopMotion();
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            backward = false;
            if (!left && !right && !forward)
            {
                StopMotion();
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            right = false;
            if (!left && !forward && !backward)
            {
                StopMotion();
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            m_Animator.SetBool(runState, false);
            
            if (!isRunning && isWalking)
            {
                
                m_Animator.SetBool(walkState, true);               
            }

        }



        if (PlayerManager.livesRemaining == 0)
        {
            m_Animator.Play("CM_Die");
        }

     }

            void StopMotion()
            {
                isWalking = false;
                isIdle = true;
                isRunning = false;
                m_Animator.SetBool(idleState, true);
                m_Animator.SetBool(walkState, false);
                m_Animator.SetBool(runState, false);
            }

        void Jump()
        {
            m_Animator.SetBool(jumpState, true);
            m_Animator.SetBool(idleState, false);
            m_Animator.SetBool(walkState, false);
            m_Animator.SetBool(runState, false);
                StartCoroutine(ConsumeJump());
        }
        void Throw()
        {
            m_Animator.SetBool(throwState, true);
            m_Animator.SetBool(idleState, false);
            m_Animator.SetBool(walkState, false);
            m_Animator.SetBool(runState, false);
                StartCoroutine(ConsumeThrow());
        }

        IEnumerator ConsumeJump()
        {
            
             AudioSource audioSource = GetComponent<AudioSource>();
             audioSource.PlayOneShot(jumpClip);
            yield return new WaitForSeconds(1.00f);
            m_Animator.SetBool(jumpState, false);
            ReturnToMoveState();
        }

        IEnumerator ConsumeThrow()
        {
            
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(throwClip);
            yield return new WaitForSeconds(0.66f);
            m_Animator.SetBool(throwState, false);

            ReturnToMoveState();
        
        }

         void ReturnToMoveState()
        {
            if (isRunning)
            {
                m_Animator.SetBool(runState, true);
            }
            if (isWalking)
            {
                m_Animator.SetBool(walkState, true);
            }
            if (isIdle)
            {
                m_Animator.SetBool(idleState, true);
            }
        }

        void Walk()
        {
            isWalking = true;
            isIdle = false;
            m_Animator.SetBool(walkState, true);
            m_Animator.SetBool(idleState, false);
        }
    
}
