using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


    public static int livesRemaining;
    public static int currentCherryCount;
    public int tempCurrentCherryCount;
    public bool isCollectingCherries;

    public Transform[] spawningZones;

    public static bool hasDead;
        

    private void Awake()
    {
        livesRemaining = 3;
        currentCherryCount = 100;
        tempCurrentCherryCount = 0;
        isCollectingCherries = false;
        hasDead = false;

    }

   

    private void Update()
    {

        if (isCollectingCherries)
        {
            if (tempCurrentCherryCount >= 60)
            {
                currentCherryCount += 1;
                tempCurrentCherryCount = 0;
                
                PointsManager.AddPoints(5);
            }
            else
            {
                tempCurrentCherryCount+= 1;
            }
        }

        if (HealthManager.currentHealth <= 0 && !hasDead)
        {
            
            hasDead = true;
            livesRemaining--;
            
            if (livesRemaining == 2)
            {
                Destroy(GameObject.Find("Life3"));
                GetComponent<Animator>().Play("CM_Die");
                StartCoroutine(RespawnPlayer());

            }
            if (livesRemaining == 1)
            {
                Destroy(GameObject.Find("Life2"));
                GetComponent<Animator>().Play("CM_Die");
                StartCoroutine(RespawnPlayer());

            }
            if (livesRemaining == 0)
            {
                Destroy(GameObject.Find("Life1"));
            }
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("CherryTree"))
        {
            isCollectingCherries = true;
            currentCherryCount += 1;
            PointsManager.AddPoints(5);

        }
    }

    private void OnTriggerExit(Collider otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("CherryTree"))
        {
            isCollectingCherries = false;
        }
    }

    IEnumerator RespawnPlayer()
    {
        int randomPos = Random.Range(0, spawningZones.Length);

        yield return new WaitForSecondsRealtime(5f);
       

        this.transform.position = spawningZones[randomPos].transform.position;
        HealthManager.currentHealth = 100;
        GetComponent<Animator>().Play("CM_Idle");
        hasDead = false;
    }
}
