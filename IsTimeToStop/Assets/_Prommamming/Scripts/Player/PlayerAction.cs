﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [HideInInspector] public PlayerState playerState;
    [HideInInspector] public bool DoUpdateCoroutine = true;
    [HideInInspector] public GameObject playerStand;
    [HideInInspector] public GameObject playerCrouch;
    private List<GameObject> objectCanTrigger = new List<GameObject>();
    private GameObject gameManager;

    private float leftAndRightMovement;
    private float upNdownMovement;
    private bool zwKey = false;//ZA WARUDO!!!
    private bool timeIsStopped = false;
    private bool jump = false;
    private bool interact = false;

    private float zwVelocity = 0.05f;
    private bool isJump = false;
    /*[HideInInspector]*/ public bool landed = true;
    [HideInInspector] Vector2 vel = new Vector2(0,0);

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
        gameManager = GameObject.Find("Game_Manager");
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        playerCrouch.SetActive(false);
    }

    void Update()
    {
        leftAndRightMovement = Input.GetAxisRaw("Horizontal");
        upNdownMovement = Input.GetAxisRaw("Vertical");
        jump = Input.GetButtonDown("Jump");
        zwKey = Input.GetButtonDown("ZaWarudo");
        interact = Input.GetButtonDown("Interact");

        switch (playerState)
        {
            case PlayerState.idle:
                //Debug.Log("idle");
                Move();
                Crouch();
                Jump();
                ZWKey();
                Interact();
                break;

            case PlayerState.movement:
                //Debug.Log("movement");
                Move();
                Crouch();
                Jump();
                ZWKey();
                Interact();
                break;

            case PlayerState.jumping:
                //Debug.Log("jumping");
                Move();
                Stand();
                Jump();
                ZWKey();
                Interact();
                break;

            default:
                break;
        }


        if (landed)
        {
            vel = new Vector2(vel.x, 0);
        }
        if(leftAndRightMovement == 0)
        {
            vel = new Vector2(0, vel.y);
        }

        rb2d.MovePosition(rb2d.position + vel * Time.deltaTime);

        //switch states
        if (landed && leftAndRightMovement == 0 && !jump)
        {
            playerState = PlayerState.idle;
        }
        else if (leftAndRightMovement != 0 && landed)
        {
            playerState = PlayerState.movement;
        }
        else if (!landed && jump)
        {
            isJump = true;
            playerState = PlayerState.jumping;
        }
        else if(!landed && !jump)
        {
            playerState = PlayerState.jumping;
        }
            
    }

    public void Move()
    {
        if(leftAndRightMovement != 0)
        {
            Debug.Log("Move");
            vel = new Vector2(leftAndRightMovement * movementSpeed, vel.y);
        }
    }

    public void Crouch()
    {
        if(upNdownMovement < 0)
        {
            playerCrouch.SetActive(true);
            playerStand.SetActive(false);
        }
        else
        {
            Stand();
        }
    }

    public void Stand()
    {
        if(playerCrouch.activeSelf == true)
        {
            playerCrouch.SetActive(false);
            playerStand.SetActive(true);
        }
    }

    public void Jump()
    {
        if (jump && landed)
        {
            landed = false;
            
            float jf = jumpForce;

            StartCoroutine(FallWithJumpCalculation(jf));
        }
        if (!jump && !landed && !isJump)
        {
            float jf = -jumpForce;

            StartCoroutine(FallWithoutJumpCalculation(jf));
        }
    }

    public void ZWKey()
    {
        
        if (zwKey && !timeIsStopped)
        {
            timeIsStopped = true;
            gameManager.GetComponent<GlobalValue>().timeIsStopped = true;
           
            StartCoroutine(ZWWait(5));
        }
    }

    public void Interact()
    {
        if(interact)
        {
            foreach(GameObject go in objectCanTrigger)
            {
                if(go.CompareTag("CanTrig"))
                {
                    go.GetComponent<InteractActive>().Interact();
                }
            }
        }
    }

    IEnumerator FallWithJumpCalculation(float jf)
    {
        while (!landed)
        {
            jf = FallingCalculation(jf);
            yield return null;
        }
        isJump = false;
    }

    IEnumerator FallWithoutJumpCalculation(float jf)
    {
        while (!landed)
        {
            jf = FallingCalculation(jf);
            yield return null;
        }
    }

    IEnumerator ZWWait(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        timeIsStopped = false;
        gameManager.GetComponent<GlobalValue>().timeIsStopped = false;
    }

    float FallingCalculation(float jf)
    {
        vel = new Vector2(vel.x, jf);
        
        if(Mathf.Abs(jf - Physics.gravity.magnitude) >= jumpForce)
        {
            jf = -jumpForce;
        }
        else
        {
            jf -= Physics.gravity.magnitude;
        }
        return jf;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectCanTrigger.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objectCanTrigger.Remove(collision.gameObject);
    }
}
