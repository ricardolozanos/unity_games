using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    

    public static PlayerController sharedInstance;//iniciar el singleton del jugador
    //(nomas si hay un jugador)

    private Rigidbody2D rigidBody;//activa al rigid body del player

    public Animator animator;//activa el animator del player

    private Vector3 startPosition;

    //Esta variable sirve para detectar la capa del suelo
    public LayerMask groundLayer;
    private int healthPoints, manaPoints;

    public float jumpForce = 22f;
    public float runningSpeed = 8f;

    public const int INITIAL_HEALTH = 100 , INITIAL_MANA=15; 

    public const float MIN_SPEED = 2.5f,HEALTH_DECREASE_SPEED= 1.0f;
    public const int MIN_COROUTINE_HEALTH = 20;


    public const float SUPERJUMP_FORCE = 1.3f;
    public const int SUPERJUMP_COST = 5;

    public const int MAX_HEALTH=150, MAX_MANA =100;



    void Awake()
    {


        rigidBody = GetComponent<Rigidbody2D>();//mira al player y busca al rigidbody2d y lo agarra
        sharedInstance = this;//inicializacion para ser unico
        startPosition = this.transform.position;//la variable toma el valor donde empieza el personaje
    }
    


    public void StartGame () {
        animator.SetBool("isAlive", true);
        animator.SetBool("isGrounded", true);
        this.transform.position = startPosition;//colocar al personaje una vez que reiniciemos o muera

        this.healthPoints = INITIAL_HEALTH;
        this.manaPoints = INITIAL_MANA;

        StartCoroutine("TirePlayer");
    }



    IEnumerator TirePlayer()//bajar vida
    {
        while (this.healthPoints > MIN_COROUTINE_HEALTH)
        {
            this.healthPoints--;
            Debug.Log(healthPoints);
            yield return new WaitForSeconds(HEALTH_DECREASE_SPEED);
        }
        yield return null;
        
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag == "Enemy")
        {
            this.healthPoints -= 10;
        }

        
    }

    // Update is called once per frame
    void Update () {
        
       

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (healthPoints <= 0)
            {
                Kill();
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {//aqui el usuario acaba de presionar espacio
                


                if (IsTouchingTheGround())
                {
                    Jump(false);
                }

            }
            if (Input.GetMouseButtonDown(0))
            {
                if(IsTouchingTheGround())
                {
                    Jump(true);
                }
            }
            animator.SetBool("isGrounded", IsTouchingTheGround());
            //checar si esta saltando pa cambiar animacion
        }
        
        
    }
    public float truerunningSpeed;
    void FixedUpdate()
    {

        //solo nos movemos si estamos en ingame
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            
            truerunningSpeed=MIN_SPEED+(runningSpeed-MIN_SPEED) * healthPoints/ 150.0f;

            if (rigidBody.velocity.x < truerunningSpeed && Input.GetKey(KeyCode.D))// 
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                rigidBody.velocity = new Vector2(truerunningSpeed, rigidBody.velocity.y);//ejes x, ejes y 
                

            }

            if (rigidBody.velocity.x > -truerunningSpeed && Input.GetKey(KeyCode.A))//
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                rigidBody.velocity = new Vector2(-truerunningSpeed, rigidBody.velocity.y);//ejes x, ejes y 


            }
            
        }

    }
    
   

    void Jump(bool isSuperJump)
    {       // F=m*a ->>>> a=F/m

        if (isSuperJump && this.manaPoints >= SUPERJUMP_COST)
        {

            rigidBody.AddForce(Vector2.up * jumpForce*SUPERJUMP_FORCE, ForceMode2D.Impulse);
            manaPoints -= SUPERJUMP_COST;
        }
        else
        {
            
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        Debug.Log("Mana: "+manaPoints);
    }

    

   

    bool IsTouchingTheGround() //Estamos tocando suelo? True o False
    {
        if (Physics2D.Raycast(this.transform.position,  //se traza el rayo desde la posicion del jugador
                              Vector2.down,             // hacia abajo
                              0.1f,                     // hasta una distancia de 10cm
                              groundLayer))             // y nos encotnramos con la capa del suelo
        {
            return true;
        }
        else {
            return false; }
    }

    public void Kill()
    {
        GameManager.sharedInstance.GameOver();
        this.animator.SetBool("isAlive", false);


        float currentMaxScore = PlayerPrefs.GetFloat("maxScore", 0);

        if(currentMaxScore< this.GetDistance())
        {
            PlayerPrefs.SetFloat("maxScore", this.GetDistance());
        }
        StopCoroutine("TirePlayer");
    }
    

    public float GetDistance()
    {
        float travelledDistance = Vector2.Distance(new Vector2(startPosition.x,0),
                                                    new Vector2(this.transform.position.x,0) );

        return travelledDistance;
    }


    public void CollectHealth(int points)
    {
        this.healthPoints += points;

        if (this.healthPoints >= MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }
        Debug.Log(healthPoints);
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if (this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
        Debug.Log(manaPoints);
    }

    public int GetHealth()
    {

        return this.healthPoints;
    }

    public int GetMana()
    {

        return this.manaPoints;
    }


}
 