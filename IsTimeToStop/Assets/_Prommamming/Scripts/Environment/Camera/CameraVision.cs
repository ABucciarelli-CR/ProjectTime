using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVision : MonoBehaviour
{
    public GameObject camBody;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            camBody.GetComponent<SecurityCam>().PlayerIsSaw();
        }
    }
}
