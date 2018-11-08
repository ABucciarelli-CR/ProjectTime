using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> background;
    public int musicBackgroundSelector = 1;
    [HideInInspector]public GameObject musicManager;
    
	void Start ()
    {
        musicManager.GetComponent<AudioSource>().clip = background[musicBackgroundSelector -1];
        musicManager.GetComponent<AudioSource>().Play();
    }
	
	void Update ()
    {
		
	}
}
