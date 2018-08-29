using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecurityCam : MonoBehaviour
{
    private GameObject gameManager;

    public int rotationDegree = 90;
    public float rotationVelocity = 10f;

    public bool roteateLeft = true;
    
    private Vector3 pointA;
    private Vector3 pointB;


    void Start ()
    {
        gameManager = GameObject.Find("Game_Manager");

        if (!roteateLeft)
        {
            pointA = gameObject.transform.eulerAngles + new Vector3(0, 0, 0);
            pointB = gameObject.transform.eulerAngles + new Vector3(0, 0, rotationDegree);
        }
        else
        {
            pointA = gameObject.transform.eulerAngles + new Vector3(0, 0, 0);
            pointB = gameObject.transform.eulerAngles + new Vector3(0, 0, -rotationDegree);
        }
    }

    private void Update()
    {
        if(!gameManager.GetComponent<GlobalValue>().timeIsStopped)
        {
            Rotation();
        }
    }

    public void Rotation()
    {
        float time = Mathf.PingPong((Time.time - gameManager.GetComponent<GlobalValue>().timeLost) * rotationVelocity, 1);
        gameObject.transform.eulerAngles = Vector3.Lerp(pointA, pointB, time);
    }

    public void PlayerIsSaw()
    {
        if (!gameManager.GetComponent<GlobalValue>().timeIsStopped)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
