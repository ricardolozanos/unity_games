using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionsScript : MonoBehaviour {

    public string student1 = "Edgar";
    public string student2 = "Ricardo";
    public string student3 = "Antonio";
    public string student4 = "Runa";

    
  
    //Todas las estructuras de datos, empiezan en la posición número 0
    //El último elemento de un array, es el de su dimensión -1

    /*
     * ARRAY
     *  - Es homogéneo (solo un tipo de dato)
     *  - Es de tamaño fijo (una vez creado, no puede contener más elementos)
     *  - Tiene un orden (se accede por posición)
    */

    public string[] studentsArray = new string[] {"Oscar", "Lety","Oscarito", "Alex", "Edgar", "Ricardo", "Antonio", "Runa"};

    public string[] familyNames = new string[4];//{ , , , }

    private int[] numberOfDoorsInMyStreet = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 11 };

    private bool[] hasPassedTheExam = new bool[] { true, false, false, true };



    /*
   * LISTA
   * using System.Collections.Generic;
   *  - Es homogéneo (solo un tipo de dato)
   *  - Es de tamaño ajustable o variable (podemos añadir más elementos en tiempo real y eliminarlos)
   *  - Tiene un orden (se acceede por posición)
   */

    public List<string> studentsNames = new List<string>();


    /*
     * ARRAYLIST
     *  - Es heterogéneo (puede guardar diferentes tipos de datos en la misma estructura)
     *  - Es de tamaño ajustable o variable (podemos añadir más elementos en tiempo real y eliminarlos)
     *  - Tiene un orden (se acceede por posición)
    */

    public ArrayList inventory = new ArrayList();

    /*
     * DICCIONARIO <-> HASHTABLE
     *  - Se puede redimensionar dinámicamente (igual que una lista)
     *  - Puede contener información heterogénea (igual que una array list)
     *  - Se accede por clave, no por posición
     */

    public Hashtable personalDetails = new Hashtable();



    // Use this for initialization
    void Start()
    {
        //Add -> añade elementos al final de la lista 
        //aquí, la lista está vacía
        studentsNames.Add("Oscar");
        

        //aquí, la lista tiene un elemento, Oscar
        studentsNames.Add("Leticia");

        //aquí, la lista tiene dos elementos, Oscar y Lety
        studentsNames.Add("Oscarito");

        //aquí, la lista tiene tres elementos, Oscar,Lety, Oscarito
        studentsNames.Add("Alejandro");

        //aquí, la lista tiene cuatro elementos, Oscar, Lety, Oscarito, Alex
        studentsNames.Add("Edgar");


        //aquí, la lista tiene cinco elementos, Oscar, Lety, Oscarito, Alex, Edgar

        studentsNames.Add("Ricardo");

        //aquí, la lista tiene seis elementos, Oscar, Lety, Oscarito, Alex, Edgar, Ricardo 

        studentsNames.Add("Antonio");
        //aquí, la lista tiene siete elementos, Oscar, Lety, Oscarito, Alex, Edgar, Ricardo, Antonio

        studentsNames.Add("Dude");

        //Contains -> nos dice si la lista contiene o no un objeto: true o false
        if (studentsNames.Contains("Dude"))
        {
            //Remove -> elimina elementos de la lista
            studentsNames.Remove("Dude");

        }


        studentsNames.Insert(7, "Runa");
        //aquí, la lista tiene siete elementos, Oscar, Lety, 
        //Oscarito, Alex, Edgar, Ricardo, Antonio, Jupiter


        //ToArray() -> Convierte una lista en un array
        string[] studentsNamesToArray = studentsNames.ToArray();

        //Clear -> Eliminar definitivamente todos los elementos de la lista
       //studentsNames.Clear();
        //ahora la lista está vacía [];   


        //Acceso a arrays y tamaño del mismo
        Debug.Log("El tamaño del array de estudiantes es: " + studentsArray.Length);

        int pos = 5;

        if (pos >= 0 && pos < studentsArray.Length)
        {
            Debug.Log("El estudiante "+ (pos+1) +" del array es: " + studentsArray[pos]);
            //El primer estudiante del array
        }


        //Acceso a listas y tamaño de las mismas
        Debug.Log("El tamaño de la lista de estudiantes es: " + studentsNames.Count);

        pos = 2;

        if (pos >= 0 && pos < studentsNames.Count)
        {
            string thirdStudent = studentsNames[pos];//El tercer estudiante de la lista
            Debug.Log("El estudiante " + (pos+1)+ " de la lista: " + studentsArray[pos]);
        }




        inventory.Add("Te amo");
        inventory.Add(3000);
        inventory.Add("s");
        inventory.Add(false);
        inventory.Add(GameObject.FindGameObjectsWithTag("Fireworks"));

        //Pedimos el tipo de dato que va a salir de la ArrayList
        Debug.Log(inventory[2].GetType());
        Debug.Log(inventory[4].GetType());


        personalDetails.Add("firstName",    "Ricardo");
        personalDetails.Add("lastName",     "Lozano");
        personalDetails.Add("age",          25);
        personalDetails.Add("gender",       "male");
        personalDetails.Add("isMarried",    false);
        personalDetails.Add("hasChildren",  false);

        if (personalDetails.Contains("firstName") && personalDetails.Contains("age"))
        {
            string username = (string)personalDetails["firstName"];
            int age = (int)personalDetails["age"];

            Debug.Log(username);
        }
        else
        {
            Debug.Log("El diccionario no contiene las claves que se han pedido");
        }







    }


	
	// Update is called once per frame
	void Update () {
		
	}
}
