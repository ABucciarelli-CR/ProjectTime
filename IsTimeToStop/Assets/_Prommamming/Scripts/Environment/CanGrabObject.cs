using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanGrabObject : MonoBehaviour
{
    private GameObject player;
    
	void Start ()
    {
        player = GameObject.Find("Player");
	}

	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == player.name)
        {
            player.GetComponent<PlayerAction>().nearGameobjectCanGrab.Add(this.gameObject.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == player.name)
        {
            player.GetComponent<PlayerAction>().nearGameobjectCanGrab.Remove(this.gameObject.transform.parent.gameObject);
        }
    }
}
