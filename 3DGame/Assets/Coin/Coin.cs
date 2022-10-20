/*
 * NAMESPACE
 * Compendio de librerias
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

/*
  CLASS 
  
 */
public class Coin : MonoBehaviour {
    //Variables y metodos

        //Variable global y estatica(1 para todas las monedas)
    public static int coinsCount = 0;

    

	// Use this for initialization
	void Start () {
        Coin.coinsCount++;
        Debug.Log("El juego ha comenzado okey, y hay "+Coin.coinsCount+" monedas");
        
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Estamos en el update");
		
	}

    /*
     * Metodo que se llama cuando un collider
     * entra en contacto con el que tiene este script
     * (en este project con la moneda)(puede funcionar
     * con cualquier collider)
     */
    private void OnTriggerEnter(Collider otherCollider)
    {

        //Si el jugador choca con una moneda, hay una menos, y esta desaparece
        if (otherCollider.CompareTag("Player") == true) {

            Debug.Log("Has recogido la moneda!"+" Quedan " + Coin.coinsCount + " monedas");

            Coin.coinsCount--;

            //En el caso de que el contador llegue a 0, el juego termina
            if (Coin.coinsCount == 0)
            {
                Debug.Log("El juego ha terminado");
                GameObject gameManager = GameObject.Find("GameManager");

                Destroy(gameManager);

                GameObject[] fireworksSystem = GameObject.FindGameObjectsWithTag("Fireworks");

                foreach (GameObject fireworks in fireworksSystem)
                {
                    fireworks.GetComponent<ParticleSystem>().Play();
                }

            }



            Destroy(gameObject);// Destruir el objeto al hacer collide
            // siempre destruir al final
        }
        


    }
}
    