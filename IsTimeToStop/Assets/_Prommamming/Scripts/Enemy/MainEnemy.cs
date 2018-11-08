using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemy : MonoBehaviour
{
    [HideInInspector]public GameObject visionArea;
    public Sprite redX;
    public float movDistance = 5f;
    public float movVelocity = 1f;

    private int direction = -1;
    private float startingposX;
    private float newPos;
    private GameObject player;
    private GameObject gameManager;
    private bool isLock = false;


    private void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<PlayerAction>().ZaWarudo += LockMyself;
        player.GetComponent<PlayerAction>().StopZaWarudo += UnlockMyself;
        gameManager = GameObject.Find("Game_Manager");

        startingposX = gameObject.transform.position.x;
        newPos = startingposX;
    }

    void Update()
    {
        if(!isLock)
        {
            switch (direction)
            {
                case -1:
                    // Moving Left
                    if (newPos > startingposX - movDistance)
                    {
                        newPos -= movVelocity;
                    }
                    else
                    {
                        direction = 1;
                    }
                    break;

                case 1:
                    // Moving Right
                    if (newPos < startingposX + movDistance)
                    {
                        newPos += movVelocity;
                    }
                    else
                    {
                        direction = -1;
                    }
                    break;
            }

            gameObject.transform.localPosition = new Vector3(newPos, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        
    }

    private void OnDestroy()
    {
        if (player != null)
        {
            player.GetComponent<PlayerAction>().ZaWarudo -= LockMyself;
            player.GetComponent<PlayerAction>().StopZaWarudo -= UnlockMyself;
        }
    }


    public void LockMyself()
    {
        if (!isLock)
        {
            isLock = true;
            visionArea.SetActive(false);
            if (gameObject.GetComponent<Rigidbody2D>())
            {
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }

    public void UnlockMyself()
    {
        if (isLock)
        {
            isLock = false;
            visionArea.SetActive(true);
            if (gameObject.GetComponent<Rigidbody2D>())
            {
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

}
