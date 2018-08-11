using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopObject : MonoBehaviour
{
    private GameObject gameManager;

    private Vector2 myVelocity;
    private bool isLock = false;
    
	void Start ()
    {
        gameManager = GameObject.Find("Game_Manager");
    }
	
	void Update ()
    {
		if(gameManager.GetComponent<GlobalValue>().timeIsStopped)
        {
            if(!isLock)
            {
                LockMyself();
            }
        }
        else
        {
            if(isLock)
            {
                UnlockMyself();
            }
        }
	}

    public void LockMyself()
    {
        isLock = true;
        if (gameObject.GetComponent<Rigidbody2D>())
        {
            myVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void UnlockMyself()
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
