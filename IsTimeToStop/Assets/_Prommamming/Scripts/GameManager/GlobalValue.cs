using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour
{
    public bool timeIsStopped = false;
    public float timeLost = 0f;//hold the time we lost when time was stopped

    private void Update()
    {
        if(timeIsStopped)
        {
            timeLost += Time.deltaTime;
        }
    }
}
