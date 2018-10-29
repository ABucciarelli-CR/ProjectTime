using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisionArea : MonoBehaviour
{
    [HideInInspector]public GameObject thisEnemy;
    private GameObject player;

	void Start ()
    {
		player = player = GameObject.Find("Player");
    }
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == player.name)
        {
            StartCoroutine(PlayerCatched());
        }
    }

    IEnumerator PlayerCatched()
    {
        yield return new WaitForSeconds(0f);
        thisEnemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Instantiate(thisEnemy.GetComponent<MainEnemy>().redX, player.transform.position, Quaternion.identity);
        thisEnemy.GetComponent<MainEnemy>().movVelocity = 0;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(WaitToRestart());
    }

    IEnumerator WaitToRestart()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
