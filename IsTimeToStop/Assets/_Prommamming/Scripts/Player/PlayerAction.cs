using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    GameObject[] allObjects;

    [HideInInspector] public PlayerState playerState;
    [HideInInspector] public bool DoUpdateCoroutine = true;
    [HideInInspector] public GameObject playerStand;
    [HideInInspector] public GameObject playerCrouch;

    private float leftAndRightMovement;
    private float upNdownMovement;
    private bool zwKey = false;//ZA WARUDO!!!
    private bool timeIsStopped = false;
    private bool jump = false;

    private float zwVelocity = 0.05f;
    private bool isJump = false;
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
        playerCrouch.SetActive(false);
        //StartCoroutine(UpdateCoroutine());
    }

    void /*IEnumerator*/ Update/*Coroutine*/()
    {
        //while (DoUpdateCoroutine)
        //{
            leftAndRightMovement = Input.GetAxisRaw("Horizontal");
            upNdownMovement = Input.GetAxisRaw("Vertical");
            jump = Input.GetButtonDown("Jump");
            zwKey = Input.GetButtonDown("ZaWarudo");

            switch (playerState)
            {
                case PlayerState.idle:
                    Debug.Log("idle");
                    Move();
                    Crouch();
                    Jump();
                    ZWKey();
                    break;

                case PlayerState.movement:
                    Debug.Log("movement");
                    Move();
                    Crouch();
                    Jump();
                    ZWKey();
                    break;

                case PlayerState.jumping:
                    Debug.Log("jumping");
                    Move();
                    Stand();
                    Jump();
                    ZWKey();
                    break;

                default:
                    break;
            }

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

            /******************************* COROUTINE END ********************************************************/
            //yield return null;// new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        //}
    }

    public void Move()
    {
        if(leftAndRightMovement != 0)
        {
            //rb2d.transform.Translate(new Vector3(rb2d.transform.position.x +(leftAndRightMovement * movementSpeed), rb2d.transform.position.y, rb2d.transform.position.z));
            gameObject.transform.Translate(Vector3.right * (leftAndRightMovement * movementSpeed));
            //rb2d.velocity = new Vector2(leftAndRightMovement * movementSpeed, rb2d.velocity.y);
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

            Vector3 playerPos = transform.position;
            float jf = jumpForce;

            StartCoroutine(FallWithJumpCalculation(jf));
            //rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
        if (!jump && !landed && !isJump)
        {
            Vector3 playerPos = transform.position;
            float jf = 0f;

            StartCoroutine(FallWithoutJumpCalculation(jf));
        }
    }

    public void ZWKey()
    {
        if (zwKey && !timeIsStopped)
        {
            timeIsStopped = true;
            /*foreach (GameObject go in allObjects)
            {
                if(!go.CompareTag("Player") && go.GetComponent<Rigidbody2D>() != null)
                {
                    go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }*/
            Time.timeScale = zwVelocity;
            Time.fixedDeltaTime = zwVelocity * 0.02f;
            StartCoroutine(ZWWait(5));
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
        /*
        foreach (GameObject go in allObjects)
        {
            if (!go.CompareTag("Player") && go.GetComponent<Rigidbody2D>() != null)
            {
                go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                go.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }*/
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    float FallingCalculation(float jf)
    {
        gameObject.transform.Translate(Vector3.up * (jf * Time.unscaledDeltaTime));
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
}
