using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


//Si algo gobierna el gameplay se hace : CREATE EMPTY


public class Timer : MonoBehaviour {

    //indicar que el dato es flotante
    public float maxTime = 60f;

    //no dejar todo publico(puede causar errores en el juego o hacks)
    private float countdown = 0f;

	// Use this for initialization
	void Start () {
        countdown = maxTime;
		
	}
	
	// Update is called once per frame
	void Update () {
        //deltatime(numero de segundos que 
        //ha pasado que se renderizo un frame)
        countdown-= Time.deltaTime; //countdown = countdown - Time.deltaTime;

        //Debug.Log("Cuenta atras" + countdown);

        if (countdown <= 0 )
        {
            Debug.Log("Te has quedado sin tiempo... PERDISTE!!");
            Coin.coinsCount = 0;

            SceneManager.LoadScene("MainScene");
            
        }
    }
}
