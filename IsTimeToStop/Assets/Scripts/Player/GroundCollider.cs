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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pa.landed = true;
    }
}
