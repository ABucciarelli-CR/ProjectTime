using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEnemy : MonoBehaviour
{
    public Sprite redX;
    public float movDistance = 5f;
    public float movVelocity = 1f;

    private int direction = -1;
    private float startingposX;
    private float newPos;

    private void Start()
    {
        startingposX = gameObject.transform.position.x;
        newPos = startingposX;
    }

    void Update()
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
