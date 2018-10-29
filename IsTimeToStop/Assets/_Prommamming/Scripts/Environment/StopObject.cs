using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopObject : MonoBehaviour
{
    private GameObject gameManager;
    private GameObject player;

    private Vector2 myVelocity;
    private bool isLock = false;
    
	void Start ()
    {
        player = GameObject.Find("Player");
        player.GetComponent<PlayerAction>().ZaWarudo += LockMyself;
        player.GetComponent<PlayerAction>().StopZaWarudo += UnlockMyself;
        gameManager = GameObject.Find("Game_Manager");
    }

    private void OnDestroy()
    {
        if(player != null)
        {
            player.GetComponent<PlayerAction>().ZaWarudo -= LockMyself;
            player.GetComponent<PlayerAction>().StopZaWarudo -= UnlockMyself;
        }
    }

    void Update ()
    {
		
	}

    public void LockMyself()
    {
        if (!isLock)
        {
            isLock = true;
            if (gameObject.GetComponent<Rigidbody2D>())
            {
                myVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    public void UnlockMyself()
    {
        if (isLock)
        {
            isLock = false;
            if (gameObject.GetComponent<Rigidbody2D>())
            {
                gameObject.GetComponent<Rigidbody2D>().velocity = myVelocity;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}
