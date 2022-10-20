using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewOnGame : MonoBehaviour {


    public Text collectableLable, scoreLabel, maxScoreLabel;
    

	
	// Update is called once per frame
	void Update () {
		
        if(GameManager.sharedInstance.currentGameState== GameState.inGame ||
            GameManager.sharedInstance.currentGameState==GameState.gameOver)
        {
            int currentObjects = GameManager.sharedInstance.collectedItems;
            this.collectableLable.text = currentObjects.ToString();
        }



        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        { 


            float travelledDistance = PlayerController.sharedInstance.GetDistance();
            this.scoreLabel.text="Score\n"+ travelledDistance.ToString("f2");


            float maxTravelledDistance = PlayerPrefs.GetFloat("maxScore", 0);
            this.maxScoreLabel.text = "Max Score\n" + maxTravelledDistance.ToString("f2");

        }

               
        

        





	}
}
