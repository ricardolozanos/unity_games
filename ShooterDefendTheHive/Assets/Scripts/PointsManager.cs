using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour {


    public static int currentPoints;
        Text pointsText;


    private void Awake()
    {
        pointsText = GetComponent<Text>();
        currentPoints = 0;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        pointsText.text = currentPoints.ToString();
	}


    public static void AddPoints (int points)
    {
        currentPoints += points;
    }
}
