using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionScript : MonoBehaviour {

    public bool willItRainToday = false;
   

	// Use this for initialization
	void Start () {

        if (willItRainToday)
        {
            Debug.Log("No te olvides de coger el paraguas");
        }
        else
        {
            Debug.Log("No lo cojas hombre, que hace mucho sol");
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
