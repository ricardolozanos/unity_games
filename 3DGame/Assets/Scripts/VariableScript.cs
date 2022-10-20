using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableScript : MonoBehaviour {


    public int myNumber1 = 5;
    public int myNumber2 = 10;

    private void Awake()
    {
        Debug.Log("EL objeto ha desperatdo");
    }



    // Use this for initialization
    void Start () {

        Debug.Log("El objeto arranco");
	}

    

    // Update is called once per frame
    void Update () {
        Debug.Log("actualizacion");
        

        if (Input.GetKeyDown(KeyCode.Return)) {
            AddTwoNumbers();
        }

	}

    void AddTwoNumbers()
    {
        Debug.Log(myNumber1 + myNumber2);
    }

    private void FixedUpdate()
    {
        Debug.Log(Time.time);
    }


}
