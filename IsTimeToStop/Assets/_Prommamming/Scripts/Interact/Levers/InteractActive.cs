using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractActive : MonoBehaviour
{
    public GameObject startingActive;
    public GameObject startingInactive;
    public GameObject otherObjectThatInteract;
    public bool activated = false;
    
    void Start ()
    {
        startingInactive.SetActive(false);
    }

    public void Interact()
    {
        activated = true;
        startingInactive.SetActive(true);
        startingActive.SetActive(false);
        if(otherObjectThatInteract != null)
        {
            otherObjectThatInteract.GetComponent<InteractActive>().Interact();
        }
    }
}
