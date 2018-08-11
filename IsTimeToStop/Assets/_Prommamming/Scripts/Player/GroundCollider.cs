using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollider : MonoBehaviour
{
    public GameObject player;
    private PlayerAction pa;

    private void Awake()
    {
        pa = player.GetComponent<PlayerAction>();
        pa.landed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("terrain"))
        {
            pa.landed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("terrain"))
        {
            pa.landed = false;
        }
    }
}
