using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacdotManager : MonoBehaviour {



    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "pacman")
        {

            UIManager.sharedInstance.ScorePoints(100);
            Destroy(this.gameObject);
        }

    }


}
