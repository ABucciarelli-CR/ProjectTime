using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    GameObject[] allObjects;

    [HideInInspector] public PlayerState playerState;

    private float leftAndRightMovement;
    private bool zwKey = false;//ZA WARUDO!!!
    private bool timeIsStopped = false;
    private bool jump = false;
    [HideInInspector] public bool landed = true;

    public float movementSpeed = 1f;
    public float jumpForce = 1f;


    Rigidbody2D rb2d;

    public enum PlayerState
    {
        idle,
        movement,
        jumping
    }

    void Start ()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        allObjects = GameObject.FindObjectsOfType<GameObject>();
    }
	
	void Update ()
    {
        leftAndRightMovement = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
        zwKey = Input.GetButtonDown("ZaWarudo");

        switch (playerState)
        {
            case PlayerState.idle:
                Debug.Log("idle");
                Move();
                Jump();
                ZWKey();
                break;

            case PlayerState.movement:
                Debug.Log("movement");
                Move();
                Jump();
                ZWKey();
                break;

            case PlayerState.jumping:
                Debug.Log("jumping");
                Move();
                Jump();
                ZWKey();
                break;

            default:
                break;
        }

        //switch states
        if(landed && leftAndRightMovement == 0 && !jump)
        {
            playerState = PlayerState.idle;
        }
        else if(leftAndRightMovement != 0 && landed)
        {
            playerState = PlayerState.movement;
        }
        else if(!landed)
        {
            playerState = PlayerState.jumping;
        }
    }

    public void Move()
    {
        if(leftAndRightMovement != 0)
        {
            //rb2d.transform.Translate(new Vector3(rb2d.transform.position.x +(leftAndRightMovement * movementSpeed), rb2d.transform.position.y, rb2d.transform.position.z));
            //gameObject.transform.Translate(Vector3.right * (leftAndRightMovement * movementSpeed));
            rb2d.velocity = new Vector2(leftAndRightMovement * movementSpeed, rb2d.velocity.y);
        }
    }

    public void Jump()
    {
        if (jump && landed)
        {
            landed = false;
            //gameObject.transform.Translate(Vector3.up * jumpForce);
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
    }

    public void ZWKey()
    {
        if (zwKey && !timeIsStopped)
        {
            timeIsStopped = true;
            foreach (GameObject go in allObjects)
            {
                if(!go.CompareTag("Player") && go.GetComponent<Rigidbody2D>() != null)
                {
                    go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }
            //Time.timeScale = 0;
            StartCoroutine(ZWWait(5));
        }
    }

    IEnumerator ZWWait(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        timeIsStopped = false;
        foreach (GameObject go in allObjects)
        {
            if (!go.CompareTag("Player") && go.GetComponent<Rigidbody2D>() != null)
            {
                go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        //Time.timeScale = 1;
    }
}
