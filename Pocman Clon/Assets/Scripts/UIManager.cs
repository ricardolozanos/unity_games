using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   


public class UIManager : MonoBehaviour {

    public Text titleLabel;
    public Text scoreLabel;
    public Text titleLabel2;
    public static UIManager sharedInstance;
    private int totalScore;


    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
        totalScore = 0;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	        if(GameManager.sharedInstance.gamePaused || !GameManager.sharedInstance.gameStarted)
        {
            titleLabel.enabled = false;
            titleLabel2.enabled = true;
        }
        else
        {
            titleLabel.enabled = true;
            titleLabel2.enabled = false;

        }
	}

    public void ScorePoints (int Points)
    {
        totalScore += Points;
        scoreLabel.text = " " + totalScore;
    }
}
