using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour {

    Text victoryText;

    private void Awake()
    {
        victoryText = GetComponent<Text>();

        //victoryText.text = "";
        victoryText.enabled = false;
    }

    private void Update()
    {
        if(BeetleManager.currentBeetleCount == 0)
        {
            //victoryText.text = "!Venciste a los escarabajos!\n\n!Has Ganado!";
            victoryText.enabled = true;
        }

        if (CucumberManager.currentCucumberCount == 0 || PlayerManager.livesRemaining == 0)
        {
            victoryText.text = "GAME OVER";
            victoryText.enabled = true;
        }
        
    }


}
