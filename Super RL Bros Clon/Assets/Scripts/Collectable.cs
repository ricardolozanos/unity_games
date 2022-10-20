using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    healthPotion,
    manaPotion,
    money
}


public class Collectable : MonoBehaviour {

    public CollectableType type = CollectableType.money;

    bool isCollected = false;

    public static Collectable sharedInstance;

    public int value = 0;


    public AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {


        if (otherCollider.tag == "Player")
        {

            Collect();
            value++;

        }
    }

    void Show()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<CircleCollider2D>().enabled = true;

        isCollected = false;
    }

    void Hide()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;
    }

    void Collect()
    {
        AudioSource audio=GetComponent<AudioSource>();

        if (audio != null && this.collectSound!=null)
        {

            audio.PlayOneShot(this.collectSound);
        }

        
        Hide();
        if (!isCollected)
        {
            switch (this.type)
            {
                case CollectableType.money:
                    GameManager.sharedInstance.CollectObject(value);
                   
                    break;
                case CollectableType.healthPotion:
                    PlayerController.sharedInstance.CollectHealth(value);//dar vida a jugador
                    break;
                case CollectableType.manaPotion:
                    PlayerController.sharedInstance.CollectMana(value);//dar mana al jugador
                    break;

            }
        }
        isCollected = true;
    }
}
