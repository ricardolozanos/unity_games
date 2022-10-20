using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryControl : MonoBehaviour {

    public Rigidbody cherryRb;
    public float throwDistance = 100f;
    public float timeToDestroy = 4f;

    private GameObject ShootSpot;

    private void Start()
    {
        ShootSpot = GameObject.FindGameObjectWithTag("ShootSpot");
    }

    // Update is called once per frame
    void Update () {
        if (Time.timeScale > 0)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
            {
                if (PlayerManager.currentCherryCount > 0)
                {
                    ThrowCherry();

                }
            }
        }
	}

    void ThrowCherry()
    {

        Ray cameraToWorldRay = Camera.main.ScreenPointToRay(Input.mousePosition);


        RaycastHit hit = new RaycastHit();

        if(Physics.Raycast(cameraToWorldRay,out hit))
        {
            Debug.DrawLine(transform.position, hit.point);


         Vector3 directionToFire = hit.point-ShootSpot.transform.position;
        

        Rigidbody cherryClone = (Rigidbody)Instantiate(cherryRb,
                                                    transform.position,
                                                    transform.rotation);
        cherryClone.useGravity = true;
        cherryClone.constraints = RigidbodyConstraints.None;
        cherryClone.AddForce(directionToFire.normalized * throwDistance);
        Destroy(cherryClone.gameObject, timeToDestroy);
        PlayerManager.currentCherryCount -= 1;
        }    
        
    }

}
